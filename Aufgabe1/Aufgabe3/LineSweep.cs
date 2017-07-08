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
        TreeSet<Vector2> sweepLine = new TreeSet<Vector2>();
        TreeSet<Event> outputList = new TreeSet<Event>();
        List<Vector2> lines = new List<Vector2>();
        double xPosition = 0.0;
  
        int bEvent = 0;
        int eEvent = 0;
        int iEvent = 0;
        

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
                        bEvent++;
                        break;
                    case EventType.end:
                        TreatRightEndpoint(currentEvent);
                        eEvent++;
                        break;
                    case EventType.intersect:
                        TreatIntersection(currentEvent);
                        iEvent++;
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
            xPosition = beginEvent.getPoint().x;
            segE.xPosition = xPosition;
            sweepLine.Add(segE);
 
            try
            {
                Vector2 segA = sweepLine.Successor(segE);
                if (segE.CheckForIntersection(segA))
                {
                    Event iEvent = new Event(EventType.intersect, segA, segE);
                    eventQueue.Add(iEvent);
                }
            }
            catch (Exception)
            {
            }

            try
            {
                Vector2 segB = sweepLine.Predecessor(segE);
                if (segE.CheckForIntersection(segB))
                {
                    Event iEvent = new Event(EventType.intersect, segE, segB);
                    eventQueue.Add(iEvent);
                }
            }
            catch (Exception)
            {
            }

            eventQueue.Remove(beginEvent);
        }

        private void TreatRightEndpoint(Event endEvent)
        {
            Vector2 segE = endEvent.line_1;
            xPosition = endEvent.getPoint().x;
            segE.xPosition = xPosition;


            try
            {
                Vector2 segA = sweepLine.Successor(segE);
                Vector2 segB = sweepLine.Predecessor(segE);

                if (segA.CheckForIntersection(segB))
                {
                    Event iEvent = new Event(EventType.intersect, segA, segB);
                    if (!eventQueue.Contains(iEvent))
                    { eventQueue.Add(iEvent); }
                }
            }
            catch (Exception)
            {
            }

            sweepLine.Remove(segE);
            eventQueue.Remove(endEvent);
        }

        private void TreatIntersection(Event intersectEvent)
        {
            outputList.Add(intersectEvent);
            double tempPosition = intersectEvent.getPoint().x;

            //segE1 should be above segE2
            Vector2 segE1 = intersectEvent.line_1;
            Vector2 segE2 = intersectEvent.line_2;

            xPosition = (xPosition + tempPosition) / 2;
            segE1.xPosition = xPosition;
            segE2.xPosition = xPosition;

            sweepLine.Remove(segE1);
            sweepLine.Remove(segE2);
          
            eventQueue.Remove(intersectEvent);

            xPosition = (tempPosition + eventQueue.FindMin().getPoint().x) / 2;
            segE1.xPosition = xPosition;
            segE2.xPosition = xPosition;

            sweepLine.Add(segE1);
            sweepLine.Add(segE2);

            xPosition = tempPosition;
            try
            {
                Vector2 segA = sweepLine.Successor(segE2);
                if (segE2.CheckForIntersection(segA))
                {
                    Event iEvent = new Event(EventType.intersect, segA, segE2);
                    if (iEvent.getPoint().x > xPosition)
                    {
                        eventQueue.Add(iEvent);
                    }
                }
            }
            catch
            {
            }

            try
            {
                Vector2 segB = sweepLine.Predecessor(segE1);
                if (segE1.CheckForIntersection(segB))
                {
                    Event iEvent = new Event(EventType.intersect, segE1, segB);
                    if (iEvent.getPoint().x > xPosition)
                    {
                        eventQueue.Add(iEvent);
                    }
                }
            }
            catch
            {
            }

            
        }
    }
}
