using System;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Commands.Shape
{
    [TestFixture]
    class BaseShapeCommandTest
    {
        private CompoundShape root;
        private DummyShapeCommand command;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            command = new DummyShapeCommand(root);
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("1,2,3")]
        public void BaseShapeCommand_ShouldNotExecuteWhileIncorrect(string input)
        {
            var args = input.Split(',');
            Assert.That(() => command.Execute(args), Throws.Nothing);
        }

        [Test]
        public void BaseShapeCommand_ShouldSuccedOnCorrectNumber()
        {
            Assert.That(() => command.Execute("0"), Throws.InstanceOf<NotImplementedException>());
        }
    }
}