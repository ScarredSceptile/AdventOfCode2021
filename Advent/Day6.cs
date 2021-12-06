using System;
using System.Linq;

namespace AdventOfCode2021.Advent
{
    public static class Day6
    {
        public static void Star1()
        {
            Console.WriteLine(IterateDays(80));
        }

        public static void Star2()
        {
            Console.WriteLine(IterateDays(256));
        }

        private static long IterateDays(int days)
        {
            var numbers = Input.Get("Day6")[0].Split(",").Select(n => int.Parse(n)).ToArray();
            return numbers.GroupBy(n => n).Select(n => n.Count() * Recursive(n.Key + 1, days)).Sum();
        }

        //Wrong on first iteration for some reason. Adding 1 to the number selected gives the right output.
        private static long Recursive(int number, int daysLeft)
        {
            if (number > daysLeft)
                return 1;
            else
            {
                daysLeft -= (number + 1);
                return Recursive(8, daysLeft) + Recursive(6, daysLeft);
            }
        }
    }
}
