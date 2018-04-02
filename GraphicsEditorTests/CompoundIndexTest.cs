using GraphicsEditor;
using NUnit.Framework;

namespace GraphicsEditorTests
{
    [TestFixture]
    public class CompoundIndexTest
    {
        [TestCase("0", ExpectedResult = true)]
        [TestCase("0:1", ExpectedResult = true)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("1::2", ExpectedResult = false)]
        [TestCase("-1:-2", ExpectedResult = false)]
        [TestCase(":", ExpectedResult = false)]
        [TestCase("a", ExpectedResult = false)]
        public bool CompoundIndex_ShouldParseCorrect(string input)
        {
            return CompoundIndex.TryParse(input, out var _);
        }

        [TestCase("0")]
        [TestCase("1:3:1:3")]
        public void CompoundIndex_ShouldConvertToStringCorrect(string input)
        {
            CompoundIndex.TryParse(input, out var index);
            Assert.That(index.ToString(), Is.EqualTo($"[{input}]"));
        }

        [Test]
        public void CompoundIndex_ShouldInformCheckCollisionCorrect()
        {
            CompoundIndex.TryParse("0:1:2", out var a);
            CompoundIndex.TryParse("0", out var b);
            CompoundIndex.TryParse("3:1:2", out var c);
            Assert.That(a.CollidesWith(b), Is.True);
            Assert.That(b.CollidesWith(a), Is.True);
            Assert.That(b.CollidesWith(c), Is.False);
        }

        [Test]
        public void CompoundIndex_ShouldReturnCorrectAttributes()
        {
            CompoundIndex.TryParse("0:1:2", out var index);
            Assert.That(index.Head, Is.EqualTo(0));
            Assert.That(index.Tail.ToString(), Is.EqualTo($"[1:2]"));
            Assert.That(index.Count, Is.EqualTo(3));
        }

        [Test]
        public void CompoundIndex_ShouldReturnOnNullCorrect()
        {
            var index = new CompoundIndex();
            Assert.That(index.Count, Is.EqualTo(0));
            Assert.That(index.Head, Is.EqualTo(-1));
            Assert.That(index.Tail, Is.Null);
            Assert.That(index.ToString(), Is.Empty);
        }

        [Test]
        public void CompoundIndex_ShouldModifyCorrect()
        {
            var index = new CompoundIndex();
            index.Append(1);
            Assert.That(index.Count, Is.EqualTo(1));
            Assert.That(index.Head, Is.EqualTo(1));
        }
    }
}