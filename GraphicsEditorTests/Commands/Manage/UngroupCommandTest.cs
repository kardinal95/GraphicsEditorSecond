using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using GraphicsEditor.Commands.Manage;
using GraphicsEditor.Shapes;
using GraphicsEditorTests.Shapes;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace GraphicsEditorTests.Commands.Manage
{
    [TestFixture]
    class UngroupCommandTest
    {
        private CompoundShape root;
        private UngroupCommand command;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            command = new UngroupCommand(root);
        }

        [Test]
        public void UngroupCommand_ShouldExecuteCorrect()
        {
            var compound = new CompoundShape(new List<IShape>(), root);
            var point = new PointShape(0, 0, compound);
            var circle = new CircleShape(new PointF(10, 10), 20, compound);
            compound.Add(point);
            compound.Add(circle);
            root.Add(compound);
            command.Execute("0");
            Assert.That(root.Shapes.Count, Is.EqualTo(2));
        }

        [Test]
        public void UngroupCommand_ShouldReturnErrorOnNotCompoundShapes()
        {
            root.Add(new DummyBasicShape(root));
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                command.Execute("0");

                var expected = $"Команда выполнима только для составных фигур!{Environment.NewLine}";
                Assert.That(sw.ToString(), Is.EqualTo(expected));
            }
        }
    }
}