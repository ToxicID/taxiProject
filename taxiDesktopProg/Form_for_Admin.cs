using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taxiDesktopProg
{
    public partial class Form_for_Admin : Form
    {
        Form1 po;
        private int startWidth;
        private int startHeight;
        //Доработать !!!!!
        private void activeSession()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var disp = db.dispatchers.Select(x=> new {x.id_dispatcher, V = x.activity.ToString(), x.surname,x.name,x.patronymic,x.mobile_phone}) ;
                dataGridView1.DataSource = disp.ToList();
                dataGridView1.Columns[0].HeaderText = "Логин";
                dataGridView1.Columns[1].HeaderText = "Активность";
                dataGridView1.Columns[2].HeaderText = "Фамилия";
                dataGridView1.Columns[3].HeaderText = "Имя";
                dataGridView1.Columns[4].HeaderText = "Отчество";
                dataGridView1.Columns[5].HeaderText = "Номер телефона";
                foreach (DataGridViewColumn data in dataGridView1.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView1.EnableHeadersVisualStyles = false;
            }
        }
        public Form_for_Admin(Form1 po)
        {
            InitializeComponent();
            this.po = po;
            activeSession();
            timer1.Enabled = true;
            customizeDesing();
            startHeight = this.Height;
            startWidth = this.Width;
        }
        private void customizeDesing()
        {
            panelDriver.Visible = false;
            panelProfiles.Visible = false;
            panelRate.Visible = false;

        }
        private void hidesubMenu()
        {
            if (panelDriver.Visible == true)
                panelDriver.Visible = false;
            if (panelProfiles.Visible == true)
                panelProfiles.Visible = false;
            if (panelRate.Visible == true)
                panelRate.Visible = false;
        }
        private void showMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hidesubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelGlavWin.Controls.Add(childForm);
            panelGlavWin.Tag = childForm;
            childForm.BringToFront();
            dataGridView1.Visible = false;
            childForm.Show();
        }

        private void Form_for_Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            po.Show();
        }

        private void driverBut_Click(object sender, EventArgs e)
        {
            showMenu(panelDriver);
        }

        private void Profile_Click(object sender, EventArgs e)
        {
            showMenu(panelProfiles);
        }
        // открытие формы с машинами
        private void buttonCar_Click(object sender, EventArgs e)
        {
            
            listCars fm = new listCars();
            fm.FormClosed += new FormClosedEventHandler(listCars_FormClosed);
            openChildForm(fm);
            this.Width = 1249;
            this.Height = 497;
            hidesubMenu();
        }

        private void buttonRate_Click(object sender, EventArgs e)
        {
            showMenu(panelRate);
        }
        // открытие формы с нарушениями
        private void buttonVio_Click(object sender, EventArgs e)
        {
           
            violationsTable fm = new violationsTable();
            fm.FormClosed += new FormClosedEventHandler(violationsTable_FormClosed);
            openChildForm(fm);
            this.Width = 1098; 
            this.Height = 597;
            hidesubMenu();
        }
        void violationsTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Width = startWidth;
            this.Height = startHeight;
            dataGridView1.Visible = true;
        }
        // открытие формы с клиентами
        private void button4_Click(object sender, EventArgs e)
        {
            listClient fm = new listClient();
            fm.FormClosed += new FormClosedEventHandler(listClient_FormClosed);
            openChildForm(fm);
            this.Width = 596;
            this.Height = 397;
            hidesubMenu();
        }
        void listClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Width = startWidth;
            this.Height = startHeight;
            dataGridView1.Visible = true;
        }
        // открытие формы с просмотром тарифов
        private void button9_Click(object sender, EventArgs e)
        {
            rateList fm = new rateList(0);
            fm.FormClosed += new FormClosedEventHandler(rateList_FormClosed);
            openChildForm(fm);
            this.Width = 794;
            this.Height = 491;
            hidesubMenu();
        }
        void rateList_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Width = startWidth;
            this.Height = startHeight;
            dataGridView1.Visible = true;
        }
        // открытие формы с добавлением тарифа
        private void button5_Click(object sender, EventArgs e)
        {
            rateList fm = new rateList(1);
            fm.FormClosed += new FormClosedEventHandler(rateList_FormClosed);
            openChildForm(fm);
            this.Width = 814;
            this.Height = 491;
            hidesubMenu();
        }
        // открытие формы с редактированием тарифа
        private void button13_Click(object sender, EventArgs e)
        {
            rateList fm = new rateList(2);
            fm.FormClosed += new FormClosedEventHandler(rateList_FormClosed);
            openChildForm(fm);
            this.Width = 814;
            this.Height = 491;
            hidesubMenu();

        }
    
     
        void listCars_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Width = startWidth;
            this.Height = startHeight;
            dataGridView1.Visible = true;
        }
        void listDriver_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Width = startWidth;
            this.Height = startHeight;
            dataGridView1.Visible = true;
        }

        private void driver_List_Click(object sender, EventArgs e)
        {
            listDriver fm = new listDriver();
            fm.FormClosed += new FormClosedEventHandler(listDriver_FormClosed);
            openChildForm(fm);
            this.Width = 1245;
            this.Height = 500;
            hidesubMenu();
        }
        void addNewDriver_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Width = startWidth;
            this.Height = startHeight;
            dataGridView1.Visible = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            addNewDriver fm = new addNewDriver();
            fm.FormClosed += new FormClosedEventHandler(addNewDriver_FormClosed);
            openChildForm(fm);
            this.Width = 645;
            this.Height = 470;
            hidesubMenu();
        }
        void listDispatcher_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Width = startWidth;
            this.Height = startHeight;
            dataGridView1.Visible = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listDispatcher fm = new listDispatcher();
            fm.FormClosed += new FormClosedEventHandler(listDispatcher_FormClosed);
            openChildForm(fm);
            this.Width = 945;
            this.Height = 470;
            hidesubMenu();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            carForDriver fm = new carForDriver();
            fm.FormClosed += new FormClosedEventHandler(carForDriver_FormClosed);
            openChildForm(fm);
            this.Width = 1255; 
            this.Height = 546;
            hidesubMenu();
        }
        void carForDriver_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Width = startWidth;
            this.Height = startHeight;
            dataGridView1.Visible = true;
        }

    

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = $"dispatcher_{e.Value}";
            }
            if (e.ColumnIndex == 1)
            {
                if(e.Value != null)
                {
                    if ( Convert.ToBoolean(this.dataGridView1.Rows[e.RowIndex].Cells[1].Value) == true)
                    {
                        e.Value = "Актив";
                        e.CellStyle.ForeColor = Color.Green;
                    }
                    else
                    {
                        e.Value = "Неактивный";
                        e.CellStyle.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            activeSession();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            historyDriver fm = new historyDriver(true);
            fm.FormClosed += new FormClosedEventHandler(historyDriver_FormClosed);
            openChildForm(fm);
            this.Width = 1195;
            this.Height = 546;
            hidesubMenu();
        }
        void historyDriver_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Width = startWidth;
            this.Height = startHeight;
            dataGridView1.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            returnFromDirector fm = new returnFromDirector();
            fm.FormClosed += new FormClosedEventHandler(returnFromDirector_FormClosed);
            openChildForm(fm);
            this.Width = 1020;
            this.Height = 410;
            hidesubMenu();
        }
        void returnFromDirector_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Width = startWidth;
            this.Height = startHeight;
            dataGridView1.Visible = true;
        }
    }
}
