using System;
using System.IO;

namespace AdventOfCode2021.Advent
{
    public static class Day1
    {
        public static void Star1()
        {
            var measurements = Input.Get("Day1");
            var numericalMeasurements = Array.ConvertAll(measurements, m => int.Parse(m));

            int larger = 0;
            int previous = numericalMeasurements[0];

            for (int i = 1; i < numericalMeasurements.Length; i++)
            {
                if (numericalMeasurements[i] > previous)
                    larger++;
                previous = numericalMeasurements[i];
            }
            Console.WriteLine($"The total amount of increases is: {larger}");
        }

        public static void Star2()
        {
            var measurements = Input.Get("Day1");
            var numericalMeasurements = Array.ConvertAll(measurements, m => int.Parse(m));

            int[] tripleNums = new int[numericalMeasurements.Length - 2];

            for (int i = 0; i <= 2; i++)
            {
                for (int j = i; j < numericalMeasurements.Length - (2 - i); j++)
                {
                    tripleNums[j - i] += numericalMeasurements[j];
                }
            }

            int larger = 0;
            int previous = 0;

            for (int i = 1; i < tripleNums.Length; i++)
            {
                if (tripleNums[i] > previous)
                    larger++;
                previous = tripleNums[i];
            }
            Console.WriteLine($"The total amount of increases is: {larger}");
        }
    }
}
