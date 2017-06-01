using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe3
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader();

            int n_1000 = 0;
            int n_10000 = 0;
            int n_100000 = 0;

            List<Vector2> vectors = reader.readFromFileToList(@"..\..\s_1000_10.dat");
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            LineSweep ls = new LineSweep(vectors);
            n_1000 = ls.StartSweep();
            sw.Stop();
            Console.WriteLine("Intersections at s_1000_10: " + n_1000 + " Time needed (ms): " + sw.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }
}
