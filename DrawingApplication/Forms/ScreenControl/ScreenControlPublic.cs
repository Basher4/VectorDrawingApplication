using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DrawingApplication
{
    public partial class ScreenControl
    {
        public Shape GetSelectedShape()
        {
            return this.selected;
        }

        public void ForceSelectShape(Shape shape, Layer layer)
        {
            this.selected = shape;
            this.selected_vert = -1;
            PropertyManager.SelectedLayer = layer;


            BufferAllImages(Renderer);
            this.selected.DrawBounds(Renderer.Graphics);
            Renderer.Render();
        }
    }
}
