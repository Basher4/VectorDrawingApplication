using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DigitalRune.Windows.Docking;

namespace DrawingApplication
{
    public partial class ObjectProps : DockableForm
    {
        Shape shape;
        Layer layer;

        public ObjectProps()
        {
            InitializeComponent();
            foreach(Control c in this.Controls)
                c.Enabled = false;

            this.button2.ContextMenu = new System.Windows.Forms.ContextMenu(
                new MenuItem[]{
                    new MenuItem("Copy Color", new EventHandler(B2CopyCol)),
                    new MenuItem("Paste Color", new EventHandler(B2PasteCol))
                });
            this.button1.ContextMenu = new System.Windows.Forms.ContextMenu(
                new MenuItem[]{
                    new MenuItem("Copy Color", new EventHandler(B1CopyCol)),
                    new MenuItem("Paste Color", new EventHandler(B1PasteCol))
                });
        }

        #region Context-Menus
        #region button1 - Line
        private void B1CopyCol(object sender, EventArgs e)
        {
            Preferences.CopyColor = button1.BackColor;
        }

        private void B1PasteCol(object sender, EventArgs e)
        {
            button1.BackColor = Preferences.CopyColor;
            if (this.button1.BackColor == Color.Black)
                this.button1.ForeColor = Color.White;

            shape.CurrentShape.LinePen = new Pen(Preferences.CopyColor);
            Program.MainDocumentInstance.screenControl1.ForceScreenRender();
        }
        #endregion
        #region button2 - Fill
        private void B2CopyCol(object sender, EventArgs e)
        {
            Preferences.CopyColor = button2.BackColor;
        }

        private void B2PasteCol(object sender, EventArgs e)
        {
            button2.BackColor = Preferences.CopyColor;
            if (this.button2.BackColor == Color.Black)
                this.button2.ForeColor = Color.White;

            shape.CurrentShape.FillBrush = new SolidBrush(Preferences.CopyColor);
            Program.MainDocumentInstance.screenControl1.ForceScreenRender();
        }
        #endregion
        #endregion

        public void UpdateVerts(Point[] p)
        {
            if (shape == null || p == null || p.Length == 0)
                return;

            if (shape.CurrentShape.Verts.Length == p.Length)
            {
                listBox1.DataSource = new List<Point>(p);
                shape.CurrentShape.Verts = p;
            }
        }

        public void EnableControls()
        {
            foreach (Control control in this.Controls)
                control.Enabled = true;
        }

        public void UpdateVerts(Shape s)
        {
            if (shape == null || s == null)
                return;

            if (shape.CurrentShape.Verts.Length == s.CurrentShape.Verts.Length)
            {
                listBox1.DataSource = new List<Point>(s.CurrentShape.Verts);
                shape.CurrentShape.Verts = s.CurrentShape.Verts;
            }
        }

        public void UpdateLayerInfo(Layer l)
        {
            if (l == null)
                return;

            layer = l;
            tbLayerName.Text = l.ToString();
            btnLayerColor.BackColor = l.BG_Color;

            tabPage2.Text = "Layer - " + l.ToString();
            tabControl1.SelectTab(tabPage2);
        }

        public void UpdateShapeInfo(Shape s)
        {
            if (s == null)
                return;

            BtnEditText.Enabled = (s.GetType() == typeof(TextShape));

            shape = s;
            try
            {
                tabControl1.SelectTab(tabPage1);

                this.obj_name_tb.Text = shape.CurrentShape.Name;
                this.obj_tyle_lbl.Text = shape.GetType().Name;
                this.ch_visibleStatus.Checked = shape.IsVisible;
                
                this.button1.BackColor = shape.CurrentShape.LinePen.Color;
                if (this.button1.BackColor == Color.Black)
                    this.button1.ForeColor = Color.White;

                try
                {
                    this.button2.Enabled = true;
                    this.button2.BackColor = (shape.CurrentShape.FillBrush as SolidBrush).Color;
                    if (this.button2.BackColor == Color.Black)
                        this.button2.ForeColor = Color.White;
                }
                catch(NullReferenceException)
                {
                    this.button2.Enabled = false;
                    this.button2.BackColor = Color.Transparent;
                }


                this.nudStrokeWidth.Value = (decimal)shape.CurrentShape.LinePen.Width;

                this.tabPage1.Text = "Shape - " + shape.ToString();

                Layer layer = Program.LayersFormInstance.layerManager1.GetLayerByShape(shape);
                this.obj_layer_lbl.Text = layer.ToString();

                listBox1.DataSource = new List<Point>(shape.CurrentShape.Verts);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occured: " + e.Message);
            }
        }

        private void ch_visibleStatus_CheckedChanged(object sender, EventArgs e)
        {
            shape.IsVisible = ch_visibleStatus.Checked;
            Program.MainDocumentInstance.screenControl1.ForceScreenRender();
            Program.ObjectTreeViewInstance.UpdateNodes(Program.LayersFormInstance.layerManager1.GetLayersCollection().ToArray());
        }

        private void obj_name_tb_TextChanged(object sender, EventArgs e)
        {
            if(PropertyManager.SelectedLayer.GetShapeByName(obj_name_tb.Text) == null)
            {
                shape.CurrentShape.Name = obj_name_tb.Text;
                this.tabPage1.Text = "Shape - " + obj_name_tb.Text;
                Program.ObjectTreeViewInstance.UpdateNodes(Program.LayersFormInstance.layerManager1.GetLayersCollection().ToArray());
            }
        }

        //Change Line color
        private void button1_Click(object sender, EventArgs e)
        {
            using(var cd = new ColorSelector())
            {
                if(cd.ShowDialog() == DialogResult.OK)
                {
                    Color c = cd.returnColor;
                    shape.CurrentShape.LinePen.Color = c;
                    button1.BackColor = c;
                    Program.MainDocumentInstance.screenControl1.ForceScreenRender();
                }
            }
        }

        //Change Fill Color
        private void change_fill_col(object sender, EventArgs e)
        {
            using (var cd = new ColorSelector())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    Color c = cd.returnColor;
                    shape.CurrentShape.FillBrush = new SolidBrush(c);
                    button2.BackColor = c;
                    Program.MainDocumentInstance.screenControl1.ForceScreenRender();
                }
            }
        }

        private void obj_tyle_lbl_Click(object sender, EventArgs e)
        {
            ShapeInfo si = shape.CurrentShape;
            Layer l = Program.LayersFormInstance.layerManager1.GetLayerByShape(shape);
            int index = l.Shapes.IndexOf(shape);
            si.FillBrush = Preferences.FillBrush;
            Shape r = new Rectangle(si);
            r.CurrentShape.Name = shape.CurrentShape.Name;
            l.Shapes[index] = r;
            Program.MainDocumentInstance.screenControl1.ForceScreenRender();
            Program.ObjectTreeViewInstance.UpdateNodes(Program.LayersFormInstance.layerManager1.GetLayersCollection().ToArray());
            this.UpdateShapeInfo(r);
        }

        private void nudStrokeWidth_ValueChanged(object sender, EventArgs e)
        {
            if (shape == null)
                return;

            shape.CurrentShape.LinePen.Width = (float)nudStrokeWidth.Value;
            shape.CurrentShape.LinePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            Program.MainDocumentInstance.screenControl1.ForceScreenRender();
        }

        private void BtnEditText_Click(object sender, EventArgs e)
        {
            if (shape == null)
                return;

            TextShape ts = shape as TextShape;
            using(TextInputDialog tid = new TextInputDialog(ts.Text, ts.TextColor, ts.TextFont))
            {
                if(tid.ShowDialog() == DialogResult.OK)
                {
                    ts.Text = tid.TextOut;
                    ts.TextColor = tid.TextColor;
                    ts.TextFont = tid.TextFont;

                    Program.ObjectTreeViewInstance.UpdateNodes(Program.LayersFormInstance.layerManager1.GetLayersCollection().ToArray());
                }
            }
        }

        private void btnLayerColor_Click(object sender, EventArgs e)
        {
            if (layer == null)
                return;

            using(ColorSelector col = new ColorSelector(btnLayerColor.BackColor))
            {
                if (col.ShowDialog() == DialogResult.OK)
                {
                    btnLayerColor.BackColor = layer.BG_Color = col.returnColor;
                    Program.MainDocumentInstance.screenControl1.ForceScreenRender();
                }
            }
        }

        private void tbLayerName_TextChanged(object sender, EventArgs e)
        {
            if (layer == null)
                return;

            layer.Text = tbLayerName.Text;
            Program.ObjectTreeViewInstance.UpdateNodes(Program.LayersFormInstance.layerManager1.GetLayersCollection().ToArray());
            Program.LayersFormInstance.layerManager1.UpdateListView();
        }
    }
}
