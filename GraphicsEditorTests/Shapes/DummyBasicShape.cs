using System;
using DrawablesUI;
using GraphicsEditor;
using GraphicsEditor.Shapes;

namespace GraphicsEditorTests.Shapes
{
    public class DummyBasicShape : BasicShape
    {
        public DummyBasicShape(CompoundShape parent) : base(parent) { }

        public override void Draw(IDrawer drawer)
        {
            throw new NotImplementedException();
        }

        public override void Transform(Transformation trans)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "DummyShape";
        }
    }
}