using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicsEditor
{
    public class CompoundIndex : IComparable
    {
        public int Size => subIndexes.Count;
        public int Top => Size != 0 ? subIndexes[0] : -1;
        public CompoundIndex Sub => GetSub();

        private readonly List<int> subIndexes;

        public CompoundIndex()
        {
            subIndexes = new List<int>();
        }

        private CompoundIndex(IEnumerable<int> indexList)
        {
            subIndexes = indexList.ToList();
        }

        public static bool TryParse(string input, out CompoundIndex index)
        {
            index = null;
            var indexList = new List<int>();
            var parts = input.Split(':');
            foreach (var part in parts)
            {
                if (!int.TryParse(part, out var partInt))
                {
                    return false;
                }

                indexList.Add(partInt);
            }

            index = new CompoundIndex(indexList);
            return true;
        }

        private CompoundIndex GetSub()
        {
            var sub = new CompoundIndex(subIndexes);
            sub.subIndexes.RemoveAt(0);
            return sub;
        }

        public CompoundIndex JoinRight(int index)
        {
            subIndexes.Add(index);
            return this;
        }

        public void JoinLeft(int index)
        {
            subIndexes.Insert(0, index);
        }

        public override string ToString()
        {
            return string.Join(":", subIndexes);
        }

        public bool Equals(CompoundIndex other)
        {
            if (Size != other?.Size)
            {
                return false;
            }

            for (var i = Size; i < other.Size; i++)
            {
                if (subIndexes[i] != other.subIndexes[i])
                {
                    return false;
                }
            }

            return true;
        }

        public int CompareTo(object obj)
        {
            switch (obj)
            {
                case null:
                    return 1;
                case CompoundIndex otherIndex:
                    var comparison = Top.CompareTo(otherIndex.Top);
                    if (comparison == 0 && Size != 1)
                    {
                        return Sub.CompareTo(otherIndex.Sub);
                    }

                    return comparison;
                default:
                    throw new ArgumentException("Object is not a Compound Index!");
            }
        }
    }
}