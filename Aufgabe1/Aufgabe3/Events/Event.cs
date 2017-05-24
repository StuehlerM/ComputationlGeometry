using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe3
{
    enum EventType { begin, end, intersect };

    class Event : IComparable<Event>
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

        public EventType eventType { get; }
        public Vector2 line_1 { get; }
        public Vector2 line_2 { get; }

        public Point getPoint()
        {
            switch (eventType)
            {
                case EventType.begin:
                    return line_1.start;
                case EventType.end:
                    return line_1.end;
                case EventType.intersect:
                    return calculateIntersection();
                default:
                    break;
            }
            return null;
        }

        private Point calculateIntersection()
        {
            double A1 = line_1.end.y - line_1.start.y;
            double B1 = line_1.start.x - line_1.end.x;
            double C1 = A1 * line_1.start.x + B1 * line_1.start.y;

            double A2 = line_2.end.y - line_2.start.y;
            double B2 = line_2.start.x - line_2.end.x;
            double C2 = A2 * line_2.start.x + B2 * line_2.start.y;

            double delta = A1 * B2 - A2 * B1;

            return new Point((B2 * C1 - B1 * C2) / delta, (A1 * C2 - A2 * C1) / delta);
        }

        /* Kleinerer x-Wert => weiter vorne in Reihenfolge
         * Wenn x-Werte gleich sind, nach y-Werten sortieren
         * Wenn y-Werte auch gleich sind (z.B. bei den 10/10ern), dann nach Endpunkt sortieren
         * voll unübersichtlich, funktionert aber hoffentlich
         */
        int IComparable<Event>.CompareTo(Event other)
        {
            int retVal = 0;
            double epsilon = 0.00000000000000001;
            if (Math.Abs(this.getPoint().x - other.getPoint().x) > epsilon)
            {
                if (this.getPoint().x < other.getPoint().x)
                {
                    retVal = -1;
                }
                else if (this.getPoint().x > other.getPoint().x)
                {
                    retVal = 1;
                }
            }
            else
            {
                if (Math.Abs(this.getPoint().y - other.getPoint().y) > epsilon)
                {
                    if (this.getPoint().y < other.getPoint().y)
                    {
                        retVal = -1;
                    }
                    else if (this.getPoint().y > other.getPoint().y)
                    {
                        retVal = 1;
                    }
                }
                //Wenn Startpunkte der begin-Events gleich sind, nach Endpunkten sortieren
   /*             else if (this.eventType == EventType.begin && other.eventType == EventType.begin)
                {
                    if (Math.Abs(this.line_1.end.x - other.line_1.end.x) > epsilon)
                    {
                        if (this.line_1.end.x < other.line_1.end.x)
                        {
                            retVal = -1;
                        }
                        else if (this.line_1.end.x > other.line_1.end.x)
                        {
                            retVal = 1;
                        }
                    }
                    else
                    {
                        if (Math.Abs(this.line_1.end.y - other.line_1.end.y) > epsilon)
                        {
                            if (this.line_1.end.y < other.line_1.end.y)
                            {
                                retVal = -1;
                            }
                            else if (this.line_1.end.y > other.line_1.end.y)
                            {
                                retVal = 1;
                            }
                        }
                    }
                }*/
            }
         return retVal;
        }
    }
}
