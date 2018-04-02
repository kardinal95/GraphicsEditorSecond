using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    public class CircleShape : BasicShape
    {
        public PointF Center { get; private set; }
        public float Radius { get; private set; }

        public CircleShape(PointF center, float radius, CompoundShape parent) : base(parent)
        {
            Center = center;
            Radius = radius;
        }

        public override void Draw(IDrawer drawer)
        {
            drawer.SelectPen(Color.Black);
            var size = new SizeF(2 * Radius, 2 * Radius);
            drawer.DrawEllipseArc(Center, size);
        }

        public override void Transform(Transformation trans)
        {
            Center = trans[Center];
            Radius *= trans.Decomposition.Scale[0];
        }

        public override string ToString()
        {
            return $"Круг(Центр({Center.X}, {Center.Y}), Радиус = {Radius})";
        }
    }
}