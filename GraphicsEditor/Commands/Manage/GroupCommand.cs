using System;
using System.Collections.Generic;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage
{
    class GroupCommand : BaseManageCommand
    {
        public override string Name => "group";
        public override string Help => "Группирует фигуры";

        public override string Description =>
            "Группирует фигуры с указанными индексами\n" +
            "Использование: \'group x y ..\', где x, y, .. - индексы фигур в команде list";

        public override string[] Synonyms => new string[] { };
        protected override int MinArg => 2;
        protected override int MaxArg => -1;

        public GroupCommand(CompoundShape core) : base(core) { }

        protected override void MakeChanges(List<IShape> shapes)
        {
            var compound = new CompoundShape(shapes, Core);
            foreach (var shape in shapes)
            {
                shape.Parent.Remove(shape);
                shape.Parent = compound;
            }
            Core.Add(compound);
        }
    }
}