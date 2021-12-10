using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.DTOs
{
    public class FloorPlan2DDTO
    {
        public int Id { get; set; }
        //public Floor Floor { get; set; }
        public string FileLocation { get; set; }

        public string TemplateLocation { get; set; }
    }
}
