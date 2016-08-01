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
	public partial class LineProps : DockableForm
	{
		public LineProps()
		{
			InitializeComponent();
            lineColorSelector_btn.BackColor = Preferences.LinePen.Color;
            numericUpDown1.Value = (decimal)Preferences.LinePen.Width;
		}

		private void linepen_buttonClicked(object sender, System.EventArgs e)
		{
			using (var col = new ColorSelector(Preferences.LinePen.Color))
			{
				var result = col.ShowDialog();
				if (result == System.Windows.Forms.DialogResult.OK)
				{
					lineColorSelector_btn.BackColor = col.returnColor;
					Preferences.LinePen = new Pen(col.returnColor, (float)numericUpDown1.Value);

					switch (comboBox1.Text.ToLower())
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

		private void numericUpDown1_ValueChanged(object sender, System.EventArgs e)
		{
			Preferences.LinePen = new Pen(Preferences.LinePen.Color, (float)numericUpDown1.Value);
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch (comboBox1.Text.ToLower())
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
