using System;
using System.Collections.Generic;
using GraphicsEditor.Commands.Manage;
using GraphicsEditor.Shapes;

namespace GraphicsEditorTests.Commands.Manage
{
    class DummyManageCommand : BaseManageCommand
    {
        public DummyManageCommand(CompoundShape root) : base(root) { }
        public override string Name => "dummy";
        public override string Help => "";
        public override string Description => "";
        public override string[] Synonyms => new string [] {};
        protected override int[] ArgRange => new[] {1, 1};

        protected override void MakeChanges(List<IShape> shapes)
        {
            throw new NotImplementedException();
        }
    }
}