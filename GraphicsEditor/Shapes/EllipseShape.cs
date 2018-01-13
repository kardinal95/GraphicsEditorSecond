using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    class EllipseShape : BasicShape
    {
        private PointF center;
        private SizeF size;
        private float degree;

        public EllipseShape(PointF center, SizeF size, float degree, CompoundShape parent) :
            base(parent)
        {
            this.center = center;
            this.size = size;
            this.degree = degree;
        }

        public override void Draw(IDrawer drawer)
        {
            drawer.SelectPen(Color.Black);
            drawer.DrawEllipseArc(center, size, 0, 360, degree);
        }

        public override void Transform(Transformation trans)
        {
            center.X = trans[center].X;
            center.Y = trans[center].Y;
            size.Width *= trans.Decomposition.Scale[0];
            size.Height *= trans.Decomposition.Scale[1];
            degree += trans.Decomposition.FirstAngle + trans.Decomposition.SecondAngle;
        }

        public override string ToString()
        {
            return $"Эллипс(Центр({center.X}, {center.Y}), " +
                   $"Размер = ({size.Height}, {size.Width}), Угол = {degree})";
        }
    }
}