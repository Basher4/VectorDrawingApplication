using System;
using System.Windows.Forms;

namespace DrawingApplication
{
	internal static class Program
	{
		public static MainForm MainFormInstance;
		public static Layers LayersFormInstance;
		public static ObjectTreeView ObjectTreeViewInstance;
		public static PropertyDock PropertyDockInstance;
        public static Document MainDocumentInstance;

		public static ToolsAvailable activeTool;

        public static bool WindowOpened = false;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			MainFormInstance = new MainForm();
			Application.Run(MainFormInstance);
		}
	}
}