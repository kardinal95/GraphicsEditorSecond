using System.Collections.Generic;
using System.Drawing;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Shape
{
    /// <inheritdoc />
    /// <summary>
    ///     Команда для создания линии
    /// </summary>
    public class LineCommand : BaseShapeCommand
    {
        protected override int ArgsCount => 4;

        public override string Name => "line";
        public override string Help => "Нарисовать линию";

        public override string Description =>
            "Рисует линию с указанными параметрами\n" +
            "Параметры: координаты начала и конца отрезка";

        public override string[] Synonyms => new[] {"ln"};

        protected override IShape CreateShape(List<float> parsed, CompoundShape core)
        {
            var pointStart = new PointF(parsed[0], parsed[1]);
            var pointEnd = new PointF(parsed[2], parsed[3]);
            return new LineShape(pointStart, pointEnd, core);
        }

        public LineCommand(CompoundShape root) : base(root) { }
    }
}