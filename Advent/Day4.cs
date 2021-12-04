using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Advent
{
    public static class Day4
    {
        public static void Star1()
        {
            var (Numbers, Cards) = Setup();

            int lastNum = 0;
            foreach (var num in Numbers)
            {
                lastNum = num;
                foreach (var card in Cards)
                {
                    if (card.num.Contains(num))
                    {
                        card.check[Array.IndexOf(card.num, num)] = true;
                    }
                }
                Cards.ForEach(card => card.CheckBingo());
                if (Cards.Any(card => card.isBingo)) break;
            }

            var bingoCard = Cards.Where(card => card.isBingo).FirstOrDefault();
            if (bingoCard != null)
                Console.WriteLine(lastNum * bingoCard.GetSum());
        }

        public static void Star2()
        {
            var (Numbers, Cards) = Setup();

            int lastNum = 0;
            foreach (var num in Numbers)
            {
                lastNum = num;
                foreach (var card in Cards)
                {
                    if (card.num.Contains(num))
                    {
                        card.check[Array.IndexOf(card.num, num)] = true;
                    }
                }
                Cards.ForEach(card => card.CheckBingo());
                if (Cards.All(card => card.isBingo))
                    break;
                Cards.RemoveAll(card => card.isBingo);
            }

            var bingoCard = Cards.FirstOrDefault();
            if (bingoCard != null)
                Console.WriteLine(lastNum * bingoCard.GetSum());
        }

        private static (int[] Numbers, List<BingoCard> Cards) Setup()
        {
            var input = Input.Get("Day4");
            string all = "";

            foreach (var line in input)
                all += line + "\n";

            var bingo = all.Split("\n\n");
            var nums = Array.ConvertAll(bingo[0].Split(","), n => int.Parse(n));
            bingo = bingo.Skip(1).ToArray();

            List<BingoCard> cards = new List<BingoCard>();
            foreach (var card in bingo)
                cards.Add(new BingoCard(card));

            return (nums, cards);
        }

        private class BingoCard
        {
            public int[] num;
            public bool[] check;
            public bool isBingo = false;

            public BingoCard(string input)
            {
                var nums = input.Replace("\n", " ").Split(" ").Where(s => s.Length != 0).ToArray();
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
