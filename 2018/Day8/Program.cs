using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    //https://adventofcode.com/2018/day/8
    class Program
    {
        private static bool test = true;

        private static readonly string testvector = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";

        static void Main(string[] args)
        {
            var db = new Queue<int>((test ? testvector : File.ReadAllText("input")).Split(' ').Select(x => int.Parse(x)));

            var tree = new Node(db);

            Console.WriteLine("Sum of metadata: {0}", tree.SumOfMeta);
            Console.WriteLine("Value of node: {0}", tree.ValueOfMeta());
        }
    }

    class Node
    {
        public Node[] Childs { get; private set; }
        public int[] Metadata { get; private set; }

        public Node(Queue<int> db)
        {
            Childs = new Node[db.Dequeue()];
            Metadata = new int[db.Dequeue()];

            for (int i = 0; i < Childs.Length; i++)
                Childs[i] = new Node(db);

            for (int i = 0; i < Metadata.Length; i++)
                Metadata[i] = db.Dequeue();
        }

        public int SumOfMeta { get { return Childs.Sum(x => x.SumOfMeta) + Metadata.Sum(); } }

        public int ValueOfMeta()
        {
            if (Childs.Length == 0)
                return Metadata.Sum();

            return Metadata.Where(x => (x > 0) && (x <= Childs.Length)).Sum(x => Childs[x - 1].ValueOfMeta());
        }
    }
}
