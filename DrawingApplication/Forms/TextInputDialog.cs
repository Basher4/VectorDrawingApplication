using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawingApplication
{
    public partial class TextInputDialog : Form
    {
        public Font TextFont { get; private set; }
        public string TextOut { get; private set; }
        public Color TextColor { get; private set; }

        private bool _bold = false, _italic = false, _underline = false;

        public TextInputDialog()
        {
            InitializeComponent();
        }

        public TextInputDialog(string text, Color color, Font textFont)
        {
            InitializeComponent();

            textInputBox.Text = TextOut = text;
            textInputBox.Font = TextFont = textFont;
            textInputBox.ForeColor = TextColor = color;
        }

        private void TextInputDialog_Load(object sender, EventArgs e)
        {
            FontFamily[] fontFamilies;
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            fontFamilies = installedFontCollection.Families;
            foreach (var fontFamily in fontFamilies)
                fontComboBox.Items.Add(fontFamily.Name);

            fontComboBox.SelectedIndexChanged += fontComboBox_SelectedIndexChanged;
            fontComboBox.SelectedItem = fontComboBox.Items[0];
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using(ColorSelector cs = new ColorSelector(btnTextColor.BackColor))
            {
                if(cs.ShowDialog() == DialogResult.OK)
                {
                    TextColor = cs.returnColor;
                    textInputBox.ForeColor = TextColor;
                    btnTextColor.BackColor = TextColor;
                }
            }
        }

        private void NudTextSide_ValueChanged(object sender, EventArgs e)
        {
            textInputBox.Font = new Font(fontComboBox.SelectedItem.ToString(), (float)NudTextSide.Value);
            UpdateBUIFont();
        }

        private void fontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            textInputBox.Font = new Font(fontComboBox.SelectedItem.ToString(), (float)NudTextSide.Value);
            UpdateBUIFont();
        }

        #region BOLD ITALIC UNDERLINE

        private void UpdateBUIFont()
        {
            FontStyle fs = FontStyle.Regular;
            if(_bold)
                fs |= FontStyle.Bold;
            if(_italic)
                fs |= FontStyle.Italic;
            if(_underline)
                fs |= FontStyle.Underline;

            textInputBox.Font = new Font(textInputBox.Font, fs);
        }

        private void bntBoldSet_Click(object sender, EventArgs e)
        {
            _bold = !_bold;
            btnBoldSet.BackColor = _bold ? SystemColors.ActiveBorder : SystemColors.Control;
            UpdateBUIFont();
        }

        private void btnItalicSet_Click(object sender, EventArgs e)
        {
            _italic = !_italic;
            btnItalicSet.BackColor = _italic ? SystemColors.ActiveBorder : SystemColors.Control;
            UpdateBUIFont();
        }

        private void btnUnderlineSet_Click(object sender, EventArgs e)
        {
            _underline = !_underline;
            btnUnderlineSet.BackColor = _underline ? SystemColors.ActiveBorder : SystemColors.Control;
            UpdateBUIFont();
        }

        #endregion

        private void TextInputDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.TextFont = textInputBox.Font;
            this.TextOut = textInputBox.Text;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if(Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void acceptBtn_Click(object sender, EventArgs e)
        {
            if (this.textInputBox.Text == "")
            {
                MessageBox.Show("Please enter text to draw");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
