using System;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    class EllipseShape : BasicShape
    {
        public PointF Center { get; private set; }
        public SizeF Size { get; private set; }
        public float Degree { get; private set; }

        public EllipseShape(PointF center, SizeF size, float degree, CompoundShape parent) :
            base(parent)
        {
            Center = center;
            Size = size;
            Degree = degree;
        }

        public override void Draw(IDrawer drawer)
        {
            drawer.SelectPen(Color.Black);
            drawer.DrawEllipseArc(Center, Size, 0, 360, Degree);
        }

        public override void Transform(Transformation trans)
        {
            var scales = trans.Decomposition.Scale;
            if (Math.Abs(scales[0] - scales[1]) > double.Epsilon)
            {
                throw new NotImplementedException();
            }

            Center = trans[Center];
            var sizeNew = new SizeF(Size.Width * scales[0], Size.Height * scales[1]);
            Size = sizeNew;
            Degree += trans.Decomposition.FirstAngle + trans.Decomposition.SecondAngle;
        }

        public override string ToString()
        {
            return $"Эллипс(Центр({Center.X}, {Center.Y}), " +
                   $"Размер = ({Size.Height}, {Size.Width}), Угол = {Degree})";
        }
    }
}