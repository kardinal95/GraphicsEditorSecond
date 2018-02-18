using System;
using System.Collections.Generic;
using ConsoleUI;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Shape
{
    /// <inheritdoc />
    /// <summary>
    ///     Базовый класс для команд, создающих фигуры (базовые)
    /// </summary>
    public abstract class BaseShapeCommand : ICommand
    {
        /// Поля интерфейса
        /// <see cref="IShape" />
        public abstract string Name { get; }

        public abstract string Help { get; }
        public abstract string Description { get; }
        public abstract string[] Synonyms { get; }

        /// Количество аргументов для команды
        protected abstract int ArgsCount { get; }

        /// Корневая фигура
        private readonly CompoundShape root;

        protected BaseShapeCommand(CompoundShape root)
        {
            this.root = root;
        }

        /// Само создание в наследуемых классах
        protected abstract IShape CreateShape(List<float> parsed, CompoundShape core);

        public void Execute(params string[] parameters)
        {
            if (parameters.Length != ArgsCount)
            {
                Console.WriteLine($"Ошибка - ожидается {ArgsCount} аргументов," +
                                  $"было получено {parameters.Length}!");
                Console.WriteLine($"Для получения справки введите \'explain {Name}\'");
                return;
            }

            var parsed = CommandLib.ParseArguments<float>(parameters, out var errors);
            if (errors.Count != 0)
            {
                Console.WriteLine($"Обнаружены ошибки ввода: {string.Join(", ", errors)}");
                return;
            }

            root.Add(CreateShape(parsed, root));
        }
    }
}