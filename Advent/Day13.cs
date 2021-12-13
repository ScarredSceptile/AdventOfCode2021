using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Advent
{
    public static class Day13
    {
        public static void Star1()
        {
            var (graph, folds) = Setup();
            graph = Fold(graph, folds[0]).GroupBy(n => new { n.X, n.Y }).Select(group => group.First()).ToList();
            Console.WriteLine(graph.Count);
        }

        public static void Star2()
        {
            var (graph, folds) = Setup();

            foreach (var fold in folds)
                graph = Fold(graph, fold).GroupBy(n => new { n.X, n.Y }).Select(group => group.First()).ToList();

            var maxX = graph.Select(n => n.X).Max();
            var maxY = graph.Select(n => n.Y).Max();
            string output = "";

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    var dot = graph.Where(n => n.X == x && n.Y == y).FirstOrDefault();
                    if (dot is null)
                        output += " ";
                    else
                        output += "#";
                }
                output += "\n";
            }
            Console.WriteLine(output);
        }

        private static (List<Dot>, string[]) Setup()
        {
            var input = Input.Get("Day13").ToList();
            var complete = "";
            input.ForEach(n => complete += n + "\n");
            var parts = complete.Split("\n\n");
            var dots = parts[0].Split("\n");
            var folds = parts[1].Split("\n");
            List<Dot> graph = new List<Dot>();

            foreach (var dot in dots)
            {
                var n = dot.Split(",");
                graph.Add(new Dot(int.Parse(n[0]), int.Parse(n[1])));
            }
            var t = folds.ToList();
            t.RemoveAt(folds.Length - 1);
            folds = t.ToArray();

            return (graph, folds);
        }

        private static List<Dot> Fold(List<Dot> graph, string folds)
        {
            var fold = folds.Split("=");
            int axis = int.Parse(fold[1]);

            if (fold[0][^1] == 'x')
                graph.Where(n => n.X > axis).ToList()
                    .ForEach(n => n.X = axis - (n.X - axis));
            else if (fold[0][^1] == 'y')
                graph.Where(n => n.Y > axis).ToList()
                    .ForEach(n => n.Y = axis - (n.Y - axis));
            return graph;
        }

        private class Dot
        {
            public int X;
            public int Y;

            public Dot(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
