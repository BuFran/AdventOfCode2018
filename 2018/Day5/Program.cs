using System;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static string Poly(string str)
        {
            char[] arr = str.ToArray();
            bool next;
            do
            {
                next = false;
                arr = arr.Where(x => x != '\0').ToArray();

                for (int i = 0; i < (arr.Length - 1); i++)
                    if ((char.ToLower(arr[i]) == char.ToLower(arr[i + 1])) &&
                        (char.IsUpper(arr[i]) != char.IsUpper(arr[i + 1])))
                    {
                        arr[i] = '\0';
                        arr[++i] = '\0';
                        next = true;
                    }
            }
            while (next);

            return new string(arr.Where(x => x != '\0').ToArray());
        }

        static void Main(string[] args)
        {
            //var input = "dabAcCaCBAcCcaDA";
            var input = File.ReadAllText("input").TrimEnd('\r', '\n');            

            var i = Poly(input);

            Console.WriteLine("Count: {0}", i.Length);

            int minl = i.Select(x => char.ToLower(x)).Distinct().Select(c => Poly(i.Replace(c, '\0').Replace(char.ToUpper(c), '\0')).Length).Min();

            Console.WriteLine("Shortest {0}", minl);
        }
    }
}
