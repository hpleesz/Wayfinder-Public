using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WayfinderServer.Entities;
using WayfinderServer.Helpers;

namespace WayfinderServer.Services
{
    public interface IMarkerService
    {
        public int AddMarker(Marker marker, int floorId);
        public Marker GetMarker(int id);
        public IEnumerable<Marker> GetAllMarkersByFloor(int floorId);
        public IEnumerable<Marker> GetAllMarkersByPlace(int placeId);
        public IEnumerable<Marker> GetMarkersSearch(string term, int placeId);
        public IEnumerable<Marker> GetMarkersSearchByFloor(string term, int floorId, int placeId);
        public int EditCoordinates(int id, Marker marker);

    }
    public class MarkerService : IMarkerService
    {
        private WayfinderContext _context;

        public MarkerService(WayfinderContext context)
        {
            _context = context;
        }

        public int AddMarker(Marker marker, int floorId)
        {
            var floor = _context.Floors.SingleOrDefault(x => x.Id == floorId);

            marker.Floor = floor;
            if (floor.Markers == null)
            {
                floor.Markers = new List<Marker>();
            }

            floor.Markers.Add(marker);

            var addResult = _context.Markers.AddAsync(marker);
            var updateResult = _context.Floors.Update(floor);

            _context.SaveChanges();
            marker.QRCode = "MARKER_" + marker.Id;
            var updateMarker = _context.Markers.Update(marker);
            _context.SaveChanges();

            return marker.Id;
        }

        public Marker GetMarker(int id)
        {
            return _context.Markers.Include(x => x.Floor.FloorPlan3D).SingleOrDefault(x => x.Id == id);

        }

        public IEnumerable<Marker> GetAllMarkersByFloor(int floorId)
        {
            return _context.Markers.Where(t => t.Floor.Id == floorId).ToList();
        }

        public IEnumerable<Marker> GetAllMarkersByPlace(int placeId)
        {
            return _context.Markers.Include(t => t.Floor).Where(t => t.Floor.Place.Id == placeId).ToList();
        }

        public IEnumerable<Marker> GetMarkersSearch(string term, int placeId)
        {
            if (term != null)
            {
                term = term.ToUpper();
                return _context.Markers.Include(f => f.Floor).Where(f => f.Floor.Place.Id == placeId && f.Name.ToUpper().Contains(term)).ToList();

            }
            else
            {
                return GetAllMarkersByPlace(placeId);
            }
        }

        public IEnumerable<Marker> GetMarkersSearchByFloor(string term, int floorId, int placeId)
        {
            if (term != null)
            {
                term = term.ToUpper();
                if (floorId > 0)
                {
                    return _context.Markers.Include(f => f.Floor).Where(f => f.Floor.Place.Id == placeId && f.Name.ToUpper().Contains(term) && f.Floor.Id == floorId).ToList();
                }
                else
                {
                    return _context.Markers.Include(f => f.Floor).Where(f => f.Floor.Place.Id == placeId && f.Name.ToUpper().Contains(term)).ToList();
                }

            }
            else
            {
                if (floorId > 0)
                {
                    return _context.Markers.Include(f => f.Floor).Where(f => f.Floor.Place.Id == placeId && f.Floor.Id == floorId).ToList();
                }
                else
                {
                    return GetAllMarkersByPlace(placeId);
                }
            }

        }
        public int EditCoordinates(int id, Marker marker)
        {
            var result = _context.Markers.SingleOrDefault(x => x.Id == id);
            result.XCoordinate = marker.XCoordinate;
            result.YCoordinate = marker.YCoordinate;
            result.ZCoordinate = marker.ZCoordinate;
            result.XRotation = marker.XRotation;
            result.YRotation = marker.YRotation;
            result.ZRotation = marker.ZRotation;

            var updated = _context.Markers.Update(result);
            _context.SaveChanges();
            //return result.IsCompletedSuccessfully;
            return result.Id;
        }

    }

}
