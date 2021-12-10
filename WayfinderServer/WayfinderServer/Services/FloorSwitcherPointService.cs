using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.Entities;

namespace WayfinderServer.Services
{
    public interface IFloorSwitcherPointService
    {
        //public IEnumerable<VirtualObjectType> GetAllVirtualObjectTypes();
        public int AddFloorSwitcherPoint(FloorSwitcherPoint floorSwitcherPoint, int floorId);
        public int EditCoordinates(int id, FloorSwitcherPoint floorSwitcherPoint);
        public IEnumerable<FloorSwitcherPoint> GetFloorSwitcherPointsByPlaceSearch(string term, int placeId);
        public IEnumerable<FloorSwitcherPoint> GetFloorSwitcherPointsByFloorSearch(string term, int floorId);
        public FloorSwitcherPoint GetFloorSwitcherPoint(int id);
        public IEnumerable<FloorSwitcherPoint> GetAllFloorSwitcherPointsByFloor(int floorId);
        public IEnumerable<FloorSwitcherPoint> GetAllFloorSwitcherPointsByPlace(int placeId);
        public int AddFloorSwitcherPointsToFloorSwitcher(List<FloorSwitcherPoint> floorSwitcherPoints, int floorSwitcherId);
        public IEnumerable<FloorSwitcherPoint> GetFloorSwitcherPointsByFloorSwitcher(int floorSwitcherId);
        public IEnumerable<FloorSwitcherPoint> GetFreeFloorSwitcherPointsAndByFloorSwitcher(int floorSwitcherId);
        public IEnumerable<FloorSwitcherPoint> GetFreeFloorSwitcherPointsByPlace(int placeId);

    }
    public class FloorSwitcherPointService : IFloorSwitcherPointService
    {
        private WayfinderContext _context;

        public FloorSwitcherPointService(WayfinderContext context)
        {
            _context = context;
        }

        public int AddFloorSwitcherPoint(FloorSwitcherPoint floorSwitcherPoint, int floorId)
        {
            var floor = _context.Floors.SingleOrDefault(x => x.Id == floorId);

            floorSwitcherPoint.Floor = floor;
            if (floor.FloorSwitcherPoints == null)
            {
                floor.FloorSwitcherPoints = new List<FloorSwitcherPoint>();
            }

            floor.FloorSwitcherPoints.Add(floorSwitcherPoint);

            var addResult = _context.FloorSwitcherPoints.AddAsync(floorSwitcherPoint);
            var updateResult = _context.Floors.Update(floor);

            _context.SaveChanges();

            return floorSwitcherPoint.Id;
        }

        public int EditCoordinates(int id, FloorSwitcherPoint floorSwitcherPoint)
        {
            var result = _context.FloorSwitcherPoints.SingleOrDefault(x => x.Id == id);
            result.XCoordinate = floorSwitcherPoint.XCoordinate;
            result.YCoordinate = floorSwitcherPoint.YCoordinate;
            result.ZCoordinate = floorSwitcherPoint.ZCoordinate;

            var updated = _context.FloorSwitcherPoints.Update(result);
            _context.SaveChanges();
            //return result.IsCompletedSuccessfully;
            return result.Id;
        }

        public int AddFloorSwitcherPointsToFloorSwitcher(List<FloorSwitcherPoint> floorSwitcherPoints, int floorSwitcherId)
        {
            var floorSwitcher = _context.FloorSwitchers.SingleOrDefault(x => x.Id == floorSwitcherId);
            floorSwitcher.FloorSwitcherPoints = new List<FloorSwitcherPoint>();


            foreach (FloorSwitcherPoint floorSwitcherPoint in floorSwitcherPoints)
            {
                var floorSwitcherPointSaved = _context.FloorSwitcherPoints.SingleOrDefault(x => x.Id == floorSwitcherPoint.Id);
                floorSwitcherPointSaved.FloorSwitcher = floorSwitcher;

                var updated = _context.FloorSwitcherPoints.Update(floorSwitcherPointSaved);

                floorSwitcher.FloorSwitcherPoints.Add(floorSwitcherPointSaved);

            }

            var updateResult = _context.FloorSwitchers.Update(floorSwitcher);


            _context.SaveChanges();
            return 1;
        }

        public IEnumerable<FloorSwitcherPoint> GetFloorSwitcherPointsByPlaceSearch(string term, int placeId)
        {
            if (term != null)
            {
                term = term.ToUpper();
                return _context.FloorSwitcherPoints.Include(f => f.Floor).Where(f => f.Floor.Place.Id == placeId && f.Name.ToUpper().Contains(term)).ToList();

            }
            else
            {
                return GetAllFloorSwitcherPointsByPlace(placeId);
            }
        }

        public IEnumerable<FloorSwitcherPoint> GetFloorSwitcherPointsByFloorSearch(string term, int floorId)
        {
            if (term != null)
            {
                term = term.ToUpper();
                return _context.FloorSwitcherPoints.Include(f => f.Floor).Where(f => f.Floor.Id == floorId && f.Name.ToUpper().Contains(term)).ToList();

            }
            else
            {
                return GetAllFloorSwitcherPointsByFloor(floorId);
            }
        }


        public IEnumerable<FloorSwitcherPoint> GetFloorSwitcherPointsByFloorSwitcher(int floorSwitcherId)
        {
            return _context.FloorSwitcherPoints.Include(f => f.FloorSwitcher).Include(f => f.Floor).Where(f => f.FloorSwitcher.Id == floorSwitcherId).ToList();
        }

        public IEnumerable<FloorSwitcherPoint> GetFreeFloorSwitcherPointsAndByFloorSwitcher(int floorSwitcherId)
        {
            return _context.FloorSwitcherPoints.Include(f => f.FloorSwitcher).Where(f => f.FloorSwitcher.Id == floorSwitcherId || f.FloorSwitcher == null).ToList();
        }

        public IEnumerable<FloorSwitcherPoint> GetFreeFloorSwitcherPointsByPlace(int placeId)
        {
            return _context.FloorSwitcherPoints.Include(f => f.FloorSwitcher).Include(f => f.Floor).Where(f => f.Floor.Place.Id == placeId && f.FloorSwitcher == null).ToList();
        }

        public FloorSwitcherPoint GetFloorSwitcherPoint(int id)
        {
            return _context.FloorSwitcherPoints.Include(x => x.Floor).Include(x => x.Floor.FloorPlan3D).SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<FloorSwitcherPoint> GetAllFloorSwitcherPointsByFloor(int floorId)
        {
            return _context.FloorSwitcherPoints.Include(t => t.Floor).Where(t => t.Floor.Id == floorId).ToList();
        }

        public IEnumerable<FloorSwitcherPoint> GetAllFloorSwitcherPointsByPlace(int placeId)
        {
            return _context.FloorSwitcherPoints.Include(t => t.Floor).Where(t => t.Floor.Place.Id == placeId).ToList();
        }
    }
}
