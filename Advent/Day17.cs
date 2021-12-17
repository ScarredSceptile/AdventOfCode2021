using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AdventOfCode2021.Advent
{
    public static class Day17
    {
        public static void Star1()
        {
            var input = Input.Get("Day17")[0].Split(",");
            var y = input[1].Split("=")[1].Split("..");
            var lower = int.Parse(y[0]);

            var topY = -(lower + 1);
            var velocity = topY;
            var maxY = 0;
            while (velocity != 0)
            {
                maxY += velocity;
                velocity--;
            }
            Console.WriteLine(maxY);
        }

        public static void Star2()
        {
            var input = Input.Get("Day17")[0].Split(",");
            var x = input[0].Split("=")[1].Split("..");
            var y = input[1].Split("=")[1].Split("..");
            Vector2 UL = new Vector2(int.Parse(x[0]), int.Parse(y[1]));
            Vector2 LR = new Vector2(int.Parse(x[1]), int.Parse(y[0]));
            var totVel = 0;
            var topY = (int)-(LR.Y + 1);
            var posX = 0;
            int X = 0;
            bool foundX = false;
            while (foundX is false)
            {
                X++;
                posX += X;
                if (posX > UL.X)
                    foundX = true;
            }

            for (int i = X; i <= LR.X; i++)
            {
                for (int j = (int)LR.Y; j <= topY; j++)
                {
                    var xPos = 0;
                    var yPos = 0;
                    var xVelo = i;
                    var yVelo = j;
                    while (yPos >= LR.Y)
                    {
                        xPos += xVelo;
                        yPos += yVelo;
                        if (xVelo != 0)
                            xVelo += -Math.Sign(xVelo) * 1;
                        yVelo--;
                        if (xPos >= UL.X && xPos <= LR.X && yPos <= UL.Y && yPos >= LR.Y)
                        {
                            totVel++;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine(totVel);
        }
    }
}
