using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taxiDesktopProg
{
    internal class forIssuingOrders
    {
        Form_for_Dispatcher form;
        public forIssuingOrders(Form_for_Dispatcher s)
        {
            form = s;
        }
        public void printNewOrders(DataGridView dataName)
        {
            using (Context db = new Context(auth.connectionString))
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
                DateTime dt = DateTime.Now;
                DateTime dtd = DateTime.Now.Add(new TimeSpan(-1, 0, 0));
                DateTime dh = DateTime.Now.AddMinutes(5);

                dataName.DataSource = list.Where(p => p.status == "В ожидании" && p.datetime_placing.Year == dt.Year
                                            && p.datetime_placing.Month == dt.Month && p.datetime_placing.Day == dt.Day
                                            && p.datetime_placing <= dh
                                            && p.datetime_placing >= dtd
                                            ).Distinct().ToList();

               dataName.Columns[0].Visible = false;
               dataName.Columns[1].HeaderText = "Адрес подачи";
               dataName.Columns[2].HeaderText = "Адрес назначения";
               dataName.Columns[3].HeaderText = "Стоимость";
               dataName.Columns[4].HeaderText = "Способ оплаты";
               dataName.Columns[5].HeaderText = "Дата и время оформления заказа";
               dataName.Columns[6].HeaderText = "Телефона";
               dataName.Columns[7].HeaderText = "Тариф";
                dataName.Columns[8].Visible = false;

                foreach (DataGridViewColumn data in dataName.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataName.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dataName.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataName.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataName.EnableHeadersVisualStyles = false;

            }
        }
        //Метод вывода в dataGrid
        public void printNowTimeOrders(DataGridView dataName)
        {
            using (Context db = new Context(auth.connectionString))
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

                dataName.DataSource = list.Where(p => p.status == "Выполнение").Distinct().ToList();
                dataName.Columns[0].Visible = false;
                dataName.Columns[1].HeaderText = "Адрес подачи";
                dataName.Columns[2].HeaderText = "Адрес назначения";
                dataName.Columns[3].HeaderText = "Стоимость";
                dataName.Columns[4].HeaderText = "Способ оплаты";
                dataName.Columns[5].HeaderText = "Дата и время оформления заказа";
                dataName.Columns[6].HeaderText = "Позывной";
                dataName.Columns[7].HeaderText = "Телефона";
                dataName.Columns[8].HeaderText = "Тариф";
                dataName.Columns[9].Visible = false;

                foreach (DataGridViewColumn data in dataName.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataName.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataName.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataName.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataName.EnableHeadersVisualStyles = false;
            }
        }
        //Метод вывода в dataGrid
        public void printPreliminaryOrders(DataGridView dataName)
        {
            using (Context db = new Context(auth.connectionString))
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

                dataName.DataSource = list.Where(p => p.status == "В ожидании" && p.datetime_placing.CompareTo(DateTime.Now) > 0 && p.datetime_placing >= dh
                                         ).Distinct().ToList();
                dataName.Columns[0].Visible = false;
                dataName.Columns[1].HeaderText = "Адрес подачи";
                dataName.Columns[2].HeaderText = "Адрес назначения";
                dataName.Columns[3].HeaderText = "Стоимость";
                dataName.Columns[4].HeaderText = "Способ оплаты";
                dataName.Columns[5].HeaderText = "Дата и время оформления заказа";
                dataName.Columns[6].HeaderText = "Телефона";
                dataName.Columns[7].HeaderText = "Тариф";
                dataName.Columns[8].Visible = false;

                foreach (DataGridViewColumn data in dataName.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataName.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataName.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataName.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataName.EnableHeadersVisualStyles = false;
            }
        }
        //Метод вывода в dataGrid
        public void printOrders(string name, DataGridView dataName)
        {
            using (Context db = new Context(auth.connectionString))
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
                dataName.Columns[3].HeaderText = "Стоимость";
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
        public void printCancelOrders(DataGridView dataName)
        {
            using (Context db = new Context(auth.connectionString))
            {
                var list = from ord in db.orders
                           join ad1 in db.addresses on ord.place_of_departure equals ad1.id_address
                           join ad2 in db.addresses on ord.destination equals ad2.id_address
                           join rateOrder in db.orders on ord.id_rate equals rateOrder.id_rate
                           join driv in db.drivers on ord.id_driver equals driv.id_driver
                           select new
                           {
                               id_order = ord.id_order,
                               reason_cancellation = ord.reason_cancellation,
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

                dataName.DataSource = list.Where(p => p.status == "Отменён").Distinct().ToList();
                dataName.Columns[0].Visible = false;
                dataName.Columns[1].HeaderText = "Причина";
                dataName.Columns[2].HeaderText = "Адрес подачи";
                dataName.Columns[3].HeaderText = "Адрес назначения";
                dataName.Columns[4].HeaderText = "Стоимость";
                dataName.Columns[5].HeaderText = "Способ оплаты";
                dataName.Columns[6].HeaderText = "Дата и время оформления заказа";
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

    }
}
