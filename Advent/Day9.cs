using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2021.Advent
{
    public static class Day9
    {
        public static void Star1()
        {
            var input = Input.Get("Day9").Select(n => n.ToCharArray()).ToArray();

            int sum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    bool lowest = true;

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            if (input[i][j] >= input[i + 1][j] || input[i][j] >= input[i][j + 1])
                                lowest = false;
                        }
                        else if (j == 99)
                        {
                            if (input[i][j] >= input[i + 1][j] || input[i][j] >= input[i][j - 1])
                                lowest = false;
                        }
                        else
                            if (input[i][j] >= input[i + 1][j] || input[i][j] >= input[i][j + 1] || input[i][j] >= input[i][j - 1])
                            lowest = false;
                    }
                    else if (i == 99)
                    {
                        if (j == 0)
                        {
                            if (input[i][j] >= input[i - 1][j] || input[i][j] >= input[i][j + 1])
                                lowest = false;
                        }
                        else if (j == 99)
                        {
                            if (input[i][j] >= input[i - 1][j] || input[i][j] >= input[i][j - 1])
                                lowest = false;
                        }
                        else
                            if (input[i][j] >= input[i - 1][j] || input[i][j] >= input[i][j + 1] || input[i][j] >= input[i][j - 1])
                            lowest = false;
                    }
                    else if (j == 0)
                    {
                        if (input[i][j] >= input[i - 1][j] || input[i][j] >= input[i][j + 1] || input[i][j] >= input[i + 1][j])
                            lowest = false;
                    }
                    else if (j == 99)
                    {
                        if (input[i][j] >= input[i - 1][j] || input[i][j] >= input[i][j - 1] || input[i][j] >= input[i + 1][j])
                            lowest = false;
                    }
                    else
                    {
                        if (input[i][j] >= input[i - 1][j] || input[i][j] >= input[i + 1][j] || input[i][j] >= input[i][j + 1] || input[i][j] >= input[i][j - 1])
                            lowest = false;
                    }

                    if (lowest)
                        sum += input[i][j] - '0' + 1;
                }
            }

            Console.WriteLine(sum);
        }

        public static void Star2()
        {
            var input = Input.Get("Day9").Select(n => n.ToCharArray()).ToArray();
            List<Tile> grid = new List<Tile>();

            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < input[i].Length; j++)
                    if (input[i][j] != '9')
                        grid.Add(new Tile(i, j, input[i][j] - '0'));

            List<int> basins = new List<int>();

            for (int i = 0; i < grid.Count; i++)
            {
                if(grid[i].Used is false)
                {
                    grid[i].Used = true;

                    int basinSum = FindBasin(grid[i], grid);
                    basins.Add(basinSum);
                }
            }
            basins = basins.OrderByDescending(n => n).ToList();
            Console.WriteLine(basins[0] * basins[1] * basins[2]);
        }

        private static int FindBasin(Tile tile, List<Tile> grid)
        {
            var nearby = grid.Where(t =>
            (tile.Placement.X == t.Placement.X && tile.Placement.Y == t.Placement.Y - 1) ||
            (tile.Placement.X == t.Placement.X && tile.Placement.Y == t.Placement.Y + 1) ||
            (tile.Placement.X == t.Placement.X - 1 && tile.Placement.Y == t.Placement.Y) ||
            (tile.Placement.X == t.Placement.X + 1 && tile.Placement.Y == t.Placement.Y))
                .Where(t => t.Used is false).ToList();

            nearby.ForEach(n => n.Used = true);
            int sum = 1;
            foreach (var t in nearby)
                sum += FindBasin(t, grid);
            return sum;
        }

        private class Tile
        {
            public Vector2 Placement;
            public int Value;
            public bool Used;

            public Tile(int x, int y, int value)
            {
                Placement = new Vector2(x, y);
                Value = value;
                Used = false;
            }
        }
    }
}
