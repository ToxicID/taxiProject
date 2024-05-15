using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
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
        auth po;

        //Метод вывода в dataGrid

        forIssuingOrders fIO;
        public Form_for_Dispatcher(long id_dis, auth po)
        {
            InitializeComponent();
             fIO = new forIssuingOrders(this);
            id = id_dis;
            this.po = po;
            using (Context db = new Context(auth.connectionString))
            {
                var dispatcher = db.dispatchers.Where(p => p.id_dispatcher == id).FirstOrDefault();
                
            }
            timer1.Enabled = true;
            fIO.printNewOrders(dataGridView1);
            
            label1.Text = "";
            dataGridView6.Visible = false;
            label3.Visible = false;
            maskedTextBox1.Visible = false;
            button1.Visible = false;


        }

        private void Form_for_Dispatcher_FormClosed(object sender, FormClosedEventArgs e)
        {
            using (Context db = new Context(auth.connectionString))
            {
                var disp = db.dispatchers.Find(id);
                disp.activity = false;
                db.SaveChanges();
            }
                Application.Restart();
            
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
                    fIO.printNewOrders(dataGridView1);
                    DataGridIndex = null;
                    break;
                case 1:
                     fIO.printNowTimeOrders(dataGridView2);
                    label3.Visible = false;
                    maskedTextBox1.Visible = false;
                    button1.Visible = false;
                    DataGridIndex = null;
                    break;
                case 2:
                   fIO.printPreliminaryOrders(dataGridView3);
                    DataGridIndex = null;
                    break;
                case 3:
                    fIO.printOrders("Завершён", dataGridView4);
                    DataGridIndex = null;
                    break;
                case 4:
                    fIO.printOrders("Ложный",dataGridView5);
                    DataGridIndex = null;
                    break;
                case 5:
                    fIO.printCancelOrders(dataGridView7);
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
                using (Context db = new Context(auth.connectionString)) {
                    var list = db.drivers.Where(p => p.status == "Свободен" && p.driver_readiness == "Готов" && p.id_car != null && p.car.technical_condition_car == "Исправлено").ToList();
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
            using (Context db = new Context(auth.connectionString))
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
                if (tabPageIndex == 0) fIO.printNewOrders(dataGridView1);
                if (tabPageIndex == 1)  fIO.printNowTimeOrders(dataGridView2);

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
                using (Context db = new Context(auth.connectionString))
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
                        if (lastOrderClient.Where(p=>p.status == "Ложный" && p.order_completion_datetime.Value.Year > dt.Year).Count() > 2)
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
                    if (tabPageIndex == 0) fIO.printNewOrders(dataGridView1);
                    if (tabPageIndex == 1)  fIO.printNowTimeOrders(dataGridView2);

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
            fIO.printNewOrders(dataGridView1);

        }
        private void orderComplete()
        {
            using (Context db = new Context(auth.connectionString))
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
            using (Context db = new Context(auth.connectionString))
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
            using (Context db = new Context(auth.connectionString))
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
                         fIO.printNowTimeOrders(dataGridView2);
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
                        fIO.printNewOrders(dataGridView1);
                    else if (tabPageIndex == 2)
                        fIO.printPreliminaryOrders(dataGridView3);
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
                fIO.printNowTimeOrders(dataGridView2);
            
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


        private void режимРаботыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            workDriver fm = new workDriver();
            fm.Show();
        }

        private void dataGridView4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 3)
            {
                e.Value = Math.Round(double.Parse(e.Value.ToString()), 0);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Value = Math.Round(double.Parse(e.Value.ToString()), 0);
            }
        }

        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Value = Math.Round(double.Parse(e.Value.ToString()), 0);
            }

        }

        private void dataGridView5_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Value = Math.Round(double.Parse(e.Value.ToString()), 0);
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Value = Math.Round(double.Parse(e.Value.ToString()), 0);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (tabPageIndex == 1)
            {
                using (Context db = new Context(auth.connectionString))
                {
                    if (DataGridIndex != null)
                    {
                        DialogResult result = MessageBox.Show("Отменить заказ?", "Отмена",
                                                                                           MessageBoxButtons.YesNo,
                                                                                           MessageBoxIcon.Information);
                        if (result == DialogResult.No) return;
                        var order = db.orders.Where(p => p.id_order == DataGridIndex).FirstOrDefault();


                        order.status = "Отменён";

                        if (order.order_completion_datetime == null)
                            order.order_completion_datetime = DateTime.Now;

                        DialogResult rs = MessageBox.Show("Заказ отменён по вине клиента?","Отмена", MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                        if(rs == DialogResult.Yes)
                        {
                            order.reason_cancellation = "По причине клиента";
                            int count = 0;
                            var lastOrderClient = db.orders.Where(p => p.id_client == order.id_client).ToList();
                            foreach (var i in lastOrderClient)
                            {
                                if (i.status == "Отменён" && i.reason_cancellation == "По причине клиента")
                                {
                                    count++;
                                }
                                else
                                    count = 0;
                                if(count == 3)
                                {
                                    var cl = db.clients.Where(p => p.id_client == order.id_client).FirstOrDefault();
                                    cl.blacklist = true;
                                    break;
                                }
                            }
                        }
                        else if(rs== DialogResult.No)
                        {
                            order.reason_cancellation = "По причине фирмы";
                        }


                        
                        var driver = db.drivers.Where(p => p.id_driver == order.id_driver).FirstOrDefault();
                        driver.status = "Свободен";
                        db.SaveChanges();
                        dataGridView6.Visible = false;
                        label1.Text = "";
                        MessageBox.Show($"Заказ был перенесён в отменённые");

                    }
                    DataGrid6Index = null;
                    DataGridIndex = null;
                    if (tabPageIndex == 1)  fIO.printNowTimeOrders(dataGridView2);

                }
            }
            else
            {
                MessageBox.Show("Заказ можно отменить из вкладки \"Выполняемые\"");
                return;
            }
        }
       

        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getIndexDataGrid(dataGridView1, e);
        }

        private void dataGridView7_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                e.Value = Math.Round(double.Parse(e.Value.ToString()), 0);
            }
        }

        private void списокВодителейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listDriver fm = new listDriver("s");
            fm.Show();
        }

        private void назначитьАвтомобильToolStripMenuItem_Click(object sender, EventArgs e)
        {
            carForDriver fm = new carForDriver(1);
            fm.Show();
        }

        private void отчётToolStripMenuItem_Click(object sender, EventArgs e)
        {
            returnInformation fm = new returnInformation();
            fm.Show();
        }

        private void историяЗаказовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            historyDriver fm = new historyDriver();
            fm.Show();
        }
    }
}
