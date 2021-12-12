using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode2021.Advent
{
    public static class Day11
    {
        public static void Star1()
        {
            var input = Input.Get("Day11").Select(n => n.Select(i => int.Parse(i.ToString())).ToArray()).ToArray();

            List<Octopus> octopi = new List<Octopus>();

            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < input[i].Length; j++)
                    octopi.Add(new Octopus(input[i][j], i, j));
            long flashed = 0;
            for (int i = 0; i < 100; i++)
            {
                octopi.ForEach(o => o.Level++);

                do
                {
                    foreach (var octo in octopi.Where(o => o.Level > 9 && o.Flashed is false).ToArray())
                    {
                        octo.Flashed = true;
                        flashed++;
                        octopi.Where(o => (Math.Abs(o.Position.X - octo.Position.X) == 1 || o.Position.X - octo.Position.X == 0)
                        && (Math.Abs(o.Position.Y - octo.Position.Y) == 1 || o.Position.Y - octo.Position.Y == 0)).ToList().ForEach(o => o.Level++);
                    }

                } while (octopi.Where(o => o.Level > 9 && o.Flashed is false).ToArray().Length > 0);

                octopi.Where(o => o.Level > 9).ToList().ForEach(o => o.Level = 0);
                octopi.Where(o => o.Flashed).ToList().ForEach(o => o.Flashed = false);
            }
            Console.WriteLine(flashed);
        }

        public static void Star2()
        {
            var input = Input.Get("Day11").Select(n => n.Select(i => int.Parse(i.ToString())).ToArray()).ToArray();

            List<Octopus> octopi = new List<Octopus>();

            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < input[i].Length; j++)
                    octopi.Add(new Octopus(input[i][j], i, j));
            int step = 0;
            bool allFlash = false;
            do
            {
                octopi.ForEach(o => o.Level++);
                step++;
                do
                {
                    foreach (var octo in octopi.Where(o => o.Level > 9 && o.Flashed is false).ToArray())
                    {
                        octo.Flashed = true;
                        octopi.Where(o => (Math.Abs(o.Position.X - octo.Position.X) == 1 || o.Position.X - octo.Position.X == 0)
                        && (Math.Abs(o.Position.Y - octo.Position.Y) == 1 || o.Position.Y - octo.Position.Y == 0)).ToList().ForEach(o => o.Level++);
                    }

                } while (octopi.Where(o => o.Level > 9 && o.Flashed is false).ToArray().Length > 0);

                allFlash = octopi.All(o => o.Flashed);
                octopi.Where(o => o.Level > 9).ToList().ForEach(o => o.Level = 0);
                octopi.Where(o => o.Flashed).ToList().ForEach(o => o.Flashed = false);
            } while (allFlash is false);
            Console.WriteLine(step);
        }

        private class Octopus
        {
            public int Level;
            public bool Flashed;
            public Vector2 Position;

            public Octopus(int level, int x, int y)
            {
                Level = level;
                Flashed = false;
                Position = new Vector2(x, y);
            }
        }
    }
}
