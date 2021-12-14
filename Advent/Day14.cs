using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Advent
{
    public static class Day14
    {
        public static void Star1()
        {
            Console.WriteLine(GetByDays(10));
        }

        public static void Star2()
        {
            Console.WriteLine(GetByDays(40));
        }

        private static long GetByDays(int days)
        {
            var input = Input.Get("Day14");
            string template = input[0];
            input = input.Skip(2).ToArray();


            Dictionary<string, long> Values = new Dictionary<string, long>();
            foreach (var pair in input)
            {
                var ins = pair.Split(" -> ");
                if (Values.ContainsKey(ins[0]) is false)
                    Values.Add(ins[0], 0);
                if (Values.ContainsKey(ins[0][0] + ins[1]) is false)
                    Values.Add(ins[0][0] + ins[1], 0);
                if (Values.ContainsKey(ins[1] + ins[0][1]) is false)
                    Values.Add(ins[1] + ins[0][1], 0);
            }

            for (int n = 0; n < template.Length - 1; n++)
            {
                if (Values.ContainsKey(template.Substring(n, 2)) is false)
                    Values.Add(template.Substring(n, 2), 1);
                else
                    Values[template.Substring(n, 2)] += 1;
            }

            Dictionary<string, char> pairInsertions = new Dictionary<string, char>();
            foreach (var pair in input)
            {
                var ins = pair.Split(" -> ");
                pairInsertions.Add(ins[0], ins[1][0]);
            }

            var allstring = string.Join("", Values.Keys);
            var val = allstring.GroupBy(n => n).ToDictionary(n => n.Key, g => (long)0);

            foreach (var c in template)
                val[c]++;

            for (int i = 0; i < days; i++)
            {
                var tempValues = Values.ToDictionary(k => k.Key, v => v.Value);
                foreach (var pair in pairInsertions)
                {
                    tempValues[pair.Key[0] + pair.Value.ToString()] += Values[pair.Key];
                    tempValues[pair.Value.ToString() + pair.Key[1]] += Values[pair.Key];
                    tempValues[pair.Key] -= Values[pair.Key];
                    val[pair.Value] += Values[pair.Key];
                }
                Values = tempValues;
            }

            var list = val.OrderByDescending(n => n.Value).ToList();
            var sum = list[0].Value - list[^1].Value;
            return sum;
        }
    }
}
