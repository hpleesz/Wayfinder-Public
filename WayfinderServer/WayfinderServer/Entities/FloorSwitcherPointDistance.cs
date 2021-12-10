using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Entities
{
    public class FloorSwitcherPointDistance
    {
        public int Id { get; set; }
        public FloorSwitcherPoint FloorSwitcherPoint1 { get; set; }
        public FloorSwitcherPoint FloorSwitcherPoint2 { get; set; }
        public float Distance { get; set; }  
    }
}
