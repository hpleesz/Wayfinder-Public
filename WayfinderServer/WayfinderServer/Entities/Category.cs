using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Place Place { get; set; }
        public List<Target> Targets { get; set; }

    }
}
