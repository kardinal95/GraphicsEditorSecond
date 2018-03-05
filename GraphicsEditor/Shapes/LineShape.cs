using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    class LineShape : BasicShape
    {
        public readonly PointF[] Bounds = new PointF[2];

        public LineShape(PointF start, PointF end, CompoundShape parent) : base(parent)
        {
            Bounds[0] = start;
            Bounds[1] = end;
        }

        public override void Draw(IDrawer drawer)
        {
            drawer.SelectPen(Color.Black);
            drawer.DrawLine(Bounds[0], Bounds[1]);
        }

        public override void Transform(Transformation trans)
        {
            Bounds[0] = trans[Bounds[0]];
            Bounds[1] = trans[Bounds[1]];
        }

        public override string ToString()
        {
            return $"Линия(Точка({Bounds[0].X}, {Bounds[0].Y}), " +
                   $"Точка({Bounds[1].X}, {Bounds[1].Y}))";
        }
    }
}