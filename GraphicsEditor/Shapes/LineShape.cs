using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    class LineShape : BasicShape
    {
        private readonly PointF[] bounds = new PointF[2];

        public LineShape(PointF start, PointF end, CompoundShape parent) : base(parent)
        {
            bounds[0] = start;
            bounds[1] = end;
        }

        public override void Draw(IDrawer drawer)
        {
            drawer.SelectPen(Color.Black);
            drawer.DrawLine(bounds[0], bounds[1]);
        }

        public override void Transform(Transformation trans)
        {
            bounds[0] = trans[bounds[0]];
            bounds[1] = trans[bounds[1]];
        }

        public override string ToString()
        {
            return $"Линия(Точка({bounds[0].X}, {bounds[0].Y}), " +
                   $"Точка({bounds[1].X}, {bounds[1].Y}))";
        }
    }
}