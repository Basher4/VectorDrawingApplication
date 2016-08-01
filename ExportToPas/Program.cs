using System;   
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ExportToPas
{
    static class Utils
    {
        public static void StdErr(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(text); Console.ForegroundColor = ConsoleColor.Gray; return;
        }

        public static void StdErr(string format, params object[] arr)
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(format, arr); Console.ForegroundColor = ConsoleColor.Gray; return;
        }
    }

    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static Point[] GetPoints(XElement node)
        {
            int count = int.Parse(node.Attribute("count").Value);
            Point[] pArr = new Point[count];

            foreach(XElement n in node.Elements())
            {
                int id = int.Parse(n.Attribute("id").Value);
                int x = int.Parse(n.Attribute("x").Value);
                int y = int.Parse(n.Attribute("y").Value);

                pArr[id] = new Point(x, y);
            }

            return pArr;
        }

        static Color GetColor(XElement node)
        {
            int argb = Convert.ToInt32(node.Attribute("color").Value, 16);
            return Color.FromArgb(argb);
        }

        static System.Drawing.Drawing2D.DashStyle GetDashStyle(XElement node)
        {
            System.Drawing.Drawing2D.DashStyle ds;
            switch (node.Attribute("style").Value.ToLower())
            {
                case "solid":
                    ds = System.Drawing.Drawing2D.DashStyle.Solid;
                    break;
                case "dot":
                    ds = System.Drawing.Drawing2D.DashStyle.Dot;
                    break;
                case "dash":
                    ds = System.Drawing.Drawing2D.DashStyle.Dash;
                    break;
                case "dashdot":
                    ds = System.Drawing.Drawing2D.DashStyle.DashDot;
                    break;
                case "dashdotdot":
                    ds = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                    break;
                default:
                    ds = System.Drawing.Drawing2D.DashStyle.Solid;
                    break;
            }

            return ds;
        }

        static Pen GetPen(XElement node)
        {
            float PenWidth = float.Parse(node.Attribute("width").Value);
            Color c = GetColor(node);
            Pen p = new Pen(c, PenWidth);
            p.DashStyle = GetDashStyle(node);
            return p;
        }

        static SolidBrush GetBrush(XElement node)
        {
            Color c = GetColor(node);
            return new SolidBrush(c);
        }

        static void Main(string[] args)
        {
            #region Main Init
            Console.Title = "ExportToPas";
            if(!(args.Contains("-in") && args.Contains("-out")))
            {
                Utils.StdErr("Wrong parameters set");
                Console.WriteLine("Usage: ExportToPas.exe -in [input_file] -out [output_file] (opt: -layerColors");
                return;
            }

            string in_file = "", out_file = "";
            bool draw_layers = false;

            for(int i = 0; i < args.Length; i++)
            {
                switch(args[i].ToLower())
                {
                    case "-in":
                        if(in_file == "")
                            in_file = args[i+1];
                        else
                        {
                            Utils.StdErr("Multiple input files");
                            return;
                        }
                        break;

                    case "-out":
                        if(out_file == "")
                            out_file = args[i+1];
                        else
                        {
                            Utils.StdErr("Multiple output files");
                            return;
                        }
                        break;

                    case "-layercolors":
                        draw_layers = true;
                        break;
                }
            }
            #endregion

            StreamWriter sw;
            XDocument doc;
            try
            {
                sw = new StreamWriter(out_file);
            }
            catch(Exception e)
            {
                if (!File.Exists(out_file))
                    Utils.StdErr("File doesn't exist");
                else
                    Utils.StdErr("Problem with output file: " +e.Message);

                return;
            }

            try
            {
                doc = XDocument.Load(in_file);
            }
            catch(Exception e)
            {
                Utils.StdErr("Error while loading DAF document: ", e.Message);
                return;
            }

            Pascal pascal = new Pascal(sw);

            try
            {
                foreach (XElement node in doc.Descendants())
                {
                    switch (node.Name.LocalName.ToLower())
                    {
                        case "layer":
                            //First layer no matter what
                            if (node.Attribute("id").Value == "0")
                            {
                                //Get layer name
                                string name = node.Attribute("name").Value;
                                string s_color = node.Attribute("bgColor").Value;
                                s_color = s_color.Substring(2);

                                pascal.WriteLayer(name, s_color, true);
                                break;
                            }

                            if (!draw_layers)
                            {
                                if (!node.Attribute("bgColor").Value.StartsWith("00"))
                                    Console.WriteLine("Ignoring color in layer {0}", node.Attribute("name").Value);
                            }
                            else
                            {
                                //Get layer name
                                string name = node.Attribute("name").Value;
                                string s_color = node.Attribute("color").Value;
                                s_color = s_color.Substring(2);

                                pascal.WriteLayer(name, s_color, true);
                            }
                            break;

                        case "line":
                            string shapeName = node.Attribute("Name").Value;
                            Point[] verts = GetPoints(node.Element("points"));
                            Pen p = GetPen(node.Element("linePen"));
                            bool draw = bool.Parse(node.Element("visible").Value);

                            pascal.WriteLine(shapeName, verts, p, draw);
                            break;

                        case "rectangle":
                            shapeName = node.Attribute("Name").Value;
                            verts = GetPoints(node.Element("points"));
                            p = GetPen(node.Element("linePen"));
                            SolidBrush b = GetBrush(node.Element("fillBrush"));
                            draw = bool.Parse(node.Element("visible").Value);

                            pascal.WriteRect(shapeName, verts, p, b, draw);
                            break;

                        case "ellipse":
                            shapeName = node.Attribute("Name").Value;
                            verts = GetPoints(node.Element("points"));
                            p = GetPen(node.Element("linePen"));
                            b = GetBrush(node.Element("fillBrush"));
                            draw = bool.Parse(node.Element("visible").Value);

                            pascal.WriteEllipse(shapeName, verts, p, b, draw);
                            break;

                        case "freehand":
                            shapeName = node.Attribute("Name").Value;
                            verts = GetPoints(node.Element("points"));
                            p = GetPen(node.Element("linePen"));
                            draw = bool.Parse(node.Element("visible").Value);

                            pascal.WriteCurve(shapeName, verts, p, draw);
                            break;

                        case "polygon":
                            shapeName = node.Attribute("Name").Value;
                            verts = GetPoints(node.Element("points"));
                            p = GetPen(node.Element("linePen"));
                            b = GetBrush(node.Element("fillBrush"));
                            draw = bool.Parse(node.Element("visible").Value);

                            pascal.WritePolygon(shapeName, verts, p, b, draw);
                            break;

                        case "textshape":
                            shapeName = node.Attribute("Name").Value;
                            string text = node.Element("text").Value;
                            verts = GetPoints(node.Element("points"));
                            Color textColor = GetColor(node.Element("linePen"));
                            draw = bool.Parse(node.Element("visible").Value);

                            string fontFam = node.Element("font").Attribute("family").Value;
                            int fontSize = int.Parse(node.Element("font").Attribute("size").Value);
                            string fontStyle = node.Element("font").Attribute("style").Value;

                            Font font = new Font(fontFam, fontSize);

                            pascal.WriteText(shapeName, text, verts[0], font, fontStyle, textColor, draw);
                            break;
                    }
                }
            }
            catch
            {
                Utils.StdErr("Invalid file or outdated version of DAF file");
                return;
            }
        }
    }
}
