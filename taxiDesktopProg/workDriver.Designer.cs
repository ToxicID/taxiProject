namespace taxiDesktopProg
{
    partial class workDriver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.startWorkDriver = new System.Windows.Forms.Button();
            this.endWorkDriver = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1005, 462);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // startWorkDriver
            // 
            this.startWorkDriver.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startWorkDriver.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.startWorkDriver.Location = new System.Drawing.Point(1011, 84);
            this.startWorkDriver.Name = "startWorkDriver";
            this.startWorkDriver.Size = new System.Drawing.Size(167, 49);
            this.startWorkDriver.TabIndex = 19;
            this.startWorkDriver.Text = "Начать смену";
            this.startWorkDriver.UseVisualStyleBackColor = true;
            this.startWorkDriver.Click += new System.EventHandler(this.AddCarBut_Click);
            // 
            // endWorkDriver
            // 
            this.endWorkDriver.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.endWorkDriver.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.endWorkDriver.Location = new System.Drawing.Point(1011, 139);
            this.endWorkDriver.Name = "endWorkDriver";
            this.endWorkDriver.Size = new System.Drawing.Size(196, 49);
            this.endWorkDriver.TabIndex = 20;
            this.endWorkDriver.Text = "Закончить смену";
            this.endWorkDriver.UseVisualStyleBackColor = true;
            this.endWorkDriver.Click += new System.EventHandler(this.endWorkDriver_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(1011, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 22);
            this.label2.TabIndex = 78;
            this.label2.Text = "График работы";
            // 
            // workDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 462);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.endWorkDriver);
            this.Controls.Add(this.startWorkDriver);
            this.Controls.Add(this.dataGridView1);
            this.Name = "workDriver";
            this.Text = "workDriver";
            this.Click += new System.EventHandler(this.workDriver_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button startWorkDriver;
        private System.Windows.Forms.Button endWorkDriver;
        private System.Windows.Forms.Label label2;
    }
}