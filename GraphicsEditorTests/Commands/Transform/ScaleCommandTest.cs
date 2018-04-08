using System.Drawing;
using GraphicsEditor.Commands.Transform;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Commands.Transform
{
    [TestFixture]
    class ScaleCommandTest
    {
        private CompoundShape root;
        private ScaleCommand command;
        private EllipseShape ellipse;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            command = new ScaleCommand(root);
            ellipse = new EllipseShape(new PointF(0, 0), new SizeF(10, 20), 0, root);
            root.Add(ellipse);
        }

        [Test]
        public void ScaleCommand_ShouldTransformCorrect()
        {
            var center = new PointF(0, 0);
            var size = new SizeF(100, 200);
            const int degree = 0;

            command.Execute("0", "0", "10", "0");
            Assert.That(ellipse.Center, Is.EqualTo(center));
            Assert.That(ellipse.Size, Is.EqualTo(size));
            Assert.That(ellipse.Degree, Is.EqualTo(degree));
        }
    }
}