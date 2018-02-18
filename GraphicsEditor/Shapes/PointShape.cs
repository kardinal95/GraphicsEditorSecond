using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    public class PointShape : BasicShape
    {
        private PointF point;
        // Для тестов
        public float CoordX => point.X;
        public float CoordY => point.Y;

        public PointShape(float x, float y, CompoundShape parent) : base(parent)
        {
            point = new PointF(x, y);
        }

        public override void Draw(IDrawer drawer)
        {
            drawer.SelectPen(Color.Black);
            drawer.DrawPoint(point);
        }

        public override void Transform(Transformation trans)
        {
            point = trans[point];
        }

        public override string ToString()
        {
            return $"Точка({point.X}, {point.Y})";
        }
    }
}