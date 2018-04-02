using System.Drawing;
using GraphicsEditor.Commands.Shape;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Commands.Shape
{
    [TestFixture]
    class PointCommandTest
    {
        private CompoundShape root;
        private PointCommand command;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            command = new PointCommand(root);
        }

        [Test]
        public void PointCommand_ShouldExecuteCorrect()
        {
            command.Execute("1", "2");
            Assert.That(root.Shapes.Count, Is.EqualTo(1));
            Assert.That(root.Shapes[0], Is.InstanceOf<PointShape>());
            var point = (PointShape) root.Shapes[0];
            Assert.That(point.Point, Is.EqualTo(new PointF(1, 2)));
        }
    }
}