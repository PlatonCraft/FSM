using System;
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
        public PictureBox pic;
    }

    /// <summary>
    /// Класс, описывающий робота и его возможности.
    /// </summary>
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


            Thread.Sleep(700);
            Interface.DrawObject(this);

        }

        public void ReachTarget(Target tar)
        {
            logic.SelectTarget(tar);
            char dir = logic.ChooseDirectionToCurrentTarget();

            while (dir != '\0')
            {
                logic.AttemptMove(dir);
                dir = logic.ChooseDirectionToCurrentTarget();
            }
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

        public void AttemptMove(char dir)
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
            do
            { //перебор направлений движения не закончится, пока мы знаем, что предполагаемое направление наверно.
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

    /// <summary>
    /// Класс, описывающий цель.
    /// </summary>
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
