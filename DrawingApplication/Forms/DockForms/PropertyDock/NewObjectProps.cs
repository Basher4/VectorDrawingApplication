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
    public partial class NewObjectProps : DockableForm
    {
        private MenuItem[] mi;
        private ContextMenu cm;
        public NewObjectProps()
        {
            InitializeComponent();

            //Initialize context menus

            this.lineColorSelector_btn.ContextMenu = new System.Windows.Forms.ContextMenu(
                new MenuItem[]{
                    new MenuItem("Copy Color", new EventHandler(BLCopyCol)),
                    new MenuItem("Paste Color", new EventHandler(BLPasteCol))
                });
            this.button1.ContextMenu = new System.Windows.Forms.ContextMenu(
                new MenuItem[]{
                    new MenuItem("Copy Color", new EventHandler(B1CopyCol)),
                    new MenuItem("Paste Color", new EventHandler(BLPasteCol))
                });

            lineColorSelector_btn.BackColor = Preferences.LinePen.Color;
            numericUpDown1.Value = (decimal)Preferences.LinePen.Width;

            button1.BackColor = (Preferences.FillBrush as SolidBrush).Color;
            freeHandSmooth_nUd.Value = (decimal)Preferences.FreeHandSmoothingDistance;
        }

        #region Context-Menus
        #region button1
        private void B1CopyCol(object sender, EventArgs e)
        {
            Preferences.CopyColor = button1.BackColor;
        }

        private void B1PasteCol(object sender, EventArgs e)
        {
            button1.BackColor = Preferences.CopyColor;
            Preferences.FillBrush = new SolidBrush(Preferences.CopyColor);
        }
        #endregion
        #region lineColorSelector_btn
        private void BLCopyCol(object sender, EventArgs e)
        {
            Preferences.CopyColor = lineColorSelector_btn.BackColor;
        }

        private void BLPasteCol(object sender, EventArgs e)
        {
            lineColorSelector_btn.BackColor = Preferences.CopyColor;
            Preferences.LinePen = new Pen(Preferences.CopyColor);
        }
        #endregion
        #endregion

        private void lineColorSelector_btn_Click(object sender, EventArgs e)
        {
            using (var col = new ColorSelector(Preferences.LinePen.Color))
            {
                var result = col.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    lineColorSelector_btn.BackColor = col.returnColor;
                    Preferences.LinePen = new Pen(col.returnColor, (float)numericUpDown1.Value);

                    switch (comboBox2.Text.ToLower())
                    {
                        case "solid":
                            Preferences.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                            break;
                        case "dash":
                            Preferences.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                            break;
                        case "dot":
                            Preferences.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                            break;
                        case "dash-dot":
                            Preferences.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                            break;
                        case "dash-dot-dot":
                            Preferences.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                            break;
                    }
                }
            }
        }

        private void UpdateColors()
        {
            Preferences.LinePen = new Pen(lineColorSelector_btn.BackColor);
            Preferences.FillBrush = new SolidBrush(button1.BackColor);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (var col = new ColorSelector((Preferences.FillBrush as SolidBrush).Color))
            {
                var result = col.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Preferences.FillBrush = new SolidBrush(col.returnColor);
                    button1.BackColor = col.returnColor;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var col = new ColorSelector(Preferences.LinePen.Color))
            {
                var result = col.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    lineColorSelector_btn.BackColor = col.returnColor;
                    Preferences.LinePen = new Pen(col.returnColor, (float)numericUpDown1.Value);

                    switch (comboBox2.Text.ToLower())
                    {
                        case "solid":
                            Preferences.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                            break;
                        case "dash":
                            Preferences.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                            break;
                        case "dot":
                            Preferences.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                            break;
                        case "dash-dot":
                            Preferences.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                            break;
                        case "dash-dot-dot":
                            Preferences.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                            break;
                    }
                }
            }
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            Preferences.LinePen = new Pen(Preferences.LinePen.Color, (float)numericUpDown1.Value);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            PropertyManager.ConnectPolyTool = checkBox1.Checked;
        }

        private void freeHandSmooth_nUd_ValueChanged(object sender, EventArgs e)
        {
            Preferences.FreeHandSmoothingDistance = (float)freeHandSmooth_nUd.Value;
        }
    }
}
