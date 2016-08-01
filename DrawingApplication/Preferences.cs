using System.Collections.Generic;
using System.Drawing;

namespace DrawingApplication
{
    internal class Preferences
    {
        public static Color CopyColor = Color.Black;
        public static Shape CopyShape = null;
		public static Pen BoundPen = new Pen(Color.Violet, 1.5f);
        public static Brush SelectedVertBrush = new SolidBrush(Color.BlueViolet);
        public static System.Drawing.Drawing2D.DashStyle SelectPenStyle = System.Drawing.Drawing2D.DashStyle.Dot;
        public static SolidBrush PolygonFilling = new SolidBrush(Color.Beige);

        public static float FreeHandSmoothingDistance = 50;

        //DEFAULT
        public static Pen LinePen = new Pen(Color.Black, 3f);
		public static Brush FillBrush = new SolidBrush(Color.LightGray);
    }
}
