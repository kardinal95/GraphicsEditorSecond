using System.Drawing;
using GraphicsEditor;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Shapes
{
    [TestFixture]
    public class LineShapeTest
    {
        private LineShape line;

        private readonly PointF[] target = {new PointF(-10f, -10f), new PointF(10f, 10f)};

        [SetUp]
        protected void SetUp()
        {
            line = new LineShape(target[0], target[1], null);
        }

        [Test]
        public void Line_ShouldCreateCorrect()
        {
            Assert.That(line.Bounds, Is.EquivalentTo(target));
        }

        [Test]
        public void Line_ShouldTransformCorrect()
        {
            var mirror = new[]
                {new PointF(-target[0].X, -target[0].Y), new PointF(-target[1].X, -target[1].Y)};
            var rotation = Transformation.Rotate(180);
            line.Transform(rotation);
            Assert.That(line.Bounds, Is.EquivalentTo(mirror));
        }
    }
}