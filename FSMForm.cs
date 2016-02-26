using System;
using System.Collections.Generic;
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

        private void EditText(float dist)
        {
            textBox1.Text = Convert.ToString(dist);
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
}
