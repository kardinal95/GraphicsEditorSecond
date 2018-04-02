using System;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Shapes
{
    [TestFixture]
    public class BasicShapeTest
    {
        private DummyBasicShape shape;
        private CompoundShape root;

        [SetUp]
        protected void SetUp()
        {
            root = new CompoundShape();
            shape = new DummyBasicShape(root);
            root.Add(shape);
        }

        [Test]
        public void Parent_ShouldReturnCorrect()
        {
            Assert.That(shape.Parent, Is.EqualTo(root));
        }

        [Test]
        public void Index_ShouldReturnCorrect()
        {
            Assert.That(shape.Index.ToString(), Is.EqualTo("[0]"));
        }

        [Test]
        public void ComplexIndex_ShouldReturnCorrect()
        {
            var wrap = new CompoundShape();
            root.Parent = wrap;
            wrap.Add(root);
            Assert.That(shape.Index.ToString(), Is.EqualTo("[0:0]"));
        }

        [Test]
        public void NullIndex_ShouldThrow()
        {
            shape.Parent = null;
            Assert.That(() => shape.Index, Throws.InstanceOf<InvalidOperationException>());
        }

        [Test]
        public void IndexedString_ShouldReturnCorrect()
        {
            Assert.That(shape.ToIndexedString(), Is.EqualTo($"[0] {shape}"));
        }
    }
}