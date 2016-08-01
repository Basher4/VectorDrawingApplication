using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using DigitalRune.Windows.Docking;

namespace DrawingApplication
{
    public class Document : DockableForm
    {
        internal ScreenControl screenControl1;

		internal int layerCounter = 1;

        //Out-of-ScreenControl input
		private bool clicked = false;
		private Point returnPoint;

		//On-focus change variables
		private List<Layer> layers;
        private TreeNodeCollection objectTreeView;

        public Document()
        {
            InitializeComponent();
			//Layer.Counter = 0;
			layers = new List<Layer>();
			layers.Add(new Layer(Color.White, "Background"));
			if (Program.MainFormInstance.dockPanel1.DocumentCount == 0)
			{
				Program.LayersFormInstance.layerManager1.Enabled = true;
                foreach (ToolStripButton b in Program.MainFormInstance.ToolButtons)
                    b.Enabled = true;
                
			}
        }

        private void InitializeComponent()
        {
            this.screenControl1 = new DrawingApplication.ScreenControl();
            this.SuspendLayout();
            // 
            // screenControl1
            // 
            this.screenControl1.GridEnabled = false;
            this.screenControl1.GridSpacing = 5;
            this.screenControl1.Location = new System.Drawing.Point(183, 128);
            this.screenControl1.Name = "screenControl1";
            this.screenControl1.Size = new System.Drawing.Size(150, 150);
            this.screenControl1.TabIndex = 0;
            // 
            // Document
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(566, 429);
            this.Controls.Add(this.screenControl1);
            this.DockAreas = ((DigitalRune.Windows.Docking.DockAreas)((((((DigitalRune.Windows.Docking.DockAreas.Float | DigitalRune.Windows.Docking.DockAreas.Left) 
            | DigitalRune.Windows.Docking.DockAreas.Right) 
            | DigitalRune.Windows.Docking.DockAreas.Top) 
            | DigitalRune.Windows.Docking.DockAreas.Bottom) 
            | DigitalRune.Windows.Docking.DockAreas.Document)));
            this.Name = "Document";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.allowNextWindow);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.onMouseDown);
            this.Resize += new System.EventHandler(this.onResize);
            this.ResumeLayout(false);

        }

		void Document_MouseLeave(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
		}

		void Document_MouseMove(object sender, MouseEventArgs e)
		{
			//throw new NotImplementedException();
		}

        private void onResize(object sender, System.EventArgs e)
        {
            screenControl1.Location = new System.Drawing.Point(this.Width / 2 - screenControl1.Width / 2, this.Height / 2 - screenControl1.Height / 2);
        }

        private void onMouseDown(object sender, MouseEventArgs e)
        {
            //screenControl1.OutOfBoundsClick(e.Location);
        }
		
        void Document_Load(object sender, EventArgs e)
        {
            if (screenControl1.Renderer == null)
                screenControl1.InitScreenControl();

            //TODO: Import layout
            Program.LayersFormInstance.layerManager1.SetLayerCollection(layers);
			Program.LayersFormInstance.layerManager1.SelectLayer(0);
            Program.ObjectTreeViewInstance.objectView.Nodes.Clear();
            Layer.Counter = layerCounter;
            Program.ObjectTreeViewInstance.UpdateNodes(layers.ToArray());
            Program.MainDocumentInstance = this;
        }

        private void allowNextWindow(object sender, FormClosedEventArgs e)
        {
            Program.WindowOpened = false;
            foreach (ToolStripButton b in Program.MainFormInstance.ToolButtons)
                b.Enabled = false;

            Program.ObjectTreeViewInstance.objectView.Nodes.Clear();
            Program.LayersFormInstance.layerManager1.SetLayerCollection(new List<Layer>());
            Program.LayersFormInstance.layerManager1.Enabled = false;
        }
    }
}
