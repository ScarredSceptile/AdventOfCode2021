using System;
using System.IO;

namespace AdventOfCode2021.Advent
{
    public static class Day1
    {
        public static void Star1()
        {
            var measurements = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Measurements.txt");
            var numericalMeasurements = Array.ConvertAll(measurements, m => int.Parse(m));

            int larger = 0;
            int previous = 0;

            for (int i = 1; i < numericalMeasurements.Length; i++)
            {
                if (numericalMeasurements[i] > previous)
                {
                    larger++;
                    Console.WriteLine($"{numericalMeasurements[i]} (increase)");
                }
                else
                    Console.WriteLine($"{numericalMeasurements[i]} (decrease)");
                previous = numericalMeasurements[i];
            }
            Console.WriteLine($"The total amount of increases is: {larger}");
        }

    }
}
