using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FSMProject
{
    /// <summary>
    /// Главный абстрактный класс для двигающихся объектов.
    /// </summary>
    abstract class MovingObject
    {
        public static List<MovingObject> objList = new List<MovingObject>(0);

        /// <summary>
        /// При вызове этого метода удаляются объекты типов Robot и Target.
        /// Изменяются указатели на адреса массивов, и сборщик должен удалить и объекты.
        /// </summary>
        public static void DeleteAll()
        {
            objList.Clear();
            Robot.robList.Clear();
            Target.tarList.Clear();
            count = 0;

            objList = new List<MovingObject>(0);
            Robot.robList = new List<Robot>(0);
            Target.tarList = new List<Target>(0);
        }

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
        public static List<Robot> robList = new List<Robot>(0); 

        Logic logic;
        public static Action up = new Action("up", 'u', 'Y'), right = new Action("right", 'r', 'X'),
            down = new Action("down", 'd', 'Y'), left = new Action("left", 'l', 'X');

        public void MakeStep(Action dir, int delay)
        {
            logic.RememberCoords();

            if (dir == up && Interface.CoordsReachable(coordX, coordY + 1))
                this.coordY++;
            else if (dir == right && Interface.CoordsReachable(coordX + 1, coordY))
                this.coordX++;
            else if (dir == down && Interface.CoordsReachable(coordX, coordY - 1))
                this.coordY--;
            else if (dir == left && Interface.CoordsReachable(coordX - 1, coordY))
                this.coordX--;

            logic.CalcDistanceToCurrentTarget();

            Interface.RefreshPlane();
            Thread.Sleep(delay);
        }

        public void ReachTarget(Target tar)
        {
            logic.ReachTarget(tar);
        }

        public void ReachTargetsAddOrder(List<Target> tarList)
        {
            foreach (Target tar in tarList)
                logic.ReachTarget(tar);
        }

        public Robot(string name, int id, int coordX, int coordY, int width, int height, string color)
        {
            logic = new Logic(this);

            this.name = name;
            this.id = id;
            this.coordX = coordX;
            this.coordY = coordY;
            this.width = width;
            this.height = height;
            this.color = color;
            
            robList.Add(this);
            objList.Add(this);
        }

        public Robot(string name, int id, string color) : this(name, id, 0, 0, 25, 25, color) { }
        public Robot(int coordX, int coordY) : this("", ++count, coordX, coordY, 25, 25, "Red") { }
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
            curTar.color = "Lime";

            CalcDistanceToCurrentTarget();
            Direction dir = ChooseDirectionToCurrentTarget();

            while (dir != null)
            {
                AttemptMove(dir);
                dir = ChooseDirectionToCurrentTarget();
            }

            curTar.color = "Green";
        }

        public void RememberCoords()
        {
            prevX = rob.coordX;
            prevY = rob.coordY;

        }
        
        public void CalcDistanceToCurrentTarget()
        {
            if (curTar == null)
                return;
            distX = curTar.coordX - rob.coordX;
            distY = curTar.coordY - rob.coordY;

            Interface.RefreshDistance(distX, distY);
        }
        
        Direction ChooseDirectionToCurrentTarget()
        {
            if (distX != 0)
                return (distX > 0) ? right : left;
            else if (distY != 0)
                return (distY > 0) ? up : down;
            else return null;

            //TODO: Нужно проверить, что ничего не зависит от названия направления, а только от соответствий команд и действий.
        }

        void LearnToMove(Direction dir)
        {
            Action step;

            do
            {
                step = ChooseRandStep(dir);
                rob.MakeStep(step, 400);

                Interface.LrnStepAdd(); //плюс шаг, потраченный на обучение

                if (IsLastStepRight(dir.axis))
                    Machine.SetRightOutputForInput(dir.symbol, step.symbol);
                else
                    Machine.SetWrongOutputForInput(dir.symbol, step.symbol);
            }
            while (!Machine.RightOutputExistsForInput(dir.symbol));//пока не подобрано верное соответствие

            Interface.RefreshList(dir.name + " (" + dir.symbol + ')' , step.name + " (" + step.symbol + ')');
        }
        
        void AttemptMove(Direction dir)
        {
            if (Machine.RightOutputExistsForInput(dir.symbol))
                rob.MakeStep((Action)Machine.GetRightOutputForInput(dir.symbol), 400);
            else
                LearnToMove(dir);
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

        bool IsLastStepRight(char axis)
        {
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
        public static List<Target> tarList = new List<Target>(0); 

        public Target(string name, int id, int coordX, int coordY, int width, int height, string color)
        {
            this.name = name;
            this.id = id;
            this.coordX = coordX;
            this.coordY = coordY;
            this.width = width;
            this.height = height;
            this.color = color;

            tarList.Add(this);
            objList.Add(this);
        }

        public Target(string name, int id, string color) : this(name, id, 4, 4, 50, 50, color) { }
        public Target(int coordX, int coordY) : this("", ++count, coordX, coordY, 50, 50, "Green") { }
    }
    
    /// <summary>
    /// Класс, условно описывающий действие, при совершении которого робот двигается в условном направлении.
    /// </summary>
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
}
