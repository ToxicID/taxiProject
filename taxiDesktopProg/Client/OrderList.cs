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
    public partial class OrderList : Form
    {
        private long? id;
       
        public void printOrders(DataGridView dataName)
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
                               status = ord.status,
                               reason_cancellation = ord.reason_cancellation,
                               place = ad1.city + " " + ad1.street + " Д" + ad1.house + " " + ad1.enrance,
                               place2 = ad2.city + " " + ad2.street + " Д" + ad2.house + " " + ad2.enrance,
                               order_cost = ord.order_cost,
                               payment_method = ord.payment_method,
                               datetime_placing = ord.datetime_placing_the_order,
                               datetime_complete_order = ord.order_completion_datetime,
                               id_driver = driv.call_sign,
                               id_rate = ord.rate.name,
                               
                               idClient = ord.id_client
                               
                           };

                dataName.DataSource = list.Where(p => p.idClient == id).Distinct().ToList();
                dataName.Columns[0].Visible = false;
                dataName.Columns[1].HeaderText = "Статус";
                dataName.Columns[2].HeaderText = "Причина отмены";
                dataName.Columns[3].HeaderText = "Адрес подачи";
                dataName.Columns[4].HeaderText = "Адрес назначения";
                dataName.Columns[5].HeaderText = "Стоимость";
                dataName.Columns[6].HeaderText = "Способ оплаты";
                dataName.Columns[7].HeaderText = "Дата и время оформления заказа";
                dataName.Columns[8].HeaderText = "Дата и время завершения заказа";
                dataName.Columns[9].HeaderText = "Позывной";
                dataName.Columns[10].HeaderText = "Тариф";
                dataName.Columns[11].Visible = false;
                foreach (DataGridViewColumn data in dataName.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataName.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataName.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataName.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataName.EnableHeadersVisualStyles = false;

            }
        }
        public OrderList(long? idClient)
        {
            InitializeComponent();
            id = idClient;
            using (Context db = new Context(Form1.connectionString))
            {
                var cl = db.clients.Where(p => p.id_client == id).FirstOrDefault();
                this.Text = "Заказы клиента " + cl.mobile_phone;
            }
                printOrders(dataGridView1);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                e.Value = Math.Round(double.Parse(e.Value.ToString()), 0);
            }
            if (e.ColumnIndex == 1)
                if (e.Value.ToString() == "Ложный" || e.Value.ToString() == "Отменён")
                    e.CellStyle.ForeColor = Color.Red;
                else if(e.Value.ToString() == "Завершён")
                    e.CellStyle.ForeColor = Color.Green;
                else if (e.Value.ToString() == "Выполнение")
                    e.CellStyle.ForeColor = Color.Yellow;
        }
    }
}
