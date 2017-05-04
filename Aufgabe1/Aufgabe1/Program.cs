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

            Vector2[] vectors = reader.readFromFileToArray(@"S:\CG\Aufgabe1\Aufgabe1\s_1000_1.dat");
            DateTime start_1000 = DateTime.Now;
            for(int i = 0; i < vectors.Length - 1; i++)
            {
                for(int j = i + 1; j < vectors.Length; j++)
                {
                    if (vectors[i].CheckForIntersection(vectors[i], vectors[j]))
                    {
                        n_1000++;
                    }
                }
            }
            DateTime end_1000 = DateTime.Now;
            Console.WriteLine("Intersections at s_1000_1: " + n_1000 + " Time needed (ms): " + (end_1000 - start_1000).Milliseconds);

            vectors = reader.readFromFileToArray(@"S:\CG\Aufgabe1\Aufgabe1\s_10000_1.dat");
            DateTime start_10000 = DateTime.Now;
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
            DateTime end_10000 = DateTime.Now;
            Console.WriteLine("Intersections at s_10000_1: " + n_10000 + " Time needed (ms): " + (end_10000 - start_10000).Milliseconds);

            vectors = reader.readFromFileToArray(@"S:\CG\Aufgabe1\Aufgabe1\s_100000_1.dat");
            DateTime start_100000 = DateTime.Now;
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
            DateTime end_100000 = DateTime.Now;
            Console.WriteLine("Intersections at s_100000_1: " + n_100000 + " Time needed (ms): " + (end_100000 - start_100000).Milliseconds);
            Console.ReadLine();
        }
    }
}
