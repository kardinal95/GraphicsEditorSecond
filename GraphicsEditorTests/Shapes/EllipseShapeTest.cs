using System;
using System.Drawing;
using GraphicsEditor;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Shapes
{
    [TestFixture]
    public class EllipseShapeTest
    {
        private EllipseShape ellipse;

        private readonly PointF center = new PointF(25, 25);
        private readonly SizeF size = new SizeF(100, 10);
        private const float Degree = 90f;

        [SetUp]
        protected void SetUp()
        {
            ellipse = new EllipseShape(center, size, Degree, null);
        }

        [Test]
        public void Ellipse_ShouldCreateCorrect()
        {
            Assert.That(ellipse.Center, Is.EqualTo(center));
            Assert.That(ellipse.Size, Is.EqualTo(size));
            Assert.That(ellipse.Degree, Is.EqualTo(Degree));
        }

        [Test]
        public void Ellipse_ShouldTransformCorrect()
        {
            // Moving center to 0, 0 without scaling
            var zeroVector = new PointF(-center.X, -center.Y);
            var translation = Transformation.Translate(zeroVector);
            ellipse.Transform(translation);
            Assert.That(ellipse.Center, Is.EqualTo(new PointF(0, 0)));
            Assert.That(ellipse.Size, Is.EqualTo(size));
            Assert.That(ellipse.Degree, Is.EqualTo(Degree));
        }

        [Test]
        public void Ellipse_ShouldThrowOnDifferentScales()
        {
            var scale = Transformation.Scale(1, 2);
            Assert.That(() => ellipse.Transform(scale),
                        Throws.InstanceOf<NotImplementedException>());
        }
    }
}