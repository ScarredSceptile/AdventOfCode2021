using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Advent
{
    public static class Day2
    {
        public static void Star1()
        {
            var measurements = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Day2.txt");

            int horizontal = measurements.Where(m => m.StartsWith("forward")).Select(forward => int.Parse(forward.Remove(0, 8))).Sum();
            int up = measurements.Where(m => m.StartsWith("up")).Select(forward => int.Parse(forward.Remove(0, 3))).Sum();
            int down = measurements.Where(m => m.StartsWith("down")).Select(forward => int.Parse(forward.Remove(0, 5))).Sum();
            int depth = down - up;

            Console.WriteLine(horizontal * depth);
        }

        public static void Star2()
        {
            var measurements = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Day2.txt");

            int horizontal = measurements.Where(m => m.StartsWith("forward")).Select(forward => int.Parse(forward.Remove(0, 8))).Sum();

            int aim = 0;
            int depth = 0;

            foreach (var task in measurements)
            {
                if (task.StartsWith("forward"))
                    depth += int.Parse(task.Remove(0, 8)) * aim;
                else if (task.StartsWith("up"))
                    aim -= int.Parse(task.Remove(0, 3));
                else if (task.StartsWith("down"))
                    aim += int.Parse(task.Remove(0, 5));
            }

            Console.WriteLine(horizontal * depth);
        }
    }
}