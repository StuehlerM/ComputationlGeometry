using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Aufgabe2
{
    class SvgReader
    {
        public void readSVG()
        {
            //Load xml
            XDocument xdoc = XDocument.Load(@"..\..\DeutschlandMitStaedten.svg");
            var paths = xdoc.Elements().Elements().Elements();

            foreach (var path in paths)
            {
                String dValue = path.Attribute("d").Value;

                List<Vector2> edges;

                foreach(var line in dValue.Split(' '))
                {
                    Point endPoint;
                    switch (line[0])
                    {
                        case 'M':
                        case 'm':
                            edges = new List<Vector2>();
                            var point = line.Split(',');
                            
                            //endPoint = new Point();
                            break;

                    }
                            
                    Console.WriteLine(line);
                }
            }

            Console.WriteLine("WTF");

        }

        private Point Interpret(string line)
        {
            string[] point = line.Split(',');
            


            return null;
        }
    }
}
