using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;

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
        private class PosInfo
        {
            public int Step, Min, Max, Zero;

            public PosInfo(int stp, int min, int max, int zr)
            {
                Step = stp; Min = min; Max = max; Zero = zr;
            }
        }

        private static FSMForm form;
        private static PictureBox picPlane;
        private static ListView lstStates;
        private static List<MovingObject> objList;
        private static PosInfo X; //шаг между координатами, минимальная, максимальная координата, координата нуля. (Всё умножается на масштаб)
        private static PosInfo Y; //-6 из-за того, что в форме координаты из верхнего левого угла, а не левого нижнего

        public static void Init(FSMForm f)
        {
            form = f;
            picPlane = form.getPictureBox();
            lstStates = form.getListView();
            objList = FSMBase.objList;
            X = new PosInfo(25, 0, picPlane.Size.Width, picPlane.Size.Width / 2);
            Y = new PosInfo(25, 0, picPlane.Size.Height, picPlane.Size.Height / 2);
        }

        public static void DrawObjects(Graphics graph)
        {
            if (objList != null && objList.Count != 0)
                foreach (MovingObject obj in objList)
                    graph.FillRectangle(new SolidBrush(Color.FromName(obj.color)), CoordToPos(X, obj.coordX, obj.width), CoordToPos(Y, obj.coordY, obj.height), obj.width, obj.height);
        }

        public static void DrawPlane(Graphics graph)
        {
            Pen penThin = new Pen(Color.Black, 3F);
            Pen penThick = new Pen(Color.Black, 6F);

            for (int i = X.Min / X.Step + 1; i < X.Max / X.Step; i++)//отрисовка вертикальных линий
            {
                if (i == X.Zero / X.Step) //условие для отрисовки толстой линии в центре
                    graph.DrawLine(penThick, i * X.Step, Y.Min / Y.Step, i * X.Step, Y.Max);
                else
                    graph.DrawLine(penThin, i * X.Step, Y.Min / Y.Step, i * X.Step, Y.Max);
            }

            for (int i = Y.Min / Y.Step + 1; i < Y.Max / Y.Step; i++)//отрисовка горизонтальных линий
            {
                if (i == Y.Zero / Y.Step) //условие для отрисовки толстой линии в центре
                    graph.DrawLine(penThick, X.Min / X.Step, i * Y.Step, X.Max, i * Y.Step);
                else
                    graph.DrawLine(penThin, X.Min / X.Step, i * Y.Step, X.Max, i * Y.Step);
            }
        }

        public static void RefreshPlane()
        {
            picPlane.Refresh();
        }

        public static void RefreshList(string direction, string action)
        {
            lstStates.Items.Add(new ListViewItem(new string[] { direction, action }));
            lstStates.Refresh();
        }


        /// <summary>
        /// Метод, преобразующий "человеческие" координаты в пиксели.
        /// </summary>
        /// <param name = "posInfo">Структура с данными о специфике преобразования.</param>
        /// <param name = "coord">"Человеческая" координата.</param>
        private static int CoordToPos(PosInfo posInfo, int coord, int size)
        {
            if (posInfo == X)
                return posInfo.Zero + coord * posInfo.Step - size / 2;
            else
                return posInfo.Zero - coord * posInfo.Step - size / 2;
        }
    }
}
