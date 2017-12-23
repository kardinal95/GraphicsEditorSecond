using System;
using ConsoleUI;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands
{
    class UngroupCommand : ICommand
    {
        private readonly Picture picture;

        public string Name => "ungroup";
        public string Help => "Разгруппирует фигуру";

        public string Description => "Разгруппирует фигуру с указанным индексом\n" +
                                     "Использование: \'ungroup x\', где x - индекс фигуры в команде list";

        public string[] Synonyms => new string[] { };

        public UngroupCommand(Picture picture)
        {
            this.picture = picture;
        }

        public void Execute(params string[] parameters)
        {
            if (parameters.Length != 1)
            {
                Console.WriteLine("Ожидается один аргумент!");
                return;
            }
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
            var shape = picture.GetShapeAt(compoundIndexes[0]);
            if (shape.GetType() == typeof(CompoundShape))
            {
                var compoundShape = (CompoundShape) shape;
                var sub = compoundShape.Shapes;
                foreach (var subShape in sub)
                {
                    picture.Add(subShape);
                }
                picture.RemoveAt(compoundIndexes[0]);
            }
            else
            {
                var compoundString = string.Join(":", compoundIndexes[0]);
                Console.WriteLine($"Фигура {compoundString} не является составной!");
            }
        }
    }
}