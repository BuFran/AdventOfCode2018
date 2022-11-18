using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day8
{
    //https://adventofcode.com/2018/day/10
    class Program
    {

        static void Main(string[] args)
        {
            var stars = File.ReadAllLines("input").Select(x=>new Star(x)).ToArray();

            var e = Enumerable.Range(8000, 12000).Select(x => new { Val = GetBounds(stars, x), Shift = x }).OrderBy(x=>x.Val.Size).ToArray();


            for (int i = 0; i < 10; i++)
                Debug.WriteLine($"{i}: {e[i].Shift} : {e[i].Val.Width} x {e[i].Val.Height}");

            var m = e.First();
            
            var desk = new bool[m.Val.Width+3, m.Val.Height+3];

            foreach (var i in stars.Select(x => x.Shifted(e[0].Shift)))
                desk[i.X - m.Val.Left + 1, i.Y - m.Val.Top + 1] = true;

            for (int y = 0; y <= desk.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= desk.GetUpperBound(0); x++)
                    Debug.Write(desk[x, y] ? 'X' : '.');

                Debug.WriteLine("");
            }
        }

        static BoundingBox GetBounds(Star[] data, int time)
        {
            return data.Aggregate(new BoundingBox(data[0].Shifted(time)), (a, x) => new BoundingBox(x.Shifted(time), a));
        }
    }

    class BoundingBox
    {
        public int Left, Top, Right, Bottom;

        public BoundingBox(Point p, BoundingBox parent = null)
        {
            if (parent == null)
            {
                Left = Right = p.X;
                Top = Bottom = p.Y;
            }
            else
            {
                Left = Math.Min(parent.Left, p.X);
                Right = Math.Max(parent.Right, p.X);
                Top = Math.Min(parent.Top, p.Y);
                Bottom = Math.Max(parent.Bottom, p.Y);
            }
        }

        public Int64 Size { get { return Width * Height; } }
        public Int64 Width { get { return (Right - Left); } }
        public Int64 Height { get { return (Bottom - Top); } }
    }

    class Star
    {
        Regex r = new Regex(@"position=< *?([\-\d]+), *?([\-\d]+)> velocity=< *?([\-\d]+), *?([\-\d]+)>", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        public Point Position;
        public Point velocity;

        public Star(string x)
        {
            Match m = r.Match(x);

            Position = new Point(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value));
            velocity = new Point(int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value));
        }

        public Point Shifted(int i)
        {
            return new Point(Position.X + velocity.X * i, Position.Y + velocity.Y * i);
        }
    }
    
}
