using System.Drawing;
using GraphicsEditor;
using NUnit.Framework;

namespace GraphicsEditorTests
{
    [TestFixture]
    public class TransformationTest
    {
        private Transformation trans;

        [Test]
        public void Transformation_ShouldReturnPointCorrect()
        {
            var point = new PointF(5, 5);
            trans = Transformation.Translate(new PointF(-5, -5));
            point = trans[point];
            Assert.That(point, Is.EqualTo(new PointF(0, 0)));
        }

        [Test]
        public void Transformation_RotateShouldReturnCorrect()
        {
            trans = Transformation.Rotate(90);
            var target = new float[,] {{0, 1, 0}, {-1, 0, 0}, {0, 0, 1}};
            Assert.That(trans.Matrix, Is.EqualTo(target).Within(1E-16));
        }

        [Test]
        public void Transformation_TranslateShouldReturnCorrect()
        {
            trans = Transformation.Translate(new PointF(-1, -1));
            var target = new float[,] {{1, 0, 0}, {0, 1, 0}, {-1, -1, 1}};
            Assert.That(trans.Matrix, Is.EqualTo(target).Within(1E-16));
        }

        [Test]
        public void Transformation_ScaleShouldReturnCorrect()
        {
            trans = Transformation.Scale(2, 2);
            var target = new float[,] {{2, 0, 0}, {0, 2, 0}, {0, 0, 1}};
            Assert.That(trans.Matrix, Is.EqualTo(target).Within(1E-16));
        }
    }

    [TestFixture]
    public class SingularValueDecompositionTest
    {
        private Transformation trans;
        private Transformation.SingularValueDecomposition svd;

        [SetUp]
        protected void SetUp()
        {
            svd = new Transformation.SingularValueDecomposition();
        }

        [Test]
        public void SVD_RotateShouldParseCorrect()
        {
            trans = Transformation.Rotate(45);
            svd.Source = trans;
            var targetScale = new float[] {1, 1};
            Assert.That(svd.FirstAngle, Is.EqualTo(0));
            Assert.That(svd.SecondAngle, Is.EqualTo(45));
            Assert.That(svd.Scale, Is.EqualTo(targetScale));
        }

        [Test]
        public void SVD_TranslateShouldParseCorrect()
        {
            trans = Transformation.Translate(new PointF(-1, -1));
            svd.Source = trans;
            var targetScale = new float[] {1, 1};
            Assert.That(svd.FirstAngle, Is.EqualTo(0));
            Assert.That(svd.SecondAngle, Is.EqualTo(0));
            Assert.That(svd.Scale, Is.EqualTo(targetScale));
        }

        [Test]
        public void SVD_ScaleShouldParseCorrect()
        {
            trans = Transformation.Scale(2, 2);
            svd.Source = trans;
            var targetScale = new float[] {2, 2};
            Assert.That(svd.FirstAngle, Is.EqualTo(0));
            Assert.That(svd.SecondAngle, Is.EqualTo(0));
            Assert.That(svd.Scale, Is.EqualTo(targetScale));
        }
    }
}