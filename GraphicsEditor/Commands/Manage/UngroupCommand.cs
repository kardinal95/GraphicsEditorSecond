using System;
using System.Collections.Generic;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage
{
    class UngroupCommand : BaseManageCommand
    {
        public override string Name => "ungroup";
        public override string Help => "Разгруппирует фигуру";

        public override string Description =>
            "Разгруппирует фигуру с указанным индексом\n" +
            "Использование: \'ungroup x\', где x - индекс фигуры в команде list";

        public override string[] Synonyms => new string[] { };
        protected override int MinArg => 1;
        protected override int MaxArg => 1;

        public UngroupCommand(CompoundShape core) : base(core) { }

        protected override void MakeChanges(List<IShape> shapes)
        {
            if (shapes[0].GetType() != typeof(CompoundShape))
            {
                Console.WriteLine("Cannot ungroup base figure");
                return;
            }

            var target = (CompoundShape) shapes[0];
            target.Ungroup();
        }
    }
}