using System.Collections.Generic;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    public interface IShape : IDrawable
    {
        void Transform(Transformation trans);

        void RemoveAt(List<int> idInts);

        IShape GetShapeAt(List<int> idInts);

        string GetStringRepresentation(string compoundIndex);
    }
}