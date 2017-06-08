using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Aufgabe2
{
    class SvgReader
    {
        public List<State> readSVG()
        {
            List<State> states = new List<State>();

            //Load xml
            XDocument xdoc = XDocument.Load(@"..\..\DeutschlandMitStaedten.svg");
            var paths = xdoc.Elements().Elements().Elements();

            foreach (var path in paths)
            {
                String dValue = path.Attribute("d").Value;
                String stateName = path.Attribute("id").Value;

                //State state = new State();
                //state.name = stateName;

                List<Vector2> edges;

                foreach (var line in dValue.Split(' '))
                {
                    Point currentPoint;
                    switch (line[0])
                    {
                        case 'M':
                        case 'm':
                            edges = new List<Vector2>();

                            var point = line.Split(',');
                            /*if ()


                                currentPoint = new Point(); */
                            break;

                    }

                    Console.WriteLine(line);
                }
            }

            Console.WriteLine("WTF");
            return null;

        }

        private Polygon createPolygon(string subpath)
        {
            List<string> lines = new List<string>(subpath.Split(' '));
            Point absStart;
            Point currentPoint;

            foreach (string line in lines)
            {
                if (line.StartsWith("M") || line.StartsWith("m"))
                {
                    //currentPoint = createPoint(line);
                    //absStart = currentPoint;
                }
                else if (line.StartsWith("l"))
                {
                    // TODO HERE!!!!
                }
            }

            return null;
        }

        private Vector2 createEdge(Point start, Point end)
        {
            return null;
        }

        private Tuple<Point, Point> createPoint(string line, Point currentPoint)
        {
            double currentPointx = 0.0;
            double currentPointy = 0.0;
            if (currentPoint != null)
            {
                currentPointx = currentPoint.x;
                currentPointy = currentPoint.y;
            }

            string[] coordinates = line.Substring(1).Split(',');
            double xCoordinate = currentPointx + Double.Parse(coordinates[0]);

            string yCoordinateString = coordinates[1];
            double yCoordinate;

            Point additionalPoint = null;

            if (!Double.TryParse(yCoordinateString, out yCoordinate))
            {
                if (yCoordinateString.Contains("H") || yCoordinateString.Contains("h"))
                {
                    return getAdditionalPoint(yCoordinateString, 'h', xCoordinate, currentPointy);
                }
                else if (yCoordinateString.Contains("V") || yCoordinateString.Contains("v"))
                {
                    return getAdditionalPoint(yCoordinateString, 'v', xCoordinate, currentPointy);
                }
            }
            Point nextCurrentPoint = new Point(xCoordinate, yCoordinate);
            return new Tuple<Point, Point>(nextCurrentPoint, additionalPoint);
        }

        private Tuple<Point, Point> getAdditionalPoint(string line, char separator, double xCoordinate, double yCoordinate)
        {
            string[] additionalPointString = line.Split(separator);
            yCoordinate = yCoordinate + Double.Parse(additionalPointString[0]);
            Point additionalPoint = null;

            switch (separator)
            {
                case 'H':
                case 'h':
                    double additionalX = xCoordinate + Double.Parse(additionalPointString[1]);
                    additionalPoint = new Point(additionalX, yCoordinate);
                    break;
                
                case 'V':
                case 'v':
                    double additionalY = yCoordinate + Double.Parse(additionalPointString[1]);
                    additionalPoint = new Point(xCoordinate, additionalY);
                    break;
            }
            Point nextCurrentPoint = new Point(xCoordinate, yCoordinate);
            return new Tuple<Point, Point>(nextCurrentPoint, additionalPoint);
        }
    }
}
