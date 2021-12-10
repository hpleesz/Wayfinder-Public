using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PlaceDTO Place { get; set; }
        public List<TargetNewDTO> Targets { get; set; }
        //public List<Target> Targets { get; set; }
    }
}
