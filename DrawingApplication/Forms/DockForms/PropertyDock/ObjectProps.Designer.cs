namespace DrawingApplication
{
    partial class ObjectProps
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.BtnEditText = new System.Windows.Forms.Button();
            this.nudStrokeWidth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.obj_layer_lbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.obj_name_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ch_visibleStatus = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.obj_tyle_lbl = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnLayerColor = new System.Windows.Forms.Button();
            this.tbLayerName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStrokeWidth)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(560, 293);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.BtnEditText);
            this.tabPage1.Controls.Add(this.nudStrokeWidth);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Controls.Add(this.obj_layer_lbl);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.obj_name_tb);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.ch_visibleStatus);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.obj_tyle_lbl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(552, 267);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Shape";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // BtnEditText
            // 
            this.BtnEditText.Location = new System.Drawing.Point(9, 112);
            this.BtnEditText.Name = "BtnEditText";
            this.BtnEditText.Size = new System.Drawing.Size(189, 23);
            this.BtnEditText.TabIndex = 24;
            this.BtnEditText.Text = "Edit Text";
            this.BtnEditText.UseVisualStyleBackColor = true;
            this.BtnEditText.Click += new System.EventHandler(this.BtnEditText_Click);
            // 
            // nudStrokeWidth
            // 
            this.nudStrokeWidth.Location = new System.Drawing.Point(78, 77);
            this.nudStrokeWidth.Name = "nudStrokeWidth";
            this.nudStrokeWidth.Size = new System.Drawing.Size(120, 20);
            this.nudStrokeWidth.TabIndex = 23;
            this.nudStrokeWidth.ValueChanged += new System.EventHandler(this.nudStrokeWidth_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Stroke width";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(373, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(171, 69);
            this.listBox1.TabIndex = 21;
            // 
            // obj_layer_lbl
            // 
            this.obj_layer_lbl.AutoSize = true;
            this.obj_layer_lbl.Location = new System.Drawing.Point(86, 53);
            this.obj_layer_lbl.Name = "obj_layer_lbl";
            this.obj_layer_lbl.Size = new System.Drawing.Size(33, 13);
            this.obj_layer_lbl.TabIndex = 19;
            this.obj_layer_lbl.Text = "None";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Object in layer";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(302, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 60);
            this.button2.TabIndex = 17;
            this.button2.Text = "Fill Color";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.change_fill_col);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 60);
            this.button1.TabIndex = 16;
            this.button1.Text = "Line Color";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // obj_name_tb
            // 
            this.obj_name_tb.Location = new System.Drawing.Point(79, 26);
            this.obj_name_tb.Name = "obj_name_tb";
            this.obj_name_tb.Size = new System.Drawing.Size(126, 20);
            this.obj_name_tb.TabIndex = 15;
            this.obj_name_tb.TextChanged += new System.EventHandler(this.obj_name_tb_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Object type:";
            // 
            // ch_visibleStatus
            // 
            this.ch_visibleStatus.AutoSize = true;
            this.ch_visibleStatus.Checked = true;
            this.ch_visibleStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ch_visibleStatus.Location = new System.Drawing.Point(138, 6);
            this.ch_visibleStatus.Name = "ch_visibleStatus";
            this.ch_visibleStatus.Size = new System.Drawing.Size(67, 17);
            this.ch_visibleStatus.TabIndex = 14;
            this.ch_visibleStatus.Text = "Is Visible";
            this.ch_visibleStatus.UseVisualStyleBackColor = true;
            this.ch_visibleStatus.CheckedChanged += new System.EventHandler(this.ch_visibleStatus_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Object name:";
            // 
            // obj_tyle_lbl
            // 
            this.obj_tyle_lbl.AutoSize = true;
            this.obj_tyle_lbl.Location = new System.Drawing.Point(76, 6);
            this.obj_tyle_lbl.Name = "obj_tyle_lbl";
            this.obj_tyle_lbl.Size = new System.Drawing.Size(33, 13);
            this.obj_tyle_lbl.TabIndex = 12;
            this.obj_tyle_lbl.Text = "None";
            this.obj_tyle_lbl.Click += new System.EventHandler(this.obj_tyle_lbl_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnLayerColor);
            this.tabPage2.Controls.Add(this.tbLayerName);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(552, 267);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Layer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnLayerColor
            // 
            this.btnLayerColor.BackColor = System.Drawing.Color.Transparent;
            this.btnLayerColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnLayerColor.Location = new System.Drawing.Point(13, 35);
            this.btnLayerColor.Name = "btnLayerColor";
            this.btnLayerColor.Size = new System.Drawing.Size(62, 56);
            this.btnLayerColor.TabIndex = 18;
            this.btnLayerColor.Text = "Layer Color";
            this.btnLayerColor.UseVisualStyleBackColor = false;
            this.btnLayerColor.Click += new System.EventHandler(this.btnLayerColor_Click);
            // 
            // tbLayerName
            // 
            this.tbLayerName.Location = new System.Drawing.Point(83, 9);
            this.tbLayerName.Name = "tbLayerName";
            this.tbLayerName.Size = new System.Drawing.Size(126, 20);
            this.tbLayerName.TabIndex = 17;
            this.tbLayerName.TextChanged += new System.EventHandler(this.tbLayerName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Layer name:";
            // 
            // ObjectProps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 293);
            this.Controls.Add(this.tabControl1);
            this.DockAreas = ((DigitalRune.Windows.Docking.DockAreas)((((((DigitalRune.Windows.Docking.DockAreas.Float | DigitalRune.Windows.Docking.DockAreas.Left) 
            | DigitalRune.Windows.Docking.DockAreas.Right) 
            | DigitalRune.Windows.Docking.DockAreas.Top) 
            | DigitalRune.Windows.Docking.DockAreas.Bottom) 
            | DigitalRune.Windows.Docking.DockAreas.Document)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ObjectProps";
            this.TabText = "Object Properties";
            this.Text = "Object Properties";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStrokeWidth)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox obj_name_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ch_visibleStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label obj_tyle_lbl;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label obj_layer_lbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox tbLayerName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudStrokeWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnEditText;
        private System.Windows.Forms.Button btnLayerColor;


    }
}