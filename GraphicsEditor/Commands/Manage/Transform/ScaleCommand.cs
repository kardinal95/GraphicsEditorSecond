﻿using System.Collections.Generic;
using System.Drawing;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage.Transform
{
    class ScaleCommand : BaseTransformCommand
    {
        public override string Name => "scale";
        public override string Help => "";
        public override string Description => "";
        public override string[] Synonyms => new string[] { };
        protected override int ArgumentsCount => 3;

        public ScaleCommand(CompoundShape root) : base(root) { }

        protected override void Process(List<float> arguments, IShape shape)
        {
            var first = Transformation.Translate(new PointF(-arguments[0], -arguments[1]));
            var second = Transformation.Scale(arguments[2], arguments[2]);
            var third = Transformation.Translate(new PointF(arguments[0], arguments[1]));
            var trans = first * second * third;
            shape.Transform(trans);
            Root.Refresh();
        }
    }
}