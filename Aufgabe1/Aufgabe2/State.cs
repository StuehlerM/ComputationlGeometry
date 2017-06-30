using System.Collections.Generic;

namespace Aufgabe2
{
    class State
    {
        public string name { get; set; }
        public List<Polygon> territory { get; set; }

        public State(string name, List<Polygon> territory)
        {
            this.name = name;
            this.territory = territory;
        }

        public double area()
        {
            double area = 0.0;
            foreach (Polygon pol in territory)
            {
                area += pol.area();
            }
            return area;
        }

        // -1 out polygon, 0 on polygon, 1 in polygon
        public bool pointInState(Point point)
        {
            bool inside = false;

            foreach (var polygon in territory)
            {
                int polygonLength = polygon.edges.Count, i = 0;
               
                // x, y for tested point.
                double pointX = point.x, pointY = point.y;
                // start / end point for the current polygon segment.
                double startX, startY, endX, endY;
                Vector2 lastEdge = polygon.edges[polygon.edges.Count-1];
                endX = lastEdge.end.x;
                endY = lastEdge.end.y;
                while (i < polygonLength)
                {
                    startX = endX; startY = endY;
                    lastEdge = polygon.edges[i++];
                    endX = lastEdge.end.x; endY = lastEdge.end.y;
                    //
                    inside ^= (endY > pointY ^ startY > pointY) /* ? pointY inside [startY;endY] segment ? */
                              && /* if so, test if it is under the segment */
                              ((pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY));
                }
            }
            return inside;
        }

        public override string ToString()
        {
            return "Bundesland: " + name + "\r\nFlaeche: " + this.area();
        }
    }
}