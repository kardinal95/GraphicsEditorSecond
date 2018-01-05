using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    public interface IShape : IDrawable
    {
        CompoundShape Parent { get; set; }

        CompoundIndex FullIndex { get; }

        void Transform(Transformation trans);

        IShape GetShapeAt(CompoundIndex index);
    }
}