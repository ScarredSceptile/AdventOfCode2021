using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Advent
{
    public static class Day12
    {
        public static void Star1()
        {
            var graph = Setup();
            var startNode = graph.Where(n => n.Name == "start").First();
            var totPaths = NavigateAll(startNode, graph);
            Console.WriteLine(totPaths);
        }

        public static void Star2()
        {
            var graph = Setup();
            var startNode = graph.Where(n => n.Name == "start").First();
            var totPaths = NavigateAll2(startNode, graph);
            Console.WriteLine(totPaths);
        }

        private static List<Node> Setup()
        {
            var input = Input.Get("Day12").Select(n => n.Split("-")).ToArray();

            List<Node> graph = new List<Node>();

            foreach (var line in input)
                foreach (var dest in line)
                    if (graph.Where(n => n.Name == dest).Count() == 0)
                        graph.Add(new Node(dest));
            foreach (var line in input)
            {
                graph.Where(n => n.Name == line[0]).First().Connections.Add(line[1]);
                graph.Where(n => n.Name == line[1]).First().Connections.Add(line[0]);
            }
            return graph;
        }

        private static int NavigateAll(Node node, List<Node> graph)
        {
            if (node.Name == "end")
                return 1;
            if (node.SmallCave && node.visited)
                return 0;

            node.visited = true;

            int paths = 0;
            foreach (var n in node.Connections)
            {
                var nextNode = graph.Where(m => m.Name == n).First();
                if (nextNode.Name != "start")
                    paths += NavigateAll(nextNode, graph);
            }
            node.visited = false;
            return paths;
        }

        private static long NavigateAll2(Node node, List<Node> graph)
        {
            if (node.Name == "end")
                return 1;

            if (node.SmallCave)
                if (node.numVisits == 1)
                    if (graph.Where(n => n.SmallCave).Where(n => n.numVisits > 1).Count() > 0)
                        return 0;
            if (node.SmallCave && node.numVisits == 2)
                return 0;

            node.numVisits++;

            long paths = 0;
            foreach (var n in node.Connections)
            {
                var nextNode = graph.Where(m => m.Name == n).First();
                if (nextNode.Name != "start")
                    paths += NavigateAll2(nextNode, graph);
            }
            node.numVisits--;
            return paths;
        }

        private class Node
        {
            public string Name;
            public List<string> Connections;
            public bool SmallCave;
            public bool visited;
            public int numVisits;

            public Node(string name)
            {
                Name = name;
                Connections = new List<string>();
                SmallCave = Name.All(char.IsLower);
            }
        }
    }
}
