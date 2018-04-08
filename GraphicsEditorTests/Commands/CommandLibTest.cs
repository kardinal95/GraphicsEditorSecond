using System;
using System.Linq;
using GraphicsEditor.Commands;
using GraphicsEditor.Shapes;
using NUnit.Framework;

namespace GraphicsEditorTests.Commands
{
    [TestFixture]
    public class CommandLibTest
    {
        // First 3 incorrect, than 3 correct
        [TestCase(1, new[] {"a", "", "12.3", "12", "0", "-50"})]
        [TestCase(1f, new[] {"a", "", "12.-3", "-12,2", "0", "50.32"})]
        public void ParseArguments_ShouldParseCorrect<T>(T obj, string[] args)
            where T : IConvertible
        {
            var parsed = CommandLib.ParseArguments<T>(args, out var errors);
            Assert.That(parsed.Count, Is.EqualTo(3));
            Assert.That(errors, Is.EquivalentTo(args.Take(3)));
        }

        [Test]
        public void ParseIndexes_ShouldParseCorrect()
        {
            var input = new[] {"1", "", "1::2", "2:3", "-2", "1:2"}; // With collision!
            var parsed = CommandLib.ParseIndexes(input, out var errors);
            Assert.That(parsed.Count, Is.EqualTo(2));
            Assert.That(errors, Is.EquivalentTo(new[] {"", "1::2", "-2", "1:2"}));
        }

        [Test]
        public void GetExisting_ShouldReturnCorrect()
        {
            var core = new CompoundShape();
            var compound = new CompoundShape {Parent = core};
            core.Add(compound);
            var point1 = new PointShape(2, 3, compound);
            var point2 = new PointShape(2, 3, compound);
            compound.Add(point2);
            compound.Add(point1);

            var indexes = CommandLib.ParseIndexes(new[] {"0:0", "0:1", "1", "2:3:1:0"}, out _);
            var shapes = CommandLib.GetExisting(indexes, out _, core);
            Assert.That(shapes, Is.EquivalentTo(new IShape[] {point2, point1}));
        }
    }
}