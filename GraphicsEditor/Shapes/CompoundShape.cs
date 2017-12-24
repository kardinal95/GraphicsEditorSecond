using System;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    class CompoundShape : IShape
    {
        public CompoundShape(IList<IShape> shapes)
        {
            Shapes = shapes;
        }

        public IList<IShape> Shapes { get; }

        public void Draw(IDrawer drawer)
        {
            foreach (var shape in Shapes)
            {
                shape.Draw(drawer);
            }
        }

        public void Transform(Transformation trans)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(CompoundIndex index)
        {
            if (index.Size != 1)
            {
                Shapes[index.Top].RemoveAt(index.Sub);
            }
            else
            {
                Shapes.RemoveAt(index.Top);
            }
        }

        public IShape GetShapeAt(CompoundIndex index)
        {
            return index.Size == 1 ? Shapes[index.Top] : Shapes[index.Top].GetShapeAt(index.Sub);
        }

        public string GetStringRepresentation(string compoundIndex)
        {
            var compound = new List<string> {$"[{compoundIndex}] Составная фигура"};
            compound.AddRange(
                Shapes.Select((t, i) => t.GetStringRepresentation($"{compoundIndex}:{i}")));
            return string.Join("\n", compound);
        }
    }
}