using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Advent
{
    public static class Day8
    {
        public static void Star1()
        {
            var input = Input.Get("Day8");
            List<Digits> digits = new List<Digits>();

            foreach (var inp in input)
                digits.Add(new Digits(inp));

            var total = digits.Select(n => n.UniqueOutputLength()).Sum();
            Console.WriteLine(total);
        }

        public static void Star2()
        {
            var input = Input.Get("Day8");
            List<Digits> digits = new List<Digits>();

            foreach (var inp in input)
                digits.Add(new Digits(inp));

            digits.ForEach(d => d.PlaceUnique());
            digits.ForEach(d => d.PlaceRest());
            var total = digits.Select(n => n.GetOutput()).Sum();
            Console.WriteLine(total);
        }

        private class Digits
        {
            public string[] Input;
            public string[] Output;
            public string A, B, C, D, E, F, G;

            public Digits(string numbers)
            {
                var put = numbers.Split(" | ");
                Input = put[0].Split(" ");
                Output = put[1].Split(" ");
            }

            public int UniqueOutputLength()
            {
                int count = 0;
                foreach (var num in Output)
                    if (num.Length == 2 || num.Length == 3 || num.Length == 4 || num.Length == 7)
                        count++;
                return count;
            }

            public void PlaceUnique()
            {
                var one = Input.Where(n => n.Length == 2).FirstOrDefault();
                var seven = Input.Where(n => n.Length == 3).FirstOrDefault();
                var four = Input.Where(n => n.Length == 4).FirstOrDefault();
                var eight = Input.Where(n => n.Length == 7).FirstOrDefault();

                C = F = one;

                var cf = C.ToCharArray();
                var acf = seven.ToCharArray();
                A = acf.Where(c => cf.Any(d => d == c) == false).FirstOrDefault().ToString();

                cf = C.ToCharArray();
                var bdcf = four.ToCharArray();
                B = D = new string(bdcf.Where(b => cf.Any(c => c == b) == false).ToArray());

                var eg = eight.ToCharArray();
                eg = eg.Where(e => e != A[0]).ToArray();
                eg = eg.Where(e => B.Any(b => b == e) == false).ToArray();
                eg = eg.Where(e => C.Any(c => c == e) == false).ToArray();

                E = G = new string(eg);
            }

            public void PlaceRest()
            {
                var fiveDigit = Input.Where(n => n.Length == 5).Distinct().ToList();

                var three = fiveDigit.Where(d => d.Intersect(C).Count() == C.Count()).FirstOrDefault();
                D = three.Where(t => D.Any(d => d == t) is true).FirstOrDefault().ToString();
                B = B.Where(b => b != D[0]).FirstOrDefault().ToString();
                G = three.Where(t => G.Any(g => g == t) is true).FirstOrDefault().ToString();
                E = E.Where(e => e != G[0]).FirstOrDefault().ToString();

                var two = fiveDigit.Where(d => d.Contains(B)).FirstOrDefault();
                F = two.Where(t => F.Any(f => f == t) is true).FirstOrDefault().ToString();
                C = C.Where(c => c != F[0]).FirstOrDefault().ToString();
            }

            public int GetOutput()
            {
                string value = "";

                foreach (var digit in Output)
                {
                    if (digit.Length == 2)
                        value += "1";
                    else if (digit.Length == 3)
                        value += "7";
                    else if (digit.Length == 4)
                        value += "4";
                    else if (digit.Length == 5)
                    {
                        if (digit.Contains(E) && digit.Contains(C))
                            value += "2";
                        else if (digit.Contains(C) && digit.Contains(F))
                            value += "3";
                        else if (digit.Contains(B) && digit.Contains(F))
                            value += "5";
                    }
                    else if (digit.Length == 6)
                    {
                        if (digit.Contains(D) is false)
                            value += "0";
                        else if (digit.Contains(C) is false)
                            value += "6";
                        else if (digit.Contains(E) is false)
                            value += "9";
                    }
                    else if (digit.Length == 7)
                        value += "8";
                }
                return int.Parse(value);
            }
        }
    }
}
