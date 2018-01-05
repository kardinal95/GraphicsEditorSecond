using System;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    class PointShape : IShape
    {
        public CompoundShape Parent { get; set; }
        public CompoundIndex FullIndex => GetFullIndex();

        private PointF Coordinates { get; }

        public PointShape(float x, float y, CompoundShape parent)
        {
            Coordinates = new PointF(x, y);
            Parent = parent;
        }

        public void Draw(IDrawer drawer)
        {
            drawer.SelectPen(Color.Black);
            drawer.DrawPoint(Coordinates);
        }

        public void Transform(Transformation trans)
        {
            throw new NotImplementedException();
        }

        public IShape GetShapeAt(CompoundIndex index)
        {
            if (index.Size != 0)
            {
                throw new InvalidOperationException(
                    "Non-compound shape do not contain sub elements!");
            }

            return this;
        }

        public override string ToString()
        {
            return $"[{FullIndex}] Точка({Coordinates.X}, {Coordinates.Y})\n";
        }

        private CompoundIndex GetFullIndex()
        {
            if (Parent == null)
            {
                throw new Exception("Critical: shapes not in accessible field!");
            }

            return Parent.FullIndex.JoinRight(Parent.GetPos(this));
        }
    }
}