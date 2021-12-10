using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.Entities;

namespace WayfinderServer.DTOs
{
    public class FloorSwitcherDTO
    {
        public int Id { get; set; }
        public Place Place { get; set; }
        public string Name { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }
        public List<FloorSwitcherPointNewDTO> FloorSwitcherPoints { get; set; }


    }
}
