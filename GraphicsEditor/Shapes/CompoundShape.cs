using System;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    public class CompoundShape : IShape
    {
        public CompoundShape Parent { get; set; }
        public CompoundIndex Index => GetFullIndex();

        public IList<IShape> Shapes
        {
            get
            {
                lock (lockObject)
                {
                    return shapes;
                }
            }
        }

        private readonly IList<IShape> shapes;
        public event Action Changed;

        private readonly object lockObject = new object();

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

        public void Add(IShape shape)
        {
            lock (lockObject)
            {
                shapes.Add(shape);
                Refresh();
            }
        }

        private void Insert(IShape shape, int position)
        {
            lock (lockObject)
            {
                shapes.Insert(position, shape);
                Refresh();
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

                Refresh();
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
            IShape shape;
            lock (lockObject)
            {
                if (index.Count == 1)
                {
                    shape = shapes[index.Head];
                }
                else
                {
                    var complex = (CompoundShape) shapes[index.Head];
                    shape = complex.GetShapeAt(index.Tail);
                }
            }

            return shape;
        }

        public string ToIndexedString()
        {
            var result = new List<string>();
            if (Parent != null)
            {
                result.Add(string.Join(" ", Index, this));
            }

            lock (lockObject)
            {
                result.AddRange(shapes.Select(shape => shape.ToIndexedString()));
            }

            return string.Join("\n", result);
        }

        private CompoundIndex GetFullIndex()
        {
            return Parent == null ? new CompoundIndex() : Parent.Index.Append(Parent.GetPos(this));
        }

        public void Transform(Transformation trans)
        {
            lock (lockObject)
            {
                foreach (var shape in shapes)
                {
                    shape.Transform(trans);
                }

                Refresh();
            }
        }

        public override string ToString()
        {
            return "Составная фигура";
        }

        public void Refresh()
        {
            Changed?.Invoke();
        }
    }
}