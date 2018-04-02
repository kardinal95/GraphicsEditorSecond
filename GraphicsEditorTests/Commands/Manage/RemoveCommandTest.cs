using System.Drawing;
using GraphicsEditor.Commands.Manage;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Commands.Manage
{
    [TestFixture]
    class RemoveCommandTest
    {
        private CompoundShape root;
        private RemoveCommand command;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            command = new RemoveCommand(root);
        }

        [Test]
        public void RemoveCommand_ShouldExecuteCorrect()
        {
            var point = new PointShape(10, 10, root);
            var circle = new CircleShape(new PointF(1, 1), 20, root);
            root.Add(point);
            root.Add(circle);
            command.Execute("0");
            Assert.That(root.Shapes.Count, Is.EqualTo(1));
            Assert.That(root.Shapes[0], Is.EqualTo(circle));
        }
    }
}