using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawingApplication
{
    public partial class NewLayer : Form
    {
        public string ret_name;
        public Color ret_color;
        public List<Shape> ret_duplicate;

        public NewLayer()
        {
            throw new NotImplementedException();
            //InitializeComponent();
        }

        public NewLayer(int def_layer_num)
        {
            InitializeComponent();
            textBox1.Text = "Layer " + def_layer_num.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorSelector cs = new ColorSelector(button1.BackColor);
            if(cs.ShowDialog() == DialogResult.OK)
            {
                this.ret_color = cs.returnColor;
                this.button1.Image = this.ret_color == Color.Transparent ? global::DrawingApplication.Properties.Resources.TransparencyGrid : null;
                (sender as Button).BackColor = this.ret_color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Validate

            if (textBox1.Text == "")
            {
                MessageBox.Show("Set layer name");
                return;
            }

            if (this.ret_name == null)
                ret_name = textBox1.Text;
            if (this.ret_color == null)
                ret_color = Color.Transparent;
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void NewLayer_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Program.LayersFormInstance.layerManager1.GetLayersCollection().ToArray());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ret_duplicate = new List<Shape>(Program.LayersFormInstance.layerManager1.GetLayerByName(comboBox1.SelectedItem.ToString()).Shapes);
        }
    }
}
