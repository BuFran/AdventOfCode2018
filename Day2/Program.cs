using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day2
{
    class Program
    {
        const int TWO = 1;
        const int THREE = 2;

        // https://adventofcode.com/2018/day/2
        static void Main(string[] args)
        {
            var fd = File.ReadAllLines("input");
            
            var cnt = fd.Select(CountOfTwo).ToArray();

            int two = cnt.Count(x => (x & TWO) != 0);
            int three = cnt.Count(x => (x & THREE) != 0);

            Console.WriteLine("{0} * {1} = {2}", two, three, two * three);

            int dl = fd[0].Length - 1;

            var p = fd.SelectMany(x => fd, (x, y) => common(x, y)).Where(x=> x.Length == dl).Distinct().ToArray();

            foreach (var i in p)
                Console.WriteLine("'{0}'", i);
        }

        static int CountOfTwo(string x)
        {
            Dictionary<char, int> d = new Dictionary<char, int>();

            foreach (char c in x)
            {
                int i;
                if (!d.TryGetValue(c, out i))
                    i = 0;

                d[c] = i + 1;
            }

            return (d.ContainsValue(2) ? TWO : 0) | (d.ContainsValue(3) ? THREE : 0);
        }

        static string common(string a, string b)
        {
            StringBuilder sb = new StringBuilder(a.Length);

            for (int i = 0; i < a.Length; i++)
                if (a[i] == b[i])
                    sb.Append(a[i]);

            return sb.ToString();
        }
    }
}
