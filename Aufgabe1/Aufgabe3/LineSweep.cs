using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C5;

namespace Aufgabe3
{
    class LineSweep
    {
        TreeSet<Event> eventQueue = new TreeSet<Event>();
        SweepLine sweepLine = new SweepLine();
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
                Event currentEvent = eventQueue.FindMin();
                switch(currentEvent.eventType)
                {
                    case EventType.begin:
                        TreatLeftEndpoint(currentEvent);
                        break;
                    case EventType.end:
                        TreatRightEndpoint(currentEvent);
                        break;
                    case EventType.intersect:
                        TreatIntersection(currentEvent);
                        break;
                }
            }

            return outputList.Count;
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
        }

        private void TreatLeftEndpoint(Event beginEvent)
        {
            Vector2 segE = beginEvent.line_1;
            sweepLine.xPosition = beginEvent.getPoint().x;
            sweepLine.Add(segE);
            Vector2 segA = sweepLine.Above(segE);
            Vector2 segB = sweepLine.Below(segE);
            if (segA != null)
            {
                if (segE.CheckForIntersection(segA))
                {
                    Event iEvent = new Event(EventType.intersect, segA, segE);
                    eventQueue.Add(iEvent);
                }
            }

            if (segB != null)
            {
                if (segE.CheckForIntersection(segB))
                {
                    Event iEvent = new Event(EventType.intersect, segE, segB);
                    eventQueue.Add(iEvent);
                }
            }
            eventQueue.Remove(beginEvent);
        }

        private void TreatRightEndpoint(Event endEvent)
        {
            Vector2 segE = endEvent.line_1;
            sweepLine.xPosition = endEvent.getPoint().x;
            Vector2 segA = sweepLine.Above(segE);
            Vector2 segB = sweepLine.Below(segE);

            sweepLine.Remove(segE);
            if (segA != null && segB != null)
            {
                if (segA.CheckForIntersection(segB))
                {
                    Event iEvent = new Event(EventType.intersect, segA, segB);
                    if(!eventQueue.Contains(iEvent)) { eventQueue.Add(iEvent); }
                }
            }
            eventQueue.Remove(endEvent);
        }

        private void TreatIntersection(Event intersectEvent)
        {
            outputList.Add(intersectEvent);

            //segE1 should be above segE2
            Vector2 segE1 = intersectEvent.line_1;
            Vector2 segE2 = intersectEvent.line_2;

            sweepLine.xPosition = (sweepLine.xPosition + intersectEvent.getPoint().x) / 2;

            //make shure that segE1 is above segE2
            if (segE1.CompareTo(segE2, sweepLine.xPosition) < 0)
            {
                Vector2 temp = segE1;
                segE1 = segE2;
                segE2 = temp;
            }

            sweepLine.Switch(segE1, segE2);
          
            double tempPosition = intersectEvent.getPoint().x;
            eventQueue.Remove(intersectEvent);
            sweepLine.xPosition = (tempPosition + eventQueue.FindMin().getPoint().x) / 2;

            Vector2 segA = sweepLine.Above(segE1);
            Vector2 segB = sweepLine.Below(segE2);

            sweepLine.xPosition = tempPosition;


            if (segA != null)
            {
                if (segE2.CheckForIntersection(segA))
                {
                    Event iEvent = new Event(EventType.intersect, segA, segE2);
                    eventQueue.Add(iEvent);
                }
            }

            if (segB != null)
            {
                if (segE1.CheckForIntersection(segB))
                {
                    Event iEvent = new Event(EventType.intersect, segE1, segB);
                    eventQueue.Add(iEvent);
                }
            }
        }

    }
}
