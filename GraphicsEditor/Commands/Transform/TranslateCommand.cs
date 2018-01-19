using System.Collections.Generic;
using System.Drawing;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Transform
{
    /// <inheritdoc />
    /// <summary>
    ///     Команда для переноса фигуры на указанный вектор
    /// </summary>
    class TranslateCommand : BaseTransformCommand
    {
        public override string Name => "translate";
        public override string Help => "Перенести фигуру на указанный вектор";

        public override string Description =>
            "Переносит фигуру на указанный вектор\n" +
            "Параметры: координаты вершины вектора, индекс фигуры";

        public override string[] Synonyms => new string[] { };
        protected override int ArgumentsCount => 2;

        public TranslateCommand(CompoundShape root) : base(root) { }

        protected override void Process(List<float> arguments, IShape shape)
        {
            var trans = Transformation.Translate(new PointF(arguments[0], arguments[1]));
            shape.Transform(trans);
            Root.Refresh();
        }
    }
}