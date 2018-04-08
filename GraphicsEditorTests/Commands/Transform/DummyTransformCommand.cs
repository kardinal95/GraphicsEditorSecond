using System;
using System.Collections.Generic;
using GraphicsEditor.Commands.Transform;
using GraphicsEditor.Shapes;

namespace GraphicsEditorTests.Commands.Transform
{
    class DummyTransformCommand : BaseTransformCommand
    {
        public DummyTransformCommand(CompoundShape root) : base(root) { }
        public override string Name => "Dummy";
        public override string Help => "";
        public override string Description => "";
        public override string[] Synonyms => new string[] { };
        protected override int ArgumentsCount => 1;

        protected override void Process(List<float> arguments, IShape shape)
        {
            throw new NotImplementedException();
        }
    }
}