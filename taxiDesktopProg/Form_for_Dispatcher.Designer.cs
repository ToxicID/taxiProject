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
            this.performedOrders = new System.Windows.Forms.TabPage();
            this.preliminaryOrders = new System.Windows.Forms.TabPage();
            this.completedOrders = new System.Windows.Forms.TabPage();
            this.falseOrders = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.newOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.menuStrip1.Size = new System.Drawing.Size(1248, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // водителиToolStripMenuItem
            // 
            this.водителиToolStripMenuItem.Name = "водителиToolStripMenuItem";
            this.водителиToolStripMenuItem.Size = new System.Drawing.Size(89, 26);
            this.водителиToolStripMenuItem.Text = "Водители";
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            this.клиентыToolStripMenuItem.Size = new System.Drawing.Size(83, 26);
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
            this.tabControl1.Location = new System.Drawing.Point(0, 110);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1248, 465);
            this.tabControl1.TabIndex = 2;
            // 
            // newOrder
            // 
            this.newOrder.Controls.Add(this.dataGridView1);
            this.newOrder.Location = new System.Drawing.Point(4, 25);
            this.newOrder.Name = "newOrder";
            this.newOrder.Padding = new System.Windows.Forms.Padding(3);
            this.newOrder.Size = new System.Drawing.Size(1240, 436);
            this.newOrder.TabIndex = 0;
            this.newOrder.Text = "Новые заказы";
            this.newOrder.UseVisualStyleBackColor = true;
            // 
            // performedOrders
            // 
            this.performedOrders.Location = new System.Drawing.Point(4, 25);
            this.performedOrders.Name = "performedOrders";
            this.performedOrders.Padding = new System.Windows.Forms.Padding(3);
            this.performedOrders.Size = new System.Drawing.Size(1240, 436);
            this.performedOrders.TabIndex = 1;
            this.performedOrders.Text = "Выполняемые";
            this.performedOrders.UseVisualStyleBackColor = true;
            // 
            // preliminaryOrders
            // 
            this.preliminaryOrders.Location = new System.Drawing.Point(4, 25);
            this.preliminaryOrders.Name = "preliminaryOrders";
            this.preliminaryOrders.Padding = new System.Windows.Forms.Padding(3);
            this.preliminaryOrders.Size = new System.Drawing.Size(1240, 436);
            this.preliminaryOrders.TabIndex = 2;
            this.preliminaryOrders.Text = "Предварительные";
            this.preliminaryOrders.UseVisualStyleBackColor = true;
            // 
            // completedOrders
            // 
            this.completedOrders.Location = new System.Drawing.Point(4, 25);
            this.completedOrders.Name = "completedOrders";
            this.completedOrders.Padding = new System.Windows.Forms.Padding(3);
            this.completedOrders.Size = new System.Drawing.Size(1240, 436);
            this.completedOrders.TabIndex = 3;
            this.completedOrders.Text = "Выполненные";
            this.completedOrders.UseVisualStyleBackColor = true;
            // 
            // falseOrders
            // 
            this.falseOrders.Location = new System.Drawing.Point(4, 25);
            this.falseOrders.Name = "falseOrders";
            this.falseOrders.Padding = new System.Windows.Forms.Padding(3);
            this.falseOrders.Size = new System.Drawing.Size(1240, 436);
            this.falseOrders.TabIndex = 4;
            this.falseOrders.Text = "Ложные";
            this.falseOrders.UseVisualStyleBackColor = true;
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
            // 
            // Form_for_Dispatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 575);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.nowTime);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_for_Dispatcher";
            this.Text = "Form_for_Dispatcher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_for_Dispatcher_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.newOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
    }
}