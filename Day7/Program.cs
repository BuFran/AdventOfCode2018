using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    //https://adventofcode.com/2018/day/7
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllLines("input").Select(parse).ToArray();

            // Part One:
            Debug.WriteLine($"Result1: {TraverseGraph(data)}");

            // Part two:
            Debug.WriteLine($"Result1: {TraverseGraphMultiElf(data, 1, 0)}");
            Debug.WriteLine($"Result3: {TraverseGraphMultiElf(data, 5, 60)}");
        }

        static Tuple<char, char> parse(string x)
        {
            var p = x.Split(' ');
            return new Tuple<char, char>(p[1][0], p[7][0]);
        }

        static string TraverseGraph(Tuple<char, char>[] graph)
        {
            var i = new Dictionary<char, int>();

            foreach (var d in graph)
            {
                i[d.Item1] = 0;
                i[d.Item2] = 0;
            }

            foreach (var d in graph)
                i[d.Item2]++;

            StringBuilder sb = new StringBuilder(i.Count);

            while (i.Count > 0)
            {
                var ic = i.Where(x=>x.Value == 0).OrderBy(x=>x.Key).First().Key;
                sb.Append(ic);
                i.Remove(ic);

                foreach (var p in graph.Where(x => x.Item1 == ic))
                    i[p.Item2]--;
            }

            return sb.ToString();
        }

        class Elf
        {
            public char Work;
            public int Time;

            public Elf(char w, int basetime=0)
            {
                Work = w;
                Time = w - 'A' + basetime;
            }

            public static string Working(Elf e)
            {
                return (e == null) ? "." : $"{e.Work}{e.Time}";
            }
        }

        static string TraverseGraphMultiElf(Tuple<char, char>[] graph, int workers, int basetime)
        {
            var unfinished = new HashSet<char>(graph.Select(x=>x.Item1).Concat(graph.Select(x=>x.Item2)));
            var finished = new HashSet<char>();
            var elves = new List<Elf>(workers);

            int time = 0;
            StringBuilder sb = new StringBuilder(unfinished.Count);
            while ((unfinished.Count > 0) || (elves.Count > 0))
            {
                elves.ForEach(x => x.Time--);

                if (elves.Count < workers)
                {
                    var available = unfinished.Where(x=> !graph.Any(y=> (y.Item2 == x) && !finished.Contains(y.Item1))).OrderBy(x=>x).Take(workers - elves.Count).ToArray();

                    unfinished.RemoveWhere(x => available.Contains(x));
                    elves.AddRange(available.Select(x => new Elf(x, basetime)));
                }

                var ended = elves.Where(x => x.Time == 0).Select(x => x.Work).ToArray();
                sb.Append(ended);

                foreach (var e in ended)
                    finished.Add(e);

                elves.RemoveAll(x => x.Time == 0);

                time++;
            }

            return $"{sb}:{time}";
        }


    }
}
