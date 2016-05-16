using System;
using System.Linq;
using System.Windows.Forms;

namespace FSMProject
{
    public partial class FSMForm : Form
    {
        public FSMForm()
        {
            InitializeComponent();
        }

        public PictureBox getPicPlane() { return pbCoordPlane; }
        public ListView getLstStates() { return lvStatesList; }
        public TextBox getTxtDist() { return tbDist; }
        public TextBox getTxtLrnSteps() { return tbLrnSteps; }

        private void FSMForm_Load(object sender, EventArgs e)
        {
            Interface.Init(this);
            FSMBase.InitObjects();

            FSMBase.Test(this);
        }

        private void FSMForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                FSMBase.curRob.MakeStep(Robot.up, 0);
            else if (e.KeyCode == Keys.S)
                FSMBase.curRob.MakeStep(Robot.down, 0);
            else if (e.KeyCode == Keys.A)
                FSMBase.curRob.MakeStep(Robot.left, 0);
            else if (e.KeyCode == Keys.D)
                FSMBase.curRob.MakeStep(Robot.right, 0);
        }

        private void pbCoordPlane_Paint(object sender, PaintEventArgs e)
        {
            Interface.DrawPlane(e.Graphics);
            Interface.DrawObjects(e.Graphics);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            FSMBase.Start();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            FSMBase.Reset();
        }

        private void tbAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            //если введена не цифра, не backspace, не минус, то событие обработано (изменять текст не нужно)
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '-')
            {
                e.Handled = true;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int addX, addY;

            try
            {
                addX = Convert.ToInt32(tbAddX.Text);
                addY = Convert.ToInt32(tbAddY.Text);

                if (Interface.CoordsReachable(addX, addY))
                    if (((Button)sender).Equals(btnAddRob))
                        FSMBase.AddRobot(addX, addY);
                    else FSMBase.AddTarget(addX, addY);
                else throw new FormatException();//ну раз он туда попадёт при ошибке, то почему бы и нет?
            }
            catch (FormatException)
            {
                MessageBox.Show("Coords should be integer numbers\nunder " + Interface.maxCoord + " and above -" + Interface.maxCoord);
            }
           
        }
    }

    static class FSMBase
    {
        public static Robot curRob;

        public static void InitObjects()
        {
            curRob = new Robot("Savvy", 0, "Pink");
            Interface.RefreshPlane();
        }

        public static void AddRobot(int coordX, int coordY)
        {
            new Robot(coordX, coordY);
            Interface.RefreshPlane();
        }

        public static void AddTarget(int coordX, int coordY)
        { 
            new Target(coordX, coordY);
            Interface.RefreshPlane();
        }

        public static void Start()
        {
            curRob.ReachTargetsAddOrder(Target.tarList);
        }

        public static void Test(FSMForm form)
        {
            TextBox txtLrnSteps = form.getTxtLrnSteps();
            int[] steps = new int[1000];

            for (int i = 0; i < 1000; i++)
            {
                new Target(4, 4); new Target(-4, 4); new Target(-4, -4); new Target(4, -4);
                Interface.RefreshPlane();
                curRob.ReachTargetsAddOrder(Target.tarList);
                steps[i] = Convert.ToInt32(txtLrnSteps.Text);
                Reset();
            }

            MessageBox.Show(steps.Average().ToString());
        }
        
        public static void Reset()
        {
            Machine.Reset();
            MovingObject.DeleteAll();
            Interface.Reset();

            InitObjects();
        }
    }
}
