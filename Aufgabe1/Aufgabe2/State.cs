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


        public double area(List<State> allStates)
        {
            double area = 0.0;
            double subtractArea = 0.0;
            foreach(State state in allStates)
            {
                if (state != this)
                {
                    foreach (Polygon pol in state.territory)
                    {
                        if (pointInState(pol.getInsidePoint()))
                        {
                            subtractArea += state.area();
                        }
                    }
                }
            }

            foreach (Polygon pol in territory)
            {
                area += pol.area();
            }

            if (subtractArea > area)
            {
                subtractArea = 0;
            }
      
            return area - subtractArea;
        }

        // -1 out polygon, 0 on polygon, 1 in polygon
        public bool pointInState(Point point)
        {
            bool inside = false;

            foreach (var polygon in territory)
            {
                int polygonLength = polygon.edges.Count;
                int i = 0;

                double startX, startY, endX, endY;

                Vector2 lastEdge = polygon.edges[polygon.edges.Count-1];

                endX = lastEdge.end.x;
                endY = lastEdge.end.y;

                while (i < polygonLength)
                {
                    startX = endX;
                    startY = endY;

                    lastEdge = polygon.edges[i++];

                    endX = lastEdge.end.x;
                    endY = lastEdge.end.y;

                    //
                    inside ^= (endY > point.y ^ startY > point.y) // pointY inside [startY;endY] segment ? */
                              && /* if so, test if it is under the segment */
                              ((point.x - endX) < (point.y - endY) * (startX - endX) / (startY - endY));
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