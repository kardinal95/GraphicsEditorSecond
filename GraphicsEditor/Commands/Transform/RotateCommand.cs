using System.Collections.Generic;
using System.Drawing;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Transform
{
    /// <inheritdoc />
    /// <summary>
    ///     Команда для поворота фигуры вокруг указанной точки
    /// </summary>
    class RotateCommand : BaseTransformCommand
    {
        public override string Name => "rotate";
        public override string Help => "Повернуть фигуру вокруг указанной точки";

        public override string Description =>
            "Поворачивает фигуру вокруг указанной точки\n" +
            "Параметры: координаты точки, угол, индекс фигуры";

        public override string[] Synonyms => new string[] { };
        protected override int ArgumentsCount => 3;

        public RotateCommand(CompoundShape root) : base(root) { }

        protected override void Process(List<float> arguments, IShape shape)
        {
            var first = Transformation.Translate(new PointF(-arguments[0], -arguments[1]));
            var second = Transformation.Rotate(arguments[2]);
            var third = Transformation.Translate(new PointF(arguments[0], arguments[1]));
            var trans = first * second * third;
            shape.Transform(trans);
            Root.Refresh();
        }
    }
}