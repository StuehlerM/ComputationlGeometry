using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe2
{
    class Program
    {
        static void Main(string[] args)
        {
            SvgReader svgReader = new SvgReader();
            List<State> states = svgReader.readSVG();

            List<City> cities = svgReader.getCities();

            foreach (var state in states)
            {
                foreach (var city in cities)
                {
                    if (state.pointInState(city.location))
                    {
                        Console.WriteLine(city.name + " is in " + state.name);
                    }
                }
            }

            Console.ReadKey();

            /*

            List<Vector2> edges = new List<Vector2>();
            Vector2 edge1 = new Vector2(7.0, 0.0, 8.0, 7.0);
            Vector2 edge2 = new Vector2(8.0, 7.0, 4.0, 9.0);
            Vector2 edge3 = new Vector2(4.0, 9.0, 1.0, 6.0);
            Vector2 edge4 = new Vector2(1.0, 6.0, 1.0, 2.0);
            Vector2 edge5 = new Vector2(1.0, 2.0, 7.0, 0.0);

            edges.Add(edge1);
            edges.Add(edge2);
            edges.Add(edge3);
            edges.Add(edge4);
            edges.Add(edge5);
            
            Polygon pol = new Polygon(edges);

            List<Vector2> edges2 = new List<Vector2>();
            Vector2 edge6 = new Vector2(7.0, 0.0, 8.0, 7.0);
            Vector2 edge7 = new Vector2(8.0, 7.0, 4.0, 9.0);
            Vector2 edge8 = new Vector2(4.0, 9.0, 1.0, 6.0);
            Vector2 edge9 = new Vector2(1.0, 6.0, 1.0, 2.0);
            Vector2 edge10 = new Vector2(1.0, 2.0, 7.0, 0.0);

            edges2.Add(edge6);
            edges2.Add(edge7);
            edges2.Add(edge8);
            edges2.Add(edge9);
            edges2.Add(edge10);

            Polygon pol2 = new Polygon(edges);
            List<Polygon> ter = new List<Polygon>();
            ter.Add(pol);
            ter.Add(pol2);

            State state = new State("Testland", ter);

            Console.WriteLine(state.ToString());
            Console.ReadLine();*/
        }
    }
}
