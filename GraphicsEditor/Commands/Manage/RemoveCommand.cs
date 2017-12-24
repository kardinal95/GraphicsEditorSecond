using System;
using System.Collections.Generic;

namespace GraphicsEditor.Commands.Manage
{
    class RemoveCommand : BaseManageCommand
    {
        private readonly Picture picture;

        public override string Name => "remove";
        public override string Help => "Удалить фигуры с картинки";

        public override string Description => "Удаляет фигуры с указанными индексами\n" +
                                              "Использование: \'remove x y ..\', где x, y, .. - индексы фигур в команде list";

        public override string[] Synonyms => new[] {"rm"};
        protected override int Argsnum => 0;

        protected override void MakeChanges(List<CompoundIndex> indexes)
        {
            foreach (var index in indexes)
            {
                try
                {
                    picture.RemoveAt(index);
                    Console.WriteLine($"Удалена фигура с индексом {index}");
                }
                catch
                {
                    Console.WriteLine("Не найдена фигура с индексом " + $"{index}!");
                }
            }
        }

        public RemoveCommand(Picture picture) : base(picture)
        {
            this.picture = picture;
        }
    }
}