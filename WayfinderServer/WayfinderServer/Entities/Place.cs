using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Entities
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<Floor> Floors { get; set; }
        public List<Category> Categories { get; set; }
        public List<FloorSwitcher> FloorSwitchers { get; set; }


    }
}
