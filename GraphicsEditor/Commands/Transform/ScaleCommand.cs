using System;
using System.Collections.Generic;
using System.Drawing;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Transform
{
    /// <inheritdoc />
    /// <summary>
    ///     Команда для изменения размера фигуры на указанный коэффициент
    /// </summary>
    class ScaleCommand : BaseTransformCommand
    {
        public override string Name => "scale";
        public override string Help => "Изменить размер фигуры";

        public override string Description =>
            "Изменяет размер фигуры на указанный коэффициент\n" +
            "Параметры: координаты точки скалирования, коэфф. скалирования, индекс фигуры";

        public override string[] Synonyms => new string[] { };
        protected override int ArgumentsCount => 3;

        public ScaleCommand(CompoundShape root) : base(root) { }

        protected override void Process(List<float> arguments, IShape shape)
        {
            var first = Transformation.Translate(new PointF(-arguments[0], -arguments[1]));
            var second = Transformation.Scale(arguments[2], arguments[2]);
            var third = Transformation.Translate(new PointF(arguments[0], arguments[1]));
            var trans = first * second * third;
            try
            {
                shape.Transform(trans);
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            Root.Refresh();
        }
    }
}