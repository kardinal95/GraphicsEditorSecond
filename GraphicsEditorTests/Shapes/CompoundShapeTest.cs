using System;
using System.Collections.Generic;
using System.Drawing;
using GraphicsEditor;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Shapes
{
    [TestFixture]
    public class CompoundShapeTest
    {
        private CompoundShape compound;

        private PointShape point;
        private LineShape line;

        [SetUp]
        protected void SetUp()
        {
            compound = new CompoundShape();
            point = new PointShape(10, 10, compound);
            line = new LineShape(new PointF(10, 10), new PointF(-10, -10), compound);
        }

        [Test]
        public void CompoundShape_ShouldAddMultipleShapesCorrect()
        {
            compound.Add(point);
            compound.Add(line);
            Assert.That(compound.Shapes.Count, Is.EqualTo(2));
        }

        [Test]
        public void CompoundShape_InternalShapesShouldBeCorrect()
        {
            compound.Add(point);
            Assert.That(compound.Shapes[0], Is.EqualTo(point));
        }

        [Test]
        public void CompoundShape_ShouldRemoveShapesCorrect()
        {
            compound.Add(point);
            compound.Add(line);
            compound.Remove(point);
            Assert.That(compound.Shapes.Count, Is.EqualTo(1));
            Assert.That(compound.Shapes[0], Is.InstanceOf<LineShape>());
        }

        [Test]
        public void CompoundShape_ShouldSupportConsistencyOnRemoval()
        {
            var subshape = new CompoundShape(new List<IShape> {point, line}, compound);
            compound.Add(subshape);
            subshape.Remove(point);
            Assert.That(compound.Shapes.Count, Is.EqualTo(1));
            Assert.That(compound.Shapes[0], Is.InstanceOf<LineShape>());
        }

        [Test]
        public void CompoundShape_ShouldUngroupCorrect()
        {
            var subshape = new CompoundShape(new List<IShape> {point, line}, compound);
            compound.Add(subshape);
            subshape.Ungroup();
            Assert.That(compound.Shapes.Count, Is.EqualTo(2));
        }

        [Test]
        public void CompoundShape_CoreShouldThrowOnUngroup()
        {
            Assert.That(() => compound.Ungroup(),
                        Throws.InstanceOf<InvalidOperationException>().And.Message.
                            EqualTo("Cannot ungroup core shape"));
        }

        [Test]
        public void CompoundShape_ShouldReturnShapePosCorrect()
        {
            compound.Add(point);
            compound.Add(line);
            Assert.That(compound.GetPos(line), Is.EqualTo(1));
        }

        [Test]
        public void CompoundShape_ShouldReturnShapeFromPosCorrect()
        {
            compound.Add(point);
            compound.Add(line);
            CompoundIndex.TryParse("1", out var index);
            Assert.That(compound.GetShapeAt(index), Is.EqualTo(line));
        }

        [Test]
        public void CompoundShape_ShouldReturnComplexIndexCorrect()
        {
            var subshape = new CompoundShape(new List<IShape> {point, line}, compound);
            compound.Add(subshape);
            Assert.That(subshape.Index.ToString(), Is.EqualTo("[0]"));
        }

        [Test]
        public void CompoundShape_ShouldReturnIndexedStringCorrect()
        {
            compound.Add(point);
            compound.Add(line);
            Assert.That(compound.ToIndexedString(),
                        Is.EqualTo($"{point.ToIndexedString()}\n{line.ToIndexedString()}"));
        }

        [Test]
        public void CompoundShape_ShouldTransformAllInternalShapes()
        {
            var rotation = Transformation.Rotate(180);
            compound.Add(point);
            compound.Add(line);
            compound.Transform(rotation);
            Assert.That(point.Point, Is.EqualTo(new PointF(-10, -10)));
            Assert.That(line.Bounds[0], Is.EqualTo(new PointF(-10, -10)));
            Assert.That(line.Bounds[1], Is.EqualTo(new PointF(10, 10)));
        }
    }
}