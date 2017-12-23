using System;
using System.Linq;
using ConsoleUI;

namespace GraphicsEditor.Commands
{
    class RemoveCommand : ICommand
    {
        private readonly Picture picture;

        public string Name => "remove";
        public string Help => "Удалить фигуры с картинки";

        public string Description => "Удаляет фигуры с указанными индексами\n" +
                                     "Использование: \'remove x y ..\', где x, y, .. - индексы фигур в команде list";

        public string[] Synonyms => new[] {"rm"};

        public RemoveCommand(Picture picture)
        {
            this.picture = picture;
        }

        public void Execute(params string[] parameters)
        {
            var parsed = CommandLib.ParseIndexes(parameters, out var errors);
            if (errors.Count != 0)
            {
                Console.WriteLine($"Обнаружены ошибки ввода: {string.Join(", ", errors)}");
                return;
            }
            var compoundIndexes =
                CommandLib.CheckIndexesExistence(parsed, out var missing, picture);
            if (missing.Count != 0)
            {
                Console.WriteLine($"Не найдены элементы с индексами: {string.Join(" ", missing)}");
            }
            if (compoundIndexes.Count == 0)
            {
                return;
            }
            foreach (var compoundIndex in compoundIndexes)
            {
                var compoundString = string.Join(":", compoundIndex);
                try
                {
                    picture.RemoveAt(compoundIndex);
                    Console.WriteLine($"Удалена фигура с индексом {compoundString}");
                }
                catch
                {
                    Console.WriteLine("Не найдена фигура с индексом " +
                                      $"{compoundString}!");
                }
            }
        }
    }
}