using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FSMProject
{
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
        public string color;
        public static int count = 0;
    }

    /// <summary>
    /// Класс, описывающий робота и его возможности.
    /// </summary>
    class Robot : MovingObject
    {
        Logic logic;
        public static Action up = new Action("up", 'u', 'Y'), right = new Action("right", 'r', 'X'),
            down = new Action("down", 'd', 'Y'), left = new Action("left", 'l', 'X');
        const int max = 10;

        public Robot(string name, int id, int coordX, int coordY, int width, int height, string color)
        {
            logic = new Logic(this);
            this.name = name;
            this.id = id;
            this.coordX = coordX;
            this.coordX = coordY;
            this.width = width;
            this.height = height;
            this.color = color;
        }

        public Robot(string name, int id, string color) : this(name, id, 0, 0, 25, 25, color) { }

        public void MakeStep(Action dir, int delay)
        {
            logic.RememberCoords();

            if (dir == up && coordY + 1 <= max)
                this.coordY++;
            else if (dir == right && coordX + 1 <= max)
                this.coordX++;
            else if (dir == down && coordY - 1 >= -max)
                this.coordY--;
            else if (dir == left && coordX - 1 >= -max)
                this.coordX--;

            Interface.RefreshPlane();
            Thread.Sleep(delay);
        }

        public void ReachTarget(Target tar)
        {
            logic.ReachTarget(tar);
        }
    }

    class Action : Direction
    {
        static List<Action> actList = new List<Action>(0); //facepalm

        public Action(string name, char symbol, char axis) : base("Move " + name, symbol, axis)
        {
            actList.Add(this);
        }


        public static explicit operator Action(char sym) //и вот надо было менять char на целый новый тип?!!!
        {
            foreach (Action act in actList)
                if (act.symbol == sym)
                    return act;
            return null;
        }
    }

    /// <summary>
    /// Класс, условно описывающий направление.
    /// </summary>
    class Direction
    {
        public string name;
        public char symbol;
        public char axis;

        public Direction(string name, char symbol, char axis)
        {
            this.name = name;
            this.symbol = symbol;
            this.axis = axis;
        }
    }

    /// <summary>
    /// Класс, реализующий логический и вычислительный функционал робота.
    /// </summary>
    class Logic
    {
        Robot rob;
        Target curTar;

        int distX, distY;
        int prevX, prevY;

        Action[] actArr;

        static Direction up = new Direction("Up", 'w', 'Y'), down = new Direction("Down", 's', 'Y'),
            left = new Direction("Left", 'a', 'X'), right = new Direction("Right", 'd', 'X'); 

        public Logic(Robot rob)
        {
            this.rob = rob;
            actArr = new Action[] { Robot.up, Robot.right, Robot.down, Robot.left };
        }
        
        public void ReachTarget(Target tar)
        {
            curTar = tar;
            Direction dir = ChooseDirectionToCurrentTarget();

            while (dir != null)
            {
                AttemptMove(dir);
                dir = ChooseDirectionToCurrentTarget();
            }
        }

        public Direction ChooseDirectionToCurrentTarget()
        {
            CalcDistanceToCurrentTarget();

            if (distX != 0)
                return (distX > 0) ? right : left;
            else if (distY != 0)
                return (distY > 0) ? up : down;
            else return null;

            //TODO: Нужно проверить, что ничего не зависит от названия направления, а только от соответствий команд и действий.
        }

        public void AttemptMove(Direction dir)
        {
            if (Machine.RightOutputExistsForInput(dir.symbol))
                rob.MakeStep((Action)Machine.GetRightOutputForInput(dir.symbol), 400);
            else
                LearnToMove(dir);
        }

        public void RememberCoords()
        {
            prevX = rob.coordX;
            prevY = rob.coordY;

        }

        void LearnToMove(Direction dir)
        {
            Action step;

            do
            {
                step = ChooseRandStep(dir);
                rob.MakeStep(step, 400);
                if (IsLastStepRight(dir.axis))
                    Machine.SetRightOutputForInput(dir.symbol, step.symbol);
                else
                    Machine.SetWrongOutputForInput(dir.symbol, step.symbol);
            }
            while (!Machine.RightOutputExistsForInput(dir.symbol));

            Interface.RefreshList(dir.name + " (" + dir.symbol + ')' , step.name + " (" + step.symbol + ')');
        }

        Action ChooseRandStep(Direction dir)
        {
            Action step = null;
            Random rnd = new Random();
            do
            { //перебор направлений движения не закончится, пока мы знаем, что предполагаемое направление наверно.
                step = actArr[rnd.Next(0, actArr.Count())];
            } while (Machine.IsOutputWrongForInput(dir.symbol, step.symbol));

            return step;
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

    /// <summary>
    /// Класс, описывающий цель.
    /// </summary>
    class Target : MovingObject
    {
        public Target(string name, int id, int coordX, int coordY, int width, int height, string color)
        {
            this.name = name;
            this.id = id;
            this.coordX = coordX;
            this.coordY = coordY;
            this.width = width;
            this.height = height;
            this.color = color;
        }

        public Target(string name, int id, string color) : this(name, id, 4, 4, 50, 50, color) { }
        public Target() : this("", ++count, 4, 4, 50, 50, "Green") { }
    }
}
