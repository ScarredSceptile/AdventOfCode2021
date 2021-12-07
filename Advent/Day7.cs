using System;
using System.Linq;

namespace AdventOfCode2021.Advent
{
    public static class Day7
    {
        public static void Star1()
        {
            var input = Input.Get("Day7")[0].Split(",").Select(n => int.Parse(n)).OrderBy(n => n).ToArray();
            var fuel = input.Select(n => Math.Abs(n - input[input.Length/2])).Sum();
            Console.WriteLine(fuel);
        }

        public static void Star2()
        {
            var input = Input.Get("Day7")[0].Split(",").Select(n => int.Parse(n)).OrderBy(n => n).ToArray();
            int mostEfficient = int.MaxValue;

            for (int i = 0; i <= input.Max(); i++)
            {
                var fuel = ExpensiveFuelConsumption(input, i);
                if (fuel < mostEfficient)
                    mostEfficient = fuel;
            }

            Console.WriteLine(mostEfficient);
        }

        private static int ExpensiveFuelConsumption(int[] input, int goal)
        {
            return input.Select(n => Enumerable.Range(0, Math.Abs(n - goal) + 1).Sum()).Sum();
        }
    }
}
