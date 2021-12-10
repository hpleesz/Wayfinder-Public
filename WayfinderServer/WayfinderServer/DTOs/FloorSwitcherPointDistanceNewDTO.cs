using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.DTOs
{
    public class FloorSwitcherPointDistanceNewDTO
    {
        public int Id { get; set; }
        public FloorSwitcherPointNewDTO FloorSwitcherPoint1 { get; set; }
        public FloorSwitcherPointNewDTO FloorSwitcherPoint2 { get; set; }
        public float Distance { get; set; }
    }
}
