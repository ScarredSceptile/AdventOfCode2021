using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2021.Advent
{
    public static class Day15
    {
        public static void Star1()
        {
            var input = Input.Get("Day15");

            List<Node> Graph = new List<Node>();
            for (int y = 0; y < input.Length; y++)
                for (int x = 0; x < input[y].Length; x++)
                    Graph.Add(new Node(x, y, int.Parse(input[y][x].ToString())));

            Graph.First().TotalCost = 0;
            var sum = Traverse(Graph.First(), Graph.Last(), Graph);
            Console.WriteLine(sum);
        }

        public static void Star2()
        {
            var input = Input.Get("Day15");

            List<Node> Graph = new List<Node>();
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            var cost = ((int.Parse(input[y][x].ToString()) + i + j - 1) % 9) + 1;
                            Graph.Add(new Node(x + i * input[y].Length, y + j * input.Length, cost));
                        }
                    }
                }
            }
            Graph.First().TotalCost = 0;
            var sum = Traverse(Graph.First(), Graph.Last(), Graph);
            Console.WriteLine(sum);
        }

        private static long Traverse(Node startNode, Node goal, List<Node> graph)
        {
            List<Node> VisitedNodes = new List<Node>
            {
                startNode
            };
            List<Node> UnvistiedNodes = graph.Skip(1).ToList();

            while (VisitedNodes.Contains(goal) is false)
            {
                var cur = VisitedNodes[^1];
                UnvistiedNodes.Where(n => (Math.Abs(n.Position.X - cur.Position.X) == 1 && n.Position.Y - cur.Position.Y == 0)
                || (Math.Abs(n.Position.Y - cur.Position.Y) == 1 && n.Position.X - cur.Position.X == 0)).ToList()
                .ForEach(n => n.TotalCost = Math.Min(n.TotalCost, cur.TotalCost + n.Cost));
                var closest = UnvistiedNodes.OrderBy(n => n.TotalCost).First();
                VisitedNodes.Add(closest);
                UnvistiedNodes.Remove(closest);
            }

            return goal.TotalCost;
        }

        private class Node
        {
            public Vector2 Position;
            public long Cost;
            public long TotalCost;

            public Node(int x, int y, int cost)
            {
                Position = new Vector2(x, y);
                Cost = cost;
                TotalCost = long.MaxValue;
            }
        }
    }
}
