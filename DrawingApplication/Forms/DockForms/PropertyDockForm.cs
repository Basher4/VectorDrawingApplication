using System.Windows.Forms;
using System.Drawing;
using DigitalRune.Windows.Docking;

namespace DrawingApplication
{
    public partial class PropertyDock : DockableForm
    {
        private Button lineColorSelector_btn;
        private Label label1;
        private Label label2;
        private Button FillColorSelebtor_btn;
    
        public PropertyDock()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lineColorSelector_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FillColorSelebtor_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lineColorSelector_btn
            // 
            this.lineColorSelector_btn.Location = new System.Drawing.Point(14, 25);
            this.lineColorSelector_btn.Name = "lineColorSelector_btn";
            this.lineColorSelector_btn.Size = new System.Drawing.Size(54, 45);
            this.lineColorSelector_btn.TabIndex = 0;
            this.lineColorSelector_btn.UseVisualStyleBackColor = true;
            this.lineColorSelector_btn.Click += new System.EventHandler(this.linepen_buttonClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Line Color:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fill Color: ";
            // 
            // FillColorSelebtor_btn
            // 
            this.FillColorSelebtor_btn.Location = new System.Drawing.Point(14, 102);
            this.FillColorSelebtor_btn.Name = "FillColorSelebtor_btn";
            this.FillColorSelebtor_btn.Size = new System.Drawing.Size(54, 45);
            this.FillColorSelebtor_btn.TabIndex = 3;
            this.FillColorSelebtor_btn.UseVisualStyleBackColor = true;
            // 
            // PropertyDock
            // 
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.FillColorSelebtor_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lineColorSelector_btn);
            this.DockAreas = ((DigitalRune.Windows.Docking.DockAreas)((((((DigitalRune.Windows.Docking.DockAreas.Float | DigitalRune.Windows.Docking.DockAreas.Left) 
            | DigitalRune.Windows.Docking.DockAreas.Right) 
            | DigitalRune.Windows.Docking.DockAreas.Top) 
            | DigitalRune.Windows.Docking.DockAreas.Bottom) 
            | DigitalRune.Windows.Docking.DockAreas.Document)));
            this.Name = "PropertyDock";
            this.TabText = "Properties";
            this.Text = "Properties";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void linepen_buttonClicked(object sender, System.EventArgs e)
        {
            Button btn = sender as Button;
            using(var col = new ColorSelector())
            {
                var result = col.ShowDialog();
                if(result == System.Windows.Forms.DialogResult.OK)
                {
                    btn.BackColor = col.returnColor;
                }
            }
        }
    }
}
