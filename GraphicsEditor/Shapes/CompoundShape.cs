using System;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    public class CompoundShape : IShape
    {
        public CompoundShape Parent { get; set; }
        public CompoundIndex FullIndex => GetFullIndex();
        private readonly IList<IShape> shapes;
        public event Action Changed;

        private readonly object lockObject = new object();

        // Constructors
        public CompoundShape()
        {
            Parent = null;
            shapes = new List<IShape>();
        }

        public CompoundShape(IList<IShape> shapes, CompoundShape parent = null)
        {
            this.shapes = shapes;
            Parent = parent;
        }

        // Compound shape exclusive

        public void Add(IShape shape)
        {
            lock (lockObject)
            {
                shapes.Add(shape);
                Changed?.Invoke();
            }
        }

        private void Insert(IShape shape, int position)
        {
            lock (lockObject)
            {
                shapes.Insert(position, shape);
                Changed?.Invoke();
            }
        }

        public void Remove(IShape shape)
        {
            lock (lockObject)
            {
                shapes.Remove(shape);
                if (shapes.Count == 1 && Parent != null)
                {
                    var pos = Parent.GetPos(this);
                    Parent.Insert(shapes[0], pos);
                    shapes[0].Parent = Parent;
                    Parent.Remove(this);
                }

                Changed?.Invoke();
            }
        }

        public void Ungroup()
        {
            if (Parent is null)
            {
                throw new InvalidOperationException("Cannot ungroup core shape");
            }

            lock (lockObject)
            {
                var pos = Parent.GetPos(this);
                foreach (var shape in shapes.Reverse())
                {
                    shape.Parent = Parent;
                    Parent.Insert(shape, pos);
                }

                Parent.Remove(this);
            }
        }

        public int GetPos(IShape shape)
        {
            int result;
            lock (lockObject)
            {
                result = shapes.IndexOf(shape);
            }

            return result;
        }

        // IShape
        public void Draw(IDrawer drawer)
        {
            lock (lockObject)
            {
                foreach (var shape in shapes)
                {
                    shape.Draw(drawer);
                }
            }
        }

        public IShape GetShapeAt(CompoundIndex index)
        {
            IShape result;
            lock (lockObject)
            {
                result = index.Size == 0 ? this : shapes[index.Top].GetShapeAt(index.Sub);
            }

            return result;
        }

        private CompoundIndex GetFullIndex()
        {
            return Parent == null ? new CompoundIndex()
                : Parent.FullIndex.JoinRight(Parent.GetPos(this));
        }

        public void Transform(Transformation trans)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            var result = "";
            if (Parent != null)
            {
                result += $"[{FullIndex}] Составная фигура\n";
            }

            lock (lockObject)
            {
                result = shapes.Aggregate(result, (current, shape) => current + shape.ToString());
            }

            return result;
        }
    }
}