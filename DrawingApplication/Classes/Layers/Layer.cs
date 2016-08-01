using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace DrawingApplication
{
    
	public class Layer
	{
		private static int counter = 0;
        public static int GetCounter { get { return counter; } }

		public static int Counter { get { return counter; } set {counter = value;} }

        public bool Visible { get; set; }
		public bool BG_Layer { get; set; }
		public Color BG_Color { get; set; }
		public string Text { get; set; }
		public int Id { get; private set; }
        public List<Shape> Shapes { get; set; }

		#region Constructors

		public Layer()
			: this(Color.Transparent, String.Empty)
		{ }

		public Layer(Color bg_color)
			: this(bg_color, String.Empty)
		{ }

		public Layer(Color bg_color, string text)
            : this(bg_color, text, null)
		{ }

        public Layer(Color bg_color, string text, List<Shape> shapes)
        {
            BG_Color = bg_color;
            Id = counter++;
            if (text == String.Empty)
                Text = "Layer " + Id.ToString();
            else
                Text = text;
            Shapes = (shapes == null) ? new List<Shape>() : shapes;
            Visible = true;
        }

		#endregion Constructors

		#region Shapes Collection control methods

		/// <summary>
		/// Add shape into shapes collection
		/// </summary>
		/// <param name="shape">Shape</param>
		public void AddShape(Shape shape)
		{
			Shapes.Add(shape);

            Program.ObjectTreeViewInstance.UpdateNodes(Program.LayersFormInstance.layerManager1.GetLayersCollection().ToArray());
		}

		/// <summary>
		/// Add shape into shapes collection
		/// </summary>
		/// <param name="shape">Shape</param>
		/// <param name="index">Insert at index</param>
		public void AddShape(Shape shape, int index)
		{
			Shapes.Insert(index, shape);

            Program.ObjectTreeViewInstance.UpdateNodes(Program.LayersFormInstance.layerManager1.GetLayersCollection().ToArray());
		}

		/// <summary>
		/// Remove shape from shapes collection
		/// </summary>
		/// <param name="shape">Shape instance to be removed</param>
		public void RemoveShape(Shape shape)
		{
			Shapes.Remove(shape);

            Program.ObjectTreeViewInstance.UpdateNodes(Program.LayersFormInstance.layerManager1.GetLayersCollection().ToArray());
		}

		/// <summary>
		/// Remove shape from shapes collection
		/// </summary>
		/// <param name="index">Instance's index in shapes collections</param>
		public void RemoveShape(int index)
		{
			Shapes.RemoveAt(index);

            Program.ObjectTreeViewInstance.UpdateNodes(Program.LayersFormInstance.layerManager1.GetLayersCollection().ToArray());
		}

		/// <summary>
		/// Get shape at index - same as Shapes[<i>index</i>]
		/// </summary>
		/// <param name="index">objects index</param>
		/// <returns>Shape at index in Shapes collection</returns>
		/// <exception cref="System.IndexOutOfRangeException">System.IndexOutOfRangeException - if occurs returns null</exception>
		public Shape GetShape(int index)
		{
			try
			{
				return Shapes[index];
			}
			catch (IndexOutOfRangeException)
			{
				return null;
			}
		}

        public Shape GetShapeByName(string name)
        {
            foreach(Shape s in Shapes)
            {
                if (s.ToString() == name)
                    return s;
            }
            return null;
        }

		public Shape SelectFrontByPosition(BufferedGraphics target, Point MousePoint)
		{
            Shape SelectedShape = null;
            for (int i = Shapes.Count - 1; i >= 0; i--)
            {
                if (Shapes[i].HitTest(MousePoint))
                {
                    Shapes[i].DrawBounds(target.Graphics);
                    SelectedShape = Shapes[i];
                    break;
                }
            }

            if (SelectedShape != null)
                target.Render();

            return SelectedShape;
		}

        public Shape SelectFrontByPosition(BufferedGraphics target, Point MousePoint, out int VertIndex)
        {
            Shape SelectedShape = null;
            VertIndex = -1;
            for (int i = Shapes.Count - 1; i >= 0; i--)
            {
                if (Shapes[i].HitTest(MousePoint, out VertIndex))
                {
                    if(VertIndex >= 0)
                        Shapes[i].DrawBounds(target.Graphics, VertIndex);
                    else
                        Shapes[i].DrawBounds(target.Graphics);

                    SelectedShape = Shapes[i];
                    break;
                }
            }

            if(SelectedShape != null)
                target.Render();

            return SelectedShape;
        }

        public Shape[] SelectShapesByPosition(Point MousePoint)
        {
            List<Shape> shapes = new List<Shape>(1);
            for(int i = Shapes.Count - 1; i >= 0; i--)
                if(Shapes[i].HitTest(MousePoint))
                    shapes.Add(Shapes[i]);

            return shapes.ToArray();
        }

		#endregion Shapes Collection control methods

        public Layer Clone()
        {
            string text = String.Format("{0} - Copy", this.Text);
            return new Layer(this.BG_Color, text, new List<Shape>(this.Shapes));
        }

		public override string ToString()
		{
			return this.Text;
		}

		public void Render(Graphics g)
		{
            if (this.Visible)
            {
                //Render Background
                g.FillRectangle(new SolidBrush(BG_Color), g.ClipBounds);

                if (Shapes.Count != 0)
                {
                    //Render shapes
                    Shape[] shapesToRender = Shapes.ToArray();
                    Array.Reverse(shapesToRender);
                    for (int i = shapesToRender.Length - 1; i >= 0; i--)
                        if (shapesToRender[i].IsVisible)
                            shapesToRender[i].DrawShape(g);
                }
            }
		}

        public XElement ExportAsXML()
        {
            XElement xel = new XElement("layer", 
                new XAttribute("name", this.Text),
                new XAttribute("bgColor", this.BG_Color.ToArgb().ToString("X")),
                new XAttribute("id", this.Id),
                new XAttribute("ShapesCount", this.Shapes.Count)
                );

            foreach(Shape s in this.Shapes)
            {
                xel.Add(s.ExportAsXML());
            }

            return xel;
        }

        public static Layer ImportFromXML(XElement node)
        {
            string name = node.Attribute("name").Value;
            int shapes_count = int.Parse(node.Attribute("ShapesCount").Value);
            Color bg_color = Color.FromArgb(Convert.ToInt32(node.Attribute("bgColor").Value, 16));
            List<Shape> shapes_collection = new List<Shape>(shapes_count);
            List<XElement> shape_nodes = new List<XElement>(node.Elements());

            for (int i = 0; i < shapes_count; i++)
            {
                switch(shape_nodes[i].Name.LocalName.ToLower())
                {
                    case "line":
                        shapes_collection.Add(Line.ImportFromXML(shape_nodes[i]));
                        break;
                    case "rectangle":
                        shapes_collection.Add(Rectangle.ImportFromXML(shape_nodes[i]));
                        break;
                    case "ellipse":
                        shapes_collection.Add(Ellipse.ImportFromXML(shape_nodes[i]));
                        break;
                    case "freehand":
                        shapes_collection.Add(FreeHand.ImportFromXML(shape_nodes[i]));
                        break;
                    case "polygon":
                        shapes_collection.Add(Polygon.ImportFromXML(shape_nodes[i]));
                        break;
                    case "textshape":
                        shapes_collection.Add(TextShape.ImportFromXML(shape_nodes[i]));
                        break;
                }
            }

            return new Layer(bg_color, name, shapes_collection);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Layer))
                return false;

            Layer l = obj as Layer;
            foreach(Shape s in l.Shapes)
            {
                if(!this.Shapes.Contains(s))
                    return false;
            }

            return true;
        }
    }
}