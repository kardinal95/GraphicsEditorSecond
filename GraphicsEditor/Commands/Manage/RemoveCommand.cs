using System.Collections.Generic;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage
{
    /// <inheritdoc />
    /// <summary>
    ///     Команда для удаления фигур
    /// </summary>
    public class RemoveCommand : BaseManageCommand
    {
        public override string Name => "remove";
        public override string Help => "Удалить фигуры";

        public override string Description =>
            "Удаляет фигуры с указанными индексами\n" + "Параметры: индексы фигур";

        public override string[] Synonyms => new[] {"rm"};
        protected override int[] ArgRange => new[] {1, -1};

        protected override void MakeChanges(List<IShape> shapes)
        {
            foreach (var shape in shapes)
            {
                shape.Parent.Remove(shape);
            }
        }

        public RemoveCommand(CompoundShape root) : base(root) { }
    }
}