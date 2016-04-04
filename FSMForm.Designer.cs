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
            this.lvStates = new System.Windows.Forms.ListView();
            this.columnDirection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pbCoordPlane)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(730, 38);
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
            this.pbCoordPlane.TabIndex = 4;
            this.pbCoordPlane.TabStop = false;
            this.pbCoordPlane.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCoordPlane_Paint);
            // 
            // lvStates
            // 
            this.lvStates.AutoArrange = false;
            this.lvStates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDirection,
            this.columnAction});
            this.lvStates.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvStates.HideSelection = false;
            this.lvStates.LabelWrap = false;
            this.lvStates.Location = new System.Drawing.Point(549, 373);
            this.lvStates.MultiSelect = false;
            this.lvStates.Name = "lvStates";
            this.lvStates.Scrollable = false;
            this.lvStates.Size = new System.Drawing.Size(280, 137);
            this.lvStates.TabIndex = 1;
            this.lvStates.TabStop = false;
            this.lvStates.TileSize = new System.Drawing.Size(160, 30);
            this.lvStates.UseCompatibleStateImageBehavior = false;
            this.lvStates.View = System.Windows.Forms.View.Details;
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
            // FSMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(971, 557);
            this.Controls.Add(this.lvStates);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pbCoordPlane);
            this.KeyPreview = true;
            this.Name = "FSMForm";
            this.Text = "FSMProject";
            this.Load += new System.EventHandler(this.FSMForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCoordPlane)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox pbCoordPlane;
        private System.Windows.Forms.ListView lvStates;
        private System.Windows.Forms.ColumnHeader columnDirection;
        private System.Windows.Forms.ColumnHeader columnAction;
    }
}

