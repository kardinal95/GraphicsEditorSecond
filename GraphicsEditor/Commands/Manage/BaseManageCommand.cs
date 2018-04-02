using System;
using System.Collections.Generic;
using ConsoleUI;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage
{
    /// <inheritdoc />
    /// <summary>
    ///     Базовый класс для команд, управляющих фигурами
    /// </summary>
    public abstract class BaseManageCommand : ICommand
    {
        protected BaseManageCommand(CompoundShape root)
        {
            Root = root;
        }

        /// Поля интерфейса
        /// <see cref="IShape" />
        public abstract string Name { get; }

        public abstract string Help { get; }
        public abstract string Description { get; }
        public abstract string[] Synonyms { get; }

        /// Количество аргументов (минимум и максимум, -1 - неограниченно)
        protected abstract int[] ArgRange { get; }

        /// Корневая фигура
        protected readonly CompoundShape Root;

        public void Execute(params string[] parameters)
        {
            if (ArgRange[0] > 0 && ArgRange[0] > parameters.Length ||
                ArgRange[1] > 0 && ArgRange[1] < parameters.Length)
            {
                Console.WriteLine($"Ошибка - было получено {parameters.Length} аргументов!");
                if (ArgRange[0] > 0)
                {
                    Console.WriteLine($"Минимум аргументов - {ArgRange[0]}.");
                }

                if (ArgRange[1] > 0)
                {
                    Console.WriteLine($"Максимум аргументов - {ArgRange[1]}.");
                }

                Console.WriteLine($"Для получения справки введите \'explain {Name}\'");
                return;
            }

            var indexes = CommandLib.ParseIndexes(parameters, out var errors);
            if (errors.Count != 0)
            {
                Console.WriteLine($"Обнаружены ошибки ввода: {string.Join(", ", errors)}");
                return;
            }

            var shapes = CommandLib.GetExisting(indexes, out var missing, Root);
            if (missing.Count != 0)
            {
                Console.WriteLine($"Не найдены элементы с индексами: {string.Join(", ", missing)}");
                return;
            }

            if (shapes.Count == 0)
            {
                return;
            }

            MakeChanges(shapes);
        }

        /// Сами преобразования в наследуемых классах
        protected abstract void MakeChanges(List<IShape> shapes);
    }
}