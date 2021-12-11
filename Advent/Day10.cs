using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2021.Advent
{
    public static class Day10
    {

        public static void Star1()
        {
            var input = Input.Get("Day10");

            char[] opening = { '(', '[', '{', '<' };
            char[] closing = { ')', ']', '}', '>' };

            Dictionary<char, int> values = new Dictionary<char, int>
            {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 }
            };

            List<char> characters = new List<char>();
            int errorScore = 0;

            foreach (var line in input)
            {
                characters.Clear();
                foreach (var c in line)
                {
                    if (opening.Contains(c))
                        characters.Add(c);
                    else if (closing.Contains(c))
                    {
                        if (characters[^1] == GetOpening(c))
                            characters.RemoveAt(characters.Count - 1);
                        else
                        {
                            errorScore += values[c];
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(errorScore);
        }

        public static void Star2()
        {
            var input = Input.Get("Day10");

            char[] opening = { '(', '[', '{', '<' };
            char[] closing = { ')', ']', '}', '>' };

            Dictionary<char, int> values = new Dictionary<char, int>
            {
                { ')', 1 },
                { ']', 2 },
                { '}', 3 },
                { '>', 4 }
            };

            List<char> characters = new List<char>();
            List<long> sortingScores = new List<long>();

            foreach (var line in input)
            {
                characters.Clear();
                bool incomplete = true;
                foreach (var c in line)
                {
                    if (opening.Contains(c))
                        characters.Add(c);
                    else if (closing.Contains(c))
                    {
                        if (characters[^1] == GetOpening(c))
                            characters.RemoveAt(characters.Count - 1);
                        else
                        {
                            incomplete = false;
                            break;
                        }
                    }
                }
                if (incomplete)
                {
                    characters.Reverse();
                    long score = 0;
                    foreach (var c in characters)
                    {
                        score *= 5;
                        score += values[GetClosing(c)];
                    }
                    sortingScores.Add(score);
                }
            }

            var median = sortingScores.OrderBy(n => n).ElementAt(sortingScores.Count / 2 );
            Console.WriteLine(median);
        }

        private static char GetOpening(char c)
        {
            if (c == ')')
                return '(';
            if (c == ']')
                return '[';
            if (c == '}')
                return '{';
            if (c == '>')
                return '<';
            return 'a';
        }

        private static char GetClosing(char c)
        {
            if (c == '(')
                return ')';
            if (c == '[')
                return ']';
            if (c == '{')
                return '}';
            if (c == '<')
                return '>';
            return 'a';
        }
    }
}
