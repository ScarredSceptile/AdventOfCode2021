using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode2021.Advent
{
    public static class Day5
    {
        public static void Star1()
        {
            var input = Input.Get("Day5");

            List<Line> Lines = new List<Line>();
            foreach (var line in input)
                Lines.Add(new Line(line));

            int[,] Diagram = new int[1000, 1000];
            var straight = Lines.Where(l => l.IsStraight()).ToArray();

            Diagram = CalculateStraights(Diagram, straight);

            int count = 0;
            foreach (var l in Diagram)
                if (l > 1) count++;
            Console.WriteLine(count);
        }

        public static void Star2()
        {
            var input = Input.Get("Day5");

            List<Line> Lines = new List<Line>();
            foreach (var line in input)
                Lines.Add(new Line(line));

            int[,] Diagram = new int[1000, 1000];
            var straight = Lines.Where(l => l.IsStraight()).ToArray();
            var diagonal = Lines.Where(l => l.IsStraight() is false).ToArray();

            Diagram = CalculateStraights(Diagram, straight);

            foreach(var line in diagonal)
            {
                int x = 0;
                if (line.From.X < line.To.X)
                {
                    if (line.From.Y < line.To.Y)
                        for (int i = (int)line.From.X; i <= line.To.X; i++)
                            Diagram[(int)line.From.X + x, (int)line.From.Y + x++]++;

                    else
                        for (int i = (int)line.From.X; i <= line.To.X; i++)
                            Diagram[(int)line.From.X + x, (int)line.From.Y - x++]++;
                }
                else
                {
                    if (line.From.Y < line.To.Y)
                        for (int i = (int)line.From.X; i >= line.To.X; i--)
                            Diagram[(int)line.From.X - x, (int)line.From.Y + x++]++;

                    else
                        for (int i = (int)line.From.X; i >= line.To.X; i--)
                            Diagram[(int)line.From.X - x, (int)line.From.Y - x++]++;
                }
            }

            int count = 0;
            foreach (var l in Diagram)
                if (l > 1) count++;
            Console.WriteLine(count);
        }

        private static int[,] CalculateStraights(int[,] Diagram, Line[] Lines)
        {
            foreach (var line in Lines)
            {
                if (line.From.X == line.To.X)
                {
                    if (line.From.Y < line.To.Y)
                        for (int i = (int)line.From.Y; i <= line.To.Y; i++)
                            Diagram[(int)line.From.X, i]++;

                    else if (line.From.Y > line.To.Y)
                        for (int i = (int)line.To.Y; i <= line.From.Y; i++)
                            Diagram[(int)line.From.X, i]++;

                    else
                        Diagram[(int)line.From.X, (int)line.From.Y]++;
                }
                else if (line.From.Y == line.To.Y)
                {
                    if (line.From.X < line.To.X)
                        for (int i = (int)line.From.X; i <= line.To.X; i++)
                            Diagram[i, (int)line.From.Y]++;

                    else if (line.From.X > line.To.X)
                        for (int i = (int)line.To.X; i <= line.From.X; i++)
                            Diagram[i, (int)line.From.Y]++;

                    else
                        Diagram[(int)line.From.X, (int)line.From.Y]++;
                }
                else
                {
                    Console.Error.WriteLine("Non-straight line got through filtering!");
                }
            }
            return Diagram;
        }

        private class Line
        {
            public Vector2 From;
            public Vector2 To;

            public Line(string input)
            {
                var vectors = input.Split(" -> ");
                var fromVector = vectors[0].Split(",");
                var toVector = vectors[1].Split(",");

                From = new Vector2(int.Parse(fromVector[0]), int.Parse(fromVector[1]));
                To = new Vector2(int.Parse(toVector[0]), int.Parse(toVector[1]));
            }

            public bool IsStraight()
            {
                return From.X == To.X || From.Y == To.Y;
            }
        }
    }
}
