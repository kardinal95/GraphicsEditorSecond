﻿using System.Collections.Generic;
using System.Drawing;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Shape
{
    class CircleCommand : BaseShapeCommand
    {
        protected override int Argsnum => 3;

        public override string Name => "circle";
        public override string Help => "Нарисовать круг";

        public override string Description => "Рисует круг по координатам x, y с радиусом z.\n" +
                                              "Использование: \'circle x y z\', где x, y, z - числа";

        public override string[] Synonyms => new[] {"cl"};

        protected override IShape CreateShape(List<float> parsed, CompoundShape core)
        {
            var center = new PointF(parsed[0], parsed[1]);
            return new CircleShape(center, parsed[2], core);
        }

        public CircleCommand(CompoundShape core) : base(core) { }
    }
}