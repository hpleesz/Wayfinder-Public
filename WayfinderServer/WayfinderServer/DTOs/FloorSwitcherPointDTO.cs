using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.Entities;

namespace WayfinderServer.DTOs
{
    public class FloorSwitcherPointDTO
    {
        public int Id { get; set; }
        public FloorNewDTO Floor { get; set; }
        public FloorSwitcherNewDTO FloorSwitcher { get; set; }
        public string Name { get; set; }
        public float XCoordinate { get; set; }
        public float YCoordinate { get; set; }
        public float ZCoordinate { get; set; }
    }
}
