using System.Drawing;
using GraphicsEditor.Commands.Shape;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Commands.Shape
{
    [TestFixture]
    class CircleCommandTest
    {
        private CompoundShape root;
        private CircleCommand command;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            command = new CircleCommand(root);
        }

        [Test]
        public void CircleCommand_ShouldExecuteCorrect()
        {
            command.Execute("50", "50", "25");
            Assert.That(root.Shapes.Count, Is.EqualTo(1));
            Assert.That(root.Shapes[0], Is.InstanceOf<CircleShape>());
            var circle = (CircleShape) root.Shapes[0];
            Assert.That(circle.Center, Is.EqualTo(new PointF(50, 50)));
            Assert.That(circle.Radius, Is.EqualTo(25));
        }
    }
}