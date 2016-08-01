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
    public partial class LayerManager : UserControl
    {
        private List<Layer> LayersCollection { get; set; }
        public Layer SelectedLayer;
        public LayerManager()
        {
            InitializeComponent();
			LayersCollection = new List<Layer>();
        }

        public LayerManager(List<Layer> layerCollection)
        {
            InitializeComponent();
            LayersCollection = layerCollection;
        }

		#region Layers Collection GET-SET
		public List<Layer> GetLayersCollection()
		{
			return this.LayersCollection;
        }

        public Layer GetLayerByShape(Shape shape)
        {
            foreach(Layer l in LayersCollection)
            {
                if (l.Shapes.Contains(shape))
                    return l;
            }

            return null;
        }

        public Layer GetLayerByName(string name)
        {
            foreach(Layer l in LayersCollection)
            {
                if (l.Text == name)
                    return l;
            }
            return null;
        }

		public void SetLayerCollection(List<Layer> collection)
		{
			LayersCollection = collection;
            for (int i = 0; i < collection.Count; i++ )
            {
                //LayersListView.
            }
            UpdateListView();
		}
		#endregion

        #region Undo Redo
        public void UndoRedoSaveStep()
        {
        }

        public void UndoRedoRecoverStep()
        {
        }
        #endregion

        private void NewLayerBtn_Click(object sender, EventArgs e)
		{
            using (NewLayer frm = new NewLayer(Layer.Counter))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LayersCollection.Add(new Layer(frm.ret_color, frm.ret_name, frm.ret_duplicate));
                    UpdateListView();
                    LayersListView.SelectedIndex = 0;
                    SelectLayer(LayersCollection.Count - 1);
                }
            }
		}

		public void SelectLayer(int index_in_collection)
		{
            SelectedLayer = LayersCollection[index_in_collection];
			PropertyManager.SelectedLayer = SelectedLayer;
            Program.PropertyDockInstance.ObjectPropsForm.UpdateLayerInfo(SelectedLayer);
		}

		public void UpdateListView()
		{
			Layer[] l = LayersCollection.ToArray();
			Array.Reverse(l);
			LayersListView.Items.Clear();
			LayersListView.Items.AddRange(l);
            for (int i = 0; i < l.Length; i++ )
            {
                if(l[i].Visible)
                {
                    LayersListView.SetItemChecked(i, true);
                }
            }
            if (LayersCollection.Count == 1) SelectLayer(0);
            Program.ObjectTreeViewInstance.UpdateNodes(this.LayersCollection.ToArray());
		}

		private void RemoveLayerBtn_Click(object sender, EventArgs e)
		{
            if (LayersCollection.Count == 1)
            {
                MessageBox.Show("Cannot remove last layer");
                return;
            }

			foreach (int index in LayersListView.SelectedIndices)
			{
                LayersCollection.RemoveAt(index < 0 ? -1 : LayersListView.Items.Count - 1 - index);
			}
			Program.MainDocumentInstance.screenControl1.ForceScreenRender();
			UpdateListView();
		}

		private void onLayerClick(object sender, MouseEventArgs e)
		{
			//ContextMenu example
			int selectionIndex = GetReverseIndexFromClick(sender as CheckedListBox, e.Location);

			if (e.Button == MouseButtons.Left && selectionIndex >= 0)
			{
                SelectLayer(selectionIndex);
			} 
			else if (e.Button == MouseButtons.Right && selectionIndex >= 0)
			{
				MenuItem[] menuItems = { new MenuItem("Properties", new EventHandler(
					(se, ev) => 
					{
						MessageBox.Show(String.Format("Prop No. {0} clicked", selectionIndex)); 
					}
					)) };
				ContextMenu menu = new ContextMenu(menuItems);
				CheckedListBox chlbx = sender as CheckedListBox;
				menu.Show(chlbx, e.Location);
			}
		}

		private int GetReverseIndexFromClick(CheckedListBox lw, Point click)
		{
			int index = lw.IndexFromPoint(click);
			return ( index < 0 ? -1 : lw.Items.Count - 1 - index);
		}

        private int GetReverseIndex<T>(IList<T> list, int index)
        {
            return ( index < 0 ? -1 : list.Count - 1 - index);
        }

        private void MoveLayerUpBtn_Click(object sender, EventArgs e)
        {
            int index = LayersListView.SelectedIndex;
            throw new NotImplementedException();
        }

        private void cleanLayerBtn_Click(object sender, EventArgs e)
        {
			if (LayersListView.CheckedIndices.Count == 0)
				MessageBox.Show("Select layer to clear");

            foreach (int i in LayersListView.CheckedIndices)
            {
				int index = (i < 0 ? -1 : LayersListView.Items.Count - 1 - i);
                LayersCollection[index].Shapes = new List<Shape>();
            }

            Program.ObjectTreeViewInstance.UpdateNodes(LayersCollection.ToArray());
			Program.MainDocumentInstance.screenControl1.ForceScreenRender();
        }

        private void changeLayerVisibility(object sender, EventArgs e)
        {
            int i = LayersListView.SelectedIndex;
            if(LayersListView.CheckedIndices.Contains(i))
            {
                LayersCollection[GetReverseIndex(LayersCollection, i)].Visible = true;
                Program.ObjectTreeViewInstance.UpdateNodes(LayersCollection.ToArray());
            }
            else
            {
                LayersCollection[GetReverseIndex(LayersCollection, i)].Visible = false;
                Program.ObjectTreeViewInstance.UpdateNodes(LayersCollection.ToArray());
            }
            Program.MainDocumentInstance.screenControl1.ForceScreenRender();
        }

        private void duplicate_layer(object sender, EventArgs e)
        {
            if (PropertyManager.SelectedLayer == null)
            {
                MessageBox.Show("No layer selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.LayersCollection.Add(PropertyManager.SelectedLayer.Clone());
            UpdateListView();
        }
    }
}
