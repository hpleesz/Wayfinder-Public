using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.DTOs
{
    public class FloorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        //public PlaceNewDTO Place { get; set; }
        public FloorPlan2DDTO FloorPlan2D { get; set; }
        public FloorPlan3DDTO FloorPlan3D { get; set; }
        public List<TargetNewDTO> Targets { get; set; }
        //public List<TargetDTO> Targets { get; set; }
        public List<MarkerNewDTO> Markers { get; set; }
    }
}
