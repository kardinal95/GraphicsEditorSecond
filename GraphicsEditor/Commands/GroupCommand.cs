using System;
using System.Linq;
using ConsoleUI;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands
{
    class GroupCommand : ICommand
    {
        private readonly Picture picture;

        public string Name => "group";
        public string Help => "Группирует фигуры";

        public string Description => "Группирует фигуры с указанными индексами\n" +
                                     "Использование: \'group x y ..\', где x, y, .. - индексы фигур в команде list";

        public string[] Synonyms => new string[] { };

        public GroupCommand(Picture picture)
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
            var shapeList = compoundIndexes.
                Select(compoundIndex => picture.GetShapeAt(compoundIndex)).ToList();
            foreach (var compoundIndex in compoundIndexes)
            {
                picture.RemoveAt(compoundIndex);
            }
            picture.Add(new CompoundShape(shapeList));
        }
    }
}