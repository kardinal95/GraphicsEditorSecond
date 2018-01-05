using System;
using System.Collections.Generic;
using ConsoleUI;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Shape
{
    abstract class BaseShapeCommand : ICommand
    {
        public abstract string Name { get; }
        public abstract string Help { get; }
        public abstract string Description { get; }
        public abstract string[] Synonyms { get; }

        protected abstract int Argsnum { get; } // Количество аргументов для команды
        private readonly CompoundShape core;

        protected BaseShapeCommand(CompoundShape core)
        {
            this.core = core;
        }

        protected abstract IShape CreateShape(List<float> parsed, CompoundShape core);

        public void Execute(params string[] parameters)
        {
            if (parameters.Length != Argsnum)
            {
                Console.WriteLine($"Ошибка - ожидается {Argsnum} аргументов," +
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
            core.Add(CreateShape(parsed, core));
        }
    }
}