namespace DrawingApplication
{
    partial class ColorSelector
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.color_showoff = new System.Windows.Forms.PictureBox();
            this.Red_nud = new System.Windows.Forms.NumericUpDown();
            this.Green_nud = new System.Windows.Forms.NumericUpDown();
            this.Blue_nud = new System.Windows.Forms.NumericUpDown();
            this.Hue_nud = new System.Windows.Forms.NumericUpDown();
            this.Sat_nud = new System.Windows.Forms.NumericUpDown();
            this.Val_nud = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.colorWheel1 = new DrawingApplication.ColorWheel();
            this.transparencyTooltip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.color_showoff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Red_nud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Green_nud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Blue_nud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hue_nud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sat_nud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Val_nud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(237, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "RED:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "GREEN:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(237, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "BLUE:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "VAL:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(237, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "SAT:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(237, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "HUE:";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(240, 187);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(240, 213);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(57, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // color_showoff
            // 
            this.color_showoff.BackColor = System.Drawing.Color.White;
            this.color_showoff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.color_showoff.Location = new System.Drawing.Point(303, 186);
            this.color_showoff.Name = "color_showoff";
            this.color_showoff.Size = new System.Drawing.Size(55, 50);
            this.color_showoff.TabIndex = 15;
            this.color_showoff.TabStop = false;
            // 
            // Red_nud
            // 
            this.Red_nud.Location = new System.Drawing.Point(303, 13);
            this.Red_nud.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Red_nud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.Red_nud.Name = "Red_nud";
            this.Red_nud.Size = new System.Drawing.Size(55, 20);
            this.Red_nud.TabIndex = 17;
            this.Red_nud.ValueChanged += new System.EventHandler(this.RGB_ValueChanged);
            // 
            // Green_nud
            // 
            this.Green_nud.Location = new System.Drawing.Point(303, 39);
            this.Green_nud.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Green_nud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.Green_nud.Name = "Green_nud";
            this.Green_nud.Size = new System.Drawing.Size(55, 20);
            this.Green_nud.TabIndex = 18;
            this.Green_nud.ValueChanged += new System.EventHandler(this.RGB_ValueChanged);
            // 
            // Blue_nud
            // 
            this.Blue_nud.Location = new System.Drawing.Point(303, 65);
            this.Blue_nud.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Blue_nud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.Blue_nud.Name = "Blue_nud";
            this.Blue_nud.Size = new System.Drawing.Size(55, 20);
            this.Blue_nud.TabIndex = 19;
            this.Blue_nud.ValueChanged += new System.EventHandler(this.RGB_ValueChanged);
            // 
            // Hue_nud
            // 
            this.Hue_nud.Location = new System.Drawing.Point(303, 105);
            this.Hue_nud.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Hue_nud.Name = "Hue_nud";
            this.Hue_nud.Size = new System.Drawing.Size(55, 20);
            this.Hue_nud.TabIndex = 20;
            this.Hue_nud.ValueChanged += new System.EventHandler(this.Hue_nud_ValueChanged);
            // 
            // Sat_nud
            // 
            this.Sat_nud.Location = new System.Drawing.Point(303, 131);
            this.Sat_nud.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Sat_nud.Name = "Sat_nud";
            this.Sat_nud.Size = new System.Drawing.Size(55, 20);
            this.Sat_nud.TabIndex = 21;
            this.Sat_nud.ValueChanged += new System.EventHandler(this.Sat_nud_ValueChanged);
            // 
            // Val_nud
            // 
            this.Val_nud.Location = new System.Drawing.Point(303, 157);
            this.Val_nud.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Val_nud.Name = "Val_nud";
            this.Val_nud.Size = new System.Drawing.Size(55, 20);
            this.Val_nud.TabIndex = 22;
            this.Val_nud.ValueChanged += new System.EventHandler(this.Val_nud_ValueChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(25, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "T";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(364, 13);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 223);
            this.trackBar1.TabIndex = 24;
            this.trackBar1.Value = 255;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // colorWheel1
            // 
            this.colorWheel1.Hue = ((byte)(0));
            this.colorWheel1.Lightness = ((byte)(0));
            this.colorWheel1.Location = new System.Drawing.Point(12, 20);
            this.colorWheel1.Name = "colorWheel1";
            this.colorWheel1.Saturation = ((byte)(0));
            this.colorWheel1.SecondaryHues = null;
            this.colorWheel1.Size = new System.Drawing.Size(219, 216);
            this.colorWheel1.TabIndex = 16;
            this.colorWheel1.Text = "colorWheel1";
            this.colorWheel1.HueChanged += new System.EventHandler(this.onHueChanged);
            this.colorWheel1.SLChanged += new System.EventHandler(this.onSLCHanged);
            // 
            // transparencyTooltip
            // 
            this.transparencyTooltip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.transparencyTooltip.ToolTipTitle = "Set color to Transparent";
            // 
            // ColorSelector
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(421, 248);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Val_nud);
            this.Controls.Add(this.Sat_nud);
            this.Controls.Add(this.Hue_nud);
            this.Controls.Add(this.Blue_nud);
            this.Controls.Add(this.Green_nud);
            this.Controls.Add(this.Red_nud);
            this.Controls.Add(this.colorWheel1);
            this.Controls.Add(this.color_showoff);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ColorSelector";
            this.Text = "Color Selector";
            ((System.ComponentModel.ISupportInitialize)(this.color_showoff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Red_nud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Green_nud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Blue_nud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hue_nud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sat_nud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Val_nud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox color_showoff;
        private ColorWheel colorWheel1;
        private System.Windows.Forms.NumericUpDown Red_nud;
        private System.Windows.Forms.NumericUpDown Green_nud;
        private System.Windows.Forms.NumericUpDown Blue_nud;
        private System.Windows.Forms.NumericUpDown Hue_nud;
        private System.Windows.Forms.NumericUpDown Sat_nud;
        private System.Windows.Forms.NumericUpDown Val_nud;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ToolTip transparencyTooltip;
    }
}