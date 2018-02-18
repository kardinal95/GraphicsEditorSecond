using System.Collections.Generic;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Shape
{
    /// <inheritdoc />
    /// <summary>
    ///     Команда для создания линии
    /// </summary>
    public class PointCommand : BaseShapeCommand
    {
        protected override int ArgsCount => 2;

        public override string Name => "point";
        public override string Help => "Нарисовать точку";

        public override string Description =>
            "Рисует точку c указанными параметрами\n" + "Параметры: координаты точки";

        public override string[] Synonyms => new[] {"pt"};

        protected override IShape CreateShape(List<float> parsed, CompoundShape core)
        {
            return new PointShape(parsed[0], parsed[1], core);
        }

        public PointCommand(CompoundShape root) : base(root) { }
    }
}