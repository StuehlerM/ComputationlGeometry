using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe3
{
    class LineSweep
    {
        List<Event> eventQueue = new List<Event>();
        List<Vector2> sweepLine = new List<Vector2>();
        List<Event> outputList = new List<Event>();
        List<Vector2> lines = new List<Vector2>();

        public LineSweep(List<Vector2> lines)
        {
            this.lines = lines;
        }

        public int StartSweep()
        {
            // Sortiert Start- und Endevents in Eventqueue
            InitializeEventQueue();

            while(eventQueue.Count > 0)
            {
                Event currentEvent = eventQueue.First();
                switch(currentEvent.eventType)
                {
                    case EventType.begin:

                    case EventType.end:

                    case EventType.intersect:
                }
            }

            return 0;
        }

        private void InitializeEventQueue()
        {
            foreach(Vector2 line in lines)
            {
                Event bEvent = new Event(EventType.begin, line);
                Event eEvent = new Event(EventType.end, line);
                eventQueue.Add(bEvent);
                eventQueue.Add(eEvent);
            }
            eventQueue.Sort((a, b) => a.getPoint().x.CompareTo(b.getPoint().x));
        }

        private void TreatLeftEndpoint()

    }
}
