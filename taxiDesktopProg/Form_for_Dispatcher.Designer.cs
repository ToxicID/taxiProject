namespace taxiDesktopProg
{
    partial class Form_for_Dispatcher
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.водителиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nowTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.newOrder = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.performedOrders = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.preliminaryOrders = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.completedOrders = new System.Windows.Forms.TabPage();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.falseOrders = new System.Windows.Forms.TabPage();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.newOrderLabel = new System.Windows.Forms.Label();
            this.editOrderLabel = new System.Windows.Forms.Label();
            this.failOrderLabel = new System.Windows.Forms.Label();
            this.directDriverLabel = new System.Windows.Forms.Label();
            this.assignDriver = new System.Windows.Forms.PictureBox();
            this.falseOrder = new System.Windows.Forms.PictureBox();
            this.editOrder = new System.Windows.Forms.PictureBox();
            this.addOrder = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.newOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.performedOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.preliminaryOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.completedOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.falseOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.assignDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.falseOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.водителиToolStripMenuItem,
            this.клиентыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1687, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // водителиToolStripMenuItem
            // 
            this.водителиToolStripMenuItem.Name = "водителиToolStripMenuItem";
            this.водителиToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.водителиToolStripMenuItem.Text = "Водители";
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            this.клиентыToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.клиентыToolStripMenuItem.Text = "Клиенты";
            // 
            // nowTime
            // 
            this.nowTime.AutoSize = true;
            this.nowTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nowTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nowTime.Location = new System.Drawing.Point(12, 61);
            this.nowTime.Name = "nowTime";
            this.nowTime.Size = new System.Drawing.Size(87, 31);
            this.nowTime.TabIndex = 1;
            this.nowTime.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.newOrder);
            this.tabControl1.Controls.Add(this.performedOrders);
            this.tabControl1.Controls.Add(this.preliminaryOrders);
            this.tabControl1.Controls.Add(this.completedOrders);
            this.tabControl1.Controls.Add(this.falseOrders);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 125);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1687, 465);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // newOrder
            // 
            this.newOrder.Controls.Add(this.label1);
            this.newOrder.Controls.Add(this.dataGridView6);
            this.newOrder.Controls.Add(this.dataGridView1);
            this.newOrder.Location = new System.Drawing.Point(4, 25);
            this.newOrder.Name = "newOrder";
            this.newOrder.Padding = new System.Windows.Forms.Padding(3);
            this.newOrder.Size = new System.Drawing.Size(1679, 436);
            this.newOrder.TabIndex = 0;
            this.newOrder.Text = "Новые заказы";
            this.newOrder.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(1332, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 22);
            this.label1.TabIndex = 11;
            this.label1.Text = "Новый заказ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView6
            // 
            this.dataGridView6.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView6.Location = new System.Drawing.Point(1202, 53);
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.ReadOnly = true;
            this.dataGridView6.RowHeadersWidth = 51;
            this.dataGridView6.RowTemplate.Height = 24;
            this.dataGridView6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView6.Size = new System.Drawing.Size(474, 380);
            this.dataGridView6.TabIndex = 1;
            this.dataGridView6.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView6_CellClick);
            this.dataGridView6.DoubleClick += new System.EventHandler(this.dataGridView6_DoubleClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1199, 430);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // performedOrders
            // 
            this.performedOrders.Controls.Add(this.dataGridView2);
            this.performedOrders.Location = new System.Drawing.Point(4, 25);
            this.performedOrders.Name = "performedOrders";
            this.performedOrders.Padding = new System.Windows.Forms.Padding(3);
            this.performedOrders.Size = new System.Drawing.Size(1679, 436);
            this.performedOrders.TabIndex = 1;
            this.performedOrders.Text = "Выполняемые";
            this.performedOrders.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(1199, 430);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // preliminaryOrders
            // 
            this.preliminaryOrders.Controls.Add(this.dataGridView3);
            this.preliminaryOrders.Location = new System.Drawing.Point(4, 25);
            this.preliminaryOrders.Name = "preliminaryOrders";
            this.preliminaryOrders.Padding = new System.Windows.Forms.Padding(3);
            this.preliminaryOrders.Size = new System.Drawing.Size(1679, 436);
            this.preliminaryOrders.TabIndex = 2;
            this.preliminaryOrders.Text = "Предварительные";
            this.preliminaryOrders.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView3.Location = new System.Drawing.Point(3, 3);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.Size = new System.Drawing.Size(1199, 430);
            this.dataGridView3.TabIndex = 2;
            this.dataGridView3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellClick);
            // 
            // completedOrders
            // 
            this.completedOrders.Controls.Add(this.dataGridView4);
            this.completedOrders.Location = new System.Drawing.Point(4, 25);
            this.completedOrders.Name = "completedOrders";
            this.completedOrders.Padding = new System.Windows.Forms.Padding(3);
            this.completedOrders.Size = new System.Drawing.Size(1679, 436);
            this.completedOrders.TabIndex = 3;
            this.completedOrders.Text = "Выполненные";
            this.completedOrders.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            this.dataGridView4.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView4.Location = new System.Drawing.Point(3, 3);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowHeadersWidth = 51;
            this.dataGridView4.RowTemplate.Height = 24;
            this.dataGridView4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView4.Size = new System.Drawing.Size(1199, 430);
            this.dataGridView4.TabIndex = 3;
            this.dataGridView4.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView4_CellClick);
            // 
            // falseOrders
            // 
            this.falseOrders.Controls.Add(this.dataGridView5);
            this.falseOrders.Location = new System.Drawing.Point(4, 25);
            this.falseOrders.Name = "falseOrders";
            this.falseOrders.Padding = new System.Windows.Forms.Padding(3);
            this.falseOrders.Size = new System.Drawing.Size(1679, 436);
            this.falseOrders.TabIndex = 4;
            this.falseOrders.Text = "Ложные";
            this.falseOrders.UseVisualStyleBackColor = true;
            // 
            // dataGridView5
            // 
            this.dataGridView5.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView5.Location = new System.Drawing.Point(3, 3);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.RowHeadersWidth = 51;
            this.dataGridView5.RowTemplate.Height = 24;
            this.dataGridView5.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView5.Size = new System.Drawing.Size(1199, 430);
            this.dataGridView5.TabIndex = 4;
            this.dataGridView5.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView5_CellClick);
            // 
            // newOrderLabel
            // 
            this.newOrderLabel.AutoSize = true;
            this.newOrderLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.newOrderLabel.Location = new System.Drawing.Point(246, 61);
            this.newOrderLabel.Name = "newOrderLabel";
            this.newOrderLabel.Size = new System.Drawing.Size(116, 22);
            this.newOrderLabel.TabIndex = 3;
            this.newOrderLabel.Text = "Новый заказ";
            this.newOrderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editOrderLabel
            // 
            this.editOrderLabel.AutoSize = true;
            this.editOrderLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editOrderLabel.Location = new System.Drawing.Point(473, 46);
            this.editOrderLabel.Name = "editOrderLabel";
            this.editOrderLabel.Size = new System.Drawing.Size(183, 22);
            this.editOrderLabel.TabIndex = 4;
            this.editOrderLabel.Text = "Редактировать заказ";
            this.editOrderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // failOrderLabel
            // 
            this.failOrderLabel.AutoSize = true;
            this.failOrderLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.failOrderLabel.Location = new System.Drawing.Point(773, 46);
            this.failOrderLabel.Name = "failOrderLabel";
            this.failOrderLabel.Size = new System.Drawing.Size(183, 22);
            this.failOrderLabel.TabIndex = 5;
            this.failOrderLabel.Text = "Редактировать заказ";
            this.failOrderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // directDriverLabel
            // 
            this.directDriverLabel.AutoSize = true;
            this.directDriverLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.directDriverLabel.Location = new System.Drawing.Point(1052, 46);
            this.directDriverLabel.Name = "directDriverLabel";
            this.directDriverLabel.Size = new System.Drawing.Size(183, 22);
            this.directDriverLabel.TabIndex = 6;
            this.directDriverLabel.Text = "Редактировать заказ";
            this.directDriverLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // assignDriver
            // 
            this.assignDriver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.assignDriver.Image = global::taxiDesktopProg.Properties.Resources.naznDriver;
            this.assignDriver.Location = new System.Drawing.Point(992, 55);
            this.assignDriver.Name = "assignDriver";
            this.assignDriver.Size = new System.Drawing.Size(54, 37);
            this.assignDriver.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.assignDriver.TabIndex = 10;
            this.assignDriver.TabStop = false;
            this.assignDriver.Click += new System.EventHandler(this.assignDriver_Click);
            // 
            // falseOrder
            // 
            this.falseOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.falseOrder.Image = global::taxiDesktopProg.Properties.Resources._false;
            this.falseOrder.Location = new System.Drawing.Point(713, 55);
            this.falseOrder.Name = "falseOrder";
            this.falseOrder.Size = new System.Drawing.Size(54, 37);
            this.falseOrder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.falseOrder.TabIndex = 9;
            this.falseOrder.TabStop = false;
            this.falseOrder.Click += new System.EventHandler(this.falseOrder_Click);
            // 
            // editOrder
            // 
            this.editOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.editOrder.Image = global::taxiDesktopProg.Properties.Resources.edit;
            this.editOrder.Location = new System.Drawing.Point(413, 55);
            this.editOrder.Name = "editOrder";
            this.editOrder.Size = new System.Drawing.Size(54, 37);
            this.editOrder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.editOrder.TabIndex = 8;
            this.editOrder.TabStop = false;
            // 
            // addOrder
            // 
            this.addOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addOrder.Image = global::taxiDesktopProg.Properties.Resources.add;
            this.addOrder.Location = new System.Drawing.Point(186, 55);
            this.addOrder.Name = "addOrder";
            this.addOrder.Size = new System.Drawing.Size(54, 37);
            this.addOrder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.addOrder.TabIndex = 7;
            this.addOrder.TabStop = false;
            // 
            // Form_for_Dispatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1687, 590);
            this.Controls.Add(this.assignDriver);
            this.Controls.Add(this.falseOrder);
            this.Controls.Add(this.editOrder);
            this.Controls.Add(this.addOrder);
            this.Controls.Add(this.directDriverLabel);
            this.Controls.Add(this.failOrderLabel);
            this.Controls.Add(this.editOrderLabel);
            this.Controls.Add(this.newOrderLabel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.nowTime);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_for_Dispatcher";
            this.Text = "Form_for_Dispatcher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_for_Dispatcher_FormClosed);
            this.Click += new System.EventHandler(this.Form_for_Dispatcher_Click);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.newOrder.ResumeLayout(false);
            this.newOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.performedOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.preliminaryOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.completedOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.falseOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.assignDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.falseOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem водителиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem клиентыToolStripMenuItem;
        private System.Windows.Forms.Label nowTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage newOrder;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage performedOrders;
        private System.Windows.Forms.TabPage preliminaryOrders;
        private System.Windows.Forms.TabPage completedOrders;
        private System.Windows.Forms.TabPage falseOrders;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.Label newOrderLabel;
        private System.Windows.Forms.Label editOrderLabel;
        private System.Windows.Forms.Label failOrderLabel;
        private System.Windows.Forms.Label directDriverLabel;
        private System.Windows.Forms.PictureBox addOrder;
        private System.Windows.Forms.PictureBox editOrder;
        private System.Windows.Forms.PictureBox falseOrder;
        private System.Windows.Forms.PictureBox assignDriver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView6;
    }
}