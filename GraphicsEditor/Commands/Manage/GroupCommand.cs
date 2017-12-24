using System.Collections.Generic;
using System.Linq;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands.Manage
{
    class GroupCommand : BaseManageCommand
    {
        private readonly Picture picture;

        public override string Name => "group";
        public override string Help => "Группирует фигуры";

        public override string Description => "Группирует фигуры с указанными индексами\n" +
                                              "Использование: \'group x y ..\', где x, y, .. - индексы фигур в команде list";

        public override string[] Synonyms => new string[] { };
        protected override int Argsnum => 0;

        public GroupCommand(Picture picture) : base(picture)
        {
            this.picture = picture;
        }

        protected override void MakeChanges(List<CompoundIndex> indexes)
        {
            var shapes = indexes.Select(index => picture.GetShapeAt(index)).ToList();
            foreach (var index in indexes)
            {
                picture.RemoveAt(index);
            }
            picture.Add(new CompoundShape(shapes));
        }
    }
}