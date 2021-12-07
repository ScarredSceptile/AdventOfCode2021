using System;
using System.Linq;

namespace AdventOfCode2021.Advent
{
    public static class Day7
    {
        public static void Star1()
        {
            var input = Input.Get("Day7")[0].Split(",").Select(n => int.Parse(n)).OrderBy(n => n).ToArray();

            bool efficient = false;
            int low = 0;
            int high = input.Length;
            int fuel = 0;

            // Runs once because the median is the most optimal. Loop is not required at all
            do
            {
                var mid = input[(low + high) / 2];
                var above = FuelConsumption(input, mid + 1);
                var target = FuelConsumption(input, mid);
                var below = FuelConsumption(input, mid - 1);

                if (above < target)
                    low = mid;
                else if (below < target)
                    high = mid;
                else
                {
                    fuel = target;
                    efficient = true;
                }
            } while (!efficient);
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

        private static int FuelConsumption(int[] input, int goal)
        {
            return input.Select(n => Math.Abs(n - goal)).Sum();
        }

        private static int ExpensiveFuelConsumption(int[] input, int goal)
        {
            return input.Select(n => Enumerable.Range(0, Math.Abs(n - goal) + 1).Sum()).Sum();
        }
    }
}
