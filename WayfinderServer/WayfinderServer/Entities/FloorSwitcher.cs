using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Entities
{
    public class FloorSwitcher
    {
        public int Id { get; set; }
        public Place Place { get; set; }
        public string Name { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }
        public List<FloorSwitcherPoint> FloorSwitcherPoints { get; set; }

    }
}
