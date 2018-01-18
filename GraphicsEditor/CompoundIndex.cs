using System.Collections.Generic;
using System.Linq;

namespace GraphicsEditor
{
    /// <summary>
    ///     Класс для хранения составных индексов
    /// </summary>
    public class CompoundIndex
    {
        /// <summary>
        ///     Количество уровней в составном индексе
        /// </summary>
        public int Count => indexes.Count;

        /// <summary>
        ///     Верхний уровень составного индекса
        /// </summary>
        public int Head
        {
            get
            {
                if (Count != 0)
                {
                    return indexes[0];
                }

                return -1;
            }
        }

        /// <summary>
        ///     Составной индекс без первого элемента
        /// </summary>
        public CompoundIndex Tail
        {
            get
            {
                var sub = new CompoundIndex(indexes);
                sub.indexes.RemoveAt(0);
                return sub;
            }
        }

        private readonly List<int> indexes;

        public CompoundIndex()
        {
            indexes = new List<int>();
        }

        private CompoundIndex(IEnumerable<int> indexes)
        {
            this.indexes = indexes.ToList();
        }

        /// <summary>
        ///     Преобразует входную строку в составной индекс
        ///     Возвращает true при успехе, false при обнаружении ошибок
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <param name="compound">Результат - составной индекс</param>
        /// <returns>True/False - статус успеха конвертации</returns>
        public static bool TryParse(string input, out CompoundIndex compound)
        {
            compound = null;
            var indexes = new List<int>();
            var parts = input.Split(':');
            foreach (var part in parts)
            {
                if (!int.TryParse(part, out var index))
                {
                    return false;
                }

                indexes.Add(index);
            }

            compound = new CompoundIndex(indexes);
            return true;
        }

        /// <summary>
        ///     Добавляет индекс на последний уровень
        /// </summary>
        /// <param name="index">Индекс для добавления</param>
        /// <returns></returns>
        public CompoundIndex Append(int index)
        {
            indexes.Add(index);
            return this;
        }

        public override string ToString()
        {
            return Count == 0 ? string.Empty : $"[{string.Join(":", indexes)}]";
        }
    }
}