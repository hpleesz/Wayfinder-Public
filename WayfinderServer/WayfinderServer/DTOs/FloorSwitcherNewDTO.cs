using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.DTOs
{
    public class FloorSwitcherNewDTO
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Up { get; set; }
            public bool Down { get; set; }

    }
}
