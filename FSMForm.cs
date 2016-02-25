using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
           // MessageBox.Show(new Random().Next(0, 2).ToString());
        }

        private void FSMForm_Load(object sender, EventArgs e)
        {
            this.KeyDown += new KeyEventHandler(KeyDownEventHandler);

            FSMBase.InitObjects();
            RegisterObjPictures(FSMBase.objList);
        }

        private void KeyDownEventHandler(object sender, KeyEventArgs e)
        {
            string key = e.KeyCode.ToString();
            char k = key.ElementAt(0);

            if (key == "Space")
                FSMBase.Start();

            if (k == 'U' || k == 'R' || k == 'D' || k == 'L')
                FSMBase.rob.MakeStep(k);
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

        public static void Start()
        {
            rob.ReachTarget(tar);
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
        public class State
        {
            public char input, output;
            public bool isRight;

            public State(char input, char output, bool flag)
            {
                this.input = input;
                this.output = output;
                isRight = flag;
            }
        }

        public static List<State> statesList = new List<State>(0);

        #region публичные методы
        public static bool RightOutputExistsForInput(char input)
        {
            return GetRightStateForInput(input) != null;
        }

        public static bool IsOutputWrongForInput(char input, char output)
        {
            List<State> stList = GetStatesForInput(input);
            if (stList == null)
                return false;

            foreach (State st in stList)
            {
                if (st.input == input && st.output == output && !st.isRight)
                    return true;
            }

            return false;
        }

        public static char GetRightOutputForInput(char input)
        {
            State st = GetRightStateForInput(input);

            return st != null ? st.output : '\0';
        }

        public static void SetRightOutputForInput(char input, char output)
        {
            statesList.Add(new State(input, output, true));
        }

        public static void SetWrongOutputForInput(char input, char output)
        {
            statesList.Add(new State(input, output, false));
        }
        #endregion 

        static List<State> GetStatesForInput(char input)
        {
            List<State> retList = new List<State>(0);

            foreach (State st in statesList)
            {
                if (st.input == input)
                    retList.Add(st);
            }
            return retList.Any() ? retList : null;
        }
        
        static State GetRightStateForInput(char input)
        {
            List<State> stList = GetStatesForInput(input);
            if (stList == null)
                return null;

            foreach (State st in stList)
            {
                if (st.input == input && st.isRight)
                    return st;
            }
            
            return null;
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
        public PictureBox pic;
    }

    class Robot : MovingObject
    {
        Logic logic;
        public const char up = 'U', right = 'R', down = 'D', left = 'L';

        public Robot(string name, int id)
        {
            logic = new Logic(this);
            this.name = name;
            this.id = id;
            coordX = 0;
            coordY = 0;
            width = height = 6;
        }

        public void MakeStep(char dir)
        {
            logic.RememberCoords();

            switch (dir)
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
            Thread.Sleep(300);
        }

        public void ReachTarget(Target tar)
        {
            logic.SelectTarget(tar);
            char dir = logic.ChooseDirectionToCurrentTarget();

            while (dir != '\0')
            {
                logic.TryToMove(dir);
                dir = logic.ChooseDirectionToCurrentTarget();
            }
        }
    }

    class Logic
    {
        Robot rob;
        Target curTar;
        
        int distX, distY;
        int prevX, prevY;

        char[] dirArr;

        public Logic(Robot rob)
        {
            this.rob = rob;
            dirArr = new char[] { Robot.up, Robot.right, Robot.down, Robot.left };
        }

        public void SelectTarget(Target tar)
        {
            curTar = tar;
        }

        public char ChooseDirectionToCurrentTarget()
        {
            CalcDistanceToCurrentTarget();

            if (distX != 0)
                return (distX > 0) ? Robot.right : Robot.left;
            else if (distY != 0)
                return (distY > 0) ? Robot.up : Robot.down;
            else return '\0';

            //TODO: Нужно проверить, что ничего не зависит от названия направления, а только от соответствий команд и действий.
        }

        public void TryToMove(char dir)
        {
            char rightOut = Machine.GetRightOutputForInput(dir);


            if (Machine.RightOutputExistsForInput(dir))
                rob.MakeStep(Machine.GetRightOutputForInput(dir));
            else
                LearnToMove(dir);
        }

        public void RememberCoords()
        {
            prevX = rob.coordX;
            prevY = rob.coordY;
        }

        void LearnToMove(char needDir)
        {
            char stepDir;
            char axis;
            axis = (needDir == Robot.right || needDir == Robot.left) ? 'X' : 'Y';

            do
            {
                stepDir = ChooseRandStep(needDir);
                rob.MakeStep(stepDir);
                if (IsLastStepRight(axis))
                    Machine.SetRightOutputForInput(needDir, stepDir);
                else
                    Machine.SetWrongOutputForInput(needDir, stepDir);
            }
            while (!Machine.RightOutputExistsForInput(needDir));
        }

        char ChooseRandStep(char needDir)
        {
            char stepDir = '\0';
            Random rnd = new Random();
            do { //перебор направлений движения не закончится, пока мы знаем, что предполагаемое направление наверно.
                stepDir = dirArr[rnd.Next(0, dirArr.Count())];
            } while (Machine.IsOutputWrongForInput(needDir, stepDir));

            return stepDir;
        }

        void CalcDistanceToCurrentTarget()
        {
            distX = curTar.coordX - rob.coordX;
            distY = curTar.coordY - rob.coordY;
        }

        bool IsLastStepRight(char axis)
        {
            CalcDistanceToCurrentTarget();

            int prevDistX = curTar.coordX - prevX;
            int prevDistY = curTar.coordY - prevY;

            if (Math.Abs(distX) < Math.Abs(prevDistX) && axis == 'X')
                return true;
            else if (Math.Abs(distY) < Math.Abs(prevDistY) && axis == 'Y') //короче, нужно дать ему понять, что приближаться ему нужно было по другой оси
                return true;
            else return false;
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
