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
            int n_1000_10 = 0;

            // Verarbeitung der Datei s_1000_1.dat
            List<Vector2> vectors = reader.readFromFileToList(@"s_1000_1.dat");
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            LineSweep ls = new LineSweep(vectors);
            n_1000 = ls.StartSweep();
            sw.Stop();
            Console.WriteLine("Intersections at s_1000_1: " + n_1000 + " Time needed (ms): " + sw.ElapsedMilliseconds);

            // Verarbeitung der Datei s_10000_1.dat
            vectors = reader.readFromFileToList(@"s_10000_1.dat");
            sw.Restart();
            ls = new LineSweep(vectors);
            n_10000 = ls.StartSweep();
            sw.Stop();
            Console.WriteLine("Intersections at s_10000_1: " + n_10000 + " Time needed (ms): " + sw.ElapsedMilliseconds);

            // Verarbeitung der Datei s_100000_1.dat
            vectors = reader.readFromFileToList(@"s_100000_1.dat");
            sw.Restart();
            ls = new LineSweep(vectors);
            n_100000 = ls.StartSweep();
            sw.Stop();
            Console.WriteLine("Intersections at s_100000_1: " + n_100000 + " Time needed (ms): " + sw.ElapsedMilliseconds);

            // Verarbeitung der Datei s_1000_10.dat
            vectors = reader.readFromFileToList(@"s_1000_10.dat");
            sw.Restart();
            ls = new LineSweep(vectors);
            n_1000_10 = ls.StartSweep();
            sw.Stop();
            Console.WriteLine("Intersections at s_1000_10: " + n_1000_10 + " Time needed (ms): " + sw.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}
