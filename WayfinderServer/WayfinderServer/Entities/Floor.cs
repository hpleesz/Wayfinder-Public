using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Entities
{
    public class Floor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public Place Place { get; set; }
        public FloorPlan2D FloorPlan2D { get; set; }
        public FloorPlan3D FloorPlan3D { get; set; }
        public List<Target> Targets { get; set; }
        public List<Marker> Markers { get; set; }
        public List<FloorSwitcherPoint> FloorSwitcherPoints { get; set; }

    }
}
