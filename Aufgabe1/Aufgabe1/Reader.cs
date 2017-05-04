using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe1
{
    class Reader
    {
        public List<Vector2> readFromFileToList(string path)
        {
            List<Vector2> vectors = new List<Vector2>();

            try
            {
                // Open the text file using a stream reader.
                Vector2 vector;
                using (StreamReader sr = new StreamReader(path))
                {
                    String line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        String[] values = line.Split(' ');
                        double sx = Double.Parse(values[0], CultureInfo.InvariantCulture);
                        double sy = Double.Parse(values[1], CultureInfo.InvariantCulture);
                        double ex = Double.Parse(values[2], CultureInfo.InvariantCulture);
                        double ey = Double.Parse(values[3], CultureInfo.InvariantCulture);
                        vector = new Vector2(sx, sy, ex, ey);

                        vectors.Add(vector);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return vectors;
        }

        public Vector2[] readFromFileToArray(string path)
        {
            Vector2[] vectors = new Vector2[File.ReadLines(path).Count()];
            int cnt = 0;

            try
            {
                // Open the text file using a stream reader.
                Vector2 vector;
                using (StreamReader sr = new StreamReader(path))
                {
                    String line;

                    // Read the stream to a string, and write the string to the console.
                    while ((line = sr.ReadLine()) != null)
                    {
                        String[] values = line.Split(' ');
                        double sx = Double.Parse(values[0], CultureInfo.InvariantCulture);
                        double sy = Double.Parse(values[1], CultureInfo.InvariantCulture);
                        double ex = Double.Parse(values[2], CultureInfo.InvariantCulture);
                        double ey = Double.Parse(values[3], CultureInfo.InvariantCulture);
                        vector = new Vector2(sx, sy, ex, ey);

                        vectors[cnt++] = vector;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return vectors;
        }
    }
}
