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
    //https://adventofcode.com/2018/day/11
    class Program
    {

        static int input = 3031;

        static void Main(string[] args)
        {

            Debug.Assert(4 == cell(3, 5, 8));
            Debug.Assert(-5 == cell(122, 79, 57));
            Debug.Assert(0 == cell(217, 196, 39));
            Debug.Assert(4 == cell(101, 153, 71));

            var g = Grid(18);
            Debug.Assert(4 == g[33, 45]);
            Debug.Assert(4 == g[34, 45]);
            Debug.Assert(4 == g[35, 45]);
            Debug.Assert(4 == g[35, 46]);
            Debug.Assert(4 == g[35, 47]);
            Debug.Assert(3 == g[33, 46]);
            Debug.Assert(3 == g[34, 46]);
            Debug.Assert(1 == g[33, 47]);
            Debug.Assert(2 == g[34, 47]);

            Debug.Assert(29 == subsum(g, 33, 45, 3));

            Debug.Assert(new Point(33, 45) == largest(Grid(18), 3).OrderByDescending(x=>x.Value).First().Key);
            Debug.Assert(new Point(21, 61) == largest(Grid(42), 3).OrderByDescending(x => x.Value).First().Key);


            var gr = Grid(input);
            var pt = largest(gr, 3).OrderByDescending(x => x.Value).First().Key;

            Debug.WriteLine($"3x3 cell with largest power is: {pt.X},{pt.Y}");

            var d = new List<Tuple<Point, int,int>>();
            for (int i=3;i<300;i++)
            {
                var p = largest(gr, i);

                if (p.Count == 0)
                {
                    Debug.WriteLine($"No more at {i}");
                    break;
                }

                var q = p.OrderByDescending(x => x.Value).First().Key;
                Debug.WriteLine($"{q.X},{q.Y},{i} = {p[q]} [{p.Count}]");
                d.Add(new Tuple<Point, int, int>(q, i, p[q]));
            }

            var m = d.OrderByDescending(x => x.Item3).First();

            Debug.WriteLine($"Largest was: {m.Item1.X},{m.Item1.Y},{m.Item2} = {m.Item3}");
        }

        public static int cell(int x, int y, int ser)
        {
            Int64 rackid = x + 10;
            Int64 p = rackid * rackid * y + ser * rackid;
            p = (p / 100) % 10;

            return (int)p - 5;
        }

        public static int[,] Grid(int ser)
        {
            int[,] grid = new int[300, 300];

            for (int x = 0; x < 300; x++)
                for (int y = 0; y < 300; y++)
                    grid[x, y] = cell(x, y, ser);

            return grid;
        }

        public static Point[] AllPoints()
        {
            List<Point> p = new List<Point>();
            for (int x = 0; x < 300; x++)
                for (int y = 0; y < 300; y++)
                    p.Add(new Point(x, y));

            return p.ToArray();
        }

        public static Dictionary<Point,int> largest(int[,] grid, int n)
        {
            var d = new Dictionary<Point, int>();

            foreach (var p in AllPoints())
            {
                int i = subsum(grid, p.X, p.Y, n);
                if (i >= 0)
                    d[p] = i;
            }

            return d;


        }

        public static int subsum(int[,] grid, int xx, int yy, int n)
        {
            int sum = 0;
            for (int x = 0; x < n; x++)
                for (int y = 0; y < n; y++)
                {
                    if ((xx + x >= 300) || (yy + y >= 300))
                        return -1;

                    int p = grid[xx + x, yy + y];
                    /*if (p < 0)
                        return -1;*/

                    sum += p;
                }
            return sum;
        }

    }
    
    
}
