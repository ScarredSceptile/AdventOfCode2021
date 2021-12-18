using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Advent
{
    public static class Day18
    {
        public static void Star1()
        {
            var input = Input.Get("Day18").ToList();
            List<Item> items = new List<Item>();
            input.ForEach(n => items.Add(new Pair(n[1..^1], null)));
            while (items.Count > 1)
            {
                Pair t = new Pair(items[0], items[1], null);
                t.Left.Parent = t;
                t.Right.Parent = t;
                items[1] = t;
                items.RemoveAt(0);
                bool reduced = true;
                while (reduced)
                {
                    reduced = items[0].CheckExplode();
                    if (!reduced)
                        reduced = items[0].CheckSplit();
                }
            }
            Console.WriteLine(items[0].Print());
            Console.WriteLine(items[0].GetNum());
        }

        public static void Star2()
        {
            var input = Input.Get("Day18").ToList();
            int maxMag = 0;
            foreach(var item in input)
            {
                foreach (var item2 in input)
                {
                    if (item != item2)
                    {
                        Item i = new Pair(item[1..^1], null);
                        Item i2 = new Pair(item2[1..^1], null);
                        Pair t = new Pair(i, i2, null);
                        t.Left.Parent = t;
                        t.Right.Parent = t;
                        bool reduced = true;
                        while (reduced)
                        {
                            reduced = t.CheckExplode();
                            if (!reduced)
                                reduced = t.CheckSplit();
                        }
                        maxMag = Math.Max(maxMag, t.GetNum());
                    }
                }
            }
            Console.WriteLine(maxMag);
        }

        private class Item
        {
            public Item Parent;
            public bool Exploded;
            public bool Split;

            public virtual string Print() { return null; }
            public virtual int GetNum() { return 0; }
            public virtual void AddLeft(int n) { }
            public virtual void AddRight(int n) { }
            public virtual bool CheckExplode() { return false; }
            public virtual bool CheckSplit() { return false; }
            public virtual bool Explodes() { return false; }

        }

        private class Pair : Item
        {
            public Item Left;
            public Item Right;

            public Pair(string children, Item parent)
            {
                Parent = parent;

                if (char.IsDigit(children[0]))
                {
                    int i = 1;
                    while (char.IsDigit(children[i]))
                        i++;
                    int l = int.Parse(children.Substring(0, i));
                    Left = new Single(l, this);
                    if (char.IsDigit(children[i + 1]))
                    {
                        int j;
                        for (j = i + 2; j < children.Length; j++)
                            if (!char.IsDigit(children[j]))
                                break;
                        int r = int.Parse(children.Substring(i + 1, j - i - 1));
                        Right = new Single(r, this);
                    }
                    else
                    {
                        int brackets = 1;
                        i++;
                        int start = i + 1;
                        while (brackets > 0)
                        {
                            i++;
                            if (children[i] == '[')
                                brackets++;
                            else if (children[i] == ']')
                                brackets--;
                        }
                        Right = new Pair(children[start..i], this);
                    }
                }
                else
                {
                    int brackets = 1;
                    int start = 1, i = 0;
                    while (brackets > 0)
                    {
                        i++;
                        if (children[i] == '[')
                            brackets++;
                        else if (children[i] == ']')
                            brackets--;
                    }
                    Left = new Pair(children[start..i], this);
                    if (char.IsDigit(children[i + 2]))
                    {
                        int j;
                        for (j = i + 3; j < children.Length; j++)
                            if (!char.IsDigit(children[j]))
                                break;
                        int r = int.Parse(children.Substring(i + 2, j - i - 2));
                        Right = new Single(r, this);
                    }
                    else
                    {
                        brackets = 1;
                        i += 2;
                        start = i + 1;
                        while (brackets > 0)
                        {
                            i++;
                            if (children[i] == '[')
                                brackets++;
                            else if (children[i] == ']')
                                brackets--;
                        }
                        Right = new Pair(children[start..i], this);
                    }
                }
            }

            public Pair(int L, int R, Item parent)
            {
                Left = new Single(L, this);
                Right = new Single(R, this);
                Parent = parent;
            }
            public Pair(Item L, Item R, Item parent)
            {
                Left = L;
                Right = R;
                Parent = parent;
            }

            public override void AddLeft(int n)
            {
                Left.AddLeft(n);
            }
            public override void AddRight(int n)
            {
                Right.AddRight(n);
            }

            public override int GetNum()
            {
                return Left.GetNum() * 3 + Right.GetNum() * 2;
            }

            public override bool Explodes()
            {
                int i = 0;
                Item t = this;
                while (t.Parent != null)
                {
                    i++;
                    t = t.Parent;
                }

                //Explode
                if (i == 4)
                {
                    Exploded = true;
                    return true;
                }
                return false;
            }

            public override bool CheckExplode()
            {
                if (Explodes())
                    return true;
                bool childExplode = Left.Explodes();
                if (childExplode)
                {
                    LeftExplode();
                    return true;
                }
                childExplode = Left.CheckExplode();
                if (childExplode)
                    return childExplode;
                childExplode = Right.Explodes();
                if (childExplode)
                {
                    RightExplode();
                    return true;
                }
                return Right.CheckExplode();
            }

            public override bool CheckSplit()
            {
                bool childSplit = Left.CheckSplit();
                if (childSplit)
                {
                    if (Left is Single)
                    {
                        var n = Left.GetNum();
                        Left = new Pair(n / 2, n / 2 + n % 2, this);
                        if (Left.Explodes())
                            LeftExplode();
                    }
                    return true;
                }
                childSplit = Right.CheckSplit();
                if (childSplit)
                {
                    if (Right is Single)
                    {
                        var n = Right.GetNum();
                        Right = new Pair(n / 2, n / 2 + n % 2, this);
                        if (Right.Explodes())
                            RightExplode();
                    }
                    return true;
                }
                return false;
            }

            private void RightExplode()
            {
                Pair pair = Right as Pair;
                int l = pair.Left.GetNum();
                int r = pair.Right.GetNum();
                if (Left is Single)
                    Left.AddLeft(l);
                else
                    Left.AddRight(l);
                Right = new Single(0, this);

                Item th = this;
                bool rightMost = false;
                while ((th.Parent as Pair).Right == th)
                {
                    th = th.Parent;
                    if (th.Parent is null)
                    {
                        rightMost = true;
                        break;
                    }
                }
                if (!rightMost)
                    (th.Parent as Pair).Right.AddLeft(r);
            }

            private void LeftExplode()
            {
                Pair pair = Left as Pair;
                int l = pair.Left.GetNum();
                int r = pair.Right.GetNum();
                if (Right is Single)
                    Right.AddRight(r);
                else
                    Right.AddLeft(r);
                Left = new Single(0, this);

                Item th = this;
                bool leftMost = false;
                while ((th.Parent as Pair).Left == th)
                {
                    th = th.Parent;
                    if (th.Parent is null)
                    {
                        leftMost = true;
                        break;
                    }
                }
                if (!leftMost)
                    (th.Parent as Pair).Left.AddRight(l);
            }

            public override string Print()
            {
                return $"[{Left.Print()},{Right.Print()}]";
            }
        }

        private class Single : Item
        {
            public int Num;

            public Single(int Nr, Item parent)
            {
                Num = Nr;
                Parent = parent;
            }

            public override int GetNum()
            {
                return Num;
            }
            public override void AddLeft(int n)
            {
                Num += n;
            }

            public override void AddRight(int n)
            {
                Num += n;
            }

            public override string Print()
            {
                return Num.ToString();
            }
            public override bool CheckSplit()
            {
                return Split = Num > 9;
            }
        }
    }
}
