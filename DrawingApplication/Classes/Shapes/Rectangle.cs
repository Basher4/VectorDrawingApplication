using System;
using System.Drawing;

namespace DrawingApplication
{
    
    public sealed class Rectangle : Shape
    {
        #region Constructors
        /// <summary>
        /// Creates instance of Rectangle
        /// </summary>
        /// <param name="p1">One vertex</param>
        /// <param name="p2">Opposing vertex </param>
        /// <param name="LinePen">Pen used for drawing outline</param>
        /// <param name="FillBrush">Brush used for filling object</param>
        /// <param name="name">Optional name for this object</param>
        public Rectangle(Point p1, Point p2, Pen LinePen, Brush FillBrush, string name = "Rectangle ")
            : base(new Point[] { p1, p2 }, LinePen, FillBrush, name)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");
            if (CurrentShape.Name == "Rectangle ")
                CurrentShape.Name += ObjectID.ToString();
            else if (!r.IsMatch(CurrentShape.Name))
            {
                CurrentShape.Name = "Rectangle " + ObjectID.ToString();
            }
        }
        /// <summary>
        /// Creates instance of Rectangle
        /// </summary>
        /// <param name="shape">Object information structure</param>
        public Rectangle(ShapeInfo shape)
            : base(shape)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");
            if (CurrentShape.Name == "Rectangle ")
                CurrentShape.Name += ObjectID.ToString();
            else if (!r.IsMatch(CurrentShape.Name))
            {
                CurrentShape.Name = "Rectangle " + ObjectID.ToString();
            }
        }
        #endregion

        /// <summary>
        /// Used for drawing rectangle without need to instantiate this class. This method automatically calculate points if they're not in order (UpperLeft, LowerRight).
        /// </summary>
        /// <param name="target">Graphics to draw on</param>
        /// <param name="p">Pen used for drawing shapes outline</param>
        /// <param name="fill_brush">Brush used for filling shape</param>
        /// <param name="p1">One point of rectangle</param>
        /// <param name="p2">Opposite point of rectangle</param>
        public static void DrawRectangle(Graphics target, Pen p, Brush fill_brush, Point p1, Point p2)
        {
            Point UL = (p1.Y < p2.Y) ? ((p1.X < p2.X) ? p1 : new Point(p2.X, p1.Y)) : ((p1.X < p2.X) ? new Point(p1.X, p2.Y) : p2);
            Point LR = (p1.Y > p2.Y) ? ((p1.X > p2.X) ? p1 : new Point(p2.X, p1.Y)) : ((p1.X > p2.X) ? new Point(p1.X, p2.Y) : p2);
            Size size = new Size(Math.Abs(LR.X - UL.X), Math.Abs(UL.Y - LR.Y));

            var r = new System.Drawing.Rectangle(UL, size);
            target.FillRectangle(fill_brush, r);
            target.DrawRectangle(p, r);
        }

        /// <summary>
        /// Draw this instance of Rectangle.
        /// </summary>
        /// <param name="target">Graphics to draw on</param>
        public override void DrawShape(Graphics target)
        {
            if (CurrentShape.Visible)
            {
                /*
                //Upper Left
                Point UL = (CurrentShape.Verts[0].Y < CurrentShape.Verts[1].Y) ?
                                ((CurrentShape.Verts[0].X < CurrentShape.Verts[1].X) ?
                                    CurrentShape.Verts[0] : new Point(CurrentShape.Verts[1].X, CurrentShape.Verts[0].Y)
                                ) : ((CurrentShape.Verts[0].X < CurrentShape.Verts[1].X) ?
                                        new Point(CurrentShape.Verts[0].X, CurrentShape.Verts[1].Y) : CurrentShape.Verts[1]
                                    );
                
                //Lower right
                Point LR = (CurrentShape.Verts[0].Y > CurrentShape.Verts[1].Y) ?
                                ((CurrentShape.Verts[0].X > CurrentShape.Verts[1].X) ?
                                    CurrentShape.Verts[0] : new Point(CurrentShape.Verts[1].X, CurrentShape.Verts[0].Y)
                                ) : ((CurrentShape.Verts[0].X > CurrentShape.Verts[1].X) ?
                                        new Point(CurrentShape.Verts[0].X, CurrentShape.Verts[1].Y) : CurrentShape.Verts[1]
                                    );

                Size size = new Size(Math.Abs(LR.X - UL.X), Math.Abs(UL.Y - LR.Y));

                var r = new System.Drawing.Rectangle(UL, size);
                 */
                target.FillRectangle(CurrentShape.FillBrush, CurrentShape.Bounds);
                target.DrawRectangle(CurrentShape.LinePen, CurrentShape.Bounds);
            }
        }


		public override bool HitTest(Point MouseClick)
		{
            if (!this.IsVisible)
                return false;

			System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
			path.AddRectangle(CurrentShape.Bounds);

			if ((CurrentShape.FillBrush as SolidBrush).Color == Color.Transparent)
				return path.IsOutlineVisible(MouseClick, CurrentShape.LinePen);

			return path.IsVisible(MouseClick) || path.IsOutlineVisible(MouseClick, CurrentShape.LinePen);
		}

        public static Rectangle ImportFromXML(System.Xml.Linq.XElement node)
        {
            return new Rectangle(ShapeInfo.ImportFromXML(node));
        }
    }
}
