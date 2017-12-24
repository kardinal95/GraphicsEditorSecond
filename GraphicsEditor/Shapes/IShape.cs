using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    public interface IShape : IDrawable
    {
        void Transform(Transformation trans);

        void RemoveAt(CompoundIndex index);

        IShape GetShapeAt(CompoundIndex index);

        string GetStringRepresentation(string compoundIndex);
    }
}