using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Aufgabe2
{
    class SvgReader
    {
        public void readSVG()
        {
            XDocument document = XDocument.Load(@"S:\CG\Aufgabe1\Aufgabe2\DeutschlandMitStaedten.svg");
            XElement svg_Element = document.Root;

            IEnumerable<XElement> test = from e1 in svg_Element.Elements("{http://www.w3.org/2000/svg}g")
                                         select e1;
            StringBuilder sb = new StringBuilder();
            foreach (XElement ee in test)
            {
                // Get "Central" and "Capital"
                sb.AppendLine(ee.Attribute("id").Value);

                IEnumerable<XElement> test2 = from e2 in ee.Elements("{http://www.w3.org/2000/svg}g")
                                              select e2;
                foreach (XElement ee2 in test2)
                {
                    sb.AppendLine("     Block No :" + ee2.Attribute("id").Value);
                    IEnumerable<XElement> test3 = from ee3 in ee2.Elements("{http://www.w3.org/2000/svg}path")
                                                  select ee3;
                    foreach (XElement epath in test3)
                    {
                        sb.AppendLine("     sPath  :" + epath.Attribute("d").Value);
                    }
                }
            }
            //txtAll.Text = sb.ToString();
        }
    }
}
