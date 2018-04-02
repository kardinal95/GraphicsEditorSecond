using System;
using System.Collections.Generic;
using GraphicsEditor.Commands.Shape;
using GraphicsEditor.Shapes;

namespace GraphicsEditorTests.Commands.Shape
{
    class DummyShapeCommand : BaseShapeCommand
    {
        public DummyShapeCommand(CompoundShape root) : base(root) { }
        public override string Name => "dummy";
        public override string Help => "";
        public override string Description => "";
        public override string[] Synonyms => new string[] {};
        protected override int ArgsCount => 1;

        protected override IShape CreateShape(List<float> parsed, CompoundShape core)
        {
            throw new NotImplementedException();
        }
    }
}