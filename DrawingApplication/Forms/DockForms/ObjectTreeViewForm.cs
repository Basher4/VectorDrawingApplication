using System.Windows.Forms;
using DigitalRune.Windows.Docking;

namespace DrawingApplication
{
    public partial class ObjectTreeView : DockableForm
    {
        internal TreeView objectView;
        private TreeNode del_node;

        public ObjectTreeView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.objectView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // objectView
            // 
            this.objectView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectView.Location = new System.Drawing.Point(0, 0);
            this.objectView.Name = "objectView";
            this.objectView.Size = new System.Drawing.Size(284, 261);
            this.objectView.TabIndex = 0;
            this.objectView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.objectView_AfterSelect);
            this.objectView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.objectView_MouseUp);
            // 
            // ObjectTreeView
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.objectView);
            this.DockAreas = ((DigitalRune.Windows.Docking.DockAreas)((((((DigitalRune.Windows.Docking.DockAreas.Float | DigitalRune.Windows.Docking.DockAreas.Left) 
            | DigitalRune.Windows.Docking.DockAreas.Right) 
            | DigitalRune.Windows.Docking.DockAreas.Top) 
            | DigitalRune.Windows.Docking.DockAreas.Bottom) 
            | DigitalRune.Windows.Docking.DockAreas.Document)));
            this.Name = "ObjectTreeView";
            this.TabText = "Objects";
            this.Text = "Objects";
            this.ResumeLayout(false);

        }

        private void DeleteImage(object sender, System.EventArgs e)
        {
            string[] tmp = del_node.FullPath.Split('\\');
            Layer l = Program.LayersFormInstance.layerManager1.GetLayerByName(tmp[0]);
            Shape s = l.GetShapeByName(tmp[1]);
            l.RemoveShape(s);
            UpdateNodes(Program.LayersFormInstance.layerManager1.GetLayersCollection().ToArray());
            Program.MainDocumentInstance.screenControl1.ForceScreenRender();
        }

        public void UpdateNodes(Layer[] layers)
        {
            objectView.Nodes.Clear();
            for(int _i = 0; _i < layers.Length; _i++)
            {
                objectView.Nodes.Add(layers[_i].Id.ToString(), layers[_i].Text);
                if(!layers[_i].Visible)
                    objectView.Nodes[_i].BackColor = System.Drawing.Color.IndianRed;

                int _q = 0;
                foreach (Shape s in layers[_i].Shapes)
                {
                    objectView.Nodes[_i].Nodes.Add(s.ObjectID.ToString(), s.ToString());
                    if (!layers[_i].Shapes[_q].IsVisible)
                        objectView.Nodes[_i].Nodes[_q].BackColor = System.Drawing.Color.IndianRed;
                    _q++;
                }
                objectView.Nodes[_i].Expand();
            }
            foreach (TreeNode node in objectView.Nodes)
            {
                node.ContextMenu = new ContextMenu(new MenuItem[] { new MenuItem("Delete", DeleteImage, Shortcut.CtrlD) });
                foreach (TreeNode n in node.Nodes)
                    n.ContextMenu = new ContextMenu(new MenuItem[] { new MenuItem("Delete", DeleteImage, Shortcut.CtrlD) });
            }
        }

        private void objectView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                del_node = objectView.GetNodeAt(e.Location);
            }
        }

        private void objectView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LayerManager lm = Program.LayersFormInstance.layerManager1;
            ScreenControl sc = Program.MainDocumentInstance.screenControl1;


            string node_path = e.Node.FullPath;
            if (e.Node.Parent == null)
            {
                System.Diagnostics.Debug.WriteLine("Layer {0}", e.Node.Name);
                return;
            }

            Shape shape; Layer layer; string[] tmp;
            tmp = node_path.Split('\\');
            layer = lm.GetLayerByName(tmp[0]);
            shape = layer.GetShapeByName(tmp[1]);

            if (sc.GetSelectedShape() == shape && shape.IsVisible)
                return;

            PropertyManager.SelectedLayer = layer;
            System.Diagnostics.Debug.WriteLine(string.Format("Shape {0}, Layer {1}", shape, layer));
            Program.PropertyDockInstance.ObjectPropsForm.UpdateShapeInfo(shape);
            sc.ForceSelectShape(shape, layer);
        }
    }
}
