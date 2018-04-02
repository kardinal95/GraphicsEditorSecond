using System.Drawing;
using GraphicsEditor;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Shapes
{
    [TestFixture]
    public class PointShapeTest
    {
        private PointShape point;

        private PointF targetPoint = new PointF(10.5f, -10.5f);

        [SetUp]
        protected void SetUp()
        {
            point = new PointShape(targetPoint.X, targetPoint.Y, null);
        }

        [Test]
        public void Point_ShouldCreateCorrect()
        {
            Assert.That(point.Point, Is.EqualTo(targetPoint));
        }

        [Test]
        public void Point_ShouldTransformCorrect()
        {
            var mirror = new PointF(-10.5f, 10.5f);
            var rotation = Transformation.Rotate(180);
            point.Transform(rotation);
            Assert.That(point.Point, Is.EqualTo(mirror));
        }
    }
}