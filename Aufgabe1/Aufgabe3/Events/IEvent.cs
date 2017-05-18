using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe3
{
    enum EventType { begin, end, intersect };

    interface IEvent
    {
        EventType eventType { get; set; }
        double Point { get; set; }
    }
}
