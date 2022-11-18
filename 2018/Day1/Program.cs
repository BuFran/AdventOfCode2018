using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    // https://adventofcode.com/2018/day/1
    class Program
    {
        static void Main(string[] args)
        {
            var l = new HashSet<int>();
            var fd = File.ReadAllLines("input").Select(x => int.Parse(x)).ToArray();

            Console.WriteLine("Resulting frequency: {0}", fd.Sum());

            var cs = 0;
            l.Add(cs);
            for (int j = 0; j < 10000; j++)
            {
                foreach (var i in fd)
                {
                    cs += i;
                    if (l.Contains(cs))
                    {
                        Console.WriteLine("Frequency reached twice: {0}", cs);
                        return;
                    }

                    l.Add(cs);
                }
                Console.WriteLine("Round: {0}", j);
            }
        }
    }
}
