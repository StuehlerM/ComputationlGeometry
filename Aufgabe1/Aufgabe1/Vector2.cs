using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe1
{
    class Vector2
    {
        public Point start { get; set; }
        public Point end { get; set; }

        public Vector2(double sx, double sy, double ex, double ey)
        {
            start = new Point(sx, sy);
            end = new Point(ex, ey);
        }

        public bool CheckForIntersection(Vector2 v1, Vector2 v2)
        {
            bool intersect = false;

            double ccw_v1_start = ccw(v1.start, v1.end, v2.start);
            double ccw_v1_end = ccw(v1.start, v1.end, v2.end);

            double ccw_v2_start = ccw(v2.start, v2.end, v1.start);
            double ccw_v2_end = ccw(v2.start, v2.end, v1.end);

            if(ccw_v1_start == 0 && ccw_v1_end == 0)
            {
                if(CheckInside(v1, v2.start) || CheckInside(v1, v2.end))
                {
                    intersect = true;
                }
            }
            else if((ccw_v1_start * ccw_v1_end <= 0) && (ccw_v2_start * ccw_v2_end <= 0))
            {
                intersect = true;
            }
            
            return intersect;
        }

        double ccw(Point p, Point q, Point r)
        {
            return ((p.x*q.y - p.y*q.x) + (q.x * r.y - q.y * r.x) + (p.y * r.x - p.x * r.y));
        }

        bool CheckInside(Vector2 v, Point p)
        {
            bool inside = false;
            double EPSILON = Math.Pow(10, -15);

            double slope = ((v.end.y - v.start.y) / (v.end.x - v.start.x));
            double yAxis = v.start.y - slope * v.start.x;

            if( Math.Abs(p.y - (slope*p.x + yAxis)) < EPSILON)
            {
                inside = true;
            }

            return inside;
        }

        public override string ToString()
        {
            return "Startpunkt: " + start.ToString() +  " Ende: " + end.ToString();
        }
    }
}
