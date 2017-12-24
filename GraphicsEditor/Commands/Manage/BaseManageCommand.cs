using System;
using System.Collections.Generic;
using ConsoleUI;

namespace GraphicsEditor.Commands.Manage
{
    abstract class BaseManageCommand : ICommand
    {
        private readonly Picture picture;

        protected BaseManageCommand(Picture picture)
        {
            this.picture = picture;
        }

        public abstract string Name { get; }
        public abstract string Help { get; }
        public abstract string Description { get; }
        public abstract string[] Synonyms { get; }
        protected abstract int Argsnum { get; } // Количество аргументов для команды

        public void Execute(params string[] parameters)
        {
            if (Argsnum != 0 && Argsnum != parameters.Length)
            {
                Console.WriteLine($"Ошибка - ожидается {Argsnum} аргументов," +
                                  $"было получено {parameters.Length}!");
                Console.WriteLine($"Для получения справки введите \'explain {Name}\'");
                return;
            }
            var parsed = CommandLib.ParseIndexes(parameters, out var errors);
            if (errors.Count != 0)
            {
                Console.WriteLine($"Обнаружены ошибки ввода: {string.Join(", ", errors)}");
                return;
            }
            var compoundIndexes = CommandLib.GetExisting(parsed, out var missing, picture);
            if (missing.Count != 0)
            {
                Console.WriteLine($"Не найдены элементы с индексами: {string.Join(", ", missing)}");
            }
            if (compoundIndexes.Count == 0)
            {
                return;
            }
            MakeChanges(compoundIndexes);
        }

        protected abstract void MakeChanges(List<CompoundIndex> indexes);
    }
}