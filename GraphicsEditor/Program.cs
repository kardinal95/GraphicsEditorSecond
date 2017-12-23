using System;
using ConsoleUI;
using DrawablesUI;
using GraphicsEditor.Commands;
using GraphicsEditor.Commands.Shape;

namespace GraphicsEditor
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var picture = new Picture();
            var ui = new DrawableGUI(picture);
            var app = new Application();

            app.AddCommand(new ExitCommand(app));
            app.AddCommand(new ExplainCommand(app));
            app.AddCommand(new HelpCommand(app));

            // Shapes
            app.AddCommand(new PointCommand(picture));

            // Other
            app.AddCommand(new ListCommand(picture));
            app.AddCommand(new RemoveCommand(picture));
            app.AddCommand(new GroupCommand(picture));
            app.AddCommand(new UngroupCommand(picture));

            picture.Changed += ui.Refresh;
            ui.Start();
            app.Run(Console.In);
            ui.Stop();
        }
    }
}