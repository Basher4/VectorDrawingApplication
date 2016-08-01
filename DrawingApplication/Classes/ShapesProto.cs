using System;
using System.Collections.Generic;
using System.Drawing;

namespace DrawingApplication
{
	/*
    public abstract class ShapeProto
    {
        public const float PenDefaultWidth = 5f;
        protected uint ShapeIndex {get; private set;}
        public ShapeInfo CurrentShape;
        protected ShapeProto(Point[] verts, Pen linePen, Brush fillBrush, string name)
        {
            CurrentShape = new ShapeInfo{
                Verts = verts,
                LinePen = linePen,
                FillBrush = fillBrush,
                Name = name + ShapeIndex,
                ID = ShapeIndex++
            };
        }

        protected ShapeProto(ShapeInfo shape){
            CurrentShape = shape;
        }

        public abstract void DrawShape(Graphics target);
    }

    public sealed class LineProto : ShapeProto
    {
        public LineProto(Point start, Point end, Pen LinePen, string name = "Line ")
            : base(new Point[] { start, end }, LinePen, null, name) { }

        public LineProto(ShapeInfo shape) : base(shape) { }

        public static void DrawLine(Graphics target, Pen p, Point p1, Point p2)
        {
            target.DrawLine(p, p1, p2);
        }

        public override void DrawShape(Graphics target)
        {
            target.DrawLine(CurrentShape.LinePen, CurrentShape.Verts[0], CurrentShape.Verts[1]);
        }
    }

    public sealed class RectangleProto : ShapeProto
    {
        public RectangleProto(Point start, Point end, Pen LinePen, Brush FillBrush, string name = "Rectangle ")
            : base( new Point[] { start, end }, LinePen, FillBrush, name) { }
        public RectangleProto(ShapeInfo shape) : base(shape) { }

        public static void DrawRectangle(Graphics target, Pen p, Brush fill_brush, Point p1, Point p2)
        {
            Point UL = (p1.Y < p2.Y) ? ((p1.X < p2.X) ? p1 : new Point(p2.X, p1.Y)) : ((p1.X < p2.X) ? new Point(p1.X, p2.Y) : p2);
            Point LR = (p1.Y > p2.Y) ? ((p1.X > p2.X) ? p1 : new Point(p2.X, p1.Y)) : ((p1.X > p2.X) ? new Point(p1.X, p2.Y) : p2);
            Size size = new Size(Math.Abs(LR.X - UL.X), Math.Abs(UL.Y - LR.Y));

            var r = new System.Drawing.Rectangle(UL, size);
            target.FillRectangle(fill_brush, r);
            target.DrawRectangle(p, r);
        }

        public override void DrawShape(Graphics target)
        {
            Point UL =  (CurrentShape.Verts[0].Y < CurrentShape.Verts[1].Y) ?
                            ((CurrentShape.Verts[0].X < CurrentShape.Verts[1].X) ?
                                CurrentShape.Verts[0] : new Point(CurrentShape.Verts[1].X, CurrentShape.Verts[0].Y)
                            ) : ((CurrentShape.Verts[0].X < CurrentShape.Verts[1].X) ?
                                    new Point(CurrentShape.Verts[0].X, CurrentShape.Verts[1].Y) : CurrentShape.Verts[1]
                                );

            Point LR =  (CurrentShape.Verts[0].Y > CurrentShape.Verts[1].Y) ?
                            ((CurrentShape.Verts[0].X > CurrentShape.Verts[1].X) ?
                                CurrentShape.Verts[0] : new Point(CurrentShape.Verts[1].X, CurrentShape.Verts[0].Y)
                            ) : ((CurrentShape.Verts[0].X > CurrentShape.Verts[1].X) ?
                                    new Point(CurrentShape.Verts[0].X, CurrentShape.Verts[1].Y) : CurrentShape.Verts[1]
                                );

            Size size = new Size(Math.Abs(LR.X - UL.X), Math.Abs(UL.Y - LR.Y));

            var r = new System.Drawing.Rectangle(UL, size);
            target.FillRectangle(CurrentShape.FillBrush, r);
            target.DrawRectangle(CurrentShape.LinePen, r);
        }
    }

    public sealed class EllipseProto : ShapeProto
    {
        public EllipseProto(ShapeInfo shape) : base(shape) { }
        public EllipseProto(Point start, Point end, Pen LinePen, Brush FillBrush, string name = "Ellipse ")
            : base(new Point[] { start, end }, LinePen, FillBrush, name) { }

        public static void DrawEllipse(Graphics target, Pen p, Brush fill_brush, Point p1, Point p2)
        {
            Point UL = (p1.Y < p2.Y) ? ((p1.X < p2.X) ? p1 : new Point(p2.X, p1.Y)) : ((p1.X < p2.X) ? new Point(p1.X, p2.Y) : p2);
            Point LR = (p1.Y > p2.Y) ? ((p1.X > p2.X) ? p1 : new Point(p2.X, p1.Y)) : ((p1.X > p2.X) ? new Point(p1.X, p2.Y) : p2);
            Size size = new Size(Math.Abs(LR.X - UL.X), Math.Abs(UL.Y - LR.Y));

            var r = new System.Drawing.Rectangle(UL, size);
            target.FillEllipse(fill_brush, r);
            target.DrawEllipse(p, r);
        }
        public override void DrawShape(Graphics target)
        {
            Point UL = (CurrentShape.Verts[0].Y < CurrentShape.Verts[1].Y) ?
                            ((CurrentShape.Verts[0].X < CurrentShape.Verts[1].X) ?
                                CurrentShape.Verts[0] : new Point(CurrentShape.Verts[1].X, CurrentShape.Verts[0].Y)
                            ) : ((CurrentShape.Verts[0].X < CurrentShape.Verts[1].X) ?
                                    new Point(CurrentShape.Verts[0].X, CurrentShape.Verts[1].Y) : CurrentShape.Verts[1]
                                );

            Point LR = (CurrentShape.Verts[0].Y > CurrentShape.Verts[1].Y) ?
                            ((CurrentShape.Verts[0].X > CurrentShape.Verts[1].X) ?
                                CurrentShape.Verts[0] : new Point(CurrentShape.Verts[1].X, CurrentShape.Verts[0].Y)
                            ) : ((CurrentShape.Verts[0].X > CurrentShape.Verts[1].X) ?
                                    new Point(CurrentShape.Verts[0].X, CurrentShape.Verts[1].Y) : CurrentShape.Verts[1]
                                );

            Size size = new Size(Math.Abs(LR.X - UL.X), Math.Abs(UL.Y - LR.Y));

            var r = new System.Drawing.Rectangle(UL, size);
            target.FillEllipse(CurrentShape.FillBrush, r);
            target.DrawEllipse(CurrentShape.LinePen, r);
        }
    }
	*/
}
