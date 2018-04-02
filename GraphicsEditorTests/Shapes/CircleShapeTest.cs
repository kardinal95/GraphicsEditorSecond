using System.Drawing;
using GraphicsEditor;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Shapes
{
    [TestFixture]
    public class CircleShapeTest
    {
        private CircleShape circle;

        private readonly PointF center = new PointF(25, 25);
        private const int Radius = 25;

        [SetUp]
        protected void SetUp()
        {
            circle = new CircleShape(center, Radius, null);
        }

        [Test]
        public void Circle_ShouldCreateCorrect()
        {
            Assert.That(circle.Center, Is.EqualTo(center));
            Assert.That(circle.Radius, Is.EqualTo(Radius));
        }

        [Test]
        public void Circle_ShouldTransformCorrect()
        {
            // Moving center to 0, 0 without scaling
            var zeroVector = new PointF(-center.X, -center.Y);
            var translation = Transformation.Translate(zeroVector);
            circle.Transform(translation);
            Assert.That(circle.Center, Is.EqualTo(new PointF(0, 0)));
            Assert.That(circle.Radius, Is.EqualTo(Radius));
        }
    }
}