using System.Collections.Generic;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage
{
    class RemoveCommand : BaseManageCommand
    {
        public override string Name => "remove";
        public override string Help => "Удалить фигуры с картинки";

        public override string Description =>
            "Удаляет фигуры с указанными индексами\n" +
            "Использование: \'remove x y ..\', где x, y, .. - индексы фигур в команде list";

        public override string[] Synonyms => new[] {"rm"};
        protected override int[] ArgRange => new[] {1, -1};

        protected override void MakeChanges(List<IShape> shapes)
        {
            foreach (var shape in shapes)
            {
                shape.Parent.Remove(shape);
            }
        }

        public RemoveCommand(CompoundShape core) : base(core) { }
    }
}