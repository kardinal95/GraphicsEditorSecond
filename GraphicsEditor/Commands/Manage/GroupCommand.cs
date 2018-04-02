using System.Collections.Generic;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage
{
    /// <inheritdoc />
    /// <summary>
    ///     Команда для группировки фигур
    /// </summary>
    public class GroupCommand : BaseManageCommand
    {
        public override string Name => "group";
        public override string Help => "Сгруппировать фигуры";

        public override string Description =>
            "Группирует фигуры с указанными индексами\n" + "Параметры: список индексов фигур";

        public override string[] Synonyms => new string[] { };
        protected override int[] ArgRange => new[] {2, -1};

        public GroupCommand(CompoundShape root) : base(root) { }

        protected override void MakeChanges(List<IShape> shapes)
        {
            var compound = new CompoundShape(shapes, Root);
            foreach (var shape in shapes)
            {
                shape.Parent.Remove(shape);
                shape.Parent = compound;
            }

            Root.Add(compound);
        }
    }
}