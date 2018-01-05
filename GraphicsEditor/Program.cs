using System;
using ConsoleUI;
using DrawablesUI;
using GraphicsEditor.Commands.Manage;
using GraphicsEditor.Commands.Shape;
using GraphicsEditor.Shapes;

namespace GraphicsEditor
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var core = new CompoundShape();
            var ui = new DrawableGUI(core);
            var app = new Application();

            app.AddCommand(new ExitCommand(app));
            app.AddCommand(new ExplainCommand(app));
            app.AddCommand(new HelpCommand(app));

            // Shapes
            app.AddCommand(new PointCommand(core));

            // Other
            app.AddCommand(new ListCommand(core));
            app.AddCommand(new RemoveCommand(core));
            app.AddCommand(new GroupCommand(core));
            app.AddCommand(new UngroupCommand(core));

            core.Changed += ui.Refresh;
            ui.Start();
            app.Run(Console.In);
            ui.Stop();
        }
    }
}