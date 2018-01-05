using System;
using ConsoleUI;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage
{
    class ListCommand : ICommand
    {
        private readonly CompoundShape core;

        public string Name => "list";
        public string Help => "Вывести список фигур на картинке";
        public string Description => "";
        public string[] Synonyms => new string[] { };

        public ListCommand(CompoundShape core)
        {
            this.core = core;
        }

        public void Execute(params string[] parameters)
        {
            Console.Write(core);
        }
    }
}