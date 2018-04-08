using System.Drawing;
using GraphicsEditor.Commands.Transform;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Commands.Transform
{
    [TestFixture]
    class RotateCommandTest
    {
        private CompoundShape root;
        private RotateCommand command;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            command = new RotateCommand(root);
        }

        [Test]
        public void RotateCommand_ShouldTransformCorrect()
        {
            var point = new PointShape(1, 1, root);
            var target = new PointF(-1, -1);
            root.Add(point);

            command.Execute("0", "0", "180", "0");
            Assert.That(point.Point, Is.EqualTo(target));
        }
    }
}