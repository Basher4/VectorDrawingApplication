namespace DrawingApplication
{
    partial class TextInputDialog
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
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.textInputBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fontComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.NudTextSide = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTextColor = new System.Windows.Forms.Button();
            this.btnBoldSet = new System.Windows.Forms.Button();
            this.btnItalicSet = new System.Windows.Forms.Button();
            this.btnUnderlineSet = new System.Windows.Forms.Button();
            this.acceptBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NudTextSide)).BeginInit();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(579, 28);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // textInputBox
            // 
            this.textInputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textInputBox.Location = new System.Drawing.Point(0, 28);
            this.textInputBox.Multiline = true;
            this.textInputBox.Name = "textInputBox";
            this.textInputBox.Size = new System.Drawing.Size(579, 226);
            this.textInputBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Font";
            // 
            // fontComboBox
            // 
            this.fontComboBox.FormattingEnabled = true;
            this.fontComboBox.Location = new System.Drawing.Point(34, 4);
            this.fontComboBox.Name = "fontComboBox";
            this.fontComboBox.Size = new System.Drawing.Size(135, 21);
            this.fontComboBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Size";
            // 
            // NudTextSide
            // 
            this.NudTextSide.Location = new System.Drawing.Point(206, 5);
            this.NudTextSide.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.NudTextSide.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NudTextSide.Name = "NudTextSide";
            this.NudTextSide.Size = new System.Drawing.Size(57, 20);
            this.NudTextSide.TabIndex = 5;
            this.NudTextSide.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.NudTextSide.ValueChanged += new System.EventHandler(this.NudTextSide_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(274, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Color";
            // 
            // btnTextColor
            // 
            this.btnTextColor.BackColor = System.Drawing.Color.Black;
            this.btnTextColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTextColor.Location = new System.Drawing.Point(309, 3);
            this.btnTextColor.Name = "btnTextColor";
            this.btnTextColor.Size = new System.Drawing.Size(28, 23);
            this.btnTextColor.TabIndex = 7;
            this.btnTextColor.UseVisualStyleBackColor = false;
            this.btnTextColor.Click += new System.EventHandler(this.Button1_Click);
            // 
            // btnBoldSet
            // 
            this.btnBoldSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnBoldSet.Location = new System.Drawing.Point(353, 3);
            this.btnBoldSet.Name = "btnBoldSet";
            this.btnBoldSet.Size = new System.Drawing.Size(26, 23);
            this.btnBoldSet.TabIndex = 8;
            this.btnBoldSet.Text = "B";
            this.btnBoldSet.UseVisualStyleBackColor = true;
            this.btnBoldSet.Click += new System.EventHandler(this.bntBoldSet_Click);
            // 
            // btnItalicSet
            // 
            this.btnItalicSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnItalicSet.Location = new System.Drawing.Point(385, 3);
            this.btnItalicSet.Name = "btnItalicSet";
            this.btnItalicSet.Size = new System.Drawing.Size(26, 23);
            this.btnItalicSet.TabIndex = 9;
            this.btnItalicSet.Text = "I";
            this.btnItalicSet.UseVisualStyleBackColor = true;
            this.btnItalicSet.Click += new System.EventHandler(this.btnItalicSet_Click);
            // 
            // btnUnderlineSet
            // 
            this.btnUnderlineSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnUnderlineSet.Location = new System.Drawing.Point(417, 3);
            this.btnUnderlineSet.Name = "btnUnderlineSet";
            this.btnUnderlineSet.Size = new System.Drawing.Size(26, 23);
            this.btnUnderlineSet.TabIndex = 10;
            this.btnUnderlineSet.Text = "U";
            this.btnUnderlineSet.UseVisualStyleBackColor = true;
            this.btnUnderlineSet.Click += new System.EventHandler(this.btnUnderlineSet_Click);
            // 
            // acceptBtn
            // 
            //this.acceptBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptBtn.Location = new System.Drawing.Point(492, 2);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(75, 23);
            this.acceptBtn.TabIndex = 11;
            this.acceptBtn.Text = "OK";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // TextInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 254);
            this.Controls.Add(this.acceptBtn);
            this.Controls.Add(this.btnUnderlineSet);
            this.Controls.Add(this.btnItalicSet);
            this.Controls.Add(this.btnBoldSet);
            this.Controls.Add(this.btnTextColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NudTextSide);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fontComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textInputBox);
            this.Controls.Add(this.splitter1);
            this.Name = "TextInputDialog";
            this.Text = "TextInputDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextInputDialog_FormClosing);
            this.Load += new System.EventHandler(this.TextInputDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NudTextSide)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox textInputBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox fontComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown NudTextSide;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnTextColor;
        private System.Windows.Forms.Button btnBoldSet;
        private System.Windows.Forms.Button btnItalicSet;
        private System.Windows.Forms.Button btnUnderlineSet;
        private System.Windows.Forms.Button acceptBtn;
    }
}