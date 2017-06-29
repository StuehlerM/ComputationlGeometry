using System;
using System.Collections.Generic;
using System.Globalization;
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
                string stateName = path.Attribute("id").Value;

                State state = new State(stateName, createPolygon(dValue));
                states.Add(state);
            }

            return states;
        }

        private List<Polygon> createPolygon(string subpath)
        {
            List<string> lines = new List<string>(subpath.Split(' '));
            List<Vector2> edges = null;

            List<Polygon> polygons = new List<Polygon>();

            Point absStart = null;
            Point currentPoint = null;

            foreach (string line in lines)
            {
                if (line.StartsWith("M"))
                {
                    currentPoint = createSinglePoint(line);
                    absStart = currentPoint;
                    edges = new List<Vector2>();
                }

                else if (line.StartsWith("m"))
                {
                    // means m should be a "M". #ThanksSVG
                    if (polygons.Count == 0)
                    {
                        currentPoint = createSinglePoint(line);
                        absStart = currentPoint;
                        edges = new List<Vector2>();
                    }
                    else
                    {
                        currentPoint = createSinglePoint(line, currentPoint);
                        absStart = currentPoint;
                    }
                    edges = new List<Vector2>();
                }

                else if (line.StartsWith("l"))
                {
                    var points = createPoint(line, currentPoint);

                    if (points.Item2 == null)
                    {
                        edges.Add(new Vector2(currentPoint.x, currentPoint.y, points.Item1.x, points.Item1.y));
                        currentPoint = points.Item1;
                    }
                    else
                    {
                        edges.Add(new Vector2(currentPoint.x, currentPoint.y, points.Item1.x, points.Item1.y));
                        edges.Add(new Vector2(points.Item1.x, points.Item1.y, points.Item2.x, points.Item2.y));
                        currentPoint = points.Item2;
                    }
                }
                else if (line.StartsWith("L"))
                {
                    var points = createPoint(line, null);

                    if (points.Item2 == null)
                    {
                        edges.Add(new Vector2(currentPoint.x, currentPoint.y, points.Item1.x, points.Item1.y));
                        currentPoint = points.Item1;
                    }
                    else
                    {
                        edges.Add(new Vector2(currentPoint.x, currentPoint.y, points.Item1.x, points.Item1.y));
                        edges.Add(new Vector2(points.Item1.x, points.Item1.y, points.Item2.x, points.Item2.y));
                        currentPoint = points.Item2;
                    }
                }
                else if (line.StartsWith("z") || line.StartsWith("Z"))
                {
                    edges.Add(new Vector2(currentPoint.x, currentPoint.y, absStart.x, absStart.y));

                    Polygon polygon = new Polygon(edges);

                    polygons.Add(polygon);
                }
            }

            return polygons;
        }

        private Vector2 createEdge(Tuple<Point, Point> tuple)
        {
            return new Vector2(tuple.Item1.x, tuple.Item1.y, tuple.Item1.y, tuple.Item2.x);
        }

        private Point createSinglePoint(string line)
        {
            string[] coordinates = line.Substring(1).Split(',');
            double x = Double.Parse(coordinates[0], CultureInfo.InvariantCulture);
            double y = Double.Parse(coordinates[1], CultureInfo.InvariantCulture);
            return new Point(x, y);
        }
        private Point createSinglePoint(string line, Point currentPoint)
        {
            string[] coordinates = line.Substring(1).Split(',');
            double x = currentPoint.x + Double.Parse(coordinates[0], CultureInfo.InvariantCulture);
            double y = currentPoint.y + Double.Parse(coordinates[1], CultureInfo.InvariantCulture);
            return new Point(x, y);
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
            double xCoordinate = currentPointx + Double.Parse(coordinates[0], CultureInfo.InvariantCulture);

            string yCoordinateString = coordinates[1];
            double yCoordinate;

            Point additionalPoint = null;

            if (!Double.TryParse(yCoordinateString, out yCoordinate))
            {
                if (yCoordinateString.Contains("H"))
                {
                    return getAdditionalPoint(yCoordinateString, 'H', xCoordinate, currentPointy);
                }
                else if (yCoordinateString.Contains("h"))
                {
                    return getAdditionalPoint(yCoordinateString, 'h', xCoordinate, currentPointy);
                }
                else if (yCoordinateString.Contains("V"))
                {
                    return getAdditionalPoint(yCoordinateString, 'V', xCoordinate, currentPointy);
                }
                else if (yCoordinateString.Contains("v"))
                {
                    return getAdditionalPoint(yCoordinateString, 'v', xCoordinate, currentPointy);
                }


            }
            Point nextCurrentPoint = new Point(xCoordinate, yCoordinate);
            return new Tuple<Point, Point>(nextCurrentPoint, additionalPoint);
        }

        private Tuple<Point, Point> getAdditionalPoint(string line, char separator, double xCoordinate, double yCoordinate)
        {
            string[] additionalPointString;
            Point additionalPoint = null;

            double additionalX, additionalY;

            switch (separator)
            {
                case 'H':
                    additionalPointString = line.Split(separator);
                    additionalX = Double.Parse(additionalPointString[1], CultureInfo.InvariantCulture);
                    additionalPoint = new Point(additionalX, yCoordinate);
                    break;
                case 'h':
                    additionalPointString = line.Split(separator);
                    yCoordinate = yCoordinate + Double.Parse(additionalPointString[0], CultureInfo.InvariantCulture);
                    additionalX = xCoordinate + Double.Parse(additionalPointString[1], CultureInfo.InvariantCulture);
                    additionalPoint = new Point(additionalX, yCoordinate);
                    break;

                case 'V':
                    additionalPointString = line.Split(separator);
                    additionalY = Double.Parse(additionalPointString[1], CultureInfo.InvariantCulture);
                    additionalPoint = new Point(xCoordinate, additionalY);
                    break;
                case 'v':
                    additionalPointString = line.Split(separator);
                    yCoordinate = yCoordinate + Double.Parse(additionalPointString[0], CultureInfo.InvariantCulture);
                    additionalY = yCoordinate + Double.Parse(additionalPointString[1], CultureInfo.InvariantCulture);
                    additionalPoint = new Point(xCoordinate, additionalY);
                    break;
            }
            Point nextCurrentPoint = new Point(xCoordinate, yCoordinate);
            return new Tuple<Point, Point>(nextCurrentPoint, additionalPoint);
        }
    }
}
