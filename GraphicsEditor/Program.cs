using System;
using ConsoleUI;
using DrawablesUI;
using GraphicsEditor.Commands;
using GraphicsEditor.Commands.Manage;
using GraphicsEditor.Commands.Manage.Transform;
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
            app.AddCommand(new LineCommand(core));
            app.AddCommand(new CircleCommand(core));
            app.AddCommand(new EllipseCommand(core));

            // Transformation
            app.AddCommand(new TranslateCommand(core));
            app.AddCommand(new RotateCommand(core));
            app.AddCommand(new ScaleCommand(core));

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