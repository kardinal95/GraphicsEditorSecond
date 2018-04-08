using System;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Commands.Transform
{
    [TestFixture]
    class BaseTransformCommandTest
    {
        private CompoundShape root;
        private DummyTransformCommand command;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            command = new DummyTransformCommand(root);
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("1,2,3")]
        [TestCase("a,2")]
        [TestCase("2.3,1::2")]
        public void BaseTransformCommand_ShouldNotExecuteWhileIncorrect(string input)
        {
            var args = input.Split(',');
            Assert.That(() => command.Execute(args), Throws.Nothing);
        }

        [Test]
        public void BaseTransformCommand_ShouldSuccedOnCorrectNumber()
        {
            var point = new PointShape(1, 1, root);
            root.Add(point);

            Assert.That(() => command.Execute("2", "0"),
                        Throws.InstanceOf<NotImplementedException>());
        }
    }
}