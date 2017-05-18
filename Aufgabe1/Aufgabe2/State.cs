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
            foreach(Polygon pol in territory)
            {
                area += pol.area();
            }
            return area;
        }

        public override string ToString()
        {
            return "Bundesland: " + name + "\r\nFlaeche: " + this.area();
        }
    }
}