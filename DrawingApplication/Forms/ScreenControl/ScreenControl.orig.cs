using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawingApplication
{
    public partial class ScreenControl : UserControl
    {
        private ContextMenu conMenu;

		//Double buffered Grahics for rendering
        public BufferedGraphics Renderer;
        private BufferedGraphicsContext Context;

		public bool CommingFromDocument = false;
		public Point P1, P2;

        private ToolsAvailable ToolSelected;

		//GRID stuff
        private bool _gridEnabled = false;
		public int GridSpacing { get; set; }
		public bool GridEnabled {
			get
			{
				return _gridEnabled;
			}

			set
			{
				_gridEnabled = value;
				if(Renderer != null)
				{
					Renderer.Graphics.Clear(Color.White);
					RenderAllImages(Renderer);
				}
			}
		}
        
        public ScreenControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //Context menu
            MenuItem mi1 = new MenuItem("Move Up", new EventHandler(MoveUpEH));
            MenuItem mi2 = new MenuItem("Move Down", new EventHandler(MoveDownEH));
            MenuItem mi_s = new MenuItem("-");
            MenuItem mi3 = new MenuItem("Bring to Front", new EventHandler(BringToFrontEH));
            MenuItem mi4 = new MenuItem("Bring to Back", new EventHandler(BringToBackEH));
            conMenu = new System.Windows.Forms.ContextMenu(new MenuItem[] { mi1, mi2, mi_s, mi3, mi4 });

            Context = BufferedGraphicsManager.Current;
            PolyPoints = new List<Point>();

            if(Renderer == null)
                Renderer = Context.Allocate(pictureBox1.CreateGraphics(), pictureBox1.ClientRectangle);

			GridSpacing = 5;
        }

        public void InitScreenControl()
        {
			//Init graphics
            if(Renderer == null)
                Renderer = Context.Allocate(pictureBox1.CreateGraphics(), pictureBox1.ClientRectangle);
        }

		public void ForceScreenRender()
		{
			RenderAllImages(Renderer);
		}

        public void DeleteSelected()
        {

        }

        private bool isNear(Point compared, Point reference, int pixel)
        {
            if (
                ((compared.X >= reference.X - pixel) && (compared.X <= reference.X + pixel))
													&&
                ((compared.Y >= reference.Y - pixel) && (compared.Y <= reference.Y + pixel))
                )
                return true;
            else
                return false;
        }

        private void BufferAllImages(BufferedGraphics g, bool clear=true)
        {
			if (clear)
				g.Graphics.Clear(Color.White);

            //Draw grid
			if(GridEnabled)
			{
				for (int x = 0; x <= pictureBox1.Size.Width; x += GridSpacing) 
				{
					g.Graphics.DrawLine(Pens.Gray, x, 0, x, pictureBox1.Size.Width);
				}
				for (int y = 0; y <= pictureBox1.Size.Height; y += GridSpacing) 
				{
					g.Graphics.DrawLine(Pens.Gray, 0, y, pictureBox1.Size.Width, y);
				}
			}

            foreach (Layer l in Program.LayersFormInstance.layerManager1.GetLayersCollection())
            {
                l.Render(g.Graphics);
            }
        }

		private  void RenderAllImages(BufferedGraphics g, bool clear=true)
		{
			BufferAllImages(g, clear);
			g.Render();
		}

        #region Help functions
        private Point RoundToGrid(Point MouseLocation, bool moveMouse = true)
        {
            Point ret = new Point();
            ret.X = (int)(MouseLocation.X / GridSpacing) * GridSpacing;
            ret.Y = (int)(MouseLocation.Y / GridSpacing) * GridSpacing;

            if (moveMouse)
            {
                Cursor.Position = ret;
            }

            return ret;
        }
        #endregion
    }
}
