using System;
using System.Collections.Generic;
using ConsoleUI;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage
{
    abstract class BaseManageCommand : ICommand
    {
        protected BaseManageCommand(CompoundShape core)
        {
            Core = core;
        }

        public abstract string Name { get; }
        public abstract string Help { get; }
        public abstract string Description { get; }
        public abstract string[] Synonyms { get; }
        protected abstract int MinArg { get; } // Количество аргументов для команды
        protected abstract int MaxArg { get; } // Для снятия ограничения назначить -1
        protected readonly CompoundShape Core;

        public void Execute(params string[] parameters)
        {
            if (MaxArg > 0 && MaxArg < parameters.Length || MinArg > 0 && MinArg > parameters.Length)
            {
                Console.WriteLine($"Ошибка - было получено {parameters.Length} аргументов!");
                if (MinArg > 0) Console.WriteLine($"Минимум аргументов - {MinArg}.");
                if (MaxArg > 0) Console.WriteLine($"Максимум аргументов - {MinArg}.");
                Console.WriteLine($"Для получения справки введите \'explain {Name}\'");
                return;
            }

            var parsed = CommandLib.ParseIndexes(parameters, out var errors);
            if (errors.Count != 0)
            {
                Console.WriteLine($"Обнаружены ошибки ввода: {string.Join(", ", errors)}");
                return;
            }

            var shapes = CommandLib.GetExisting(parsed, out var missing, Core);
            if (missing.Count != 0)
            {
                Console.WriteLine($"Не найдены элементы с индексами: {string.Join(", ", missing)}");
            }

            if (shapes.Count == 0)
            {
                return;
            }

            MakeChanges(shapes);
        }

        protected abstract void MakeChanges(List<IShape> indexes);
    }
}