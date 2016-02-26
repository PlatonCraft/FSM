using System.Windows.Forms;
using System.Drawing;

namespace FSMProject
{
    /// <summary>
    /// Класс, обеспечивающий отрисовку объектов.
    /// </summary>
    static class Interface
    {
        /// <summary>
        /// Структура, хранящая переменные, зависящие от текстуры фона.
        /// </summary>
        private struct PosInfo
        {
            public int TxtScl, Step, Min, Max, Zero;
            

            public PosInfo(int scl, int stp, int min, int max, int zr)
            {
                TxtScl = scl; //Texture Scale - масштабирование текстуры
                Step = stp * scl; Min = min * scl; Max = max * scl; Zero = zr * scl;
            }
        }

        private static PosInfo X = new PosInfo(5, 6, -3, 98, 49); //масштаб, шаг между координатами, минимальная, максимальная координата, координата нуля. (Всё умножается на масштаб)
        private static PosInfo Y = new PosInfo(5, -6, -3, 98, 49); //-6 из-за того, что в форме координаты из верхнего левого угла, а не левого нижнего

        /// <summary>
        /// Метод, преобразующий "человеческие" координаты в пиксели.
        /// </summary>
        /// <param name = "posInfo">Структура с данными о специфике преобразования.</param>
        /// <param name = "coord">"Человеческая" координата.</param>
        private static int CoordToPos(PosInfo posInfo, int coord)
        {
            return posInfo.Zero + coord * posInfo.Step;
        }

        /// <summary>
        /// Метод для отрисовки картинки объекта на заданных координатах.
        /// </summary>
        public static void DrawObject(MovingObject obj)
        {
            if (CoordToPos(X, obj.coordX) <= X.Max && CoordToPos(X, obj.coordX) >= X.Min)
                obj.pic.Left = CoordToPos(X, obj.coordX) - obj.width * X.TxtScl / 2;
            if (CoordToPos(Y, obj.coordY) <= Y.Max && CoordToPos(Y, obj.coordY) >= Y.Min)
                obj.pic.Top = CoordToPos(Y, obj.coordY) - obj.height * Y.TxtScl / 2;
        }

        /// <summary>
        /// Метод, задающий картинку двигающемуся объекту.
        /// </summary>
        /// <param name = "obj">Объект, содержащий картинку.</param>
        /// <param name = "col">Название цвета картинки.</param>
        public static void SetObjPicture(MovingObject obj, string col)
        {
            obj.pic = new PictureBox();
            obj.pic.Name = obj.name + " " + obj.id;
            obj.pic.Width = obj.width * X.TxtScl;
            obj.pic.Height = obj.height * Y.TxtScl;
            obj.pic.BackColor = Color.FromName(col);
        }
    }
}
