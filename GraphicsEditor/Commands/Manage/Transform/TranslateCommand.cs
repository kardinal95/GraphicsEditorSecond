using System.Collections.Generic;
using System.Drawing;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage.Transform
{
    class TranslateCommand : BaseTransformCommand
    {
        public override string Name => "translate";
        public override string Help => "";
        public override string Description => "";
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