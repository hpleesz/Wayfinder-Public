using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.DTOs
{
    public class FloorSwitcherPointStep
    {
        public FloorSwitcherPointDTO floorSwitcherPoint { get; set; }
        public double distance { get; set; }
    }
}
