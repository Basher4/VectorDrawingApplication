using System;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace DrawingApplication
{
    /// <summary>
    /// Parrent class to all shapes.
    /// </summary>
    public abstract class Shape
    {
        private static ushort _shapeIDNum = 0;
        private bool assigned = false;
        private uint _objectID;
        public static ushort ShapeIDNum
        {
            get
            {
                return _shapeIDNum;
            }
            private set
            {
                _shapeIDNum = value;
            }
        }                  //Shape ID

        /// <summary>
        /// Structure holding informations about shape instance
        /// </summary>
        public ShapeInfo CurrentShape { get; set; }                      //All shape information
        /// <summary>
        /// Get object id.
        /// </summary>
        public uint ObjectID                              //Object ID Property
        {
            get
            {
                return _objectID;
            }

            private set
            {
                if (!assigned)
                {
                    _objectID = value;
                    assigned = true;
                }
                else
                {
                    throw new System.Exception("Cannot change object ID once its set");
                }
            }
        }

        /// <summary>
        /// Variable determining wether object is visible or not.
        /// </summary>
        public bool IsVisible { get { return CurrentShape.Visible; } set { CurrentShape.Visible = value; } }

        #region Contructors
        /// <summary>
        /// Default counstuctor for classes inherited from Shape.
        /// </summary>
        /// <param name="Verts">Array of <typeparamref name="System.Drawing.Point"/>Point</param>
        /// <param name="LinePen">Pen used for drawing outline</param>
        /// <param name="FillBrush">Brush used for filling objects</param>
        /// <param name="name">Name of object</param>
        protected Shape(Point[] Verts, Pen LinePen, Brush FillBrush, string name)
        {
            CurrentShape = new ShapeInfo
            {
                Verts = Verts,
                LinePen = (Pen)LinePen.Clone(),
                FillBrush = (FillBrush == null) ? null : (Brush)FillBrush.Clone(),
                Name = name,
                Visible = true,
            };
            ObjectID = _shapeIDNum++;
            Point anchor;
            Point min, max;
            min = max = Verts[0];
            foreach (Point p in Verts)
            {
                if (p.X < min.X) min.X = p.X;
                if (p.X > max.X) max.X = p.X;
                if (p.Y < min.Y) min.Y = p.Y;
                if (p.Y > max.Y) max.Y = p.Y;
            }
            anchor = new Point((min.X + max.X) / 2, (min.Y + max.Y) / 2);

			Size size = new Size(max.X - min.X, max.Y - min.Y);         //Get Rectangle size
			CurrentShape.Bounds = new System.Drawing.Rectangle(min, size);            //Create rectangle
        }
        /// <summary>
        /// Default counstuctor for classes inherited from Shape.
        /// </summary>
        /// <param name="Shape">Structure holding informations about Shape</param>
        protected Shape(ShapeInfo Shape)
        {
            CurrentShape = Shape;
            ObjectID = _shapeIDNum++;
        }
        #endregion

        #region CurrentShape struct controls

        #region Update Bounds
        public void UpdateBounds()
        {
            Point min, max;
            min = max = CurrentShape.Verts[0];
            foreach (Point p in CurrentShape.Verts)
            {
                if (p.X < min.X) min.X = p.X;
                if (p.X > max.X) max.X = p.X;
                if (p.Y < min.Y) min.Y = p.Y;
                if (p.Y > max.Y) max.Y = p.Y;
            }

            Size size = new Size(PointMath.Subtract(max, min));
            CurrentShape.Bounds = new System.Drawing.Rectangle(min, size);
        }
        #endregion

        #region Move anchor point
        /// <summary>
        /// Method to move anchor point to specific location
        /// </summary>
        /// <param name="location"></param>
        public void MoveAnchor(Point location)
        {
            CurrentShape.Anchor = location;
        }

        #endregion

        #region Visibility
        /// <summary>
        /// Toggle object visibility property
        /// </summary>
        public void ToggleVisibility()
        {
            CurrentShape.Visible = !CurrentShape.Visible;
        }

        /// <summary>
        /// Set shapes visibility
        /// </summary>
        /// <param name="visible">Boolean determining objects visibility.</param>
        public void SetVisibility(bool visible)
        {
            CurrentShape.Visible = visible;
        }
        #endregion

        #region Rename
        /// <summary>
        /// Change name of this instance of shape
        /// </summary>
        /// <param name="name">New name for shape</param>
        public void RenameShape(string name)
        {
            CurrentShape.Name = name;
        }
        #endregion

        #region Change Line Pen
        /// <summary>
        /// Change pen used for drawing outline of this shape
        /// </summary>
        public void ChangeLinePen(Pen pen)
        {
            CurrentShape.LinePen = pen;
        }
        #endregion

        #region Change Fill Brush
        /// <summary>
        /// Change brush used for filling this shape
        /// </summary>
        public void ChangeFillBrush(Brush brush)
        {
            CurrentShape.FillBrush = brush;
        }
        #endregion

        #region Vertecies Functions -- Update & GetClosest

        #region Update Verts Functions

        /// <summary>
        /// Update whole Array of vertecies within this shape
        /// </summary>
        /// <param name="verts">Array of points, each representing one vertex</param>
        public void UpdateVerts(Point[] verts)
        {
            CurrentShape.Verts = verts;
        }

        #region Move Vertex

        #region Relative
        /// <summary>
        /// Shift this shape's vertex by offset
        /// </summary>
        /// <param name="vertex_index">Vertex index in CurrentShape.Verts array</param>
        /// <param name="offset">Shift offset using Point</param>
        public void MoveVertexRelative(int vertex_index, Point offset)
        {
            CurrentShape.Verts[vertex_index] = PointMath.Add(CurrentShape.Verts[vertex_index], offset);
            UpdateBounds();
        }

        /// <summary>
        /// Shift this shape's vertex by offset
        /// </summary>
        /// <param name="vertex_index">Vertex index in CurrentShape.Verts array</param>
        /// <param name="offsetX">Shift offset in X axys</param>
        /// <param name="offsetY">Shift offset in Y axys</param>
        public void MoveVertexRelative(int vertex_index, int offsetX, int offsetY)
        {
            CurrentShape.Verts[vertex_index].X += offsetX;
            CurrentShape.Verts[vertex_index].Y += offsetY;
            UpdateBounds();
        }
        #endregion
        #region Absolute
        /// <summary>
        /// Set vertex's position
        /// </summary>
        /// <param name="vertex_index">Vertex index in CurrentShape.Verts array</param>
        /// <param name="location">Vertex's new position</param>
        public void MoveVertexAbsolute(int vertex_index, Point position)
        {
            CurrentShape.Verts[vertex_index] = position;
            UpdateBounds();
        }

        /// <summary>
        /// Set vertex's position
        /// </summary>
        /// <param name="vertex_index">Vertex index in CurrentShape.Verts array</param>
        /// <param name="posX">Vertex's new X position</param>
        /// <param name="posY">Vertex's new Y position</param>
        public void MoveVertexAbsolute(int vertex_index, int posX, int posY)
        {
            CurrentShape.Verts[vertex_index] = new Point(posX, posY);
            UpdateBounds();
        }
        #endregion

        #endregion

        #endregion
        #region Get Closeet Vert

        /// <summary>
        /// Get Closest Vertex to click Point
        /// </summary>
        /// <param name="clickPoint">Point representing coorinates of click position</param>
        /// <returns>Point representing location of closest vertex</returns>
        public Point GetClosestVertex(Point clickPoint)
        {
            return CurrentShape.Verts[GetClosestVertIndex(clickPoint)];
        }

        /// <summary>
        /// Get Closest Vertex to click Point
        /// </summary>
        /// <param name="clickPoint">Point representing coorinates of click position</param>
        /// <returns>Index of <b>Point</b> in <i>CurrentShape.Verts</i> structure representing location of closest vertex</returns>
        public int GetClosestVertIndex(Point clickPoint)
        {
            int length = CurrentShape.Verts.Length;
            double[] distances = new double[length];

            for (int c = 0; c < length; c++)
                distances[c] = Get2dDistance(clickPoint, CurrentShape.Verts[c]);

            return GetMinimumIndex(distances);
        }

        private int GetMinimumIndex(double[] array)
        {
            int index = 0;

            for (int i = 0; i < array.Length; i++)
                if (array[i] < array[index])
                    index = i;
            
            return index;
        }

        private double Get2dDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2));
        }

        #endregion

        #endregion

        #endregion

        #region Drawing Methods

        #region Draw Current Shape
        /// <summary>
        /// Method for drawing shape on graphics
        /// </summary>
        /// <param name="target">Graphics</param>
        public abstract void DrawShape(Graphics target);
        #endregion

        #region Draw Object Bounds
        /// <summary>
        /// Draw rectangle around current shape. Used when object is selected
        /// </summary>
        /// <param name="target">Graphics to draw on</param>
        public virtual void DrawBounds(Graphics target)
        {
            target.DrawRectangle(Preferences.BoundPen, CurrentShape.Bounds);
            
            foreach(Point p in CurrentShape.Verts)
            {
                target.FillEllipse(Brushes.White, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
                target.DrawEllipse(Pens.Black, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
            }
        }

        public virtual void DrawBounds(Graphics target, int selected_vert)
        {
            DrawBounds(target);
            Point p = CurrentShape.Verts[selected_vert];
            target.FillEllipse(Preferences.SelectedVertBrush, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
            target.DrawEllipse(Pens.Black, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
        }
        #endregion

        #region Draw Anchor Point Circle
        /// <summary>
        /// Draw circle representing anchor point on graphics
        /// </summary>
        /// <param name="target">Graphics to draw on</param>
        public virtual void DrawAnchorPoint(Graphics target)
        {
            target.DrawEllipse(CurrentShape.LinePen.Color == Color.Black ? Pens.White : Pens.Black, CurrentShape.Anchor.X - 5, CurrentShape.Anchor.Y - 5, 10, 10);
        }

        #endregion

        #endregion

        #region Transformations

        #region Move

        #region Relative
        /// <summary>
        /// Move object by offset
        /// </summary>
        /// <param name="Offset">Offset defined by System.Drawing.Point</param>
        public virtual void Move(Point Offset)
        {
            Move(Offset.X, Offset.Y);
        }
        /// <summary>
        /// Move object by offset
        /// </summary>
        /// <param name="offsetX">Offset X in pixels</param>
        /// <param name="offsetY">Offset Y in pixels</param>
        public virtual void Move(int offsetX, int offsetY)
        {
            for (int i = 0; i < CurrentShape.Verts.Length; i++)
            {
                CurrentShape.Verts[i].X += offsetX;
                CurrentShape.Verts[i].Y += offsetY;
            }

            CurrentShape.Anchor.X = offsetX;
            CurrentShape.Anchor.Y += offsetY;
            CurrentShape.Bounds.X += offsetX;
            CurrentShape.Bounds.Y += offsetY;
        }
        #endregion

        #region Absolute
        public virtual void MoveAbsolute(Point newAnchor)
        {
            //Get point diff
            Point offset = PointMath.Subtract(CurrentShape.Anchor, newAnchor);

            Move(offset);
        }

        public virtual void MoveAbsolute(int newAnchorX, int newAnchorY)
        {
            MoveAbsolute(new Point(newAnchorX, newAnchorY));
        }
        #endregion

        #endregion

        #region Scale
        /// <summary>
        /// Scale object
        /// </summary>
        /// <param name="ScaleX">Scale factor in X axis</param>
        /// <param name="ScaleY">Scale factor in Y axis</param>
        public virtual void Scale(float ScaleX, float ScaleY)
        {
            Point[] tmp = AbsoluteToRelative(CurrentShape.Verts, CurrentShape.Anchor);
            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i].X = (int)((float)tmp[i].X * ScaleX);
                tmp[i].Y = (int)((float)tmp[i].Y * ScaleY);
            }
            CurrentShape.Verts = RelativeToAbsolute(tmp, CurrentShape.Anchor);

            //Recalculate bounds
            UpdateBounds();
        }

        /// <summary>
        /// Turns array of absolutely positioned points into vectors
        /// </summary>
        /// <param name="Verts">Array of vertecies</param>
        /// <param name="Base">System Base Coordinates</param>
        /// <returns>Array of points representing vectors</returns>
        private Point[] AbsoluteToRelative(Point[] Verts, Point Base)
        {
            //initialization
            Point[] arr = new Point[Verts.Length];

            //Fill array
            for (int i = 0; i < Verts.Length; i++)
                arr[i] = new Point(Verts[i].X - Base.X, Verts[i].Y - Base.Y);

            return arr;
        }

        /// <summary>
        /// Turns array of vectors into points
        /// </summary>
        /// <param name="Verts">Array of vertecies</param>
        /// <param name="Base">System Base Coordinates</param>
        /// <returns>Array of points representing points on screen</returns>
        private Point[] RelativeToAbsolute(Point[] Verts, Point Base)
        {
            //initialization
            Point[] arr = new Point[Verts.Length];

            //Fill array
            for (int i = 0; i < Verts.Length; i++)
                arr[i] = new Point(Verts[i].X + Base.X, Verts[i].Y + Base.Y);

            return arr;
        }
        #endregion

        #endregion

        #region Hit Tests
        /// <summary>
        /// Test if point is in shape or on it's outline
        /// </summary>
        /// <param name="MouseClick">Point to test</param>
        /// <returns>Boolean determinating test result</returns>
		public abstract bool HitTest(Point MouseClick);

        /// <summary>
        /// Test if point is in shape or on it's outline
        /// </summary>
        /// <param name="MouseClick">Point to testDraw</param>
        /// <param name="vert_index">If clicked on vertex, store it's index in CurrentShape.Verts[] array</param>
        /// <returns>Boolean determinating test result</returns>
        public virtual bool HitTest(Point MouseClick, out int vert_index)
        {
            vert_index = -1;
            if (!this.IsVisible)
                return false;

            bool ret = this.HitTest(MouseClick);
            int condition = CurrentShape.Verts.Length;
            for(int i = 0; i < condition; i++)
            {
                System.Drawing.Rectangle r = new System.Drawing.Rectangle((Point)(PointMath.Subtract(CurrentShape.Verts[i], new Point(3, 3))), new Size(6, 6));
                if(r.Contains(MouseClick))
                {
                    vert_index = i;
                    return ret;
                }
            }

            return ret;
        }

        public virtual int HitVert(Point MouseClick, int size = 6)
        {
            for(int i = 0; i < CurrentShape.Verts.Length; i++)
            {
                System.Drawing.Rectangle r = new System.Drawing.Rectangle((Point)(PointMath.Subtract(CurrentShape.Verts[i], new Point(size / 2, size / 2))), new Size(size, size));
                if (r.Contains(MouseClick))
                {
                    return i;
                }
            }

            return -1;
        }
        #endregion

		public static float Get2DDistance(Point p1, Point p2)
		{
			return (float)Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2));
		}

        /// <summary>
        /// Methos to export this shape as xml tags
        /// </summary>
        /// <returns>XElement</returns>
		public virtual XElement ExportAsXML()
        {
            string class_name = this.GetType().Name;
            XElement e = new XElement(class_name, new XAttribute("Name", CurrentShape.Name), new XAttribute("ID", this.ObjectID),
                CurrentShape.ExportToXML());
            return e;
        }

        /// <summary>
        /// Get this shape's name
        /// </summary>
        /// <returns>This shape's name</returns>
        public override string ToString()
        {
            return CurrentShape.Name;
        }
    }
}

