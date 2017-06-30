using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe3
{
    public class Vector2 : IComparable<Vector2>
    {
        public Point start { get; set; }
        public Point end { get; set; }
        public double xPosition = 0;

        public bool IsNull { get; set; }

        double epsilon = 0.0000000000001;

        //public static int insiders = 0;

        public Vector2(double sx, double sy, double ex, double ey)
        {
            if (sx < ex)
            {
                start = new Point(sx, sy);
                end = new Point(ex, ey);
            }
            else
            {
                end = new Point(sx, sy);
                start = new Point(ex, ey);
            }

            IsNull = false;
        }

        public Vector2()
        {
            IsNull = true;
        }

        public bool CheckForIntersection(Vector2 v2)
        {
            bool intersect = false;

            double ccw_v1_start = ccw(this.start, this.end, v2.start);
            double ccw_v1_end = ccw(this.start, this.end, v2.end);

            double ccw_v2_start = ccw(v2.start, v2.end, this.start);
            double ccw_v2_end = ccw(v2.start, v2.end, this.end);

            if ((ccw_v1_start == 0) && (ccw_v1_end == 0))
            {
                if (CheckInside(v2.start) || CheckInside(v2.end))
                {
                    intersect = true;
                }
            }
            else if ((ccw_v1_start * ccw_v1_end <= 0) && (ccw_v2_start * ccw_v2_end <= 0))
            {
                intersect = true;
            }

            return intersect;
        }

        double ccw(Point p, Point q, Point r)
        {
            return ((p.x * q.y - p.y * q.x) + (q.x * r.y - q.y * r.x) + (p.y * r.x - p.x * r.y));
        }

        bool CheckInside(Point p)
        {
            bool inside = false;
            double EPSILON = Math.Pow(10, -15);

            double slope = ((this.end.y - this.start.y) / (this.end.x - this.start.x));
            double yAxis = this.start.y - slope * this.start.x;

            if (Math.Abs(p.y - (slope * p.x + yAxis)) < EPSILON)
            {
                inside = true;
            }

            return inside;
        }

        public double CalculateValue(double x)
        {
            double slope = ((this.end.y - this.start.y) / (this.end.x - this.start.x));
            double yAxis = this.start.y - slope * this.start.x;

            return slope * x + yAxis;
        }

        public override string ToString()
        {
            return "Startpunkt: " + start.ToString() + " Ende: " + end.ToString();
        }

        // Kleinerer y-Wert => weiter vorne in Reihenfolge
        public int CompareTo(Vector2 other)
        {
            double usedPosition = this.xPosition > other.xPosition ? this.xPosition : other.xPosition;
            int retVal = 0;
            if (this.Equals(other))
            {
                retVal = 0;
            }
            else if (this.CalculateValue(usedPosition) < other.CalculateValue(usedPosition))
            {
                retVal = -1;
            }
            else if (this.CalculateValue(usedPosition) > other.CalculateValue(usedPosition))
            {
                retVal = 1;
            }
            return retVal;
        }

        public int CompareTo(Vector2 other, double xPosition)
        {
            int retVal = 0;          
            if (this.Equals(other))
            {
                retVal = 0;
            }
            else
            {
                double thisCalc = this.CalculateValue(xPosition);
                double otherCalc = other.CalculateValue(xPosition);

                //y-values are not identical 
                //if (Math.Abs(thisCalc - otherCalc) > epsilon)
                {
                    if (thisCalc < otherCalc)
                    {
                        retVal = -1;
                    }
                    else
                    {
                        retVal = 1;
                    }
                }
                //y-values are identical
               /* else if (Math.Abs(this.end.y - other.end.y) > epsilon)
                {
                    if (this.end.y < other.end.y)
                    {
                        retVal = -1;
                    } else
                    {
                        retVal = 1;
                    }
                }
                else
                {
                    if (this.end.x > other.end.x)
                    {
                        retVal = -1;
                    } else
                    {
                        retVal = 1;
                    }
                }*/
            }
            return retVal;
        }
    }
}
