using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Entities
{
    public class FloorSwitcherPoint: IPoint
    {
        public int Id { get; set; }
        public Floor Floor { get; set; }
        public FloorSwitcher FloorSwitcher { get; set; }
        public string Name { get; set; }
        public float XCoordinate { get; set; }
        public float YCoordinate { get; set; }
        public float ZCoordinate { get; set; }


    }
}
