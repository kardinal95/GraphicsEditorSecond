using System;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;
using GraphicsEditor.Shapes;

namespace GraphicsEditor
{
    public class Picture : IShape
    {
        private readonly List<IShape> shapes = new List<IShape>();
        private readonly object lockObject = new object();

        public event Action Changed;

        public void Remove(IShape shape)
        {
            lock (lockObject)
            {
                shapes.Remove(shape);
            }
        }

        public void Add(IShape shape)
        {
            lock (lockObject)
            {
                shapes.Add(shape);
                Changed?.Invoke();
            }
        }

        public void Add(int index, IShape shape)
        {
            lock (lockObject)
            {
                shapes.Insert(index, shape);
                Changed?.Invoke();
            }
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

        // Complex functions

        public void Transform(Transformation trans)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(List<int> idInts)
        {
            lock (lockObject)
            {
                if (idInts.Count != 1)
                {
                    shapes[idInts[0]].RemoveAt(idInts.GetRange(1, idInts.Count - 1));
                }
                else
                {
                    shapes.RemoveAt(idInts[0]);
                }
            }

        }

        public IShape GetShapeAt(List<int> idInts)
        {
            List<IShape> shapeCopy;
            lock (lockObject)
            {
                shapeCopy = shapes;
            }
            return idInts.Count != 1 ? shapeCopy[idInts[0]].GetShapeAt(idInts.GetRange(1, idInts.Count - 1)) : shapeCopy[idInts[0]];
        }

        public string GetStringRepresentation(string compoundIndex)
        {
            var compound = new List<string>();
            lock (lockObject)
            {
                compound.AddRange(shapes.Select((t, i) => t.GetStringRepresentation(i.ToString())));
            }
            return string.Join("\n", compound);
        }
    }
}