using System;
using System.Collections.Generic;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    class PointShape : IShape
    {
        private PointF Coordinates { get; }

        public PointShape(float x, float y)
        {
            Coordinates = new PointF(x, y);
        }

        public void Draw(IDrawer drawer)
        {
            drawer.SelectPen(Color.Black, 1);
            drawer.DrawPoint(Coordinates);
        }

        public void Transform(Transformation trans)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(List<int> idInts)
        {
            throw new InvalidOperationException("Non-compound shape do not contain sub elements!");
        }

        public IShape GetShapeAt(List<int> idInts)
        {
            throw new InvalidOperationException("Non-compound shape do not contain sub elements!");
        }

        public string GetStringRepresentation(string compoundIndex)
        {
            return $"[{compoundIndex}] Точка({Coordinates.X}, {Coordinates.Y})";
        }
    }
}