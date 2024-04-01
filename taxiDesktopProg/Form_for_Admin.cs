using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public Form_for_Admin(Form1 po)
        {
            InitializeComponent();
            this.po = po;
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
        }
        void listDriver_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Width = startWidth;
            this.Height = startHeight;
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
    }
}
