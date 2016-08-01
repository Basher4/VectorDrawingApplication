using System;
using System.Windows.Forms;

namespace DrawingApplication
{
	public partial class NewDocument : Form
	{
		public NewDocumentInfo docInfo { get; private set; }

		public NewDocument()
		{
			InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (numericUpDown1.Value == 0 || numericUpDown2.Value == 0 || textBox1.Text == "")
				MessageBox.Show("Please fill all fields");
			else
			{
				docInfo = new NewDocumentInfo((int)numericUpDown1.Value, (int)numericUpDown2.Value, textBox1.Text);
				this.Close();
			}
		}

		private void nudClick(object sender, EventArgs e)
		{
			NumericUpDown n = (NumericUpDown)sender;

			n.Select(0, n.Value.ToString().Length);
		}
	}
}