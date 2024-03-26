using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taxiDesktopProg
{
    public partial class Form_for_Dispatcher : Form
    {
        public static long id;
        private long? DataGridIndex = null;
        Form1 po;
        
        //Метод вывода в dataGrid
        public void printNewOrders()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var list = from ord in db.orders
                           join ad1 in db.addresses on ord.place_of_departure equals ad1.id_address
                           join ad2 in db.addresses on ord.destination equals ad2.id_address
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
                DateTime dt = DateTime.Now;
                DateTime dtd = DateTime.Now.Add(new TimeSpan(-1,0,0));
                DateTime dh = DateTime.Now.AddMinutes(5);

                dataGridView1.DataSource = list.Where(p => p.status == "В ожидании" && p.datetime_placing.Year == dt.Year
                                            && p.datetime_placing.Month == dt.Month && p.datetime_placing.Day == dt.Day
                                            && p.datetime_placing <= dh
                                            && p.datetime_placing >= dtd
                                            ).Distinct().ToList();
              
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
                
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView1.EnableHeadersVisualStyles = false;
              
            }
        }
        //Метод вывода в dataGrid
        public void printNowTimeOrders()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var list = from ord in db.orders
                           join ad1 in db.addresses on ord.place_of_departure equals ad1.id_address
                           join ad2 in db.addresses on ord.destination equals ad2.id_address
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

                dataGridView2.DataSource = list.Where(p => p.status == "Выполнение").Distinct().ToList();
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
                dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView2.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView2.EnableHeadersVisualStyles = false;
            }
        }
        //Метод вывода в dataGrid
        public void printPreliminaryOrders()
        {
            using (Context db = new Context(Form1.connectionString))
            {
            
                
                var list = from ord in db.orders
                           join ad1 in db.addresses on ord.place_of_departure equals ad1.id_address
                           join ad2 in db.addresses on ord.destination equals ad2.id_address
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

               
                DateTime dh = DateTime.Now.AddMinutes(5.0);

                dataGridView3.DataSource = list.Where(p => p.status == "В ожидании" && p.datetime_placing.CompareTo(DateTime.Now) > 0 && p.datetime_placing >= dh
                                                      ).Distinct().ToList();
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
                dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView3.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView3.EnableHeadersVisualStyles = false;
            }
        }
        //Метод вывода в dataGrid
        public void printOrders(string name, DataGridView dataName)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var list = from ord in db.orders
                           join ad1 in db.addresses on ord.place_of_departure equals ad1.id_address
                           join ad2 in db.addresses on ord.destination equals ad2.id_address
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
                               datetime_complete_order = ord.order_completion_datetime,
                               id_driver = driv.call_sign,
                               id_client = ord.client.mobile_phone,
                               id_rate = ord.rate.name,
                               status = ord.status
                           };

                dataName.DataSource = list.Where(p => p.status == name).Distinct().ToList();
                dataName.Columns[0].Visible = false;
                dataName.Columns[1].HeaderText = "Адрес подачи";
                dataName.Columns[2].HeaderText = "Адрес назначения";
                dataName.Columns[3].HeaderText = "Примерная стоимость";
                dataName.Columns[4].HeaderText = "Способ оплаты";
                dataName.Columns[5].HeaderText = "Дата и время оформления заказа";
                dataName.Columns[6].HeaderText = "Дата и время завершения заказа";
                dataName.Columns[7].HeaderText = "Позывной";
                dataName.Columns[8].HeaderText = "Телефона";
                dataName.Columns[9].HeaderText = "Тариф";
                dataName.Columns[10].Visible = false;
                foreach (DataGridViewColumn data in dataName.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataName.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataName.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataName.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataName.EnableHeadersVisualStyles = false;

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
                
            }
            timer1.Enabled = true;
            printNewOrders();
            
            label1.Text = "";
            dataGridView6.Visible = false;
            label3.Visible = false;
            maskedTextBox1.Visible = false;
            button1.Visible = false;


        }

        private void Form_for_Dispatcher_FormClosed(object sender, FormClosedEventArgs e)
        {
            po.Show();
            
        }
        //Таймер
        private void timer1_Tick(object sender, EventArgs e)
        {
            nowTime.Text = DateTime.Now.ToLongTimeString().ToString() ;
        }
        private int tabPageIndex = 0;
        //Вывод данных в dataGrid при переходе в определённую вкладку

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            tabPageIndex = e.TabPageIndex;
            switch (tabPageIndex)
            {
                case 0:
                    printNewOrders();
                    DataGridIndex = null;
                    break;
                case 1:
                    printNowTimeOrders();
                    label3.Visible = false;
                    maskedTextBox1.Visible = false;
                    button1.Visible = false;
                    DataGridIndex = null;
                    break;
                case 2:
                    printPreliminaryOrders();
                    DataGridIndex = null;
                    break;
                case 3:
                    printOrders("Завершён", dataGridView4);
                    DataGridIndex = null;
                    break;
                case 4:
                    printOrders("Ложный",dataGridView5);
                    DataGridIndex = null;
                    break;
                      

            }
        }

        private void Form_for_Dispatcher_Click(object sender, EventArgs e)
        {
            DataGridIndex = null;
        }
        //Метож для получения id
        private void getIndexDataGrid(DataGridView data, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridIndex = (long)data.Rows[e.RowIndex].Cells[0].Value;
            }
            catch
            {
                DataGridIndex = null;
            }
        } 
        //Получение id и запись в DataGrigindex из всех datagrid
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getIndexDataGrid(dataGridView1, e);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getIndexDataGrid(dataGridView2, e);
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getIndexDataGrid(dataGridView3, e);
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getIndexDataGrid(dataGridView4, e);
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getIndexDataGrid(dataGridView5, e);
        }

        //Вывод свободных водителей
        private void assignDriver_Click(object sender, EventArgs e)
        {
            if (tabPageIndex == 0)
            {
                label1.Text = "Назначить водителя";
                dataGridView6.Visible = true;
                using (Context db = new Context(Form1.connectionString)) {
                    var list = db.drivers.Where(p => p.status == "Свободен" && p.driver_readiness == "Готов").ToList();
                    dataGridView6.DataSource = list;
                    dataGridView6.Columns[0].Visible = false;
                    dataGridView6.Columns[1].Visible = false;
                    dataGridView6.Columns[2].Visible = false;
                    dataGridView6.Columns[3].HeaderText = "Позывной";
                    dataGridView6.Columns[4].Visible = false;
                    dataGridView6.Columns[5].Visible = false;
                    dataGridView6.Columns[6].Visible = false;
                    dataGridView6.Columns[7].Visible = false;
                    dataGridView6.Columns[8].Visible = false;
                    dataGridView6.Columns[9].Visible = false;
                    dataGridView6.Columns[10].Visible = false;
                    dataGridView6.Columns[11].Visible = false;
                    dataGridView6.Columns[12].Visible = false;
                    dataGridView6.Columns[13].Visible = false;
                    dataGridView6.Columns[14].Visible = false;
                    dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                    dataGridView6.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                    dataGridView6.EnableHeadersVisualStyles = false;
                }
                
            }
            else
            {
                MessageBox.Show("Назначить автомобиль можно во вкладке \"Новые заказы\", чтобы выполнить эту функцию перейдите в соответствующую вкладку");
                return;
            }
        }
        private void directDriver()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                if (DataGridIndex != null && DataGrid6Index != null)
                {
                    DialogResult result = MessageBox.Show("Назначить этого водителя на выбранный заказ?", "Назначение",
                                                                                       MessageBoxButtons.YesNo,
                                                                                       MessageBoxIcon.Information);
                    if (result == DialogResult.No) return;
                    var order = db.orders.Where(p => p.id_order == DataGridIndex).FirstOrDefault();
                    var driver = db.drivers.Where(p => p.id_driver == DataGrid6Index).FirstOrDefault();


                    order.id_driver = driver.id_driver;
                    driver.status = "Занят";
                    order.status = "Выполнение";
                    db.SaveChanges();
                    dataGridView6.Visible = false;
                    label1.Text = "";
                    MessageBox.Show($"Водитель {driver.call_sign} отправлен на заказ");

                }
                DataGrid6Index = null;
                DataGridIndex = null;
                if (tabPageIndex == 0) printNewOrders();
                if (tabPageIndex == 1) printNowTimeOrders();

            }

        }
        //Направление автомобиля на заказ
        private void dataGridView6_DoubleClick(object sender, EventArgs e)
        {
            directDriver();
        }
        //Получение id у водителя
        private long? DataGrid6Index = null;
        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGrid6Index = (long)dataGridView6.Rows[e.RowIndex].Cells[0].Value;
            }
            catch
            {
                DataGrid6Index = null;
            }
        }
        //Добавление заказа в ложный
        private void falseOrder_Click(object sender, EventArgs e)
        {
            if (tabPageIndex == 1)
            {
                using (Context db = new Context(Form1.connectionString))
                {
                    if (DataGridIndex != null)
                    {
                        DialogResult result = MessageBox.Show("Перенести заказ в ложные?", "Перенос",
                                                                                           MessageBoxButtons.YesNo,
                                                                                           MessageBoxIcon.Information);
                        if (result == DialogResult.No) return;
                        var order = db.orders.Where(p => p.id_order == DataGridIndex).FirstOrDefault();
                       

                        order.status = "Ложный";
                        if (order.order_completion_datetime == null)
                            order.order_completion_datetime = DateTime.Now;

                        DateTime dt = DateTime.Now.AddYears(-1);

                        var lastOrderClient = db.orders.Where(p => p.id_client == order.id_client);
                        if (lastOrderClient.Where(p=>p.status == "Ложный").Count() > 2 &&  lastOrderClient.ToList().LastOrDefault().order_completion_datetime.Value.Year>dt.Year)
                        {

                            
                            var cl = db.clients.Where(p => p.id_client == order.id_client).FirstOrDefault();
                            cl.blacklist = true;
                        }
                        var driver = db.drivers.Where(p => p.id_driver == order.id_driver).FirstOrDefault();
                        driver.status = "Свободен";
                        db.SaveChanges();
                        dataGridView6.Visible = false;
                        label1.Text = "";
                        MessageBox.Show($"Заказ был перенесён в ложные");

                    }
                    DataGrid6Index = null;
                    DataGridIndex = null;
                    if (tabPageIndex == 0) printNewOrders();
                    if (tabPageIndex == 1) printNowTimeOrders();

                }
            }
            else
            {
                MessageBox.Show("Заказ уже завершён или ещё не началась его обработка");
                return;
            }
        }

        private void addOrder_Click(object sender, EventArgs e)
        {
            addOrEditOrders fm = new addOrEditOrders();
            fm.ShowDialog();
            printNewOrders();

        }
        private void orderComplete()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var order = db.orders.Where(p => p.id_order == DataGridIndex).FirstOrDefault();
                order.status = "Завершён";
                var driver = db.drivers.Where(p => p.id_driver == order.id_driver).FirstOrDefault();
                driver.status = "Свободен";
                db.SaveChanges();
                MessageBox.Show($"Заказ был завершён");
            }
        }
        private void orderComplete(string stringTime)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var str = stringTime.Split(':');
                if (string.IsNullOrWhiteSpace(str[0]) || string.IsNullOrWhiteSpace(str[1]))
                {
                    MessageBox.Show("Заполните поле","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               
                
                var minTime = int.Parse(str[0]);
                var order = db.orders.Where(p => p.id_order == DataGridIndex).FirstOrDefault();
                order.status = "Завершён";
                minTime -= 10;
                    if(minTime>0)
                    order.order_cost += order.rate.cost_downtime * minTime;
                var driver = db.drivers.Where(p => p.id_driver == order.id_driver).FirstOrDefault();
                driver.status = "Свободен";
                db.SaveChanges();
                MessageBox.Show($"Заказ был завершён\n" +
                                $"Итоговая стоимость заказа >> {order.order_cost}");
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                if (DataGridIndex != null)
                {
                    DialogResult result = MessageBox.Show("Завершить выбранный заказа?", "Завершение",
                                                                                       MessageBoxButtons.YesNo,
                                                                                       MessageBoxIcon.Information);
                    if (result == DialogResult.No) return;
                   
                    DialogResult res = MessageBox.Show("Водитель ждал клиента?", "Подсчёт",
                                                                                  MessageBoxButtons.YesNo,
                                                                                  MessageBoxIcon.Information);
                    if (res == DialogResult.No)
                    {
                        orderComplete();
                        DataGridIndex = null;
                        printNowTimeOrders();
                    }
                    else
                    {
                        label3.Visible = true;
                        maskedTextBox1.Visible = true;
                        button1.Visible= true;
                    }
                    
                }
            }
        }
        private void editOrders()
        {
            if (tabPageIndex == 0 || tabPageIndex == 2)
            {
                if (DataGridIndex != null)
                {
                    addOrEditOrders fm = new addOrEditOrders(DataGridIndex, tabPageIndex);
                    fm.ShowDialog();

                    if (tabPageIndex == 0)
                        printNewOrders();
                    else if (tabPageIndex == 2)
                        printPreliminaryOrders();
                }
                else
                {
                    MessageBox.Show("Выберите заказ, который нужно редактировать, после чего нажмите на кнопку редактировать заказ," +
                                    " либо дважды нажмите на заказ, который нужно редактировать");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Заказ может быть изменён либо во вкладке \"Новые заказы\", либо \"Предварительные\"\n" +
                    "Чтобы поспользоваться функцией редактирования заказа, перейдите в любую из вышеописанных вкладок и выберите заказ, который нужно редактировать");
                return;
            }
        }
        private void editOrder_Click(object sender, EventArgs e)
        {
            editOrders();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            editOrders();
        }

        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {
            editOrders();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DataGridIndex != null)
            {
                DialogResult result = MessageBox.Show("Завершить выбранный заказа?", "Завершение",
                                                                                   MessageBoxButtons.YesNo,
                                                                                   MessageBoxIcon.Information);
                if (result == DialogResult.No) return;
                orderComplete(maskedTextBox1.Text);
                DataGridIndex = null;
                printNowTimeOrders();
            
                label3.Visible = false;
                maskedTextBox1.Visible = false;
                button1.Visible = false;
                maskedTextBox1.Clear();
            }
            else MessageBox.Show("Выбирите заказ, который нужно завершить");
            
        }

        private void списокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rateList fm = new rateList(0);
            fm.Show();
        }

        private void добававитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rateList fm = new rateList(1);
            fm.Show();
        }

        private void изменитьПоказателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rateList fm = new rateList(2);
            fm.Show();
        }

        private void списокToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listClient fm = new listClient();
            fm.Show();
        }

        private void списокToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            listCars fm = new listCars();
            fm.Show();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addOrEditCar fm = new addOrEditCar();
            fm.Show();
        }

        private void нарушенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            violationsTable fm = new violationsTable();
            fm.Show();
        }

        private void режимРаботыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            workDriver fm = new workDriver();
            fm.Show();
        }
    }
}
