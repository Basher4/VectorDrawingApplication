using System;
using System.Drawing;

namespace DrawingApplication
{
    
    public sealed class Ellipse : Shape
    {
        #region Constructors
        /// <summary>
        /// Creates instance of Ellipse.
        /// </summary>
        /// <param name="shape">Object information structure</param>
        public Ellipse(ShapeInfo shape)
            : base(shape)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");
            if (CurrentShape.Name == "Ellipse ")
                CurrentShape.Name += ObjectID.ToString();
            else if (!r.IsMatch(CurrentShape.Name))
            {
                CurrentShape.Name = "Ellpise " + ObjectID.ToString();
            }
        }
        /// <summary>
        /// Creates instance of Ellipse
        /// </summary>
        /// <param name="p1">One vertex</param>
        /// <param name="p2">Opposing vertex </param>
        /// <param name="LinePen">Pen used for drawing outline</param>
        /// <param name="FillBrush">Brush used for filling object</param>
        /// <param name="name">Optional name for this object</param>
        public Ellipse(Point start, Point end, Pen LinePen, Brush FillBrush, string name = "Ellipse ")
            : base(new Point[] { start, end }, LinePen, FillBrush, name)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");
            if (CurrentShape.Name == "Ellipse ")
                CurrentShape.Name += ObjectID.ToString();
            else if (!r.IsMatch(CurrentShape.Name))
            {
                CurrentShape.Name = "Ellpise " + ObjectID.ToString();
            }
        }
        #endregion

        /// <summary>
        /// Used for drawing ellipse without need to instantiate this class. This method automatically calculate points if they're not in order (UpperLeft, LowerRight).
        /// </summary>
        /// <param name="target">Graphics to draw on</param>
        /// <param name="p">Pen used for drawing shapes outline</param>
        /// <param name="fill_brush">Brush used for filling shape</param>
        /// <param name="p1">One point of rectangle</param>
        /// <param name="p2">Opposite point of rectangle</param>
        public static void DrawEllipse(Graphics target, Pen p, Brush fill_brush, Point p1, Point p2)
        {
            Point UL = (p1.Y < p2.Y) ? ((p1.X < p2.X) ? p1 : new Point(p2.X, p1.Y)) : ((p1.X < p2.X) ? new Point(p1.X, p2.Y) : p2);
            Point LR = (p1.Y > p2.Y) ? ((p1.X > p2.X) ? p1 : new Point(p2.X, p1.Y)) : ((p1.X > p2.X) ? new Point(p1.X, p2.Y) : p2);
            Size size = new Size(Math.Abs(LR.X - UL.X), Math.Abs(UL.Y - LR.Y));

            var r = new System.Drawing.Rectangle(UL, size);
            target.FillEllipse(fill_brush, r);
            target.DrawEllipse(p, r);
        }

        /// <summary>
        /// Draw this instance of Ellipse.
        /// </summary>
        /// <param name="target">Graphics to draw on</param>
        public override void DrawShape(Graphics target)
        {
            if (CurrentShape.Visible)
            {
                target.FillEllipse(CurrentShape.FillBrush, CurrentShape.Bounds);
				target.DrawEllipse(CurrentShape.LinePen, CurrentShape.Bounds);
            }
        }

		public override void DrawBounds(Graphics target)
		{
			target.DrawEllipse(Preferences.BoundPen, CurrentShape.Bounds);
			base.DrawBounds(target);
		}

		public override bool HitTest(Point MouseClick)
		{
            if (!this.IsVisible)
                return false;

			System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
			path.AddEllipse(CurrentShape.Bounds);

			if ((CurrentShape.FillBrush as SolidBrush).Color == Color.Transparent)
				return path.IsOutlineVisible(MouseClick, CurrentShape.LinePen);

			return path.IsVisible(MouseClick) || path.IsOutlineVisible(MouseClick, CurrentShape.LinePen);
		}

        public static Ellipse ImportFromXML(System.Xml.Linq.XElement node)
        {
            return new Ellipse(ShapeInfo.ImportFromXML(node));
        }
    }
}
