using GraphicsEditor.Shapes;
using NUnit.Framework;
using NUnit.Framework.Constraints;

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
        public void CreatePointTest()
        {
            point = new PointShape(10, -10, root);
            Assert.AreEqual(point.Point.X, 10f);
            Assert.AreEqual(point.Point.Y, -10f);
            Assert.AreEqual(point.Parent, root);
        }

        [Test]
        public void IndexTest()
        {
            var pointA = new PointShape(0, 0, null);
            var pointB = new PointShape(0, 0, root);
            var pointC = new PointShape(0, 0, root);
            Assert.That(pointA.Index, Throws.InvalidOperationException);
            Assert.That(pointB.Index.ToString(), Is.EqualTo("0"));
            Assert.That(pointC.Index.ToString(), Is.EqualTo("1"));
        }
    }
}