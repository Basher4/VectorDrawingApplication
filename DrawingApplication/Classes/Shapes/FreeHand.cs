using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawingApplication
{
    
    public sealed class FreeHand : Shape
    {
        private bool IsClosed;

        #region Constructors
        /// <summary>
        /// Creates instance of Polygon
        /// </summary>
        /// <param name="shape">Object information structure</param>
        public FreeHand(ShapeInfo Shape) 
            : base(Shape)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");
            if (CurrentShape.Name == "FreeHand ")
                CurrentShape.Name += ObjectID.ToString();
            else if (!r.IsMatch(CurrentShape.Name))
            {
                CurrentShape.Name = "FreeHand " + ObjectID.ToString();
            }

            if (CurrentShape.Verts[0] == CurrentShape.Verts[CurrentShape.Verts.Length - 1])
                IsClosed = true;
            else
                IsClosed = false;
        }

        /// <summary>
        /// Creates instance of Polygon
        /// </summary>
        /// <param name="Verts">Array of vertecies</param>
        /// <param name="LinePen">Pen used for drawing outline</param>
        /// <param name="FillBrush">Brush used for filling object</param>
        /// <param name="name">Optional name for this object</param>
        public FreeHand(Point[] Verts, Pen LinePen, Brush FillBrush, string name = "FreeHand ")
            : base(Verts, LinePen, FillBrush, name)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");
            if (CurrentShape.Name == "FreeHand ")
                CurrentShape.Name += ObjectID.ToString();
            else if (!r.IsMatch(CurrentShape.Name))
            {
                CurrentShape.Name = "FreeHand " + ObjectID.ToString();
            }

            if (CurrentShape.Verts[0] == CurrentShape.Verts[CurrentShape.Verts.Length - 1])
                IsClosed = true;
            else
                IsClosed = false;
        }
        #endregion

        public static void DrawFreeHand(Graphics target, Pen p, Point[] Verts)
        {
            if (Verts.Count() > 1)
                target.DrawCurve(p, Verts);
        }

        public override void DrawShape(Graphics target)
        {
            if (CurrentShape.Visible)
            {
                if (IsClosed)
                {
                    target.FillClosedCurve(CurrentShape.FillBrush, CurrentShape.Verts);
                    target.DrawClosedCurve(CurrentShape.LinePen, CurrentShape.Verts);
                }
                else
                    target.DrawCurve(CurrentShape.LinePen, CurrentShape.Verts);
            }
        }

		public override bool HitTest(Point MouseClick)
		{
            if (!this.IsVisible)
                return false;

			GraphicsPath path = new GraphicsPath();
            if (!IsClosed)
            {
                path.AddClosedCurve(CurrentShape.Verts);
                return path.IsOutlineVisible(MouseClick, CurrentShape.LinePen);
            }
            else
                path.AddClosedCurve(CurrentShape.Verts);

			return path.IsVisible(MouseClick) || path.IsOutlineVisible(MouseClick, CurrentShape.LinePen);
		}

        public static FreeHand ImportFromXML(System.Xml.Linq.XElement node)
        {
            return new FreeHand(ShapeInfo.ImportFromXML(node));
        }
    }
}
