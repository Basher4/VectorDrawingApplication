using System;
using System.Drawing;
using System.Collections.Generic;

namespace DrawingApplication
{
    class PropertyManager
    {
        //Layers Info
        public static Layer SelectedLayer;
        public static int SelectedLayerIndex;

        public static bool ConnectPolyTool = false;

        public static string TextToDraw = String.Empty;
        public static Font FontToDraw;
        public static Color TextColor;
        public static bool DrawText = false;

        public static Dictionary<object, Type> AditionalParameters;
    }
}
