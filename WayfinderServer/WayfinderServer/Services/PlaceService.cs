using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.Entities;

namespace WayfinderServer.Services
{
    public interface IPlaceService
    {
        public int AddPlace(Place place);

        public Task AddPlaceImage(IFormFile form, int id);
        public int EditPlace(int id, Place place);

        public IEnumerable<Place> GetAllPlaces();
        public IEnumerable<Place> GetPlacesSearch(string term);
        public Place GetPlace(int id);
        public IQRCodePoint GetQRCodeByPlace(int id, string qrValue);
        public IEnumerable<IQRCodePoint> GetQRCodesByPlace(int id);

    }
    public class PlaceService : IPlaceService
    {
        private WayfinderContext _context;

        public PlaceService(WayfinderContext context)
        {
            _context = context;
        }
        public int AddPlace(Place place)
        {
            var result = _context.Places.AddAsync(place);
            _context.SaveChanges();
            //return result.IsCompletedSuccessfully;
            return place.Id;
        }

        public int EditPlace(int id, Place place)
        {
            var result = _context.Places.SingleOrDefault(x => x.Id == id);
            result.Name = place.Name;
            result.Country = place.Country;
            result.Zip = place.Zip;
            result.City = place.City;
            result.Address = place.Address;
            result.Description = place.Description;

            var updated = _context.Places.Update(result);
            _context.SaveChanges();
            //return result.IsCompletedSuccessfully;
            return result.Id;
        }

        public IEnumerable<Place> GetAllPlaces()
        {
            return _context.Places.ToList();
        }
        public IEnumerable<Place> GetPlacesSearch(string term)
        {
            if(term!= null)
            {
                term = term.ToUpper();
                return _context.Places.Where(p => p.Name.ToUpper().Contains(term) || p.Country.ToUpper().Contains(term) || p.City.ToUpper().Contains(term) ||
                                                    p.Zip.ToUpper().Contains(term) || p.Address.ToUpper().Contains(term)).ToList();

            }
            else
            {
                return GetAllPlaces();
            }
        }

        public Place GetPlace(int id)
        {
            //return _context.Events.Include(x => x.Categories).Where(x => x.Active).SingleOrDefault(x => x.Id == id);
            return _context.Places.Include(x => x.Floors).SingleOrDefault(x => x.Id == id);
            
        }

        public async Task AddPlaceImage(IFormFile file, int id)
        {
            var filePath = "Resources/Places/Images/" + id + ".png";

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
                var result = _context.Places.SingleOrDefault(x => x.Id == id);
                result.Image = filePath;

                var updated = _context.Places.Update(result);
                _context.SaveChanges();

            }
            
        }

        public IQRCodePoint GetQRCodeByPlace(int id, string qrValue)
        {

            Target target = _context.Targets.Include(x => x.Floor.FloorPlan3D).SingleOrDefault(x => x.QRCode == qrValue && x.Floor.Place.Id == id);
            Marker marker = _context.Markers.Include(x => x.Floor.FloorPlan3D).SingleOrDefault(x => x.QRCode == qrValue && x.Floor.Place.Id == id);

            if (target != null)
            {
                return target;
            }
            else
            {
                return marker;
            }

        }

        public IEnumerable<IQRCodePoint> GetQRCodesByPlace(int id)
        {

            IEnumerable<IQRCodePoint> targets = _context.Targets.Where(t => t.Floor.Place.Id == id).ToList();
            IEnumerable<IQRCodePoint> markers = _context.Markers.Where(t => t.Floor.Place.Id == id).ToList();

            IEnumerable<IQRCodePoint> joint = targets.Union(markers);

            return targets;

        }
    }
}
