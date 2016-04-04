using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FSMProject
{
    public partial class FSMForm : Form
    {
        public FSMForm()
        {
            InitializeComponent();
        }

        public PictureBox getPictureBox() { return pbCoordPlane; }
        public ListView getListView() { return lvStates; }

        private void Picture_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(new Random().Next(0, 2).ToString());
        }

        private void FSMForm_Load(object sender, EventArgs e)
        {
            this.KeyDown += new KeyEventHandler(KeyDownEventHandler);

            FSMBase.InitObjects();
            Interface.Init(this);
        }

        private void KeyDownEventHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                FSMBase.rob.MakeStep(Robot.up, 0);
            else if (e.KeyCode == Keys.S)
                FSMBase.rob.MakeStep(Robot.down, 0);
            else if (e.KeyCode == Keys.A)
                FSMBase.rob.MakeStep(Robot.left, 0);
            else if (e.KeyCode == Keys.D)
                FSMBase.rob.MakeStep(Robot.right, 0);

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
            rob = new Robot("Savvy", 0, "Red");
            tar = new Target("Portal", 0, "Green");
            objList.Add(tar);
            objList.Add(rob);
        }

        public static void Start()
        {
            rob.ReachTarget(tar);
            //rob.MakeStep(Robot.left);
        }
    }
}
