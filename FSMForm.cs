using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSMForm
{
    public partial class FSMForm : Form
    {
        Machine machine = new Machine();

        public FSMForm()
        {
            InitializeComponent();
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            //Interface.DrawOnOffset(Interface.picList[1], 1, -1);
        }

        private void FSMForm_Load(object sender, EventArgs e)
        {
            machine.Init();
            RegisterPictures(Interface.picList);
        }

        /// <summary>
        /// Метод для регистрации картинок, соответствующих двигающимся объектам.
        /// </summary>
        private void RegisterPictures(List<Picture> picList)
        {
            foreach (Picture pic in picList)
            {
                ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
                this.SuspendLayout();

                pic.Click += new System.EventHandler(this.Picture_Click);
                this.Controls.Add(pic);

                ((System.ComponentModel.ISupportInitialize)pic).EndInit();
                this.ResumeLayout(false);
            }
        }
    }

    /// <summary>
    /// Класс, обеспечивающий отрисовку объектов.
    /// </summary>
    static class Interface
    {
        /// <summary>
        /// Структура, хранящая переменные, зависящие от текстуры фона.
        /// </summary>
        /// <param name = "Step">"Человеческая" координата.</param>
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

        public static List<Picture> picList = new List<Picture>(0);

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
        /// Метод для отрисовки картинки на заданных координатах.
        /// </summary>
        public static void DrawOnCoords(Picture pic, int coordX, int coordY)
        {
           if (CoordToPos(X, coordX) <= X.Max && CoordToPos(X, coordX) >= X.Min)
                pic.Left = CoordToPos(X, coordX);
           if (CoordToPos(Y, coordY) <= Y.Max && CoordToPos(Y, coordY) >= Y.Min)
                pic.Top = CoordToPos(Y, coordY);
        }

        /// <summary>
        /// Метод для смещения картинки на заданные координаты.
        /// </summary>
        public static void DrawOnOffset(Picture pic, int offsetX, int offsetY)
        {
            
            if (pic.Left + offsetX * X.Step <= X.Max && pic.Left + offsetX * X.Step >= X.Min)
                pic.Left += offsetX * X.Step;
            if (pic.Top + offsetY * Y.Step <= Y.Max && pic.Top + offsetY * Y.Step >= Y.Min)
                pic.Top += offsetY * Y.Step;
        }

        /// <summary>
        /// Метод, задающий картинку двигающемуся объекту.
        /// </summary>
        public static void SetObjPicture(MovingObject obj)
        {
            picList.Add(new Picture(obj.name, obj.id, obj.width * X.TxtScl, obj.height * Y.TxtScl,
                CoordToPos(X, obj.coordX) - obj.width * X.TxtScl / 2, CoordToPos(Y, obj.coordY) - obj.height * Y.TxtScl / 2));
            obj.pic = picList[picList.Count-1];
        }
    }

    /// <summary>
    /// Главный класс конечного автомата.
    /// </summary>
    class Machine
    {
        public Robot rob;
        public Target tar;

        public Machine() { }

        /// <summary>
        /// Выполняется при инициализации конечного автомата.
        /// Здесь создаются и инициализируются двигающиеся объекты.
        /// </summary>
        public void Init() {
            rob = new Robot("Savvy", 0);
            tar = new Target("Portal", 0);
            Interface.SetObjPicture(rob);
            rob.pic.BackColor = System.Drawing.Color.Red;
            Interface.SetObjPicture(tar);
            tar.pic.BackColor = System.Drawing.Color.Green;
        }

    }

    /// <summary>
    /// Главный абстрактный класс для двигающихся объектов.
    /// </summary>
    abstract class MovingObject
    {
        public string name;
        public int id;
        public int coordX;
        public int coordY;
        public int width;
        public int height;
        public Picture pic;

        public MovingObject() { }

    }

    class Robot : MovingObject
    {
        public Robot(string name, int id)
        {
            this.name = name;
            this.id = id;
            this.coordX = 0;
            this.coordY = 0;
            this.width = this.height = 6;
        }
    }

    class Target : MovingObject
    {
        public Target(string name, int id)
        {
            this.name = name;
            this.id = id;
            this.coordX = 0;
            this.coordY = 0;
            this.width = this.height = 8;
        }
    }

    /// <summary>
    /// Класс для мнгновенной инициализации основных полей картинки.
    /// </summary>
    public class Picture : System.Windows.Forms.PictureBox
    {
        /// <summary>
        /// Метод инициализации основных полей картинки.
        /// </summary>
        /// <param name = "name">Имя картинки, аналогичное имени двигающегося объекта.</param>
        /// <param name = "id">ID картинки (объекта).</param>
        /// <param name = "width">Ширина картинки.</param>
        /// <param name = "height">Высота картинки.</param>
        /// <param name = "posX">Пиксельная координата X центра картинки.</param>
        /// <param name = "posY">Пиксельная координата Y центра картинки.</param>
        public Picture(string name, int id, int width, int height, int posX, int posY)
        {
            this.Name = name + " " + id;
            this.Size = new System.Drawing.Size(width, height);
            this.Location = new System.Drawing.Point(posX, posY);
        }
    }
}
