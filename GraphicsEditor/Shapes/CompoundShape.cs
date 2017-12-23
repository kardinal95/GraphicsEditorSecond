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

        public void RemoveAt(List<int> idInts)
        {
            if (idInts.Count != 1)
            {
                Shapes[idInts[0]].RemoveAt(idInts.GetRange(1, idInts.Count - 1));
            }
            else
            {
                Shapes.RemoveAt(idInts[0]);
            }
        }

        public IShape GetShapeAt(List<int> idInts)
        {
            return idInts.Count != 1 ? Shapes[idInts[0]].GetShapeAt(idInts.GetRange(1, idInts.Count - 1)) : Shapes[idInts[0]];
        }

        public string GetStringRepresentation(string compoundIndex)
        {
            var compound = new List<string> {$"[{compoundIndex}] Составная фигура"};
            compound.AddRange(Shapes.Select((t, i) => t.GetStringRepresentation($"{compoundIndex}:{i}")));
            return string.Join("\n", compound);
        }
    }
}