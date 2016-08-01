using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ExportToPas
{
    public class Pascal
    {
        private StreamWriter sw;
        private Pen active_pen;
        private SolidBrush active_brush;

        public Pascal(StreamWriter strWriter)
        {
            sw = strWriter;
            active_brush = null;
            active_pen = null;
        }

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns>0 = clear, 1 = color, -1 = active</returns>
        private int SetPen(Pen p)
        {
            if (active_pen != null)
                if (active_pen.Color == p.Color)
                    return -1;

            active_pen = p;

            string penStyle = "";

            if (p.Color.A == 0)
            {
                penStyle = "psClear";
                return 0;
            }
            else
            {
                switch (p.DashStyle)
                {
                    case DashStyle.Solid:
                        penStyle = "psSolid";
                        break;
                    case DashStyle.Dot:
                        penStyle = "psDot";
                        break;
                    case DashStyle.Dash:
                        penStyle = "psDash";
                        break;
                    case DashStyle.DashDot:
                        penStyle = "psDashDot";
                        break;
                    case DashStyle.DashDotDot:
                        penStyle = "psDashDotDot";
                        break;
                }

                ImgCanvas("Pen.Color:=RGBToColor({0}, {1}, {2})", p.Color.R, p.Color.G, p.Color.B);
                ImgCanvas("Pen.Style:={0}", penStyle);
                ImgCanvas("Pen.Width:={0}", p.Width);

                return 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns>0 = clear, 1 = color, -1 = active</returns>
        private int SetBrush(SolidBrush b)
        {
            if (active_brush != null)
                if (active_brush.Color == b.Color)
                    return -1;

            active_brush = b;

            if (b.Color.A == 0)
            {
                //Set clear brush
                ImgCanvas("Brush.Style:=bsClear");
                return 0;
            }

            ImgCanvas("Brush.Color:=RGBToColor({0}, {1}, {2})", b.Color.R, b.Color.G, b.Color.B);
            return 1;
        }

        private void ImgCanvas(string command, params object[] args)
        {
            sw.WriteLine("Image1.Canvas.{0};", String.Format(command, args));
            sw.Flush();
        }

        private void IgnoreShape(string shape, string name)
        {
            Console.WriteLine("\tIgnoring {0} '{1}'", shape, name);
        }

        private string PointArray(Point[] p)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            
            for (int i = 0; i < p.Length; i++)
            {
                sb.Append("Point({0}, {1})", p[i].X, p[i].Y);
                if (i != (p.Length - 1))
                    sb.Append(", ");
            }

            sb.Append("]");

            return sb.ToString();
        }

        #endregion

        #region Exposed

        public void Comment(string command, params object[] args)
        {
            sw.WriteLine('{' + String.Format(command, args) + '}');
            sw.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color">Color in HEX - RGB</param>
        /// <param name="draw"></param>
        public void WriteLayer(string name, string hexColor, bool draw)
        {
            if(draw)
            {
                Comment("Layer: {0}", name);
                ImgCanvas("Brush.Color:=${0}", hexColor);
                ImgCanvas("FillRect(Image1.ClientRect)");
            }
            else
                Console.WriteLine("Ignoring background in layer '{0}'", name);
        }

        public void WriteLine(string name, Point[] Verts, Pen p, bool draw)
        {
            if (draw)
            {
                Comment(name);
                if(SetPen(p) != 0)
                    ImgCanvas("Line({0}, {1}, {2}, {3})", Verts[0].X, Verts[0].Y, Verts[1].X, Verts[1].Y);
            }
            else
                IgnoreShape("line", name);
        }

        public void WriteRect(string name, Point[] Verts, Pen p, SolidBrush b, bool draw)
        {
            if (draw)
            {
                Comment(name);

                if(!(SetPen(p) == 0 && SetBrush(b) == 0))
                    ImgCanvas("Rectangle({0}, {1}, {2}, {3})", Verts[0].X, Verts[0].Y, Verts[1].X, Verts[1].Y);
            }
            else
                IgnoreShape("rectangle", name);
        }

        public void WriteEllipse(string name, Point[] Verts, Pen p, SolidBrush b, bool draw)
        {
            if (draw)
            {
                Comment(name);
                if (!(SetPen(p) == 0 && SetBrush(b) == 0))
                    ImgCanvas("Ellipse({0}, {1}, {2}, {3})", Verts[0].X, Verts[0].Y, Verts[1].X, Verts[1].Y);
            }
            else
                IgnoreShape("ellipse", name);
        }

        public void WriteCurve(string name, Point[] Verts, Pen p, bool draw)
        {
            if (draw)
            {
                Comment(name);
                if (SetPen(p) != 0)
                    ImgCanvas("Polyline({0})", PointArray(Verts));
            }
            else
                IgnoreShape("Freehand", name);
        }

        public void WritePolygon(string name, Point[] Verts, Pen p, SolidBrush b, bool draw)
        {
            if (draw)
            {
                Comment(name);
                if (!(SetPen(p) == 0 && SetBrush(b) == 0))
                    ImgCanvas("Polygon({0})", PointArray(Verts));
            }
            else
                IgnoreShape("Polygon", name);
        }

        public void WriteText(string name, string text, Point outpoint, Font font, Color textColor, bool draw)
        {
            if (draw)
            {
                Comment(name);
                ImgCanvas("Font.Height:={0}", font.Height);
                ImgCanvas("Font.Color:=RGBToColor({0}, {1}, {2})", textColor.R, textColor.G, textColor.B);
                ImgCanvas("Font.Name:={0}", font.Name);

                ImgCanvas("TextOut({0}, {1}, '{2}'", outpoint.X, outpoint.Y, text);
            }
            else
                IgnoreShape("Text", name);
        }

        public void WriteText(string name, string text, Point outpoint, Font font, string fontStyle, Color textColor, bool draw)
        {
            if (draw)
            {
                Comment(name);
                ImgCanvas("Brush.Style:=bsClear");
                ImgCanvas("Font.Height:={0}", font.Height);
                ImgCanvas("Font.Color:=RGBToColor({0}, {1}, {2})", textColor.R, textColor.G, textColor.B);
                ImgCanvas("Font.Name:='{0}'", font.Name);

                List<string> fs = new List<string>(3);
                if (fontStyle.ToLower().Contains("bold"))
                    fs.Add("fsBold");
                if (fontStyle.ToLower().Contains("italic"))
                    fs.Add("fsItalic");
                if (fontStyle.ToLower().Contains("underline"))
                    fs.Add("fsUnderline");

                string s = String.Join(", ", fs.ToArray(), 0, 3);

                ImgCanvas("Font.Style:=[{0}]", s);

                ImgCanvas("TextOut({0}, {1}, '{2}')", outpoint.X, outpoint.Y, text);
            }
            else
                IgnoreShape("Text", name);
        }

        #endregion
    }
}
