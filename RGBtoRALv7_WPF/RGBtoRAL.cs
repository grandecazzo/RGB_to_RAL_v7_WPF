using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RGBtoRALv7_WPF
{
    class RGBtoRAL
    {
        public string Number;
        public string Type;
        public string Description;
        public string CMYKC;
        public string CMYKU;
        public string HTML;
        public int R;
        public int G;
        public int B;

        public System.Windows.Media.Color ToColor()
        {
            return System.Windows.Media.Color.FromRgb((byte)R,(byte)G,(byte)B);
        }
    }

    class RGBFinder
    {
        public static RGBtoRAL Finder(string name, int R, int G, int B)
        {
            List<RGBtoRAL> List = fromXML(name);

            RGBtoRAL bestElement = null;

            int fi;
            int f_min = 1000000;

            for (int i = 0; i < List.Count; i++)
            {
                fi = 30 * (List[i].R - R) * (List[i].R - R) + 59 * (List[i].G - G) * (List[i].G - G) + 11 * (List[i].B - B) * (List[i].B - B);
                if (fi < f_min)
                {
                    bestElement = List[i];
                    f_min = fi;
                }
            }

            if (bestElement.Type == null || bestElement.Type == "")
            {
                bestElement.Type = "-";
            }

            return bestElement;

        }

         public static List<RGBtoRAL> fromXML(string name)
        {
            List<RGBtoRAL> rallist = new List<RGBtoRAL>();
            XmlDocument xml = new XmlDocument();
            if (name == "Classic")
                xml.LoadXml(Properties.Resources.Classic);
            else
            {

                if (name == "Effect")
                    xml.LoadXml(Properties.Resources.Effect);
                else
                {

                    if (name == "Design")
                        xml.LoadXml(Properties.Resources.Design);
                    else
                        xml.LoadXml(Properties.Resources.Classic);
                }
            }

            bool isTypable = bool.Parse(xml.FirstChild.Attributes["HaveTypeDesc"].InnerText);

            XmlNodeList colors = xml.DocumentElement.ChildNodes;

            foreach (XmlNode color in colors)
            {
                RGBtoRAL colorclass = new RGBtoRAL();
                colorclass.Number = color.Attributes["Name"].InnerText;
                colorclass.CMYKC = color["CMYKC"].InnerText;
                colorclass.CMYKU = color["CMYKU"].InnerText;
                colorclass.HTML = color["HEX"].InnerText;
                colorclass.R = Int32.Parse(color["RGB"].Attributes["R"].InnerText);
                colorclass.G = Int32.Parse(color["RGB"].Attributes["G"].InnerText);
                colorclass.B = Int32.Parse(color["RGB"].Attributes["B"].InnerText);

                if(isTypable)
                {
                    if (Properties.Settings.Default.Language == "ru")
                    {
                        if (color["Type"] != null)
                        {
                            colorclass.Type = color["Type"].Attributes["ru"].InnerText;
                        }
                        colorclass.Description = color["Description"].Attributes["ru"].InnerText;
                    }
                    else
                    {
                        /*for test */

                        if (color["Type"] != null)
                        {
                            colorclass.Type = color["Type"].Attributes["en"].InnerText;
                        }
                        colorclass.Description = color["Description"].Attributes["en"].InnerText;
                    }
                }
                else
                {
                    colorclass.Type = "-";
                    colorclass.Description = "-";
                }

                rallist.Add(colorclass);
            }

            return rallist;
        }
    }
}
