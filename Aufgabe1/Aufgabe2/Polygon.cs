using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe2
{
    class Polygon
    {
        public List<Vector2> edges { get; set; }

        public Polygon(List<Vector2> edges)
        {
            this.edges = edges;
        }

        public double area()
        {
            double area = 0.0;

            for(int i = 0; i < edges.Count(); i++)
            {
                area += (edges[i].start.y + edges[(i + 1) % edges.Count()].start.y) * (edges[i].start.x - edges[(i + 1) % edges.Count()].start.x);
            }
            return Math.Abs(area / 2);
        }

        public Point getInsidePoint()
        {
            double xCenter = 0.0;
            double yCenter = 0.0;

            double numberPoints = edges.Count;

            foreach(Vector2 vec in edges)
            {
                xCenter += vec.start.x;
                yCenter += vec.start.y;
            }

            return new Point(xCenter/ numberPoints, yCenter/ numberPoints);
        }

        public override string ToString()
        {
            return "Polygon besteht aus " + edges.Count() + " Kanten und hat eine Fläche von " + this.area() + ".";
        }
    }
}
