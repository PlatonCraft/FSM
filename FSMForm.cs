using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSMProject
{
    public partial class FSMForm : Form
    {
        public FSMForm()
        {
            InitializeComponent();
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Robot.D.Down.ToString());
        }

        private void FSMForm_Load(object sender, EventArgs e)
        {
            this.KeyDown += new KeyEventHandler(KeyDownEventHandler);

            Machine.Init();
            RegisterPictures(Interface.picList);
        }

        private void KeyDownEventHandler(object sender, KeyEventArgs e)
        {
            Robot.RobotController(Machine.rob, e.KeyCode.ToString());
        }

        /// <summary>
        /// Метод для регистрации картинок, соответствующих двигающимся объектам.
        /// </summary>
        private void RegisterPictures(List<Picture> picList)
        {
            foreach (Picture pic in picList)
            {
                pic.Click += new System.EventHandler(this.Picture_Click);
                this.Controls.Add(pic);
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
        public static void SetObjPicture(MovingObject obj)
        {
            picList.Add(new Picture(obj.name, obj.id, obj.width * X.TxtScl, obj.height * Y.TxtScl));
            obj.pic = picList[picList.Count-1];
        }
    }

    /// <summary>
    /// Главный класс конечного автомата.
    /// </summary>
    static class Machine
    {
        public static Robot rob;
        public static Target tar;

        /// <summary>
        /// Выполняется при инициализации конечного автомата.
        /// Здесь создаются и инициализируются двигающиеся объекты.
        /// </summary>
        public static void Init() {
            rob = new Robot("Savvy", 0);
            tar = new Target("Portal", 0);
            Interface.SetObjPicture(rob);
            Interface.DrawObject(rob);
            Interface.SetObjPicture(tar);
            Interface.DrawObject(tar);
            rob.pic.BackColor = System.Drawing.Color.Red;
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
        public enum D { Up, Right, Left, Down } //Direction - направление
        public static string[] dir = { "Up", "Right", "Left", "Down" };
        public static int[] delta = { 0, 0 };

        public Robot(string name, int id)
        {
            this.name = name;
            this.id = id;
            this.coordX = 0;
            this.coordY = 0;
            this.width = this.height = 6;
        }

        /// <summary>
        /// Статический метод для управления роботом.
        /// </summary>
        public static void RobotController(Robot rob, string key)
        {
            switch (key)
            {
                case "Up":
                    rob.MoveUp();
                    break;

                case "Right":
                    rob.MoveRight();
                    break;

                case "Down":
                    rob.MoveDown();
                    break;

                case "Left":
                    rob.MoveLeft();
                    break;

                default:
                    break;
            }
        }
        public void MoveUp()
        {
            if(coordY + 1 <= 8)
                this.coordY++;
            Interface.DrawObject(this);
        }
        public void MoveRight()
        {
            if (coordX + 1 <= 8)
            {
                this.coordX++;
                Interface.DrawObject(this);
            }
        }
        public void MoveDown()
        {
            if (coordY - 1 >= -8)
            {
                this.coordY--;
                Interface.DrawObject(this);
            }
        }
        public void MoveLeft()
        {
            if (coordX - 1 >= -8)
            {
                this.coordX--;
                Interface.DrawObject(this);
            }
        }
    }

    class Target : MovingObject
    {
        public Target(string name, int id)
        {
            this.name = name;
            this.id = id;
            this.coordX = -3;
            this.coordY = 4;
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
        public Picture(string name, int id, int width, int height)
        {
            this.Name = name + " " + id;
            this.Size = new System.Drawing.Size(width, height);
        }
    }
}
