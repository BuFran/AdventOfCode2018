using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day4
{
    class Program
    {
        struct cmd
        {
            public DateTime p1;
            public string p2;
        }

        class guard
        {
            public int id = 0;
            public DateTime sleep;
            public DateTime wake;
            public double length { get { return (wake - sleep).TotalMinutes; } }

            public IEnumerable<int> Mins
            {
                get
                {
                    for (int sta = sleep.Minute; sta < wake.Minute; sta++)
                        yield return sta;
                }
            }
        }
        static void Main(string[] args)
        {
            var p = File.ReadAllLines("input").Select(Transform).OrderBy(x=>x.p1).ToArray();
           // var p = File.ReadAllLines("TextFile1.txt").Select(Transform).OrderBy(x => x.p1).ToArray();

            var l = new List<guard>();

            guard actual = null;

            for (int i=0;i<p.Length;i++)
            {
                if (p[i].p2.Contains("Guard"))
                {
                    string[] pp = p[i].p2.Split(' ', '#');
                    
                    actual = new guard() { id = int.Parse(pp[3]) };
                    l.Add(actual);
                }
                else if (p[i].p2.Contains("falls"))
                {
                    Debug.Assert(actual != null);
                    actual.sleep = p[i].p1;
                }
                else if (p[i].p2.Contains("wakes"))
                {
                    Debug.Assert(actual != null);
                    actual.wake = p[i].p1;
                    actual = new guard() { id = actual.id };
                    l.Add(actual);
                }
            }

            var grp = l.GroupBy(x => x.id).OrderByDescending(x => x.Sum(y=> y.length)).First();

            Dictionary<int, int> mins = new Dictionary<int, int>();

            for (int i=0;i<60;i++)
                mins.Add(i, 0);

            foreach (var u in grp)
            {
                foreach (var min in u.Mins)
                    mins[min]++;
            }

            var m = mins.OrderByDescending(x => x.Value).First().Key;

            Console.WriteLine("Guard #{0} sleeps on {1} minute {2}", grp.Key, m, grp.Key * m);

            var grp3 = l.GroupBy(x => x.id).ToArray();

            Dictionary<int, Dictionary<int, int>> times = new Dictionary<int, Dictionary<int, int>>();

            foreach (var x in l)
            {
                Dictionary<int, int> v;
                if (!times.TryGetValue(x.id, out v))
                {
                    v = new Dictionary<int, int>();

                    for (int i = 0; i < 60; i++)
                        v.Add(i, 0);

                    times.Add(x.id, v);
                }

                foreach (var min in x.Mins)
                    v[min]++;                
            }

            var ord = times.OrderByDescending(x => x.Value.Values.Max()).ToArray();
        }

        static cmd Transform(string s)
        {
            string[] strs = s.Split('[', ']');
            cmd result = new cmd();
            result.p1 = DateTime.Parse(strs[1]);
            result.p2 = strs[2];
            return result;
        }
    }
}
