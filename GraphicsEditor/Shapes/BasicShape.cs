using System;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    /// <summary>
    ///     Базовый класс простейших фигур
    /// </summary>
    public abstract class BasicShape : IShape
    {
        public abstract void Draw(IDrawer drawer);

        public CompoundShape Parent { get; set; }
        public CompoundIndex Index => GetFullIndex();
        public abstract void Transform(Transformation trans);

        protected BasicShape(CompoundShape parent)
        {
            Parent = parent;
        }

        public IShape GetShapeAt(CompoundIndex index)
        {
            if (index.Count != 0)
            {
                throw new InvalidOperationException("No subelements in basic shape!");
            }

            return this;
        }

        public string ToIndexedString()
        {
            return string.Join(" ", Index, this);
        }

        private CompoundIndex GetFullIndex()
        {
            if (Parent is null)
            {
                throw new Exception("Shape's not in accessible field!");
            }

            return Parent.Index.Append(Parent.GetPos(this));
        }
    }
}