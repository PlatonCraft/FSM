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
            
        }

        private void FSMForm_Load(object sender, EventArgs e)
        {
            this.KeyDown += new KeyEventHandler(KeyDownEventHandler);

            FSMBase.InitObjects();
            RegisterObjPictures(FSMBase.objList);
        }

        private void KeyDownEventHandler(object sender, KeyEventArgs e)
        {
            FSMBase.rob.Move(e.KeyCode.ToString().ElementAt(0));
        }

        /// <summary>
        /// Метод для регистрации картинок, соответствующих двигающимся объектам.
        /// </summary>
        private void RegisterObjPictures(List<MovingObject> objList)
        {
            foreach (MovingObject obj in objList)
            {
                obj.pic.Click += new System.EventHandler(this.Picture_Click);
                this.Controls.Add(obj.pic);
            }
        }
    }

    static class FSMBase
    {
        public static Robot rob;
        public static Target tar;

        public static List<MovingObject> objList = null;

        /// <summary>
        /// Выполняется при инициализации запуске программы.
        /// Здесь создаются и инициализируются двигающиеся объекты.
        /// </summary>
        public static void InitObjects()
        {
            objList = new List<MovingObject>(0);
            objList.Add(new Robot("Savvy", 0));
            objList.Add(new Target("Portal", 0));
            rob = (Robot)objList[0];
            tar = (Target)objList[1];
            Interface.SetObjPicture(rob, "Red");
            Interface.DrawObject(rob);
            Interface.SetObjPicture(tar, "Green");
            Interface.DrawObject(tar);
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

    /// <summary>
    /// Главный класс конечного автомата.
    /// </summary>
    static class Machine
    {

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
        public PictureBox pic;
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

        public void Move(char dir)
        {
            const char up = 'U', right = 'R', down = 'D', left = 'L';

            switch(dir)
            {
                case up:
                    if (coordY + 1 <= 8)
                        this.coordY++;
                    break;

                case right:
                    if (coordX + 1 <= 8)
                        this.coordX++;
                    break;

                case down:
                    if (coordY - 1 >= -8)
                        this.coordY--;
                    break;

                case left:
                    if (coordX - 1 >= -8)
                        this.coordX--;
                    break;

                default:
                    break;
            }

            Interface.DrawObject(this);
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
}
