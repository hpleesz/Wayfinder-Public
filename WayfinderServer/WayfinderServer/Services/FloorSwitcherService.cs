using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.Entities;

namespace WayfinderServer.Services
{
    public interface IFloorSwitcherService
    {
        //public IEnumerable<VirtualObjectType> GetAllVirtualObjectTypes();
        public int AddFloorSwitcher(FloorSwitcher floorSwitcher, int floorId);
        public IEnumerable<FloorSwitcher> GetFloorSwitchersSearch(string term, int placeId);
        public FloorSwitcher GetFloorSwitcher(int id);
        public IEnumerable<FloorSwitcher> GetAllFloorSwitchersByPlace(int placeId);


    }
    public class FloorSwitcherService : IFloorSwitcherService
    {
        private WayfinderContext _context;

        public FloorSwitcherService(WayfinderContext context)
        {
            _context = context;
        }

        public int AddFloorSwitcher(FloorSwitcher floorSwitcher, int placeId)
        {
            var place = _context.Places.SingleOrDefault(x => x.Id == placeId);

            floorSwitcher.Place = place;
            if (place.FloorSwitchers == null)
            {
                place.FloorSwitchers = new List<FloorSwitcher>();
            }

            place.FloorSwitchers.Add(floorSwitcher);

            var addResult = _context.FloorSwitchers.AddAsync(floorSwitcher);
            var updateResult = _context.Places.Update(place);

            _context.SaveChanges();

            return floorSwitcher.Id;
        }

        public FloorSwitcher GetFloorSwitcher(int id)
        {
            return _context.FloorSwitchers.Include(x => x.FloorSwitcherPoints).SingleOrDefault(x => x.Id == id);

        }
        public IEnumerable<FloorSwitcher> GetAllFloorSwitchersByPlace(int placeId)
        {
            return _context.FloorSwitchers.Include(t => t.FloorSwitcherPoints).Where(t => t.Place.Id == placeId).ToList();
        }

        public IEnumerable<FloorSwitcher> GetFloorSwitchersSearch(string term, int placeId)
        {
            if (term != null)
            {
                term = term.ToUpper();
                return _context.FloorSwitchers.Where(fs => fs.Place.Id == placeId && fs.Name.ToUpper().Contains(term)).ToList();

            }
            else
            {
                return GetAllFloorSwitchersByPlace(placeId);
            }
        }
    }
}
