using System;
using System.Linq;

namespace AdventOfCode2021.Advent
{
    public static class Day3
    {
        public static void Star1()
        {
            var measurements = Input.Get("Day3");

            var gamma = "";
            var epsilon = "";

            for (int i = 0; i < measurements[0].Length; i++)
            {
                gamma += measurements.GroupBy(arr => arr[i]).OrderByDescending(m => m.Count()).FirstOrDefault().Key;
            }

            foreach (var c in gamma)
            {
                if (c == '1')
                    epsilon += '0';
                else if (c == '0')
                    epsilon += '1';
            }

            var gammaNum = Convert.ToInt32(gamma, 2);
            var epsilonNum = Convert.ToInt32(epsilon, 2);

            Console.WriteLine(gammaNum * epsilonNum);
        }

        public static void Star2()
        {
            var measurements = Input.Get("Day3");

            var oxygen = measurements;
            for (int i = 0; i < oxygen[0].Length; i++)
            {
                if (oxygen.Length == 1)
                    break;
                var mostCommon = oxygen.GroupBy(arr => arr[i]).OrderByDescending(m => m.Key).OrderByDescending(m => m.Count()).FirstOrDefault().Key;
                oxygen = oxygen.Where(o => o[i] == mostCommon).ToArray();
            }

            var scrubber = measurements;
            for (int i = 0; i < oxygen[0].Length; i++)
            {
                if (scrubber.Length == 1)
                    break;
                var leastCommon = scrubber.GroupBy(arr => arr[i]).OrderBy(m => m.Key).OrderBy(m => m.Count()).FirstOrDefault().Key;
                scrubber = scrubber.Where(o => o[i] == leastCommon).ToArray();
            }

            var oxyNum = Convert.ToInt32(oxygen[0], 2);
            var scrubNum = Convert.ToInt32(scrubber[0], 2);

            Console.WriteLine(oxyNum * scrubNum);
        }
    }
}
