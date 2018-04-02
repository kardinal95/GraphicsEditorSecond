using System.Drawing;
using GraphicsEditor.Commands.Shape;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Commands.Shape
{
    [TestFixture]
    class EllipseCommandTest
    {
        private CompoundShape root;
        private EllipseCommand command;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            command = new EllipseCommand(root);
        }

        [Test]
        public void EllipseCommand_ShouldExecuteCorrect()
        {
            command.Execute("50", "50", "25", "40", "45");
            Assert.That(root.Shapes.Count, Is.EqualTo(1));
            Assert.That(root.Shapes[0], Is.InstanceOf<EllipseShape>());
            var ellipse = (EllipseShape) root.Shapes[0];
            Assert.That(ellipse.Center, Is.EqualTo(new PointF(50, 50)));
            Assert.That(ellipse.Size, Is.EqualTo(new SizeF(25, 40)));
            Assert.That(ellipse.Degree, Is.EqualTo(45));
        }
    }
}