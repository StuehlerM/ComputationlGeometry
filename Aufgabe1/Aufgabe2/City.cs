using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe2
{
    class City
    {
        public Point location { get; set; }
        public string name { get; set; }

        public City(Point location, string name)
        {
            this.location = location;
            this.name = name;
        }
    }
}
