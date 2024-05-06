namespace taxiDesktopProg
{
    partial class Form_for_Admin
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
            this.button3 = new System.Windows.Forms.Button();
            this.buttonCar = new System.Windows.Forms.Button();
            this.panelDriver = new System.Windows.Forms.Panel();
            this.panelGlavWin = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panelProfiles = new System.Windows.Forms.Panel();
            this.driver_List = new System.Windows.Forms.Button();
            this.Profile = new System.Windows.Forms.Button();
            this.driverBut = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.buttonVio = new System.Windows.Forms.Button();
            this.panelRate = new System.Windows.Forms.Panel();
            this.button13 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.buttonRate = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button6 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.panelDriver.SuspendLayout();
            this.panelGlavWin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelProfiles.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelRate.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Highlight;
            this.button3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.button3.Size = new System.Drawing.Size(249, 40);
            this.button3.TabIndex = 9;
            this.button3.Text = "Добавить водителя";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonCar
            // 
            this.buttonCar.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonCar.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonCar.FlatAppearance.BorderSize = 0;
            this.buttonCar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonCar.Location = new System.Drawing.Point(0, 423);
            this.buttonCar.Name = "buttonCar";
            this.buttonCar.Size = new System.Drawing.Size(251, 39);
            this.buttonCar.TabIndex = 10;
            this.buttonCar.Text = "Автомобили";
            this.buttonCar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCar.UseVisualStyleBackColor = false;
            this.buttonCar.Click += new System.EventHandler(this.buttonCar_Click);
            // 
            // panelDriver
            // 
            this.panelDriver.BackColor = System.Drawing.SystemColors.Highlight;
            this.panelDriver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDriver.Controls.Add(this.button2);
            this.panelDriver.Controls.Add(this.button6);
            this.panelDriver.Controls.Add(this.button3);
            this.panelDriver.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDriver.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelDriver.Location = new System.Drawing.Point(0, 145);
            this.panelDriver.Name = "panelDriver";
            this.panelDriver.Size = new System.Drawing.Size(251, 115);
            this.panelDriver.TabIndex = 5;
            // 
            // panelGlavWin
            // 
            this.panelGlavWin.BackColor = System.Drawing.Color.White;
            this.panelGlavWin.Controls.Add(this.dataGridView1);
            this.panelGlavWin.Controls.Add(this.label1);
            this.panelGlavWin.Controls.Add(this.label2);
            this.panelGlavWin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGlavWin.Location = new System.Drawing.Point(253, 0);
            this.panelGlavWin.Name = "panelGlavWin";
            this.panelGlavWin.Size = new System.Drawing.Size(749, 697);
            this.panelGlavWin.TabIndex = 10;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 89);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(749, 608);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(71, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 40);
            this.label1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(204, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(244, 26);
            this.label2.TabIndex = 4;
            this.label2.Text = "Активные пользователи";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Highlight;
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(0, 39);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(249, 39);
            this.button1.TabIndex = 10;
            this.button1.Text = "Список дистпетчеров";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Highlight;
            this.button4.Dock = System.Windows.Forms.DockStyle.Top;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Name = "button4";
            this.button4.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.button4.Size = new System.Drawing.Size(249, 39);
            this.button4.TabIndex = 9;
            this.button4.Text = "Список клиентов";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panelProfiles
            // 
            this.panelProfiles.BackColor = System.Drawing.SystemColors.Highlight;
            this.panelProfiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProfiles.Controls.Add(this.driver_List);
            this.panelProfiles.Controls.Add(this.button1);
            this.panelProfiles.Controls.Add(this.button4);
            this.panelProfiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProfiles.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelProfiles.Location = new System.Drawing.Point(0, 299);
            this.panelProfiles.Name = "panelProfiles";
            this.panelProfiles.Size = new System.Drawing.Size(251, 124);
            this.panelProfiles.TabIndex = 8;
            // 
            // driver_List
            // 
            this.driver_List.BackColor = System.Drawing.SystemColors.Highlight;
            this.driver_List.Dock = System.Windows.Forms.DockStyle.Top;
            this.driver_List.FlatAppearance.BorderSize = 0;
            this.driver_List.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.driver_List.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.driver_List.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.driver_List.Location = new System.Drawing.Point(0, 78);
            this.driver_List.Name = "driver_List";
            this.driver_List.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.driver_List.Size = new System.Drawing.Size(249, 39);
            this.driver_List.TabIndex = 11;
            this.driver_List.Text = "Список водителей";
            this.driver_List.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.driver_List.UseVisualStyleBackColor = false;
            this.driver_List.Click += new System.EventHandler(this.driver_List_Click);
            // 
            // Profile
            // 
            this.Profile.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Profile.Dock = System.Windows.Forms.DockStyle.Top;
            this.Profile.FlatAppearance.BorderSize = 0;
            this.Profile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Profile.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Profile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Profile.Location = new System.Drawing.Point(0, 260);
            this.Profile.Name = "Profile";
            this.Profile.Size = new System.Drawing.Size(251, 39);
            this.Profile.TabIndex = 6;
            this.Profile.Text = "Профили";
            this.Profile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Profile.UseVisualStyleBackColor = false;
            this.Profile.Click += new System.EventHandler(this.Profile_Click);
            // 
            // driverBut
            // 
            this.driverBut.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.driverBut.Dock = System.Windows.Forms.DockStyle.Top;
            this.driverBut.FlatAppearance.BorderSize = 0;
            this.driverBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.driverBut.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.driverBut.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.driverBut.Location = new System.Drawing.Point(0, 106);
            this.driverBut.Name = "driverBut";
            this.driverBut.Size = new System.Drawing.Size(251, 39);
            this.driverBut.TabIndex = 4;
            this.driverBut.Text = "Водители";
            this.driverBut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.driverBut.UseVisualStyleBackColor = false;
            this.driverBut.Click += new System.EventHandler(this.driverBut_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.AutoScroll = true;
            this.panelMenu.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenu.Controls.Add(this.button7);
            this.panelMenu.Controls.Add(this.buttonVio);
            this.panelMenu.Controls.Add(this.panelRate);
            this.panelMenu.Controls.Add(this.buttonRate);
            this.panelMenu.Controls.Add(this.buttonCar);
            this.panelMenu.Controls.Add(this.panelProfiles);
            this.panelMenu.Controls.Add(this.Profile);
            this.panelMenu.Controls.Add(this.panelDriver);
            this.panelMenu.Controls.Add(this.driverBut);
            this.panelMenu.Controls.Add(this.panel2);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(253, 697);
            this.panelMenu.TabIndex = 9;
            // 
            // buttonVio
            // 
            this.buttonVio.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonVio.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonVio.FlatAppearance.BorderSize = 0;
            this.buttonVio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonVio.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonVio.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonVio.Location = new System.Drawing.Point(0, 604);
            this.buttonVio.Name = "buttonVio";
            this.buttonVio.Size = new System.Drawing.Size(251, 34);
            this.buttonVio.TabIndex = 14;
            this.buttonVio.Text = "Нарушения";
            this.buttonVio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonVio.UseVisualStyleBackColor = false;
            this.buttonVio.Click += new System.EventHandler(this.buttonVio_Click);
            // 
            // panelRate
            // 
            this.panelRate.BackColor = System.Drawing.SystemColors.Highlight;
            this.panelRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRate.Controls.Add(this.button13);
            this.panelRate.Controls.Add(this.button5);
            this.panelRate.Controls.Add(this.button9);
            this.panelRate.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelRate.Location = new System.Drawing.Point(0, 501);
            this.panelRate.Name = "panelRate";
            this.panelRate.Size = new System.Drawing.Size(251, 103);
            this.panelRate.TabIndex = 13;
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.SystemColors.Highlight;
            this.button13.Dock = System.Windows.Forms.DockStyle.Top;
            this.button13.FlatAppearance.BorderSize = 0;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button13.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button13.Location = new System.Drawing.Point(0, 64);
            this.button13.Name = "button13";
            this.button13.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button13.Size = new System.Drawing.Size(249, 32);
            this.button13.TabIndex = 11;
            this.button13.Text = "Изменить тариф";
            this.button13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.Highlight;
            this.button5.Dock = System.Windows.Forms.DockStyle.Top;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button5.Location = new System.Drawing.Point(0, 32);
            this.button5.Name = "button5";
            this.button5.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button5.Size = new System.Drawing.Size(249, 32);
            this.button5.TabIndex = 10;
            this.button5.Text = "Добавить тариф";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.Highlight;
            this.button9.Dock = System.Windows.Forms.DockStyle.Top;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button9.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button9.Location = new System.Drawing.Point(0, 0);
            this.button9.Name = "button9";
            this.button9.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button9.Size = new System.Drawing.Size(249, 32);
            this.button9.TabIndex = 9;
            this.button9.Text = "Список тарифов";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // buttonRate
            // 
            this.buttonRate.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonRate.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonRate.FlatAppearance.BorderSize = 0;
            this.buttonRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonRate.Location = new System.Drawing.Point(0, 462);
            this.buttonRate.Name = "buttonRate";
            this.buttonRate.Size = new System.Drawing.Size(251, 39);
            this.buttonRate.TabIndex = 12;
            this.buttonRate.Text = "Тарифы";
            this.buttonRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRate.UseVisualStyleBackColor = false;
            this.buttonRate.Click += new System.EventHandler(this.buttonRate_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(251, 106);
            this.panel2.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.Highlight;
            this.button6.Dock = System.Windows.Forms.DockStyle.Top;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button6.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button6.Location = new System.Drawing.Point(0, 40);
            this.button6.Name = "button6";
            this.button6.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.button6.Size = new System.Drawing.Size(249, 36);
            this.button6.TabIndex = 11;
            this.button6.Text = "Назначить водителя";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Highlight;
            this.button2.Dock = System.Windows.Forms.DockStyle.Top;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button2.Location = new System.Drawing.Point(0, 76);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.button2.Size = new System.Drawing.Size(249, 36);
            this.button2.TabIndex = 12;
            this.button2.Text = "История поездок";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button7.Dock = System.Windows.Forms.DockStyle.Top;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button7.Location = new System.Drawing.Point(0, 638);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(251, 34);
            this.button7.TabIndex = 15;
            this.button7.Text = "Отчётность";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // Form_for_Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 697);
            this.Controls.Add(this.panelGlavWin);
            this.Controls.Add(this.panelMenu);
            this.Name = "Form_for_Admin";
            this.Text = "Form_for_Admin";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_for_Admin_FormClosed);
            this.panelDriver.ResumeLayout(false);
            this.panelGlavWin.ResumeLayout(false);
            this.panelGlavWin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelProfiles.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.panelRate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonCar;
        private System.Windows.Forms.Panel panelDriver;
        private System.Windows.Forms.Panel panelGlavWin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panelProfiles;
        private System.Windows.Forms.Button Profile;
        private System.Windows.Forms.Button driverBut;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonRate;
        private System.Windows.Forms.Button buttonVio;
        private System.Windows.Forms.Panel panelRate;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button driver_List;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}