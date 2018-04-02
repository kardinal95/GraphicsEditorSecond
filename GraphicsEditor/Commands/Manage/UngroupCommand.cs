using System;
using System.Collections.Generic;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage
{
    /// <inheritdoc />
    /// <summary>
    ///     Команда для разгруппировки фигур
    /// </summary>
    public class UngroupCommand : BaseManageCommand
    {
        public override string Name => "ungroup";
        public override string Help => "Разгруппировать фигуру";

        public override string Description =>
            "Разгруппирует фигуру с указанным индексом\n" + "Параметры: индекс составной фигуры";

        public override string[] Synonyms => new string[] { };
        protected override int[] ArgRange => new[] {1, 1};

        public UngroupCommand(CompoundShape root) : base(root) { }

        protected override void MakeChanges(List<IShape> shapes)
        {
            if (shapes[0].GetType() != typeof(CompoundShape))
            {
                Console.WriteLine("Команда выполнима только для составных фигур!");
                return;
            }

            var target = (CompoundShape) shapes[0];
            target.Ungroup();
        }
    }
}