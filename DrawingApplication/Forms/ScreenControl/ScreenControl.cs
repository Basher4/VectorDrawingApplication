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
		public int GridSpacing { get; set; }
        public bool GridEnabled { get; set; }
        
        public ScreenControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //Context menu
            MenuItem mi1 = new MenuItem("Move Up", new EventHandler(MoveUpEH));
            MenuItem mi2 = new MenuItem("Move Down", new EventHandler(MoveDownEH));
            MenuItem mi3 = new MenuItem("Bring to Front", new EventHandler(BringToFrontEH));
            MenuItem mi4 = new MenuItem("Send to Back", new EventHandler(BringToBackEH));
            MenuItem mi_delete = new MenuItem("Delete", new EventHandler(DeleteShape), Shortcut.Del);
            MenuItem mi_paste = new MenuItem("Paste", new EventHandler(PasteShape), Shortcut.CtrlV);
            MenuItem mi_copy = new MenuItem("Copy", new EventHandler(CopyShape), Shortcut.CtrlC);
            MenuItem mi_cut;
            MenuItem[] pos = new MenuItem[] { mi1, mi2, new MenuItem("-"), mi3, mi4 };
            MenuItem item1 = new MenuItem("Layout", pos);
            MenuItem[] shape = new MenuItem[] { mi_copy, mi_paste, mi_delete };
            MenuItem item2 = new MenuItem("Shapes", shape);
            conMenu = new System.Windows.Forms.ContextMenu(new MenuItem[] { item1, item2 });

            Context = BufferedGraphicsManager.Current;
            PolyPoints = new List<Point>();
            PolyPointsTemp = new List<Point>();

			GridSpacing = 5;
        }

        public void InitScreenControl()
        {
			//Initialize Event Handlers
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.onMD);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.onMouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onMU);

			//Init graphics
            Renderer = Context.Allocate(pictureBox1.CreateGraphics(), this.ClientRectangle);
        }

        public void Render()
        {
            Renderer.Render();
        }

		public void ForceScreenRender()
		{
			RenderAllImages(Renderer);
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

        private void BufferAllImages(bool clear=true)
        {
            BufferAllImages(Renderer, clear);
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

        private void ResetSelectVariables()
        {
            selected = null; selected_vert = -1;
        }
        #endregion
    }
}
