using DigitalRune.Windows.Docking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;

namespace DrawingApplication
{
	public partial class MainForm : Form
	{
		public ToolStripButton[] ToolButtons;
        private NewDocumentInfo ndi;

		public MainForm()
		{
			InitializeComponent();

			//Show docks
			Program.LayersFormInstance = new Layers();
			Program.LayersFormInstance.Show(dockPanel1, DockState.DockRight);

			Program.ObjectTreeViewInstance = new ObjectTreeView();
			Program.ObjectTreeViewInstance.Show(dockPanel1, DockState.DockLeft);

			Program.PropertyDockInstance = new PropertyDock();
			Program.PropertyDockInstance.Show(dockPanel1, DockState.DockBottom);

			ToolButtons = new[] {	toolStripLineBtn, toolStripRectBtn, toolStripOvalBtn,toolStripPolyBtn,
									toolStripFreeHand, toolStripText, toolStripSelectBtn, toolStripMoveBtn };
		}

		#region MDI Toolstrip Button Event Handlers

		private void ShowNewForm(object sender, EventArgs e)
		{
            if (!Program.WindowOpened)
            {
                var form = new NewDocument();
                form.ShowDialog();
                ndi = form.docInfo;
                form.Dispose();
                if (ndi.Title != null)
                {
                    var doc = new Document();
                    doc.Text = ndi.Title;
                    doc.screenControl1.Size = new System.Drawing.Size(ndi.Width, ndi.Height);
                    doc.screenControl1.Location = new System.Drawing.Point(doc.Width / 2 - ndi.Width / 2, doc.Height / 2 - ndi.Height / 2);
                    doc.Show(dockPanel1, DockState.Document);
                    Program.WindowOpened = true;
                    Program.PropertyDockInstance.ObjectPropsForm.EnableControls();
                }
            }
		}

		private void OpenFile(object sender, EventArgs e)
		{
            if (Program.WindowOpened)
                if(MessageBox.Show("Do you want to save current file?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.OK)
                    SaveAsToolStripMenuItem_Click(sender, e);

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                openFileDialog.Filter = "Drawing Application file (*.daf)|*.daf|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    if(Program.WindowOpened)
                        Program.MainDocumentInstance.Close();

                    XDocument xdoc = XDocument.Load(openFileDialog.FileName);
                    XElement doc = xdoc.Element("document");

                    //Get document metrics
                    XElement screenNode = doc.Element("screen");
                    int screenWidth = int.Parse(screenNode.Attribute("width").Value);
                    int screenHeight = int.Parse(screenNode.Attribute("height").Value);
                    string text = doc.Attribute("name").Value;

                    //Layers
                    XElement layers = doc.Element("layers");
                    int layerCount = int.Parse(layers.Attribute("count").Value);
                    List<Layer> layerCollection = new List<Layer>(layerCount);
                    List<XElement> layerNodes = new List<XElement>(layers.Elements());

                    for(int i = 0; i < layerCount; i++)
                    {
                        layerCollection.Add(Layer.ImportFromXML(layerNodes[i]));
                    }

                    var docForm = new Document(layerCollection);
                    docForm.Text = text;
                    docForm.screenControl1.Size = new System.Drawing.Size(screenWidth, screenHeight);
                    docForm.screenControl1.Location = new System.Drawing.Point(docForm.Width / 2 - screenWidth / 2, docForm.Height / 2 - screenHeight / 2);
                    docForm.Show(dockPanel1, DockState.Document);

                    docForm.screenControl1.ForceScreenRender();

                    Program.WindowOpened = true;
                    Program.PropertyDockInstance.ObjectPropsForm.EnableControls();
                    Program.MainDocumentInstance = docForm;
                }
            }
		}

		private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
            if (!Program.WindowOpened)
                return;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                saveFileDialog.Filter = "Drawing Application file (*.daf)|*.daf";
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string FileName = saveFileDialog.FileName;

                    Program.MainDocumentInstance.ExportToXml(FileName);
                }
            }

            MessageBox.Show("Done");
		}

		private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void CutToolStripMenuItem_Click(object sender, EventArgs e)
		{
            
		}

		private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			toolStrip.Visible = toolBarToolStripMenuItem.Checked;
		}

		private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			statusStrip.Visible = statusBarToolStripMenuItem.Checked;
		}
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Program.WindowOpened)
            {
                Program.LayersFormInstance.layerManager1.UndoRedoRecoverStep();
            }
        }

		#endregion

		private void toolStripToolBtnClick(object sender, EventArgs e)
		{
			Program.activeTool = ToolsAvailableMethods.GetToolFromIndex(Array.IndexOf(ToolButtons, sender));

			EnableOne(sender as ToolStripButton);
		}

        private void toolStripToolTextBtnClick(object sender, EventArgs e)
        {
            using (TextInputDialog tid = new TextInputDialog())
            {
                if (tid.ShowDialog() == DialogResult.OK)
                {
                    PropertyManager.TextColor = tid.TextColor;
                    PropertyManager.TextToDraw = tid.TextOut;
                    PropertyManager.FontToDraw = tid.TextFont;
                    PropertyManager.DrawText = true;

                    Program.activeTool = ToolsAvailable.Text;
                    EnableOne(sender as ToolStripButton);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            IDockableForm doc = dockPanel1.ActiveContent;
            ScreenControl sc = (doc as Document).screenControl1;
            sc.GridEnabled = !sc.GridEnabled;
            toolStripButton1.Checked = sc.GridEnabled;
        }

		#region Help Methods

		#region Enable One Button
		private void EnableOne(int button_index)
		{
			foreach (ToolStripButton b in ToolButtons)
			{
				b.Checked = false;
			}

			ToolButtons[button_index].Checked = true;
		}

		private void EnableOne(ToolStripButton button)
		{
			foreach (ToolStripButton b in ToolButtons)
			{
				b.Checked = false;
			}
			button.Checked = true;
		}

		#endregion


        #endregion
	}
}