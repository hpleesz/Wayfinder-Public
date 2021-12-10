using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Helpers
{
    public class Vertex
    {
        public Vertex(int key)
        {
            Key = key;
        }

        public int Key { get; set; }
    }
}
