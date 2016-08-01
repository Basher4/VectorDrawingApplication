using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawingApplication
{
    
    public sealed class Line : Shape
    {
        #region Constructors
        /// <summary>
        /// Creates instance of Line
        /// </summary>
        /// <param name="start">Start point</param>
        /// <param name="end">End point</param>
        /// <param name="LinePen">Pen used for drawing this line</param>
        /// <param name="name">Optional name for this object</param>
        public Line(Point start, Point end, Pen LinePen, string name = "Line ")
            : base(new Point[] { start, end }, LinePen, null, name)
        {
            if (CurrentShape.Name == "Line ")
                CurrentShape.Name += ObjectID.ToString();
        }

        /// <summary>
        /// Creates instance of line
        /// </summary>
        /// <param name="Shape">Object information structure</param>
        public Line(ShapeInfo Shape) : base(Shape)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");
            if (CurrentShape.Name == "Line ")
                CurrentShape.Name += ObjectID.ToString();
            else if (!r.IsMatch(CurrentShape.Name))
            {
                CurrentShape.Name = "Line " + ObjectID.ToString();
            }
        }
        #endregion

        /// <summary>
        /// Used for drawing line without need to instantiate this class
        /// </summary>
        /// <param name="target">Graphics to draw on</param>
        /// <param name="p">Pen used for drawing this line</param>
        /// <param name="p1">Start point</param>
        /// <param name="p2">End point</param>
        public static void DrawLine(Graphics target, Pen p, Point p1, Point p2)
        {
            target.DrawLine(p, p1, p2);
        }

        public static void DrawLines(Graphics target, Pen p, Point[] Points)
        {
            target.DrawLines(p, Points);
        }

        /// <summary>
        /// Draw this instance of Line.
        /// </summary>
        /// <param name="target">Graphics to draw on</param>
        public override void DrawShape(Graphics target)
        {
            if(CurrentShape.Visible)
                target.DrawLine(CurrentShape.LinePen, CurrentShape.Verts[0], CurrentShape.Verts[1]);
        }

		public override void DrawBounds(Graphics target)
		{
			target.DrawLine(Preferences.BoundPen, CurrentShape.Verts[0], CurrentShape.Verts[1]);
			base.DrawBounds(target);
		}

		public override bool HitTest(Point MouseClick)
		{
            if (!this.IsVisible)
                return false;

			GraphicsPath path = new GraphicsPath();
			path.AddLine(CurrentShape.Verts[0], CurrentShape.Verts[1]);

			return path.IsOutlineVisible(MouseClick, CurrentShape.LinePen);
		}

        public static Line ImportFromXML(System.Xml.Linq.XElement node)
        {
            return new Line(ShapeInfo.ImportFromXML(node));
        }
    }
}
