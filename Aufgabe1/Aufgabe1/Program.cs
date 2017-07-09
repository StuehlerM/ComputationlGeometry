using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe1
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
            Vector2[] vectors = reader.readFromFileToArray(@"s_1000_1.dat");
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < vectors.Length - 1; i++)
            {
                for (int j = i + 1; j < vectors.Length; j++)
                {
                    if (vectors[i].CheckForIntersection(vectors[i], vectors[j]))
                    {
                        n_1000++;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("Intersections at s_1000_1: " + n_1000 + " Time needed (ms): " + sw.ElapsedMilliseconds);

            // Verarbeitung der Datei s_10000_1.dat
            vectors = reader.readFromFileToArray(@"s_10000_1.dat");
            sw.Restart();
            for (int i = 0; i < vectors.Length - 1; i++)
            {
                for (int j = i + 1; j < vectors.Length; j++)
                {
                    if (vectors[i].CheckForIntersection(vectors[i], vectors[j]))
                    {
                        n_10000++;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("Intersections at s_10000_1: " + n_10000 + " Time needed (ms): " + sw.ElapsedMilliseconds);

            // Verarbeitung der Datei s_100000_1.dat
            vectors = reader.readFromFileToArray(@"s_100000_1.dat");
            sw.Restart();
            for (int i = 0; i < vectors.Length - 1; i++)
            {
                for (int j = i + 1; j < vectors.Length; j++)
                {
                    if (vectors[i].CheckForIntersection(vectors[i], vectors[j]))
                    {
                        n_100000++;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("Intersections at s_100000_1: " + n_100000 + " Time needed (ms): " + sw.ElapsedMilliseconds);

            // Verarbeitung der Datei s_1000_10.dat
            vectors = reader.readFromFileToArray(@"s_1000_10.dat");
            sw.Restart();
            for (int i = 0; i < vectors.Length - 1; i++)
            {
                for (int j = i + 1; j < vectors.Length; j++)
                {
                    if (vectors[i].CheckForIntersection(vectors[i], vectors[j]))
                    {
                        n_1000_10++;                       
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("Intersections at s_1000_10: " + n_1000_10 + " Time needed (ms): " + sw.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }
}
