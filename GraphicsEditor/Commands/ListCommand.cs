using System;
using ConsoleUI;

namespace GraphicsEditor.Commands
{
    class ListCommand : ICommand
    {
        private readonly Picture picture;

        public string Name => "list";
        public string Help => "Вывести список фигур на картинке";
        public string Description => "";
        public string[] Synonyms => new string[] { };

        public ListCommand(Picture picture)
        {
            this.picture = picture;
        }

        public void Execute(params string[] parameters)
        {
            var data = picture.GetStringRepresentation(null);
            if (data.Length != 0)
            {
                data += '\n';
            }
            Console.Write(data);
        }
    }
}