using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taxiDesktopProg
{
    public partial class Form_for_Dispatcher : Form
    {
        public static long id;
        Form1 po;
        private string surnamenameDis = "";
        private string nameDispatcher = "";
        private string otchesDisp = "";
        public void printNewOrders()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var list = from ord in db.orders
                           join ad1 in db.addresses on ord.address.id_address equals ad1.id_address
                           join ad2 in db.addresses on ord.address1.id_address equals ad2.id_address
                           join rateOrder in db.orders on ord.id_rate equals rateOrder.id_rate
                           
                           select new
                           {
                               id_order = ord.id_order,
                               place = ad1.city+ " " + ad1.street + " Д" + ad1.house + " " + ad1.enrance,
                               place2 = ad2.city + " " + ad2.street + " Д" + ad2.house + " " + ad2.enrance,
                               order_cost = ord.order_cost,
                               payment_method = ord.payment_method,
                               datetime_placing = ord.datetime_placing_the_order,
                              
                               id_client = ord.client.mobile_phone,
                               id_rate = ord.rate.name,
                               status = ord.status
                           };
                
                dataGridView1.DataSource = list.Where(p=>p.status == "В ожидании").ToList();
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Адрес подачи";
                dataGridView1.Columns[2].HeaderText = "Адрес назначения";
                dataGridView1.Columns[3].HeaderText = "Примерная стоимость";
                dataGridView1.Columns[4].HeaderText = "Способ оплаты";
                dataGridView1.Columns[5].HeaderText = "Дата и время оформления заказа";
                dataGridView1.Columns[6].HeaderText = "Телефона";
                dataGridView1.Columns[7].HeaderText = "Тариф";
                dataGridView1.Columns[8].Visible = false;

                foreach (DataGridViewColumn data in dataGridView1.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        public void printNowTimeOrders()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var list = from ord in db.orders
                           join ad1 in db.addresses on ord.address.id_address equals ad1.id_address
                           join ad2 in db.addresses on ord.address1.id_address equals ad2.id_address
                           join rateOrder in db.orders on ord.id_rate equals rateOrder.id_rate
                           join driv in db.drivers on ord.id_driver equals driv.id_driver
                           select new
                           {
                               id_order = ord.id_order,
                               place = ad1.city + " " + ad1.street + " Д" + ad1.house + " " + ad1.enrance,
                               place2 = ad2.city + " " + ad2.street + " Д" + ad2.house + " " + ad2.enrance,
                               order_cost = ord.order_cost,
                               payment_method = ord.payment_method,
                               datetime_placing = ord.datetime_placing_the_order,
                               id_driver = driv.call_sign,
                               id_client = ord.client.mobile_phone,
                               id_rate = ord.rate.name,
                               status = ord.status
                           };

                dataGridView2.DataSource = list.Where(p => p.status == "Выполнение").ToList();
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].HeaderText = "Адрес подачи";
                dataGridView2.Columns[2].HeaderText = "Адрес назначения";
                dataGridView2.Columns[3].HeaderText = "Примерная стоимость";
                dataGridView2.Columns[4].HeaderText = "Способ оплаты";
                dataGridView2.Columns[5].HeaderText = "Дата и время оформления заказа";
                dataGridView2.Columns[6].HeaderText = "Позывной";
                dataGridView2.Columns[7].HeaderText = "Телефона";
                dataGridView2.Columns[8].HeaderText = "Тариф";
                dataGridView2.Columns[9].Visible = false;

                foreach (DataGridViewColumn data in dataGridView2.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        public void printPreliminaryOrders()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var list = from ord in db.orders
                           join ad1 in db.addresses on ord.address.id_address equals ad1.id_address
                           join ad2 in db.addresses on ord.address1.id_address equals ad2.id_address
                           join rateOrder in db.orders on ord.id_rate equals rateOrder.id_rate
                           select new
                           {
                               id_order = ord.id_order,
                               place = ad1.city + " " + ad1.street + " Д" + ad1.house + " " + ad1.enrance,
                               place2 = ad2.city + " " + ad2.street + " Д" + ad2.house + " " + ad2.enrance,
                               order_cost = ord.order_cost,
                               payment_method = ord.payment_method,
                               datetime_placing = ord.datetime_placing_the_order,
                               id_client = ord.client.mobile_phone,
                               id_rate = ord.rate.name,
                               status = ord.status
                           };

                DateTime dt = DateTime.Now.AddDays(3);

                dataGridView3.DataSource = list.Where(p => p.status == "В ожидании" && p.datetime_placing>DateTime.Now && p.datetime_placing <= dt).Distinct().ToList();
                dataGridView3.Columns[0].Visible = false;
                dataGridView3.Columns[1].HeaderText = "Адрес подачи";
                dataGridView3.Columns[2].HeaderText = "Адрес назначения";
                dataGridView3.Columns[3].HeaderText = "Примерная стоимость";
                dataGridView3.Columns[4].HeaderText = "Способ оплаты";
                dataGridView3.Columns[5].HeaderText = "Дата и время оформления заказа";
                dataGridView3.Columns[6].HeaderText = "Телефона";
                dataGridView3.Columns[7].HeaderText = "Тариф";
                dataGridView3.Columns[8].Visible = false;

                foreach (DataGridViewColumn data in dataGridView3.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        public Form_for_Dispatcher(long id_dis, Form1 po)
        {
            InitializeComponent();
            id = id_dis;
            this.po = po;
            using (Context db = new Context(Form1.connectionString))
            {
                var dispatcher = db.dispatchers.Where(p => p.id_dispatcher == id).FirstOrDefault();
                surnamenameDis = dispatcher.surname;
                nameDispatcher = dispatcher.name;
                otchesDisp = dispatcher.patronymic;
            }
            timer1.Enabled = true;
            printNewOrders();
        }

        private void Form_for_Dispatcher_FormClosed(object sender, FormClosedEventArgs e)
        {
            po.Show();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nowTime.Text = DateTime.Now.ToLongTimeString().ToString() ;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    printNewOrders();
                    break;
                case 1:
                    printNowTimeOrders();
                    break;
                case 2:
                    printPreliminaryOrders();
                    break;

            }
        }
    }
}
