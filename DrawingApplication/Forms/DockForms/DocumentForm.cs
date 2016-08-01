using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using DigitalRune.Windows.Docking;

namespace DrawingApplication
{
    public class Document : DockableForm
    {
        internal ScreenControl screenControl1;

        //Out-of-ScreenControl input
		private bool clicked = false;
		private Point returnPoint;

        public Document()
        {
            InitializeComponent();
            //Layer.@staticCounter = 0;
			List<Layer> layers = new List<Layer>();
			layers.Add(new Layer(Color.White, "Background"));

			Program.LayersFormInstance.layerManager1.Enabled = true;
            foreach (ToolStripButton b in Program.MainFormInstance.ToolButtons)
                b.Enabled = true;

            if (screenControl1.Renderer == null)
                screenControl1.InitScreenControl();

            //TODO: Import layout
            Program.LayersFormInstance.layerManager1.SetLayerCollection(layers);
            Program.LayersFormInstance.layerManager1.SelectLayer(0);
            Layer.Counter = 1;
            Program.ObjectTreeViewInstance.UpdateNodes(layers.ToArray());
            Program.MainDocumentInstance = this;
        }

        public Document(List<Layer> layers)
        {
            InitializeComponent();
            
            Program.LayersFormInstance.layerManager1.Enabled = true;
            foreach (ToolStripButton b in Program.MainFormInstance.ToolButtons)
                b.Enabled = true;

            if (screenControl1.Renderer == null)
                screenControl1.InitScreenControl();

            //TODO: Import layout
            Program.LayersFormInstance.layerManager1.SetLayerCollection(layers);
            Program.LayersFormInstance.layerManager1.SelectLayer(0);
            Layer.Counter = layers.Count;
            Program.ObjectTreeViewInstance.UpdateNodes(layers.ToArray());
            Program.MainDocumentInstance = this;
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
            this.screenControl1.Location = new System.Drawing.Point(0, 0);
            this.screenControl1.Margin = new System.Windows.Forms.Padding(0);
            this.screenControl1.Name = "screenControl1";
            this.screenControl1.Size = new System.Drawing.Size(2000, 2000);
            this.screenControl1.TabIndex = 0;
            // 
            // Document
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(639, 479);
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
			//screw this
        }
		
        void Document_Load(object sender, EventArgs e)
        {
        }

        public void ExportToXml(string path)
        {
            XDocument xdoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("document", new XAttribute("name", this.Text))
                );

            XElement screen = new XElement("screen", new XAttribute("width", screenControl1.Width), new XAttribute("height", screenControl1.Height));

            var layersCol = Program.LayersFormInstance.layerManager1.GetLayersCollection();
            XElement layers = new XElement("layers", new XAttribute("count", layersCol.Count));
            foreach(Layer l in layersCol)
            {
                layers.Add(l.ExportAsXML());
            }

            xdoc.Root.Add(screen);
            xdoc.Root.Add(layers);
            xdoc.Save(path);
        }

        public void ImportFromXML(XElement elem)
        {

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
