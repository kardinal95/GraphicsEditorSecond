using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Shapes
{
    [TestFixture]
    public class PointShapeTest
    {
        private PointShape point;
        private CompoundShape root;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
        }

        [Test]
        public void CreatePointCorrect()
        {
            point = new PointShape(20, -20, null);
            Assert.AreEqual(point.CoordX, 20f);
            Assert.AreEqual(point.CoordY, -20f);
            Assert.AreEqual(point.Parent, null);
        }
    }
}