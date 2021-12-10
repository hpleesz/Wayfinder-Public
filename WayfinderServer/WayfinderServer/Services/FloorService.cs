using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.Entities;

namespace WayfinderServer.Services
{
    public interface IFloorService
    {
        public int AddFloor(Floor floor, int placeId);
        public int EditFloor(int floorId, Floor floor);
        public Floor GetFloor(int id);
        public IEnumerable<Floor> GetAllFloorsByPlace(int placeId);
        public IEnumerable<Floor> GetFloorsSearch(string term, int placeId);
        public Task AddFloorPlan3D(string fileContents, int floorId);
        public Task AddFloorPlan2D(IFormFile file, int floorId);
        public Task AddFloorPlanTemplateImage(IFormFile file, int floorId);

    }
    public class FloorService : IFloorService
    {
        private WayfinderContext _context;

        public FloorService(WayfinderContext context)
        {
            _context = context;
        }

        public int AddFloor(Floor floor, int placeId)
        {
            var place = _context.Places.SingleOrDefault(x => x.Id == placeId);
            floor.Place = place;
            if(place.Floors == null)
            {
                place.Floors = new List<Floor>();
            }
            place.Floors.Add(floor);

            var addResult = _context.Floors.AddAsync(floor);
            var updateResult = _context.Places.Update(place);

            _context.SaveChanges();

            return floor.Id;
        }

        public int EditFloor(int id, Floor floor)
        {
            var result = _context.Floors.SingleOrDefault(x => x.Id == id);
            result.Name = floor.Name;
            result.Number = floor.Number;

            var updated = _context.Floors.Update(result);
            _context.SaveChanges();
            //return result.IsCompletedSuccessfully;
            return result.Id;
        }

        public Floor GetFloor(int id)
        {
            //return _context.Events.Include(x => x.Categories).Where(x => x.Active).SingleOrDefault(x => x.Id == id);
            return _context.Floors.Include(x => x.Targets).Include(x => x.FloorPlan2D).SingleOrDefault(x => x.Id == id);

        }

        public IEnumerable<Floor> GetAllFloorsByPlace(int placeId)
        {
            return _context.Floors.Include(f => f.Targets).Include(x => x.FloorPlan2D).Where(f => f.Place.Id == placeId).ToList();
        }

        public IEnumerable<Floor> GetFloorsSearch(string term, int placeId)
        {
            if (term != null)
            {
                term = term.ToUpper();
                return _context.Floors.Include(f => f.Targets).Include(x => x.FloorPlan2D).Where(f => f.Place.Id == placeId && (f.Name.ToUpper().Contains(term) || f.Number.ToString().ToUpper().Contains(term))).ToList();

            }
            else
            {
                return GetAllFloorsByPlace(placeId);
            }
        }

        //3D MODEL

        public async Task AddFloorPlan3D(string fileContents, int floorId)
        {
            var floor = _context.Floors.SingleOrDefault(x => x.Id == floorId);

            string filepath = "Resources/Floors/Models/3DModel-" + floorId + ".stl";
            fileContents = fileContents.Replace("\\n", "\n");
            fileContents = fileContents.Replace("\\t", "\t");
            System.IO.File.WriteAllText(filepath, fileContents);

            var floorPlan = _context.FloorPlan3Ds.SingleOrDefault(x => x.Floor.Id == floorId);
            if (floorPlan == null)
            {
                floorPlan = new FloorPlan3D();
                floorPlan.Floor = floor;
                floorPlan.FileLocation = filepath;
                var addResult = _context.FloorPlan3Ds.AddAsync(floorPlan);

            }
            else
            {
                floorPlan.FileLocation = filepath;
                var updateResult2 = _context.FloorPlan3Ds.Update(floorPlan);

            }

            floor.FloorPlan3D = floorPlan;
            var updateResult = _context.Floors.Update(floor);

            _context.SaveChanges();

        }

        public async Task AddFloorPlan2D(IFormFile file, int floorId)
        {
            var filePath = "Resources/Floors/Maps/" + floorId + ".jpg";

            using (var stream = System.IO.File.Create(filePath))
            {
                var floor = _context.Floors.SingleOrDefault(x => x.Id == floorId);

                var floor2d = _context.FloorPlan2Ds.SingleOrDefault(x => x.Floor.Id == floorId);

                //new
                if (floor2d == null)
                {
                    floor2d = new FloorPlan2D();
                    floor2d.Floor = floor;

                    floor.FloorPlan2D = floor2d;

                    floor2d.FileLocation = filePath;

                    var addResult = _context.FloorPlan2Ds.AddAsync(floor2d);

                }
                else
                {
                    floor2d.FileLocation = filePath;
                    var updateResult2 = _context.FloorPlan2Ds.Update(floor2d);

                }
                var updateResult = _context.Floors.Update(floor);

                await file.CopyToAsync(stream);
                _context.SaveChanges();

            }
        }

        public async Task AddFloorPlanTemplateImage(IFormFile file, int floorId)
        {
            var filePath = "Resources/Floors/Templates/" + floorId + ".jpg";

            using (var stream = System.IO.File.Create(filePath))
            {
                var floor = _context.Floors.SingleOrDefault(x => x.Id == floorId);

                var floor2d = _context.FloorPlan2Ds.SingleOrDefault(x => x.Floor.Id == floorId);

                //new
                if (floor2d == null)
                {
                    floor2d = new FloorPlan2D();
                    floor2d.Floor = floor;

                    floor.FloorPlan2D = floor2d;

                    floor2d.TemplateLocation = filePath;

                    var addResult = _context.FloorPlan2Ds.AddAsync(floor2d);

                }
                else
                {
                    floor2d.TemplateLocation = filePath;
                    var updateResult2 = _context.FloorPlan2Ds.Update(floor2d);

                }
                var updateResult = _context.Floors.Update(floor);

                await file.CopyToAsync(stream);
                _context.SaveChanges();

            }
        }

    }

}
