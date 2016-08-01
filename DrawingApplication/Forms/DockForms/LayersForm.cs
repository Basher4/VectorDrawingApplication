using DigitalRune.Windows.Docking;

namespace DrawingApplication
{
	public partial class Layers : DockableForm
	{
		internal LayerManager layerManager1;

		public Layers()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.layerManager1 = new DrawingApplication.LayerManager();
			this.SuspendLayout();
			// 
			// layerManager1
			// 
			this.layerManager1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layerManager1.Enabled = false;
			this.layerManager1.Location = new System.Drawing.Point(0, 0);
			this.layerManager1.Name = "layerManager1";
			this.layerManager1.Size = new System.Drawing.Size(284, 490);
			this.layerManager1.TabIndex = 0;
			// 
			// Layers
			// 
			this.ClientSize = new System.Drawing.Size(284, 490);
			this.Controls.Add(this.layerManager1);
			this.DockAreas = ((DigitalRune.Windows.Docking.DockAreas)((((((DigitalRune.Windows.Docking.DockAreas.Float | DigitalRune.Windows.Docking.DockAreas.Left) 
            | DigitalRune.Windows.Docking.DockAreas.Right) 
            | DigitalRune.Windows.Docking.DockAreas.Top) 
            | DigitalRune.Windows.Docking.DockAreas.Bottom) 
            | DigitalRune.Windows.Docking.DockAreas.Document)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "Layers";
			this.TabText = "Layers";
			this.Text = "Layers";
			this.ResumeLayout(false);

		}
	}
}