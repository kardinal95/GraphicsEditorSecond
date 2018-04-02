using GraphicsEditor.Commands.Manage;
using GraphicsEditor.Shapes;
using GraphicsEditorTests.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Commands.Manage
{
    [TestFixture]
    class GroupCommandTest
    {
        private CompoundShape root;
        private GroupCommand command;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            command = new GroupCommand(root);
        }

        [Test]
        public void GroupCommand_ShouldExecuteCorrect()
        {
            root.Add(new DummyBasicShape(root));
            root.Add(new DummyBasicShape(root));
            command.Execute("0", "1");
            Assert.That(root.Shapes.Count, Is.EqualTo(1));
            Assert.That(root.Shapes[0], Is.InstanceOf<CompoundShape>());
            var compound = (CompoundShape) root.Shapes[0];
            Assert.That(compound.Shapes.Count, Is.EqualTo(2));
        }
    }
}