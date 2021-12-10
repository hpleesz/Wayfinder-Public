using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Entities
{
    public class TargetDistance
    {
        public int Id { get; set; }
        public FloorSwitcherPoint FloorSwitcherPoint { get; set; }
        public Target Target { get; set; }
        public float Distance { get; set; }

    }
}
