using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleUI;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage.Transform
{
    public abstract class BaseTransformCommand : ICommand
    {
        public abstract string Name { get; }
        public abstract string Help { get; }
        public abstract string Description { get; }
        public abstract string[] Synonyms { get; }

        protected readonly CompoundShape Root;
        protected abstract int ArgumentsCount { get; }

        protected BaseTransformCommand(CompoundShape root)
        {
            Root = root;
        }

        public void Execute(params string[] parameters)
        {
            if (parameters.Length != ArgumentsCount + 1)
            {
                Console.WriteLine("Некорректное количество аргументов!");
                Console.WriteLine($"Ожидалось {ArgumentsCount} аргументов и идентификатор фигуры!");
                return;
            }

            var arguments = parameters.ToList().GetRange(0, ArgumentsCount);
            var parsed = CommandLib.ParseArguments<float>(arguments, out var errors);
            if (!CompoundIndex.TryParse(parameters[parameters.Length-1], out var index))
            {
                errors.Add(parameters[parameters.Length]);
            }

            if (errors.Count != 0)
            {
                Console.WriteLine($"Обнаружены ошибки ввода: {string.Join(", ", errors)}");
                return;
            }

            try
            {
                var shape = Root.GetShapeAt(index);
                Process(parsed, shape);
            }
            catch
            {
                Console.WriteLine($"Не найдена фигура с идентификатором {index}!");
            }
        }

        protected abstract void Process(List<float> arguments, IShape shape);
    }
}