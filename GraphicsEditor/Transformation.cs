using System;
using System.Drawing;

namespace GraphicsEditor
{
    public class Transformation
    {
        /// Возвращает преобразование поворота на угол angle вокруг точки (0,0)
        public static Transformation Rotate(float angle)
        {
            throw new NotImplementedException();
        }

        /// Возвращает преобразование параллельного переноса на вектор ((0,0), point)
        public static Transformation Translate(PointF point)
        {
            throw new NotImplementedException();
        }

        /// Возвращает преобразование масштабирования с коэффициентами scaleX и scaleY
        /// относительно точки (0, 0).
        /// отрицательные значения параметров соответствуют инверсии
        public static Transformation Scale(float scaleX, float scaleY)
        {
            throw new NotImplementedException();
        }

        /// Возвращает центральное аффинное преобразование, заданное матрицей 2x2
        public static Transformation FromMatrix(float[,] matrix)
        {
            throw new NotImplementedException();
        }

        /// Возвращает преобразование, получающееся последовательным применением
        /// преобразований a и b
        public static Transformation operator *(Transformation a, Transformation b)
        {
            throw new NotImplementedException();
        }

        /// Для любой точки плоскости возвращает её образ
        public PointF this[PointF point] => PointF.Empty;

        private Transformation() { }
    }
}