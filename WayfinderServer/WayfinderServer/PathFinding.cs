﻿using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

public class Program
{
    public class Edge
    {
        public Edge(Vertex source, Vertex destination, int weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }

        public int Weight { get; set; }
        public Vertex Source { get; set; }
        public Vertex Destination { get; set; }
    }

    public class Vertex
    {
        public Vertex(int key)
        {
            Key = key;
        }

        public int Key { get; set; }
    }

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

        public void AddEdgeDirected(Vertex source, Vertex destination, int weight)
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

        // Don't actually do this (i.e. actually write 2 classes for directed/undirected):
        public void AddEdgeUndirected(Vertex source, Vertex destination, int weight)
        {
            if (adjList.ContainsKey(source))
            {
                adjList[source].Add(new Edge(source, destination, weight));
            }
            else
            {
                adjList.Add(source, new List<Edge> { new Edge(source, destination, weight) });
            }

            if (adjList.ContainsKey(destination))
            {
                adjList[destination].Add(new Edge(destination, source, weight));
            }
            else
            {
                adjList.Add(destination, new List<Edge> { new Edge(destination, source, weight) });
            }
        }
    }

    public static int ShortestPath(Graph graph, Vertex a, Vertex b)
    {
        var shortestPaths = DijstrasShortestPath(graph, a);

        return shortestPaths[b];
    }

    private static Dictionary<Vertex, Vertex> outerParents;

    public static Dictionary<Vertex, int> DijstrasShortestPath(Graph graph, Vertex source)
    {
        var pq = new SimplePriorityQueue<Vertex, int>();
        var weights = new Dictionary<Vertex, int>();
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

                int calcWeight = weights[current] + adjEdge.Weight;
                // O(1)
                int adjWeight = pq.GetPriority(adj);

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

    public static void PrintShortestPaths(Graph graph, Vertex source)
    {
        var path = DijstrasShortestPath(graph, source);

        foreach (var kvp in path)
        {
            var vertex = kvp.Key;
            var shortestPathWeight = kvp.Value;
            Console.WriteLine($"{vertex.Key} ({shortestPathWeight})");
        }
    }

    public static void PrintShortestPath(Graph graph, Vertex source, Vertex destination)
    {
        var path = DijstrasShortestPath(graph, source);

        // print shortest between source and destination vertices
        PrintPath(destination, outerParents, path);
    }

    private static void PrintPath(Vertex vertex, Dictionary<Vertex, Vertex> parents, Dictionary<Vertex, int> path)
    {
        if (vertex == null || !parents.ContainsKey(vertex))
        {
            return;
        }

        PrintPath(parents[vertex], parents, path);
        Console.WriteLine($" {vertex.Key} ({path[vertex]})");
    }

    public class Node
    {
        public Node(Vertex v, int priority)
        {
            V = v;
            Priority = priority;
        }

        public int Priority { get; set; }
        public Vertex V { get; set; }
    }

    // Use only C# standard library. As there is no priority queue there is less efficiency
    // Big O in part derived from: https://github.com/RehanSaeed/.NET-Big-O-Algorithm-Complexity-Cheat-Sheet
    public static Dictionary<Vertex, int> DijstrasShortestPath2(Graph graph, Vertex source)
    {
        var map = new Dictionary<Vertex, Node>();
        // set list capacity to number of vertices to keep Add() at O(1)
        // For details see:
        // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.add?view=netframework-4.8#remarks
        var pq = new List<Node>(graph.AdjList.Keys.Count);
        var weights = new Dictionary<Vertex, int>();
        var parents = new Dictionary<Vertex, Vertex>();
        var dq = new HashSet<Vertex>();

        foreach (var v in graph.AdjList.Keys)
        {
            var node = new Node(v, 0);
            if (v.Key == source.Key)
            {
                map.Add(v, node);
                // O(1) unless capacity is exceeded
                pq.Add(node);
            }
            else
            {
                node.Priority = int.MaxValue;
                map.Add(v, node);
                // O(1) unless capacity is exceeded
                pq.Add(node);
            }
        }

        weights.Add(source, 0);
        parents.Add(source, null);

        while (pq.Count > 0)
        {
            // sort by priority. O(n log n)
            pq.Sort((a, b) => a.Priority.CompareTo(b.Priority));

            var temp = pq[0];
            // O(n)
            pq.RemoveAt(0);
            var current = temp.V;
            var weight = temp.Priority;

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

                // skip already dequeued vertices
                if (dq.Contains(adj))
                {
                    continue;
                }

                int calcWeight = weights[current] + adjEdge.Weight;
                var adjNode = map[adj];
                int adjWeight = adjNode.Priority;

                // is tense?
                if (calcWeight < adjWeight)
                {
                    // relax
                    map[adj].Priority = calcWeight;
                    // potentially O(n)
                    pq.Find(n => n == adjNode).Priority = calcWeight;

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

    public static Dictionary<Vertex, Vertex> ShortestPathUnWeighted(Graph graph, Vertex source, Vertex dest)
    {
        var parents = new Dictionary<Vertex, Vertex>();
        var visited = new HashSet<Vertex>();
        var q = new Queue<Vertex>();
        q.Enqueue(source);
        visited.Add(source);
        parents.Add(source, null);

        while (q.Count > 0)
        {
            var current = q.Dequeue();

            foreach (var node in graph.AdjList[current])
            {
                var adj = node.Source.Key == current.Key
                    ? node.Destination
                    : node.Source;

                if (visited.Contains(adj))
                {
                    continue;
                }

                parents.Add(adj, current);

                if (adj.Key == dest.Key)
                {
                    return parents;
                }

                visited.Add(adj);
                q.Enqueue(adj);
            }
        }

        return new Dictionary<Vertex, Vertex> { { source, null } };
    }

    public static void PrintShortestPathUnWeighted(Graph graph, Vertex source, Vertex dest)
    {
        var parents = ShortestPathUnWeighted(graph, source, dest);

        PrintPath(dest, parents);
    }

    private static void PrintPath(Vertex v, Dictionary<Vertex, Vertex> parents)
    {
        if (v == null || !parents.ContainsKey(v))
        {
            return;
        }

        PrintPath(parents[v], parents);
        Console.WriteLine($" {v.Key}");
    }

    public class Graph2
    {
        private int[,] adjMatrix;

        public Graph2(int vertices)
        {
            adjMatrix = new int[vertices, vertices];
        }

        public int[,] AdjMatrix
        {
            get
            {
                return adjMatrix;
            }
        }

        public void AddEdgeDirected(int source, int destination, int weight)
        {
            adjMatrix[source, destination] = weight;
        }

        public void AddEdgeUndirected(int source, int destination, int weight)
        {
            adjMatrix[source, destination] = weight;
            adjMatrix[destination, source] = weight;
        }
    }

    private static Dictionary<int, int> outerParents2;

    public static int[] DijkstraShortestPathMatrix(Graph2 graph, int source)
    {
        var parents = new Dictionary<int, int>();
        var numV = graph.AdjMatrix.GetLength(0);
        var visited = new HashSet<int>();
        var q = new Queue<int>();
        var weights = new int[numV];

        for (int i = 0; i < numV; i++)
        {
            weights[i] = int.MaxValue;
        }

        weights[source] = 0;
        q.Enqueue(source);
        parents.Add(source, -1);

        for (int i = 0; i < numV; i++)
        {
            // pick the vertex with min distance
            // just like a pq for adj list, need to work in order of priority
            var u = GetMinWeightVertex(weights, visited);
            visited.Add(u);

            for (int j = 0; j < numV; j++)
            {
                if (graph.AdjMatrix[u, j] > 0 && !visited.Contains(j))
                {
                    var edgeWeight = graph.AdjMatrix[u, j];
                    var calcWeight = weights[u] + edgeWeight;
                    var adjWeight = weights[j];

                    // is tense?
                    if (calcWeight < adjWeight)
                    {
                        // relax
                        weights[j] = calcWeight;

                        if (!parents.TryAdd(j, u))
                        {
                            parents[j] = u;
                        }
                    }
                }
            }
        }

        outerParents2 = parents;

        return weights;
    }

    private static int GetMinWeightVertex(int[] weights, HashSet<int> visited)
    {
        var minWeightVertex = -1;
        var minWeight = int.MaxValue;

        for (int i = 0; i < weights.Length; i++)
        {
            if (!visited.Contains(i) && weights[i] <= minWeight)
            {
                minWeight = weights[i];
                minWeightVertex = i;
            }
        }

        return minWeightVertex;
    }

    public static void PrintShortestPathMatrix(Graph2 graph, int source, int dest)
    {
        var path = DijkstraShortestPathMatrix(graph, source);

        PrintPath(dest, outerParents2, path);
    }

    private static void PrintPath(int v, Dictionary<int, int> parents, int[] path)
    {
        if (!parents.ContainsKey(v))
        {
            return;
        }

        PrintPath(parents[v], parents, path);
        Console.WriteLine($"{v} ({path[v]})");
    }

    public static Dictionary<int, int> ShortestPathUnWeightedMatrix(Graph2 graph, int source, int dest)
    {
        var numVertices = graph.AdjMatrix.GetLength(0);
        var parents = new Dictionary<int, int>();
        var visited = new HashSet<int>();
        var q = new Queue<int>();
        visited.Add(source);
        q.Enqueue(source);
        parents.Add(source, -1);

        while (q.Count > 0)
        {
            var current = q.Dequeue();

            for (int i = 0; i < numVertices; i++)
            {
                if (graph.AdjMatrix[current, i] > 0 & !visited.Contains(i))
                {
                    if (!parents.TryAdd(i, current))
                    {
                        parents[i] = current;
                    }

                    if (i == dest)
                    {
                        return parents;
                    }

                    q.Enqueue(i);
                    visited.Add(i);
                }
            }
        }

        return new Dictionary<int, int> { { source, -1 } };
    }

    public static void PrintShortestPathUnWeightedMatrix(Graph2 graph, int source, int dest)
    {
        var parents = ShortestPathUnWeightedMatrix(graph, source, dest);

        PrintPath(dest, parents);
    }

    private static void PrintPath(int v, Dictionary<int, int> parents)
    {
        if (!parents.ContainsKey(v) || parents[v] == -1)
        {
            return;
        }

        PrintPath(parents[v], parents);
        Console.WriteLine($" {v}");
    }

    public static void Test()
    {
        // Adjacency List:

        var directedAL = new Graph();
        var undirectedAL = new Graph();

        // 1 --> 2 --> 3 --> 4 --> 7 = 56
        // 1 --> 6 --> 7 = 70
        var source = new Vertex(1);
        var two = new Vertex(2);
        var three = new Vertex(3);
        var four = new Vertex(4);
        var five = new Vertex(5);
        var six = new Vertex(6);
        var dest = new Vertex(7);
        directedAL.AddEdgeDirected(source, two, 12);
        directedAL.AddEdgeDirected(two, three, 12);
        directedAL.AddEdgeDirected(three, four, 12);
        directedAL.AddEdgeDirected(four, dest, 20);
        directedAL.AddEdgeDirected(source, five, 12);
        directedAL.AddEdgeDirected(source, six, 30);
        directedAL.AddEdgeDirected(six, dest, 40);

        undirectedAL.AddEdgeUndirected(source, two, 12);
        undirectedAL.AddEdgeUndirected(two, three, 12);
        undirectedAL.AddEdgeUndirected(three, four, 12);
        undirectedAL.AddEdgeUndirected(four, dest, 20);
        undirectedAL.AddEdgeUndirected(source, five, 12);
        undirectedAL.AddEdgeUndirected(source, six, 30);
        undirectedAL.AddEdgeUndirected(six, dest, 40);

        var dijkstra = DijstrasShortestPath(directedAL, source);
        var dijkstra2 = DijstrasShortestPath2(directedAL, source);
        var dijkstra3 = DijstrasShortestPath(undirectedAL, source);
        var dijkstra4 = DijstrasShortestPath2(undirectedAL, source);
        var shortest = ShortestPathUnWeighted(directedAL, source, dest);
        var shortest2 = ShortestPathUnWeighted(undirectedAL, source, dest);

        PrintShortestPath(directedAL, source, dest);
        PrintShortestPath(undirectedAL, source, dest);
        PrintShortestPaths(directedAL, source);
        PrintShortestPaths(undirectedAL, source);
        PrintShortestPathUnWeighted(directedAL, source, dest);
        PrintShortestPathUnWeighted(undirectedAL, source, dest);

        // -----------------------------------------------------------------------------
        // Adjacency Matrix:

        var directedAM = new Graph2(10);
        var undirectedAM = new Graph2(10);

        // 1 --> 2 --> 3 --> 4 --> 7 = 56
        // 1 --> 6 --> 7 = 70
        directedAM.AddEdgeDirected(1, 2, 12);
        directedAM.AddEdgeDirected(2, 3, 12);
        directedAM.AddEdgeDirected(3, 4, 12);
        directedAM.AddEdgeDirected(4, 7, 20);
        directedAM.AddEdgeDirected(1, 5, 12);
        directedAM.AddEdgeDirected(1, 6, 30);
        directedAM.AddEdgeDirected(6, 7, 40);

        undirectedAM.AddEdgeUndirected(1, 2, 12);
        undirectedAM.AddEdgeUndirected(2, 3, 12);
        undirectedAM.AddEdgeUndirected(3, 4, 12);
        undirectedAM.AddEdgeUndirected(4, 7, 20);
        undirectedAM.AddEdgeUndirected(1, 5, 12);
        undirectedAM.AddEdgeUndirected(1, 6, 30);
        undirectedAM.AddEdgeUndirected(6, 7, 40);

        var dijkstraAM = DijkstraShortestPathMatrix(directedAM, 1);
        var dijkstraAM2 = DijkstraShortestPathMatrix(undirectedAM, 1);
        var shortestAM = ShortestPathUnWeightedMatrix(directedAM, 1, 7);
        var shortestAM2 = ShortestPathUnWeightedMatrix(undirectedAM, 1, 7);

        PrintShortestPathMatrix(directedAM, 1, 7);
        PrintShortestPathMatrix(undirectedAM, 1, 7);
    }
}