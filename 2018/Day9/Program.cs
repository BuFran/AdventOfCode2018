using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Day8
{
    //https://adventofcode.com/2018/day/9
    // input: 446 players 71522 points
    class Program
    {

        static void Main(string[] args)
        {
            Debug.Assert(32 == Play(9, 25));
            Debug.Assert(8317 == Play(10, 1618));
            Debug.Assert(146373 == Play(13, 7999));
            Debug.Assert(2764 == Play(17, 1104)); // this doesnt work ?!?!!!
            Debug.Assert(54718 == Play(21, 6111));
            Debug.Assert(37305 == Play(30, 5807));

            Console.WriteLine("Result: {0}", Play(446, 71522));
            Console.WriteLine("Result: {0}", Play(446, 7152200));
        }

        static Int64 Play(int players, int marbles)
        {
            Marble circle = new Marble(0);

            var d = new Dictionary<int, Int64>();
            for (int i = 0; i < players; i++)
                d[i] = 0;

            for (int marble = 1; marble <= marbles; marble++)
            {
                if (marble % 23 != 0)
                    circle = circle.Rotate(1).Append(marble);
                else
                {
                    int valu;
                    circle = circle.Rotate(-7).Remove(out valu);
                    d[marble % players] += marble + valu;
                }
            }

            return d.Values.Max();
        }

    }

    class Marble
    {
        public int Value { get; private set; }

        public Marble Next { get; private set; }
        public Marble Prev { get; private set; }

        public Marble(int value)
        {
            Value = value;
            Next = this;
            Prev = this;
        }

        public Marble Rotate(int index)
        {
            Marble m = this;
            bool fwd = index > 0;
            index = Math.Abs(index);

            while (index-- > 0)
                m = fwd ? m.Next : m.Prev;

            return m;
        }

        public Marble Append(int v)
        {
            Marble marble = new Marble(v);

            marble.Prev = this;
            marble.Next = this.Next;
            marble.Next.Prev = marble;
            this.Next = marble;
            
            return marble;
        }

        public Marble Remove(out int val)
        {
            Prev.Next = Next;
            Next.Prev = Prev;
            val = Value;
            return Next;
        }
    }
}
