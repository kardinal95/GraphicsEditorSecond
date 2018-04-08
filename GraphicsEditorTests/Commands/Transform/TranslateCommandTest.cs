using System.Drawing;
using GraphicsEditor.Commands.Transform;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Commands.Transform
{
    [TestFixture]
    class TranslateCommandTest
    {
        private CompoundShape root;
        private TranslateCommand command;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            command = new TranslateCommand(root);
        }

        [Test]
        public void TranslateCommand_ShouldTransformCorrect()
        {
            var point = new PointShape(-10, -10, root);
            var target = new PointF(0, 0);
            root.Add(point);

            command.Execute("10", "10", "0");
            Assert.That(point.Point, Is.EqualTo(target));
        }
    }
}