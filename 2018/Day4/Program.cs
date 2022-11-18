using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        class Row
        {
            public DateTime d;
            public int id;

            public Row(string s)
            {
                var ss = s.Split('[',']', '#');
                d = DateTime.Parse(ss[1]);

                if (s.Contains("falls"))
                    id = -1;
                else if (s.Contains("wakes"))
                    id = -2;
                else
                    id = int.Parse(ss[3].Split(' ')[0]);
            }
        }

        static void Main(string[] args)
        {
            var data = File.ReadAllLines("input").Select(x => new Row(x)).OrderBy(x => x.d).ToArray();

            var table = new Dictionary<int,Dictionary<int,int>>();

            int current = 0;
            int start = 0;
            foreach (var row in data)
                switch (row.id)
                {
                    default:
                        current = row.id;
                        break;

                    case -1:
                        start = row.d.Minute;
                        break;

                    case -2:
                        Dictionary<int,int> mins;

                        if (!table.TryGetValue(current, out mins))
                        {
                            mins = new Dictionary<int, int>();
                            for (int i = 0 ; i < 60 ; i++)
                                mins[i] = 0;

                            table.Add(current, mins);
                        }

                        for (int min = start ; min < row.d.Minute ; min++)
                            mins[min]++;
                        break;
                }

            var sleepy = table.OrderByDescending(x=>x.Value.Sum(y=> y.Value)).First();
            var minute = sleepy.Value.OrderByDescending(x=>x.Value).First().Key;

            Debug.WriteLine("Key {0} on minute {1} hash {2}", sleepy.Key, minute, sleepy.Key * minute);


            var psa = table.OrderByDescending(x => x.Value.Max(y => y.Value)).First();
            var minutea = psa.Value.OrderByDescending(x=>x.Value).First().Key;

            Debug.WriteLine("Key {0} on minute {1} hash {2}", psa.Key, minutea, psa.Key * minutea);


        }
    }
}
