using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.DTOs;

namespace WayfinderServer.DTOs
{
    public class TargetDistanceNewDTO
    {
        public int Id { get; set; }
        public FloorSwitcherPointNewDTO FloorSwitcherPoint { get; set; }
        public TargetNewDTO Target { get; set; }
        public float Distance { get; set; }
    }
}
