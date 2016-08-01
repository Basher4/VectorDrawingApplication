namespace DrawingApplication
{
    partial class LayerManager
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayerManager));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.LayersListView = new System.Windows.Forms.CheckedListBox();
            this.MoveLayerUpBtn = new System.Windows.Forms.ToolStripButton();
            this.MoveLayerDownBtn = new System.Windows.Forms.ToolStripButton();
            this.NewLayerBtn = new System.Windows.Forms.ToolStripButton();
            this.RemoveLayerBtn = new System.Windows.Forms.ToolStripButton();
            this.cleanLayerBtn = new System.Windows.Forms.ToolStripButton();
            this.btn_duplicateLayer = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveLayerUpBtn,
            this.MoveLayerDownBtn,
            this.toolStripSeparator1,
            this.NewLayerBtn,
            this.RemoveLayerBtn,
            this.btn_duplicateLayer,
            this.toolStripSeparator2,
            this.cleanLayerBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(233, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // LayersListView
            // 
            this.LayersListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayersListView.FormattingEnabled = true;
            this.LayersListView.Location = new System.Drawing.Point(0, 25);
            this.LayersListView.Name = "LayersListView";
            this.LayersListView.Size = new System.Drawing.Size(233, 414);
            this.LayersListView.TabIndex = 1;
            this.LayersListView.SelectedIndexChanged += new System.EventHandler(this.changeLayerVisibility);
            this.LayersListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onLayerClick);
            // 
            // MoveLayerUpBtn
            // 
            this.MoveLayerUpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveLayerUpBtn.Image = ((System.Drawing.Image)(resources.GetObject("MoveLayerUpBtn.Image")));
            this.MoveLayerUpBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveLayerUpBtn.Name = "MoveLayerUpBtn";
            this.MoveLayerUpBtn.Size = new System.Drawing.Size(23, 22);
            this.MoveLayerUpBtn.Text = "Layer UP";
            this.MoveLayerUpBtn.Click += new System.EventHandler(this.MoveLayerUpBtn_Click);
            // 
            // MoveLayerDownBtn
            // 
            this.MoveLayerDownBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveLayerDownBtn.Image = global::DrawingApplication.Properties.Resources.ArrowDown;
            this.MoveLayerDownBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveLayerDownBtn.Name = "MoveLayerDownBtn";
            this.MoveLayerDownBtn.Size = new System.Drawing.Size(23, 22);
            this.MoveLayerDownBtn.Text = "Layer Down";
            // 
            // NewLayerBtn
            // 
            this.NewLayerBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewLayerBtn.Image = global::DrawingApplication.Properties.Resources.AddLayerIcon;
            this.NewLayerBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewLayerBtn.Name = "NewLayerBtn";
            this.NewLayerBtn.Size = new System.Drawing.Size(23, 22);
            this.NewLayerBtn.Text = "New Layer";
            this.NewLayerBtn.Click += new System.EventHandler(this.NewLayerBtn_Click);
            // 
            // RemoveLayerBtn
            // 
            this.RemoveLayerBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemoveLayerBtn.Image = global::DrawingApplication.Properties.Resources.RmLayerIcon;
            this.RemoveLayerBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveLayerBtn.Name = "RemoveLayerBtn";
            this.RemoveLayerBtn.Size = new System.Drawing.Size(23, 22);
            this.RemoveLayerBtn.Text = "Delete Layer";
            this.RemoveLayerBtn.Click += new System.EventHandler(this.RemoveLayerBtn_Click);
            // 
            // cleanLayerBtn
            // 
            this.cleanLayerBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cleanLayerBtn.Image = global::DrawingApplication.Properties.Resources.CleanLayer;
            this.cleanLayerBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cleanLayerBtn.Name = "cleanLayerBtn";
            this.cleanLayerBtn.Size = new System.Drawing.Size(23, 22);
            this.cleanLayerBtn.Text = "Clean Selected Layer";
            this.cleanLayerBtn.Click += new System.EventHandler(this.cleanLayerBtn_Click);
            // 
            // btn_duplicateLayer
            // 
            this.btn_duplicateLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_duplicateLayer.Image = global::DrawingApplication.Properties.Resources.DuplicateLayer;
            this.btn_duplicateLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_duplicateLayer.Name = "btn_duplicateLayer";
            this.btn_duplicateLayer.Size = new System.Drawing.Size(23, 22);
            this.btn_duplicateLayer.Text = "Duplicate layer";
            this.btn_duplicateLayer.Click += new System.EventHandler(this.duplicate_layer);
            // 
            // LayerManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LayersListView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "LayerManager";
            this.Size = new System.Drawing.Size(233, 439);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton MoveLayerUpBtn;
        private System.Windows.Forms.ToolStripButton MoveLayerDownBtn;
        private System.Windows.Forms.CheckedListBox LayersListView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton NewLayerBtn;
        private System.Windows.Forms.ToolStripButton RemoveLayerBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cleanLayerBtn;
        private System.Windows.Forms.ToolStripButton btn_duplicateLayer;
    }
}
