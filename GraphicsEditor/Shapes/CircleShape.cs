using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    class CircleShape : BasicShape
    {
        private PointF center;
        private float radius;

        public CircleShape(PointF center, float radius, CompoundShape parent) : base(parent)
        {
            this.center = center;
            this.radius = radius;
        }

        public override void Draw(IDrawer drawer)
        {
            drawer.SelectPen(Color.Black);
            var size = new SizeF(2 * radius, 2 * radius);
            drawer.DrawEllipseArc(center, size);
        }

        public override void Transform(Transformation trans)
        {
            center = trans[center];
            radius *= trans.Decomposition.Scale[0];
        }

        public override string ToString()
        {
            return $"Круг(Центр({center.X}, {center.Y}), Радиус = {radius})";
        }
    }
}