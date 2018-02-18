using GraphicsEditor.Commands.Shape;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests
{
    [TestFixture]
    public class PointCommandTest
    {
        private CompoundShape root;
        private PointCommand command;

        [SetUp]
        protected void SetUp()
        { 
            root = new CompoundShape();
            command = new PointCommand(root);
        }

        [Test]
        public void AddCorrectPoint()
        {
            // Точка с координатами 100, -100 добавляется корректно
            command.Execute("100", "-100");
            Assert.AreEqual(root.Shapes.Count, 1);
            var point = root.Shapes[0] as PointShape;
            Assert.IsNotNull(point);
            Assert.AreEqual(point.CoordX, 100f);
            Assert.AreEqual(point.CoordY, -100f);
        }
    }
}
