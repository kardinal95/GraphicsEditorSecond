using System;
using GraphicsEditor.Shapes;
using GraphicsEditorTests.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Commands.Manage
{
    [TestFixture]
    class BaseManageCommandTest
    {
        private CompoundShape root;
        private DummyManageCommand command;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            command = new DummyManageCommand(root);
        }

        [TestCase("")]
        [TestCase("a,b")]
        [TestCase("a")]
        [TestCase("12")]
        public void BaseManageCommand_ShouldNotExecuteWhileIncorrect(string input)
        {
            var args = input.Split(',');
            Assert.That(() => command.Execute(args), Throws.Nothing);
        }

        [Test]
        public void BaseManageCommand_ShouldSuccedOnCorrectNumber()
        {
            root.Add(new DummyBasicShape(root));
            Assert.That(() => command.Execute("0"), Throws.InstanceOf<NotImplementedException>());
        }
    }
}