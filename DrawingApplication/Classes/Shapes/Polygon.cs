using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawingApplication
{
    
	public sealed class Polygon : Shape
	{
		#region Constructors
		public Polygon(ShapeInfo Shape)
			: base(Shape)
		{
			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");
			if (CurrentShape.Name == "Rectangle ")
				CurrentShape.Name += ObjectID.ToString();
			else if (!r.IsMatch(CurrentShape.Name))
			{
				CurrentShape.Name = "Rectangle " + ObjectID.ToString();
			}

			if (CurrentShape.Verts[0] != CurrentShape.Verts[CurrentShape.Verts.Length - 1])
			{
				List<Point> p = CurrentShape.Verts.ToList();
				p.Add(CurrentShape.Verts[0]);
				CurrentShape.Verts = p.ToArray();
			}
		}

		public Polygon(Point[] Verts, Pen LinePen, Brush FillBrush, string name = "Polygon ")
			: base(Verts, LinePen, FillBrush, name)
		{
			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");
			if (CurrentShape.Name == "Polygon ")
				CurrentShape.Name += ObjectID.ToString();
			else if (!r.IsMatch(CurrentShape.Name))
			{
				CurrentShape.Name = "Polygon " + ObjectID.ToString();
			}

            /*
			if (CurrentShape.Verts[0] != CurrentShape.Verts[CurrentShape.Verts.Length - 1])
			{
				Shapes<Point> p = CurrentShape.Verts.ToList();
				p.Add(CurrentShape.Verts[0]);
				CurrentShape.Verts = p.ToArray();
			}
             */
		}
		#endregion

		public static void DrawPolygon(Graphics target, Point[] Verts, Pen p, Brush fill_brush)
		{
			target.FillPolygon(fill_brush, Verts);
			target.DrawPolygon(p, Verts);
		}

		public override void DrawShape(Graphics target)
		{
			if(CurrentShape.Visible)
			{
				target.FillPolygon(CurrentShape.FillBrush, CurrentShape.Verts);
				target.DrawPolygon(CurrentShape.LinePen, CurrentShape.Verts);
			}
		}

        public override void DrawBounds(Graphics target)
        {
            List<Point> tmp = new List<Point>(CurrentShape.Verts);
            tmp.Add(tmp[0]);
            target.DrawLines(Preferences.BoundPen, tmp.ToArray());
            base.DrawBounds(target);
        }

		public override bool HitTest(Point MouseClick)
		{
            if (!this.IsVisible)
                return false;

			System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
			path.AddPolygon(CurrentShape.Verts);

			return path.IsVisible(MouseClick) || path.IsOutlineVisible(MouseClick, CurrentShape.LinePen);
		}

        public static Polygon ImportFromXML(System.Xml.Linq.XElement node)
        {
            return new Polygon(ShapeInfo.ImportFromXML(node));
        }
	}
}
