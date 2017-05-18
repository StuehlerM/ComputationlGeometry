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

        public Event(EventType type, Vector2 line)
        {
            this.eventType = type;
            this.line_1 = line;
            this.line_2 = null;
        }

        public Event(EventType type, Vector2 line_1, Vector2 line_2)
        {
            this.eventType = type;
            this.line_1 = line_1;
            this.line_2 = line_2;
        }

        EventType eventType { get; }
        Vector2 line_1 { get; }
        Vector2 line_2 { get; }

        public Point getPoint()
        {
            switch (eventType)
            {
                case EventType.begin:
                    return line_1.start;
                case EventType.end:
                    return line_1.end;
                case EventType.intersect:
                    /*double delta = line_1.start * line_2.end - line_1.end* line_2.start;
                    if (delta == 0)
                        throw new ArgumentException("Lines are parallel");

                    float x = (B2 * C1 - B1 * C2) / delta;
                    float y = (A1 * C2 - A2 * C1) / delta;

                    return line_1.start;*/
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}
