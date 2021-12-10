using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WayfinderServer.Helpers
{

    public sealed class PathFinding
    {
        private static PathFinding instance = null;
        private static readonly object padlock = new object();

        PathFinding()
        {
        }

        public static PathFinding Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PathFinding();
                    }
                    return instance;
                }
            }
        }


        private static Dictionary<Vertex, Vertex> outerParents;
        public static List<PathStep> pathSteps;
        public Dictionary<Vertex, double> DijstrasShortestPath(Graph graph, Vertex source)
        {
            var pq = new SimplePriorityQueue<Vertex, double>();
            var weights = new Dictionary<Vertex, double>();
            var parents = new Dictionary<Vertex, Vertex>();
            var dq = new HashSet<Vertex>();

            foreach (var v in graph.AdjList.Keys)
            {
                var vertex = v;

                if (v.Key == source.Key)
                {
                    // O(log n)
                    pq.Enqueue(v, 0);
                }
                else
                {
                    // O(log n)
                    pq.Enqueue(v, int.MaxValue);
                }
            }

            weights.Add(source, 0);
            parents.Add(source, null);

            while (pq.Count > 0)
            {
                // O(log n)
                var current = pq.First();
                var weight = pq.GetPriority(current);
                pq.Dequeue();

                dq.Add(current);

                // update shortest dist of current vertex from source
                if (!weights.TryAdd(current, weight))
                {
                    weights[current] = weight;
                }

                foreach (var adjEdge in graph.AdjList[current])
                {
                    var adj = adjEdge.Source.Key == current.Key
                        ? adjEdge.Destination
                        : adjEdge.Source;

                    // skip already dequeued vertices. O(1)
                    if (dq.Contains(adj))
                    {
                        continue;
                    }

                    double calcWeight = weights[current] + adjEdge.Weight;
                    // O(1)
                    double adjWeight = pq.GetPriority(adj);

                    // is tense?
                    if (calcWeight < adjWeight)
                    {
                        // relax
                        // O(log n)
                        pq.UpdatePriority(adj, calcWeight);

                        if (!parents.TryAdd(adj, current))
                        {
                            parents[adj] = current;
                        }
                    }
                }

            }

            // only here for PrintShortestPaths() & PrintShortestPath() - not recommended
            outerParents = parents;

            return weights;
        }

        private void PrintPath(Vertex vertex, Dictionary<Vertex, Vertex> parents, Dictionary<Vertex, double> path)
        {
            if (vertex == null || !parents.ContainsKey(vertex))
            {
                return;
            }

            PrintPath(parents[vertex], parents, path);
            Console.WriteLine($" {vertex.Key} ({path[vertex]})");

            PathStep pathStep = new PathStep(vertex.Key, path[vertex]);
            pathSteps.Add(pathStep);
        }


        public void PrintShortestPath(Graph graph, Vertex source, Vertex destination)
        {
            var path = DijstrasShortestPath(graph, source);

            // print shortest between source and destination vertices
            pathSteps = new List<PathStep>();
            PrintPath(destination, outerParents, path);
        }

        public static Dictionary<Vertex, double> Path;

        public void MakeGraph(Graph graph, Vertex source)
        {
            Path = DijstrasShortestPath(graph, source);

        }

        public void PrintPrintShortestPathToDestination(Vertex destination)
        {
            pathSteps = new List<PathStep>();
            PrintPath(destination, outerParents, Path);
        }

    }
}
