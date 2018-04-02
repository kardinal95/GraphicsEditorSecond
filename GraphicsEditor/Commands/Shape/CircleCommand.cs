using System.Collections.Generic;
using System.Drawing;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Shape
{
    /// <inheritdoc />
    /// <summary>
    ///     Команда для создания круга
    /// </summary>
    public class CircleCommand : BaseShapeCommand
    {
        protected override int ArgsCount => 3;

        public override string Name => "circle";
        public override string Help => "Нарисовать круг";

        public override string Description =>
            "Рисует круг с указанными параметрами\n" + "Параметры: координаты центра, радиус";

        public override string[] Synonyms => new[] {"cl"};

        protected override IShape CreateShape(List<float> parsed, CompoundShape core)
        {
            var center = new PointF(parsed[0], parsed[1]);
            return new CircleShape(center, parsed[2], core);
        }

        public CircleCommand(CompoundShape root) : base(root) { }
    }
}