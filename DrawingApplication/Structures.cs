namespace DrawingApplication
{
    public struct NewDocumentInfo
    {
        public int Width, Height;
        public string Title;
        public NewDocumentInfo(int width, int height, string title)
        {
            Width = width; Height = height; Title = title;
        }
    }

    
    public class ShapeInfo
    {
        public System.Drawing.Pen LinePen;
        public System.Drawing.Brush FillBrush;
        public System.Drawing.Point[] Verts;
        public System.Drawing.Point Anchor;
        public System.Drawing.Rectangle Bounds;
        public bool Visible;
        public string Name;

        public System.Xml.Linq.XElement[] ExportToXML(bool ExportName = true)
        {
            System.Collections.Generic.List<System.Xml.Linq.XElement> e = new System.Collections.Generic.List<System.Xml.Linq.XElement>();
            if (LinePen != null)
                e.Add(
                    new System.Xml.Linq.XElement("linePen",
                        new System.Xml.Linq.XAttribute("color", LinePen.Color.ToArgb().ToString("X")),
                        new System.Xml.Linq.XAttribute("width", LinePen.Width),
                        new System.Xml.Linq.XAttribute("style", LinePen.DashStyle.ToString())
                    ));

            if(FillBrush != null)
                e.Add(
                    new System.Xml.Linq.XElement("fillBrush",
                        new System.Xml.Linq.XAttribute("color", (FillBrush as System.Drawing.SolidBrush).Color.ToArgb().ToString("X"))));

            if(Verts != null)
            {
                System.Xml.Linq.XElement points = new System.Xml.Linq.XElement("points", new System.Xml.Linq.XAttribute("count", Verts.Length));
                for(int i = 0; i < Verts.Length; i++)
                {
                    points.Add(
                        new System.Xml.Linq.XElement("point",
                            new System.Xml.Linq.XAttribute("id", i),
                            new System.Xml.Linq.XAttribute("x", Verts[i].X),
                            new System.Xml.Linq.XAttribute("y", Verts[i].Y)));
                }
                e.Add(points);
            }

            if(Bounds != null)
                e.Add(
                    new System.Xml.Linq.XElement("bounds",
                        new System.Xml.Linq.XAttribute("x", Bounds.X),
                        new System.Xml.Linq.XAttribute("y", Bounds.Y),
                        new System.Xml.Linq.XAttribute("width", Bounds.Width),
                        new System.Xml.Linq.XAttribute("height", Bounds.Height)));

            e.Add(new System.Xml.Linq.XElement("visible", Visible));

            if (Name != null && ExportName)
                e.Add(new System.Xml.Linq.XElement("name", Name));

            return e.ToArray();
        }

        public static ShapeInfo ImportFromXML(System.Xml.Linq.XElement node, bool ImportName = true)
        {
            ShapeInfo si = new ShapeInfo();

            try
            {
                //LinePen
                System.Xml.Linq.XElement linePenNode = node.Element("linePen");
                System.Drawing.Color color = System.Drawing.Color.FromArgb(System.Convert.ToInt32(linePenNode.Attribute("color").Value, 16));
                float width = System.Convert.ToSingle(linePenNode.Attribute("width").Value);
                si.LinePen = new System.Drawing.Pen(color, width);

                switch (linePenNode.Attribute("style").Value.ToLower())
                {
                    case "solid":
                        si.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        break;
                    case "dot":
                        si.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        break;
                    case "dash":
                        si.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        break;
                    case "dashdot":
                        si.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                        break;
                    case "dashdotdot":
                        si.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                        break;
                }

                //FillBrush
                System.Xml.Linq.XElement fillBrushNode = node.Element("fillBrush");
                if (fillBrushNode != null)
                {
                    color = System.Drawing.Color.FromArgb(System.Convert.ToInt32(fillBrushNode.Attribute("color").Value, 16));
                    si.FillBrush = new System.Drawing.SolidBrush(color);
                }

                //Vertecies
                int PointsCount = int.Parse(node.Element("points").Attribute("count").Value);
                System.Collections.Generic.List<System.Xml.Linq.XElement> pointsArr = new System.Collections.Generic.List<System.Xml.Linq.XElement>(node.Element("points").Elements());
                si.Verts = new System.Drawing.Point[PointsCount];
                for (int i = 0; i < PointsCount; i++)
                {
                    int id = int.Parse(pointsArr[i].Attribute("id").Value);

                    if (id == i)
                    {
                        int x = int.Parse(pointsArr[i].Attribute("x").Value);
                        int y = int.Parse(pointsArr[i].Attribute("y").Value);
                        si.Verts[i] = new System.Drawing.Point(x, y);
                    }
                }

                //Bounds
                System.Xml.Linq.XElement boundsNode = node.Element("bounds");
                int boundsX = int.Parse(boundsNode.Attribute("x").Value);
                int boundsY = int.Parse(boundsNode.Attribute("y").Value);
                int boundsWidth = int.Parse(boundsNode.Attribute("width").Value);
                int boundsHeight = int.Parse(boundsNode.Attribute("height").Value);
                si.Bounds = new System.Drawing.Rectangle(boundsX, boundsY, boundsWidth, boundsHeight);

                //Visible
                System.Xml.Linq.XElement visibleNode = node.Element("visible");
                if (visibleNode.Value.ToLower() == "true")
                    si.Visible = true;
                else
                    si.Visible = false;

                //Name
                if(ImportName)
                {
                    System.Xml.Linq.XElement nameNode = node.Element("name");
                    if(nameNode != null)
                        si.Name = nameNode.Value;
                }

                return si;
            }
            catch
            {
                throw new System.Exception("Invalid file format");
            }
        }
    }

    public enum ToolsAvailable
    {
        Line = 0, Rectangle = 1, Ellipse = 2, Polygon = 3, FreeHand = 4, Text = 5, Select = 6, Move = 7
    }

	public static class ToolsAvailableMethods
	{
		public static ToolsAvailable GetToolFromIndex(int index)
		{
			switch (index)
			{
				case 0:
					return ToolsAvailable.Line;
				case 1:
					return ToolsAvailable.Rectangle;
				case 2:
					return ToolsAvailable.Ellipse;
				case 3:
					return ToolsAvailable.Polygon;
				case 4:
					return ToolsAvailable.FreeHand;
                case 5:
                    return ToolsAvailable.Text;
				case 6:
					return ToolsAvailable.Select;
				case 7:
					return ToolsAvailable.Move;
				default:
					throw new System.IndexOutOfRangeException();
			}
		}
	}

    [System.Serializable]
    public struct FileInfoStruct
    {
        int Width, Heigth;
        string Title;
        System.Collections.Generic.List<Layer> Layers;

        public FileInfoStruct(int width, int heigth, string title, System.Collections.Generic.List<Layer> layers)
        {
            this.Width = width;
            this.Heigth = heigth;
            this.Title = title;
            this.Layers = layers;
        }
    }
}
