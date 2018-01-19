using System;
using ConsoleUI;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands
{
    /// <inheritdoc />
    /// <summary>
    ///     Комманда вывода списка существующих фигур
    /// </summary>
    class ListCommand : ICommand
    {
        private readonly CompoundShape root;

        public string Name => "list";
        public string Help => "Вывести список фигур на картинке";
        public string Description => "Выводит список всех фигур с их индексами";
        public string[] Synonyms => new string[] { };

        public ListCommand(CompoundShape root)
        {
            this.root = root;
        }

        public void Execute(params string[] parameters)
        {
            var fullString = root.ToIndexedString();
            if (fullString != string.Empty)
            {
                Console.WriteLine(root.ToIndexedString());
            }
        }
    }
}