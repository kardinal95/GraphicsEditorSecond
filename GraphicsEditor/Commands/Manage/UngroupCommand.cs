using System;
using System.Collections.Generic;
using ConsoleUI;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage
{
    class UngroupCommand : BaseManageCommand
    {
        private readonly Picture picture;

        public override string Name => "ungroup";
        public override string Help => "Разгруппирует фигуру";

        public override string Description => "Разгруппирует фигуру с указанным индексом\n" +
                                     "Использование: \'ungroup x\', где x - индекс фигуры в команде list";

        public override string[] Synonyms => new string[] { };
        protected override int Argsnum => 1;

        public UngroupCommand(Picture picture) : base(picture)
        {
            this.picture = picture;
        }

        protected override void MakeChanges(List<CompoundIndex> indexes)
        {
            var shape = picture.GetShapeAt(indexes[0]);
            if (shape.GetType() == typeof(CompoundShape))
            {
                var compoundShape = (CompoundShape)shape;
                var sub = compoundShape.Shapes;
                foreach (var subShape in sub)
                {
                    picture.Add(subShape);
                }
                picture.RemoveAt(indexes[0]);
            }
            else
            {
                Console.WriteLine($"Фигура {shape} не является составной!");
            }
        }
    }
}