using System;
using System.Drawing;
using System.Linq;

namespace GraphicsEditor
{
    public class Transformation
    {
        /// <summary>
        ///     Образ точки после трансформации
        /// </summary>
        public PointF this[PointF point]
        {
            get
            {
                var modified = new PointF
                {
                    X = point.X * matrix[0, 0] + point.Y * matrix[1, 0] + matrix[2, 0],
                    Y = point.X * matrix[0, 1] + point.Y * matrix[1, 1] + matrix[2, 1]
                };
                return modified;
            }
        }

        /// <summary>
        ///     Декомпозиция для осуществления операций с кругом/эллипсом
        /// </summary>
        public SingularValueDecomposition Decomposition { get; }

        /// <summary>
        ///     Матрица преобразования (3x3)
        /// </summary>
        private readonly float[,] matrix;

        private Transformation()
        {
            matrix = new float[3, 3];
            Decomposition = new SingularValueDecomposition {Source = this};
        }

        /// <summary>
        ///     Возвращает преобразование поворота на угол angle вокруг точки (0,0)
        /// </summary>
        public static Transformation Rotate(float angle)
        {
            var rad = angle * Math.PI / 180; // Мат. библиотека работает с радианами!
            var matrix = new[,]
            {
                {(float) Math.Cos(rad), (float) Math.Sin(rad)},
                {(float) -Math.Sin(rad), (float) Math.Cos(rad)}
            };
            return FromMatrix(matrix);
        }

        /// <summary>
        ///     Возвращает преобразование параллельного переноса на вектор ((0,0), point)
        /// </summary>
        public static Transformation Translate(PointF point)
        {
            var result = FromMatrix(new float[,] {{1, 0}, {0, 1}});
            result.matrix[2, 0] = point.X;
            result.matrix[2, 1] = point.Y;
            return result;
        }

        /// <summary>
        ///     Возвращает преобразование масштабирования с коэффициентами scaleX и scaleY.
        ///     Относительно точки (0, 0).
        ///     отрицательные значения параметров соответствуют инверсии
        /// </summary>
        public static Transformation Scale(float scaleX, float scaleY)
        {
            var matrix = new[,] {{scaleX, 0}, {0, scaleY}};
            return FromMatrix(matrix);
        }

        /// <summary>
        ///     Возвращает центральное аффинное преобразование, заданное матрицей 2x2
        /// </summary>
        /// <param name="matrix">Входная матрица 2х2</param>
        private static Transformation FromMatrix(float[,] matrix)
        {
            var result = new Transformation();
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    result.matrix[i, j] = matrix[i, j];
                }
            }

            result.matrix[2, 2] = 1;
            return result;
        }

        /// <summary>
        ///     Возвращает преобразование, получающееся последовательным
        ///     применением преобразований a и b
        /// </summary>
        public static Transformation operator *(Transformation a, Transformation b)
        {
            var result = new Transformation();
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    // Каждый элемент - произведение соотв. строк и столбцов
                    result.matrix[i, j] = a.matrix[i, 0] * b.matrix[0, j] +
                                          a.matrix[i, 1] * b.matrix[1, j] +
                                          a.matrix[i, 2] * b.matrix[2, j];
                }
            }

            return result;
        }

        /// <summary>
        ///     Разложение матрицы преобразования на углы поворота
        ///     и коэфф. маштабирования
        /// </summary>
        public class SingularValueDecomposition
        {
            public Transformation Source;
            private float[,] Matrix => Source.matrix;

            private double A
            {
                get
                {
                    var targets = new[] {Matrix[0, 0], Matrix[1, 1], Matrix[0, 1], Matrix[1, 0]};
                    return targets.Sum(element => Math.Pow(element, 2)) / 2;
                }
            }

            private double D => Matrix[0, 0] * Matrix[1, 1] - Matrix[0, 1] * Matrix[1, 0];

            public float FirstAngle => 0;

            public float[] Scale =>
                new[]
                {
                    (float) (Math.Sqrt((A + D) / 2) + Math.Sqrt((A - D) / 2)),
                    (float) (Math.Sqrt((A + D) / 2) - Math.Sqrt((A - D) / 2))
                };

            public float SecondAngle =>
                (float) (Math.Acos(Matrix[0, 0] / Scale[0]) / Math.PI * 180) *
                Math.Sign(Matrix[0, 1]);
        }
    }
}