using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Advent
{
    public static class Day4
    {
        public static void Star1()
        {
            var input = Input.Get("Day4");
            string all = "";

            foreach (var line in input)
                all += line + "\n";

            var bingo = all.Split("\n\n");
            var numbers = bingo[0];
            bingo = bingo.Skip(1).ToArray();

            List<BingoCard> cards = new List<BingoCard>();
            foreach (var card in bingo)
            {
                var b = new BingoCard(card);
                cards.Add(b);
            }

            var nums = Array.ConvertAll(numbers.Split(","), n => int.Parse(n));
            int lastNum = 0;
            foreach (var num in nums)
            {
                lastNum = num;
                foreach (var card in cards)
                {
                    if (card.num.Contains(num))
                    {
                        card.check[Array.IndexOf(card.num, num)] = true;
                    }
                }
                cards.ForEach(card => card.CheckBingo());
                if (cards.Any(card => card.isBingo)) break;
            }

            var bingoCard = cards.Where(card => card.isBingo).FirstOrDefault();
            if (bingoCard != null)
                Console.WriteLine(lastNum * bingoCard.GetSum());
        }

        public static void Star2()
        {
            var input = Input.Get("Day4");
            string all = "";

            foreach (var line in input)
                all += line + "\n";

            var bingo = all.Split("\n\n");
            var numbers = bingo[0];
            bingo = bingo.Skip(1).ToArray();

            List<BingoCard> cards = new List<BingoCard>();
            foreach (var card in bingo)
            {
                var b = new BingoCard(card);
                cards.Add(b);
            }

            var nums = Array.ConvertAll(numbers.Split(","), n => int.Parse(n));
            int lastNum = 0;
            foreach (var num in nums)
            {
                lastNum = num;
                if (cards.Count != 1)
                {
                    foreach (var card in cards)
                    {
                        if (card.num.Contains(num))
                        {
                            card.check[Array.IndexOf(card.num, num)] = true;
                        }
                    }
                    cards.ForEach(card => card.CheckBingo());
                    cards.RemoveAll(card => card.isBingo);
                }
                else
                {
                    if (cards[0].num.Contains(num))
                        cards[0].check[Array.IndexOf(cards[0].num, num)] = true;
                    cards[0].CheckBingo();
                    if (cards[0].isBingo) break;
                }
            }

            var bingoCard = cards.FirstOrDefault();
            if (bingoCard != null)
                Console.WriteLine(lastNum * bingoCard.GetSum());
        }

        private class BingoCard
        {
            public int[] num;
            public bool[] check;
            public bool isBingo = false;

            public BingoCard(string input)
            {
                var nums = input.Split("\n");
                string full = "";
                foreach (var line in nums)
                    full += line + " ";

                nums = full.Split(" ");
                nums = nums.Where(s => s.Length != 0).ToArray();
                num = Array.ConvertAll(nums, n => int.Parse(n));
                check = new bool[num.Length];
            }

            public void CheckBingo()
            {
                bool bingo = false;
                for (int i = 0; i < 5; i++)
                {
                    //Rows
                    bingo = check[i * 5] is true && check[i * 5 + 1] is true && check[i * 5 + 2] is true && check[i * 5 + 3] is true && check[i * 5 + 4] is true;
                    if (bingo) break;
                    //Colums
                    bingo = check[i] is true && check[i + 5] is true && check[i + 10] is true && check[i + 15] is true && check[i + 20] is true;
                    if (bingo) break;
                }
                isBingo = bingo;
            }

            public int GetSum()
            {
                int sum = 0;
                for (int i = 0; i < 25; i++)
                    if (check[i] is false) sum += num[i];
                return sum;
            }
        }
    }
}
