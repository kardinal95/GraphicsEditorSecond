using System.Drawing;
using GraphicsEditor.Commands.Shape;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Commands.Shape
{
    [TestFixture]
    class LineCommandTest
    {
        private CompoundShape root;
        private LineCommand command;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            command = new LineCommand(root);
        }

        [Test]
        public void LineCommand_ShouldExecuteCorrect()
        {
            command.Execute("1", "1", "3", "3");
            Assert.That(root.Shapes.Count, Is.EqualTo(1));
            Assert.That(root.Shapes[0], Is.InstanceOf<LineShape>());
            var line = (LineShape) root.Shapes[0];
            Assert.That(line.Bounds, Is.EqualTo(new[] {new PointF(1, 1), new PointF(3, 3)}));
        }
    }
}