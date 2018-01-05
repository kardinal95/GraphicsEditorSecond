using System.Collections.Generic;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Shape
{
    class PointCommand : BaseShapeCommand
    {
        protected override int Argsnum => 2;

        public override string Name => "point";
        public override string Help => "Нарисовать точку";

        public override string Description => "Рисует точку по координатам x, y.\n" +
                                              "Использование: \'point x y\', где x,y - числа типа float";

        public override string[] Synonyms => new[] { "pt" };

        protected override IShape CreateShape(List<float> parsed, CompoundShape core)
        {
            return new PointShape(parsed[0], parsed[1], core);
        }

        public PointCommand(CompoundShape core) : base(core) { }
    }
}