using System.Collections.Generic;
using System.Drawing;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Shape
{
    /// <inheritdoc />
    /// <summary>
    ///     Команда для создания эллипса
    /// </summary>
    public class EllipseCommand : BaseShapeCommand
    {
        protected override int ArgsCount => 5;

        public override string Name => "ellipse";
        public override string Help => "Нарисовать эллипс";

        public override string Description =>
            "Рисует эллипс с указанными параметрами\n" +
            "Параметры: координаты центра, размеры осей, угол поворота";

        public override string[] Synonyms => new[] {"elp"};

        protected override IShape CreateShape(List<float> parsed, CompoundShape core)
        {
            var center = new PointF(parsed[0], parsed[1]);
            var size = new SizeF(parsed[2], parsed[3]);
            return new EllipseShape(center, size, parsed[4], core);
        }

        public EllipseCommand(CompoundShape root) : base(root) { }
    }
}