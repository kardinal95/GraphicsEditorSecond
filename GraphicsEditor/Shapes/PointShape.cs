using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    public class PointShape : BasicShape
    {
        public PointF Point { get; private set; }

        public PointShape(float x, float y, CompoundShape parent) : base(parent)
        {
            Point = new PointF(x, y);
        }

        public override void Draw(IDrawer drawer)
        {
            drawer.SelectPen(Color.Black);
            drawer.DrawPoint(Point);
        }

        public override void Transform(Transformation trans)
        {
            Point = trans[Point];
        }

        public override string ToString()
        {
            return $"Точка({Point.X}, {Point.Y})";
        }
    }
}