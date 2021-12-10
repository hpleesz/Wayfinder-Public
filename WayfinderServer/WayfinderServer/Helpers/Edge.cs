using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Helpers
{
    public class Edge
    {
        public Edge(Vertex source, Vertex destination, double weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }

        public double Weight { get; set; }
        public Vertex Source { get; set; }
        public Vertex Destination { get; set; }
    }
}
