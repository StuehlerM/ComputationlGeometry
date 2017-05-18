using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe3
{
    enum EventType { begin, end, intersect };

    class Event
    {
        EventType eventType { get; set; }
        double x { get; set; }
        public Vector2 line1 { get; set; }
        public Vector2 line2 { get; set; }
    }
}
