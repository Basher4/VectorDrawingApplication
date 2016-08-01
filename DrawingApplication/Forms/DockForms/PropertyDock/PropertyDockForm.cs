using System.Windows.Forms;
using System.Drawing;
using DigitalRune.Windows.Docking;

namespace DrawingApplication
{
    public partial class PropertyDock : DockableForm
	{
		private DockPanel dockPanel1;

        public NewObjectProps NewObjectPropsForm;
        public ObjectProps ObjectPropsForm;
        public PropertyDock()
        {
            InitializeComponent();
            dockPanel1.DockLeftPortion  = 238;
            dockPanel1.DockRightPortion = 238;

            NewObjectPropsForm = new NewObjectProps();
            NewObjectPropsForm.Show(dockPanel1, DockState.DockLeft);

            ObjectPropsForm = new ObjectProps();
            ObjectPropsForm.Show(dockPanel1, DockState.Document);
        }

        private void InitializeComponent()
        {
			this.dockPanel1 = new DigitalRune.Windows.Docking.DockPanel();
			this.SuspendLayout();
			// 
			// dockPanel1
			// 
			this.dockPanel1.ActiveAutoHideContent = null;
			this.dockPanel1.DefaultFloatingWindowSize = new System.Drawing.Size(300, 300);
			this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel1.Location = new System.Drawing.Point(0, 0);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.Size = new System.Drawing.Size(584, 261);
			this.dockPanel1.TabIndex = 4;
			// 
			// PropertyDock
			// 
			this.ClientSize = new System.Drawing.Size(584, 261);
			this.Controls.Add(this.dockPanel1);
			this.DockAreas = ((DigitalRune.Windows.Docking.DockAreas)((((((DigitalRune.Windows.Docking.DockAreas.Float | DigitalRune.Windows.Docking.DockAreas.Left) 
            | DigitalRune.Windows.Docking.DockAreas.Right) 
            | DigitalRune.Windows.Docking.DockAreas.Top) 
            | DigitalRune.Windows.Docking.DockAreas.Bottom) 
            | DigitalRune.Windows.Docking.DockAreas.Document)));
			this.Name = "PropertyDock";
			this.TabText = "Properties";
			this.Text = "Properties";
			this.ResumeLayout(false);

        }

		/*
		private void FillColorSelebtor_btn_Click(object sender, System.EventArgs e)
		{
			using (var col = new ColorSelector())
			{
				var result = col.ShowDialog();
				if (result == System.Windows.Forms.DialogResult.OK)
				{
					FillColorSelebtor_btn.BackColor = col.returnColor;
					Preferences.FillBrush = new SolidBrush(col.returnColor);
				}
			}
		}
		 */
    }
}
