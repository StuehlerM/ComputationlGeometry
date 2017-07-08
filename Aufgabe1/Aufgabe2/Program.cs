using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Aufgabe2
{
    class Program
    {
        static void Main(string[] args)
        {
            const double REAL_SCALE_FACTOR = 1.1783559140664975386260859046027;

            SvgReader svgReader = new SvgReader();
            List<State> states = svgReader.readSVG();

            List<City> cities = svgReader.getCities();

            //plot(states);

            foreach (State state in states)
            {
                double area = state.area(states);
                Console.Write(state.name + " hat eine Fläche von ");
                Console.Write(area + " .svg Einheiten | bzw. ~");
                Console.WriteLine(area * REAL_SCALE_FACTOR + " km²");

                foreach (var city in cities)
                {
                    if (state.pointInState(city.location))
                    {
                        Console.WriteLine(city.name + " ist in " + state.name);
                        Console.WriteLine();
                    }
                }
            }

            Console.ReadKey();
        }

        #region Debug
        //static void plot(List<State> states)
        //{
        //    using (StreamWriter sw = new StreamWriter("result.csv"))
        //    {
        //        foreach (var state in states)
        //        {
        //            foreach (var poly in state.territory)
        //            {
        //                foreach (var edge in poly.edges)
        //                {
        //                    sw.Write(edge.start.x.ToString().Replace(',','.') + ", " + edge.start.y.ToString().Replace(',', '.') + "\n");
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion Debug
    }
}
