using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Helpers
{
    public class Graph
    {
        private Dictionary<Vertex, List<Edge>> adjList;

        public Graph()
        {
            adjList = new Dictionary<Vertex, List<Edge>>();
        }

        public Dictionary<Vertex, List<Edge>> AdjList
        {
            get
            {
                return adjList;
            }
        }

        public void AddEdgeDirected(Vertex source, Vertex destination, double weight)
        {
            if (adjList.ContainsKey(source))
            {
                adjList[source].Add(new Edge(source, destination, weight));
            }
            else
            {
                adjList.Add(source, new List<Edge> { new Edge(source, destination, weight) });
            }

            if (!adjList.ContainsKey(destination))
            {
                adjList.Add(destination, new List<Edge>());
            }
        }
    }
}
