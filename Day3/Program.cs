using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day3
{
    class Program
    {
        class Claim
        {
            private static Regex r = new Regex("#([0-9]+) @ ([0-9]+),([0-9]+): ([0-9]+)x([0-9]+)", RegexOptions.Compiled);

            public int id;
            public int sizex;
            public int sizey;
            public int top;
            public int left;

            public Claim(string str)
            {
                var m = r.Match(str);
                id = int.Parse(m.Groups[1].Value);
                left = int.Parse(m.Groups[2].Value);
                top = int.Parse(m.Groups[3].Value);
                sizex = int.Parse(m.Groups[4].Value);
                sizey = int.Parse(m.Groups[5].Value);
            }

            public void check(Dictionary<string, int> area)
            {
                
                for (int x = 0 ; x < sizex ; x++)
                    for (int y = 0; y < sizey; y++)
                    {
                        string s = $"{x + left},{y + top}";
                        int i;
                        if (!area.TryGetValue(s, out i))
                            i = 0;
                        area[s] = i + 1;
                    }
            }

            public void map(Dictionary<string, int> area)
            {
                for (int x = 0; x < sizex; x++)
                    for (int y = 0; y < sizey; y++)
                    {
                        string s = $"{x + left},{y + top}";
                        area[s] = area.ContainsKey(s) ? -1 : id;
                    }
            }

            public bool full(Dictionary<string, int> area)
            {
                for (int x = 0; x < sizex; x++)
                    for (int y = 0; y < sizey; y++)
                    {
                        string s = $"{x + left},{y + top}";

                        int i;
                        if (!area.TryGetValue(s, out i) || i != id)
                            return false;
                    }
                return true;
            }
        }

        static void Main(string[] args)
        {
            var area = new Dictionary<string, int>();

            var claims = File.ReadAllLines("input").Select(x => new Claim(x));
            foreach (var c in claims)
                c.check(area);

            int result = area.Count(x => x.Value > 1);
            Console.WriteLine("Claimed more: {0}", result);

            area.Clear();
            foreach (var c in claims)
                c.map(area);

            foreach (var c in claims)
                if (c.full(area))
                    Console.WriteLine("Not overlapping id: {0}", c.id);
        }
    }
}
