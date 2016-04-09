namespace FSMProject
{
    partial class FSMForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.pbCoordPlane = new System.Windows.Forms.PictureBox();
            this.lvStatesList = new System.Windows.Forms.ListView();
            this.columnDirection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelStatesList = new System.Windows.Forms.Label();
            this.tbDist = new System.Windows.Forms.TextBox();
            this.labelDist = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.tbLrnSteps = new System.Windows.Forms.TextBox();
            this.labelLrnSteps = new System.Windows.Forms.Label();
            this.btnAddTar = new System.Windows.Forms.Button();
            this.btnAddRob = new System.Windows.Forms.Button();
            this.tbAddY = new System.Windows.Forms.TextBox();
            this.tbAddX = new System.Windows.Forms.TextBox();
            this.labelAddX = new System.Windows.Forms.Label();
            this.labelAddY = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbCoordPlane)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(549, 10);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pbCoordPlane
            // 
            this.pbCoordPlane.BackColor = System.Drawing.Color.White;
            this.pbCoordPlane.Location = new System.Drawing.Point(10, 10);
            this.pbCoordPlane.Name = "pbCoordPlane";
            this.pbCoordPlane.Size = new System.Drawing.Size(500, 500);
            this.pbCoordPlane.TabIndex = 6;
            this.pbCoordPlane.TabStop = false;
            this.pbCoordPlane.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCoordPlane_Paint);
            // 
            // lvStatesList
            // 
            this.lvStatesList.AutoArrange = false;
            this.lvStatesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDirection,
            this.columnAction});
            this.lvStatesList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvStatesList.HideSelection = false;
            this.lvStatesList.LabelWrap = false;
            this.lvStatesList.Location = new System.Drawing.Point(549, 370);
            this.lvStatesList.MultiSelect = false;
            this.lvStatesList.Name = "lvStatesList";
            this.lvStatesList.Scrollable = false;
            this.lvStatesList.Size = new System.Drawing.Size(300, 140);
            this.lvStatesList.TabIndex = 5;
            this.lvStatesList.TabStop = false;
            this.lvStatesList.TileSize = new System.Drawing.Size(160, 30);
            this.lvStatesList.UseCompatibleStateImageBehavior = false;
            this.lvStatesList.View = System.Windows.Forms.View.Details;
            // 
            // columnDirection
            // 
            this.columnDirection.Text = "Direction";
            this.columnDirection.Width = 140;
            // 
            // columnAction
            // 
            this.columnAction.Text = "Action";
            this.columnAction.Width = 140;
            // 
            // labelStatesList
            // 
            this.labelStatesList.AutoSize = true;
            this.labelStatesList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelStatesList.ForeColor = System.Drawing.SystemColors.InfoText;
            this.labelStatesList.Location = new System.Drawing.Point(546, 350);
            this.labelStatesList.Name = "labelStatesList";
            this.labelStatesList.Size = new System.Drawing.Size(105, 17);
            this.labelStatesList.TabIndex = 4;
            this.labelStatesList.Text = "Conclusions list";
            // 
            // tbDist
            // 
            this.tbDist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbDist.HideSelection = false;
            this.tbDist.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tbDist.Location = new System.Drawing.Point(549, 304);
            this.tbDist.Name = "tbDist";
            this.tbDist.ReadOnly = true;
            this.tbDist.Size = new System.Drawing.Size(147, 22);
            this.tbDist.TabIndex = 3;
            this.tbDist.TabStop = false;
            this.tbDist.WordWrap = false;
            // 
            // labelDist
            // 
            this.labelDist.AutoSize = true;
            this.labelDist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDist.ForeColor = System.Drawing.SystemColors.InfoText;
            this.labelDist.Location = new System.Drawing.Point(546, 284);
            this.labelDist.Name = "labelDist";
            this.labelDist.Size = new System.Drawing.Size(150, 16);
            this.labelDist.TabIndex = 2;
            this.labelDist.Text = "Distance (X | Y | Radius)";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(549, 39);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // tbLrnSteps
            // 
            this.tbLrnSteps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbLrnSteps.HideSelection = false;
            this.tbLrnSteps.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tbLrnSteps.Location = new System.Drawing.Point(745, 304);
            this.tbLrnSteps.Name = "tbLrnSteps";
            this.tbLrnSteps.ReadOnly = true;
            this.tbLrnSteps.Size = new System.Drawing.Size(104, 22);
            this.tbLrnSteps.TabIndex = 1;
            this.tbLrnSteps.TabStop = false;
            this.tbLrnSteps.WordWrap = false;
            // 
            // labelLrnSteps
            // 
            this.labelLrnSteps.AutoSize = true;
            this.labelLrnSteps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLrnSteps.ForeColor = System.Drawing.SystemColors.InfoText;
            this.labelLrnSteps.Location = new System.Drawing.Point(742, 285);
            this.labelLrnSteps.Name = "labelLrnSteps";
            this.labelLrnSteps.Size = new System.Drawing.Size(107, 16);
            this.labelLrnSteps.TabIndex = 0;
            this.labelLrnSteps.Text = "Steps in learning";
            // 
            // btnAddTar
            // 
            this.btnAddTar.Location = new System.Drawing.Point(630, 10);
            this.btnAddTar.Name = "btnAddTar";
            this.btnAddTar.Size = new System.Drawing.Size(75, 52);
            this.btnAddTar.TabIndex = 2;
            this.btnAddTar.Text = "Add\r\nTarget";
            this.btnAddTar.UseVisualStyleBackColor = true;
            this.btnAddTar.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAddRob
            // 
            this.btnAddRob.Location = new System.Drawing.Point(774, 10);
            this.btnAddRob.Name = "btnAddRob";
            this.btnAddRob.Size = new System.Drawing.Size(75, 52);
            this.btnAddRob.TabIndex = 5;
            this.btnAddRob.Text = "Add\r\nRobot";
            this.btnAddRob.UseVisualStyleBackColor = true;
            this.btnAddRob.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tbAddY
            // 
            this.tbAddY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbAddY.Location = new System.Drawing.Point(734, 40);
            this.tbAddY.MaxLength = 3;
            this.tbAddY.Name = "tbAddY";
            this.tbAddY.Size = new System.Drawing.Size(34, 22);
            this.tbAddY.TabIndex = 4;
            this.tbAddY.Text = "0";
            this.tbAddY.WordWrap = false;
            this.tbAddY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAdd_KeyPress);
            // 
            // tbAddX
            // 
            this.tbAddX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbAddX.Location = new System.Drawing.Point(734, 12);
            this.tbAddX.MaxLength = 3;
            this.tbAddX.Name = "tbAddX";
            this.tbAddX.Size = new System.Drawing.Size(34, 22);
            this.tbAddX.TabIndex = 3;
            this.tbAddX.Text = "0";
            this.tbAddX.WordWrap = false;
            this.tbAddX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAdd_KeyPress);
            // 
            // labelAddX
            // 
            this.labelAddX.AutoSize = true;
            this.labelAddX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAddX.ForeColor = System.Drawing.SystemColors.InfoText;
            this.labelAddX.Location = new System.Drawing.Point(711, 15);
            this.labelAddX.Name = "labelAddX";
            this.labelAddX.Size = new System.Drawing.Size(17, 16);
            this.labelAddX.TabIndex = 8;
            this.labelAddX.Text = "X";
            // 
            // labelAddY
            // 
            this.labelAddY.AutoSize = true;
            this.labelAddY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAddY.ForeColor = System.Drawing.SystemColors.InfoText;
            this.labelAddY.Location = new System.Drawing.Point(711, 43);
            this.labelAddY.Name = "labelAddY";
            this.labelAddY.Size = new System.Drawing.Size(18, 16);
            this.labelAddY.TabIndex = 9;
            this.labelAddY.Text = "Y";
            // 
            // FSMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(857, 521);
            this.Controls.Add(this.labelAddY);
            this.Controls.Add(this.labelAddX);
            this.Controls.Add(this.tbAddX);
            this.Controls.Add(this.tbAddY);
            this.Controls.Add(this.btnAddRob);
            this.Controls.Add(this.btnAddTar);
            this.Controls.Add(this.labelLrnSteps);
            this.Controls.Add(this.tbLrnSteps);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.labelDist);
            this.Controls.Add(this.tbDist);
            this.Controls.Add(this.labelStatesList);
            this.Controls.Add(this.lvStatesList);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pbCoordPlane);
            this.KeyPreview = true;
            this.Name = "FSMForm";
            this.Text = "FSMProject";
            this.Load += new System.EventHandler(this.FSMForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FSMForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbCoordPlane)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox pbCoordPlane;
        private System.Windows.Forms.ListView lvStatesList;
        private System.Windows.Forms.ColumnHeader columnDirection;
        private System.Windows.Forms.ColumnHeader columnAction;
        private System.Windows.Forms.Label labelStatesList;
        private System.Windows.Forms.TextBox tbDist;
        private System.Windows.Forms.Label labelDist;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox tbLrnSteps;
        private System.Windows.Forms.Label labelLrnSteps;
        private System.Windows.Forms.Button btnAddTar;
        private System.Windows.Forms.Button btnAddRob;
        private System.Windows.Forms.TextBox tbAddY;
        private System.Windows.Forms.TextBox tbAddX;
        private System.Windows.Forms.Label labelAddX;
        private System.Windows.Forms.Label labelAddY;
    }
}

