using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Entities
{
    public class FloorPlan2D
    {
        public int Id { get; set; }
        [ForeignKey("FloorId")]
        public Floor Floor { get; set; }
        public string FileLocation { get; set; }

        public string TemplateLocation { get; set; }
    }
}
