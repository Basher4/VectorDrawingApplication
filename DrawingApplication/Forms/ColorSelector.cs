using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Unclassified.Drawing;

namespace DrawingApplication
{
    public partial class ColorSelector : Form
    {
        private const float CIRCLE_SIZE = 10;
        private bool fromUpdate = false;

        public Color returnColor { get; set; }

        public ColorSelector(byte Hue, byte Saturation, byte Lightness)
        {
            InitializeComponent();

            colorWheel1.Hue = Hue;
            colorWheel1.Saturation = Saturation;
            colorWheel1.Lightness = Lightness;

            UpdateColor();
        }

        public ColorSelector(Color color)
        {
            InitializeComponent();

            HslColor col = ColorMath.RgbToHsl(color);

            colorWheel1.Hue = col.H;
            colorWheel1.Saturation = col.S;
            colorWheel1.Lightness = col.L;

            UpdateColor();
        }

        public ColorSelector()
        {
            InitializeComponent();

            colorWheel1.Hue = 0;
            colorWheel1.Saturation = 0;
            colorWheel1.Lightness = 0;

            UpdateColor();
        }

        public void SetColor(Color color)
        {
            HslColor col = ColorMath.RgbToHsl(color);

            colorWheel1.Hue = col.H;
            colorWheel1.Saturation = col.S;
            colorWheel1.Lightness = col.L;

            UpdateColor();
        }

        public void SetColor(byte Hue, byte Saturation, byte Lightness)
        {
            colorWheel1.Hue = Hue;
            colorWheel1.Saturation = Saturation;
            colorWheel1.Lightness = Lightness;

            UpdateColor();
        }

        private void onHueChanged(object sender, EventArgs e)
        {
            UpdateColor();
        }

        private void onSLCHanged(object sender, EventArgs e)
        {
            UpdateColor();
        }

        private void UpdateColor()
        {
            fromUpdate = true;

            byte h = colorWheel1.Hue;
            byte s = colorWheel1.Saturation;
            byte l = colorWheel1.Lightness;

            //Get RGB value from wheel
            Unclassified.Drawing.HslColor hsl = new Unclassified.Drawing.HslColor(h, s, l);
            Color color = Unclassified.Drawing.ColorMath.HslToRgb(hsl);
            
            //Update textboxes
            Red_nud.Value = color.R;
            Green_nud.Value = color.G;
            Blue_nud.Value = color.B;

            Hue_nud.Value = h;
            Sat_nud.Value = s;
            Val_nud.Value = l;

            //Update vars and showoff
            color_showoff.BackColor = color;
            returnColor = color;

            fromUpdate = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region --RESPOND TO HSV CHANGES

        private void Hue_nud_ValueChanged(object sender, EventArgs e)
        {
            colorWheel1.Hue = (byte)Hue_nud.Value;
            UpdateColor();
        }

        private void Sat_nud_ValueChanged(object sender, EventArgs e)
        {
            colorWheel1.Saturation = (byte)Sat_nud.Value;
            UpdateColor();
        }

        private void Val_nud_ValueChanged(object sender, EventArgs e)
        {
            colorWheel1.Lightness = (byte)Val_nud.Value;
            UpdateColor();
        }
        #endregion
        
        #region --RESPOND TO RGB CHANGES
        private void RGB_ValueChanged(object sender, EventArgs e)
        {
            if (!fromUpdate)
            {
                Unclassified.Drawing.HslColor hsl = Unclassified.Drawing.ColorMath.RgbToHsl(
                    Color.FromArgb((int)trackBar1.Value, (int)Red_nud.Value, (int)Green_nud.Value, (int)Blue_nud.Value));

                colorWheel1.Hue = hsl.H;
                colorWheel1.Saturation = hsl.S;
                colorWheel1.Lightness = hsl.L;
                UpdateColor();
            }
        }
        #endregion

		private void button3_Click(object sender, EventArgs e)
		{
			color_showoff.BackColor = Color.Transparent;
			returnColor = Color.Transparent;
		}

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int alpha = trackBar1.Value;
            Color c = (alpha == 255) ? Color.Transparent : Color.FromArgb(alpha, color_showoff.BackColor);
            color_showoff.BackColor = returnColor = c;
        }
    }
}
