using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawingApplication
{
    public partial class ScreenControl
    {
        //Select shape - implement to accept more and group
        Shape selected = null;
        int selected_vert = -1;
        bool move_object, move_vertex; Point orig_pos;

        //Drawing vars
        Point ObjStart, ObjEnd;
        List<Point> PolyPoints;
        List<Point> PolyPointsTemp;
        bool Clicked = false;
        bool PolygonBegan = false;
        bool PolygonAddPointAvail = true;

        private void onMD(object sender, MouseEventArgs e)
        {
            if (Renderer == null)
            {
                Renderer = Context.Allocate(this.pictureBox1.CreateGraphics(), this.ClientRectangle);
                Renderer.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                Renderer.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Renderer.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Renderer.Graphics.Clear(Color.White);
                Renderer.Render();
            }

            //Layer check
            if (PropertyManager.SelectedLayer == null)
            {
                MessageBox.Show("Please select layer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (!PropertyManager.SelectedLayer.Visible)
            {
                MessageBox.Show("Cannot draw on invisible layer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //TODO: Round to grid
            if (GridEnabled)
                ObjStart = RoundToGrid(e.Location);
            else
                ObjStart = e.Location;

            ToolSelected = Program.activeTool;

            Clicked = true;

            if (ToolSelected != ToolsAvailable.Select && ToolSelected != ToolsAvailable.Move)
                selected = null;

            if (e.Button == MouseButtons.Left)
            {
                switch (ToolSelected)
                {
                    case ToolsAvailable.Polygon:
                        if (!PolygonBegan)
                        {
                            PolygonBegan = true;
                            PolyPoints.Clear();
                            PolyPointsTemp.Clear();
                            PolyPointsTemp.Add(new Point());
                        }

                        if (PolygonAddPointAvail)
                        {
                            PolyPoints.Add(e.Location);
                            PolyPointsTemp[PolyPoints.Count - 1] = e.Location;
                            PolyPointsTemp.Add(new Point());

                            PolygonAddPointAvail = false;
                        }
                        break;

                    case ToolsAvailable.FreeHand:
                        PolyPoints.Clear();
                        break;
                }
            }

            switch (ToolSelected)
            {
                case ToolsAvailable.Select:
                    if (e.Button == MouseButtons.Left)
                    {
                        selected = PropertyManager.SelectedLayer.SelectFrontByPosition(Renderer, e.Location);

                        if(selected != null)
                            Program.PropertyDockInstance.ObjectPropsForm.UpdateShapeInfo(selected);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        selected = PropertyManager.SelectedLayer.SelectFrontByPosition(Renderer, e.Location);

                        if(selected != null)
                            conMenu.Show(this, e.Location);
                    }
                    break;

                case ToolsAvailable.Move:
                    if (e.Button == MouseButtons.Left)
                    {
                        selected = PropertyManager.SelectedLayer.SelectFrontByPosition(Renderer, e.Location);
                        move_object = true;
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        if (selected == null)
                        {
                            MessageBox.Show("Please, select shape first");
                            break;
                        }

                        selected_vert = selected.GetClosestVertIndex(e.Location);
                        move_vertex = true;
                    }
                    break;
            }
        }

        private void onMU(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2 && ToolSelected == ToolsAvailable.Polygon)
                return;

            ObjEnd = e.Location;
            Clicked = false;

            if(PolygonBegan)
            {
                PolygonAddPointAvail = true;
                Clicked = true;
                return;
            }

            if (ObjEnd != ObjStart)
            {
                switch (ToolSelected)
                {
                    case ToolsAvailable.Line:
                        PropertyManager.SelectedLayer.AddShape(new Line(ObjStart, ObjEnd, Preferences.LinePen));
                        break;
                    case ToolsAvailable.Rectangle:
                        PropertyManager.SelectedLayer.AddShape(new Rectangle(ObjStart, ObjEnd, Preferences.LinePen, Preferences.FillBrush));
                        break;
                    case ToolsAvailable.Ellipse:
                        PropertyManager.SelectedLayer.AddShape(new Ellipse(ObjStart, ObjEnd, Preferences.LinePen, Preferences.FillBrush));
                        break;
                    case ToolsAvailable.FreeHand:
                        for (int i = 1; i < PolyPoints.Count; i++ )
                        {
                            if(Shape.Get2DDistance(PolyPoints[i], PolyPoints[i-1]) < Preferences.FreeHandSmoothingDistance)
                            {
                                PolyPoints.RemoveAt(i);
                                i = 1;
                            }
                        }
                        PropertyManager.SelectedLayer.AddShape(new FreeHand(PolyPoints.ToArray(), Preferences.LinePen, Preferences.FillBrush));
                        break;
                }

                ForceScreenRender();
            }

            switch(ToolSelected)
            {
                case ToolsAvailable.Select:
                    try
                    {
                        if (move_vertex)
                        {
                            selected.UpdateBounds();
                            selected_vert = -1;
                            move_vertex = false;
                        }
                    }
                    catch { }
                    break;

                case ToolsAvailable.Move:
                    if (selected != null)
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            if (move_object)
                                move_object = false;
                            selected.DrawBounds(Renderer.Graphics);
                            Render();
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            if (move_vertex)
                            {
                                move_vertex = false;
                                selected_vert = -1;
                            }
                            selected.DrawBounds(Renderer.Graphics);
                            Render();
                        }
                    }
                    break;

                case ToolsAvailable.Text:
                    if(PropertyManager.DrawText)
                    {
                        PropertyManager.DrawText = false;
                        var si = new ShapeInfo
                        {
                            Verts = new Point[] {e.Location},
                            FillBrush = null,
                            LinePen = new Pen(PropertyManager.TextColor),
                            Visible = true
                        };
                        PropertyManager.SelectedLayer.AddShape(new TextShape(si, PropertyManager.TextToDraw, PropertyManager.FontToDraw));
                        ForceScreenRender();
                    }
                    break;
            }

            Program.LayersFormInstance.layerManager1.UndoRedoSaveStep();
        }

        private void onMouseMove(object sender, MouseEventArgs e)
        {
            if (Clicked)
            {
                BufferAllImages();
                if (PolygonBegan)
                {
                    if (PolyPoints.Count > 0)
                    {
                        PolyPointsTemp[PolyPointsTemp.Count - 1] = e.Location;
                        Polygon.DrawPolygon(Renderer.Graphics, PolyPointsTemp.ToArray(), Preferences.BoundPen, Preferences.PolygonFilling);
                        if (PolyPoints.Count > 1)
                            Renderer.Graphics.DrawLines(Preferences.LinePen, PolyPoints.ToArray());
                    }
                }
                else
                {
                    switch (ToolSelected)
                    {
                        case ToolsAvailable.Line:
                            Line.DrawLine(Renderer.Graphics, Preferences.LinePen, ObjStart, e.Location);
                            break;
                        case ToolsAvailable.Rectangle:
                            Rectangle.DrawRectangle(Renderer.Graphics, Preferences.LinePen, Preferences.FillBrush, ObjStart, e.Location);
                            break;
                        case ToolsAvailable.Ellipse:
                            Ellipse.DrawEllipse(Renderer.Graphics, Preferences.LinePen, Preferences.FillBrush, ObjStart, e.Location);
                            break;
                        case ToolsAvailable.FreeHand:
                            if (!PolyPoints.Contains(e.Location))
                                PolyPoints.Add(e.Location);
                            FreeHand.DrawFreeHand(Renderer.Graphics, Preferences.LinePen, PolyPoints.ToArray());
                            break;

                        case ToolsAvailable.Select:
                            if (move_vertex)
                            {
                                if (selected_vert != -1)
                                {
                                    selected.CurrentShape.Verts[selected_vert] = e.Location;
                                    RenderAllImages(Renderer);
                                }
                            }
                            break;

                        case ToolsAvailable.Move:
                            if(move_object)
                            {
                                //Get offset
                                Point offset = PointMath.Subtract(e.Location, ObjStart);
                                ObjStart = e.Location;
                                selected.Move(offset);
                                selected.DrawBounds(Renderer.Graphics);
                            } else if(move_vertex)
                            {
                                selected.MoveVertexAbsolute(selected_vert, e.Location);
                                selected.DrawBounds(Renderer.Graphics);
                            }
                            break;
                    }
                }

                Render();
            }

            if(ToolSelected == ToolsAvailable.Text)
            {
                if (PropertyManager.DrawText)
                {
                    BufferAllImages();
                    TextShape.DrawText(Renderer.Graphics, PropertyManager.TextToDraw, e.Location, PropertyManager.FontToDraw, PropertyManager.TextColor);
                    Render();
                }
            }

#if DEBUG
            //Update status bar
            if (Program.MainFormInstance.statusStrip.Visible)
            {
                ToolStripItemCollection items = Program.MainFormInstance.statusStrip.Items;
                if (items.Count > 0)
                    items[0].Text = String.Format("X: {0}, Y: {1}; Clicked: {2} - TOOL: {3}", e.X, e.Y, Clicked, ToolSelected.ToString());
                else
                    items.Add(String.Format("X: {0}, Y: {1}; Clicked: {2} - TOOL: {3}", e.X, e.Y, Clicked, ToolSelected.ToString()));
            }
#elif RELEASE
            //Update status bar
            if (Program.MainFormInstance.statusStrip.Visible)
            {
                ToolStripItemCollection items = Program.MainFormInstance.statusStrip.Items;
                if (items.Count > 0)
                    items.Add(String.Format("X: {0}, Y: {1} - TOOL: {2}", e.X, e.Y, ToolSelected.ToString()));
                else
                    items.Add(String.Format("X: {0}, Y: {1} - TOOL: {2}", e.X, e.Y, ToolSelected.ToString()));
            }
#endif

        }

        private void onMDC(object sender, MouseEventArgs e)
        {
            //Add Polygon
            PolyPoints = new List<Point>(PolyPoints.Distinct());
            PropertyManager.SelectedLayer.AddShape(new Polygon(PolyPoints.ToArray(), Preferences.LinePen, Preferences.FillBrush));

            //Clear shit
            PolygonBegan = false;
            PolygonAddPointAvail = true;
            ForceScreenRender();

            Program.LayersFormInstance.layerManager1.UndoRedoSaveStep();
        }

        #region Context-Menu
        #region Arrangement
        private void MoveUpEH(object sender, EventArgs e)
        {
            MessageBox.Show(PropertyManager.SelectedLayer.Shapes[0].ExportAsXML().ToString());
            if (selected != null)
            {
                int selected_z = PropertyManager.SelectedLayer.Shapes.IndexOf(selected);
                PropertyManager.SelectedLayer.Shapes.Move(selected_z, ListExtensions.MoveDirection.Up);
                AfterContextMenuExecute();
            }
        }

        private void MoveDownEH(object sender, EventArgs e)
        {
            if (selected != null)
            {
                int selected_z = PropertyManager.SelectedLayer.Shapes.IndexOf(selected);
                PropertyManager.SelectedLayer.Shapes.Move(selected_z--, ListExtensions.MoveDirection.Down);
                AfterContextMenuExecute();
            }
        }

        private void BringToFrontEH(object sender, EventArgs e)
        {
            if (selected != null)
            {
                int selected_z = PropertyManager.SelectedLayer.Shapes.IndexOf(selected);
                PropertyManager.SelectedLayer.Shapes.BringToFront(selected_z);
                AfterContextMenuExecute();
            }
        }

        private void BringToBackEH(object sender, EventArgs e)
        {
            if (selected != null)
            {
                int selected_z = PropertyManager.SelectedLayer.Shapes.IndexOf(selected);
                PropertyManager.SelectedLayer.Shapes.SendToBack(selected_z);
                AfterContextMenuExecute();
            }
        }

        #endregion

        private void AfterContextMenuExecute()
        {
            this.ForceScreenRender();
            Program.ObjectTreeViewInstance.UpdateNodes(Program.LayersFormInstance.layerManager1.GetLayersCollection().ToArray());
        }

        private void DeleteShape(object sender, EventArgs e)
        {
            if (selected != null)
            {
                try
                {
                    PropertyManager.SelectedLayer.Shapes.Remove(selected);
                    AfterContextMenuExecute();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CopyShape(object sender, EventArgs e)
        {
            try
            {
                if (selected != null)
                {
                    Preferences.CopyShape = selected;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PasteShape(object sender, EventArgs e)
        {
            try
            {
                if (Preferences.CopyShape != null)
                {
                    if (PropertyManager.SelectedLayer != null)
                    {
                        PropertyManager.SelectedLayer.AddShape(Preferences.CopyShape);
                        AfterContextMenuExecute();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}