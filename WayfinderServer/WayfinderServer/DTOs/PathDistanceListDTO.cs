using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.DTOs
{
    public class PathDistanceListDTO
    {
        public Dictionary<int, int> floorSwitcherPointDistances { get; set; }

    }
}
