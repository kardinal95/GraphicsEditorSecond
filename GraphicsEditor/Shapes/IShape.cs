﻿using DrawablesUI;

namespace GraphicsEditor.Shapes
{
    public interface IShape : IDrawable
    {
        /// <summary>
        ///     Указатель на родительскую фигуру
        ///     Родительской может являться только составная фигура
        ///     Для корневой фигуры родитель - null
        ///     Базовые фигуры обязаны иметь родителя!
        /// </summary>
        CompoundShape Parent { get; set; }

        /// <summary>
        ///     Полный индекс фигуры (относительно корневой)
        /// </summary>
        CompoundIndex Index { get; }

        /// <summary>
        ///     Преобразование фигуры
        /// </summary>
        void Transform(Transformation trans);

        /// <summary>
        ///     Возвращает содержащуюся в данной фигуру по указанному индексу
        ///     При попытке получить вложенную фигуру для базовой -
        ///     бросить InvalidArgumentException
        /// </summary>
        IShape GetShapeAt(CompoundIndex index);

        /// <summary>
        ///     Строковое представление фигуры вместе с индексом
        ///     Составная должна возвращать описание себя и всех вложенных
        /// </summary>
        string ToIndexedString();
    }
}