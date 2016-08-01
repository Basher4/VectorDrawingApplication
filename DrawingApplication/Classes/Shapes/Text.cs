using System;
using System.Drawing;
using System.Drawing.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;

namespace DrawingApplication
{
    
    class TextShape : Shape
    {
        public string Text { get; set; }
        public Font TextFont { get; set; }
        public Color TextColor
        {
            get
            {
                return this.CurrentShape.LinePen.Color;
            }

            set
            {
                if (value != null)
                    this.CurrentShape.LinePen.Color = value;
            }
        }

        public TextShape(ShapeInfo si, string text, Font font)
            :base(si)
        {
            this.CurrentShape.Name = "Text " + ObjectID.ToString();
            this.TextFont = font;

            Size s = TextRenderer.MeasureText(text, font);

            CurrentShape.Bounds = new System.Drawing.Rectangle(CurrentShape.Verts[0], s);            //Create rectangle
            this.Text = text;
        }

        public override void DrawShape(Graphics target)
        {
            TextRenderer.DrawText(target, this.Text, this.TextFont, CurrentShape.Verts[0], CurrentShape.LinePen.Color);
        }

        #region --STATIC DRAW METHODS
        public static void DrawText(Graphics target, string text, Point location)
        {
            TextRenderer.DrawText(target, text, SystemFonts.GetFontByName("Arial"), location, Color.Black);
        }

        public static void DrawText(Graphics target, string text, Point location, Font font)
        {
            TextRenderer.DrawText(target, text, font, location, Color.Black);
        }

        public static void DrawText(Graphics target, string text, Point location, Font font, Color color)
        {
            TextRenderer.DrawText(target, text, font, location, color);
        }
        #endregion

        public override void DrawBounds(Graphics target)
        {
            target.DrawRectangle(Preferences.BoundPen, this.CurrentShape.Bounds);
        }

        public override bool HitTest(Point MouseClick)
        {
            return CurrentShape.Bounds.Contains(MouseClick);
        }

        public override XElement ExportAsXML()
        {
            XElement e = base.ExportAsXML();
            e.Add(new XElement("text", Text),
                new XElement("font",
                    new XAttribute("family", this.TextFont.FontFamily.Name),
                    new XAttribute("size", this.TextFont.Size),
                    new XAttribute("style", this.TextFont.Style)
                    ));
            return e;
        }

        public static TextShape ImportFromXML(XElement node)
        {
            string text = node.Element("text").Value;
            XElement fontNode = node.Element("font");
            string FontFamily = fontNode.Attribute("family").Value;
            float FontSize = float.Parse(fontNode.Attribute("size").Value);
            string FStyle = fontNode.Attribute("style").Value.ToLower();

            FontStyle fs = FontStyle.Regular;
            if (FStyle.Contains("bold")) fs |= FontStyle.Bold;
            if (FStyle.Contains("italic")) fs |= FontStyle.Italic;
            if (FStyle.Contains("underline")) fs |= FontStyle.Underline;

            Font textFont = new Font(FontFamily, FontSize, fs);

            return new TextShape(ShapeInfo.ImportFromXML(node), text, textFont);
        }
    }
}
