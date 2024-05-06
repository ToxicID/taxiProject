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
    public partial class historyDriver : Form
    {
        public historyDriver()
        {
            InitializeComponent();
            button2.Visible = false;
            label2.Text = "Поиск по дате оформления\nзаказа";
            listDriver();
            fontDatagrid();
        }
        public historyDriver(bool s)
        {
            InitializeComponent();
            button2.Visible = true;
            label2.Text = "Поиск по дате оформления\nзаказа";
            listDriver();
            fontDatagrid();
        }
        private void listDriver()
        {
            using (Context db = new Context(Form1.connectionString))
            {

                var drInOrder = db.order_driver_car.Select(x => new
                {
                    x.call_sign,
                    x.surname,
                    x.name,
                    x.patronymic,
                    x.mobile_phone,
                    V = x.rented_car.ToString(),
                    x.car_brand,
                    x.car_model,
                    x.colour,
                    x.state_number,
                    x.region_code,
                    x.place_of_departure,
                    x.destination,
                    x.datetime_placing_the_order,
                    x.order_completion_datetime,
                    x.client_mobile_phone
                });
                dataGridView1.DataSource = drInOrder.ToList();
            }
        }
        private void searchDriver()
        {
                using (Context db = new Context(Form1.connectionString))
                {

                    var drInOrder = db.order_driver_car.Select(x => new
                    {
                        x.call_sign,
                        x.surname,
                        x.name,
                        x.patronymic,
                        x.mobile_phone,
                        V = x.rented_car.ToString(),
                        x.car_brand,
                        x.car_model,
                        x.colour,
                        x.state_number,
                        x.region_code,
                        x.place_of_departure,
                        x.destination,
                        x.datetime_placing_the_order,
                        x.order_completion_datetime,
                        x.client_mobile_phone
                    });
                    dataGridView1.DataSource = drInOrder.Where(x => x.surname.Contains(textBox2.Text)).ToList();
                }
        }
        private void searchDriver(DateTimePicker dateTimePicker)
        {
            using (Context db = new Context(Form1.connectionString))
            {

                var drInOrder = db.order_driver_car.Select(x => new
                {
                    x.call_sign,
                    x.surname,
                    x.name,
                    x.patronymic,
                    x.mobile_phone,
                    V = x.rented_car.ToString(),
                    x.car_brand,
                    x.car_model,
                    x.colour,
                    x.state_number,
                    x.region_code,
                    x.place_of_departure,
                    x.destination,
                    x.datetime_placing_the_order,
                    x.order_completion_datetime,
                    x.client_mobile_phone
                });
                dataGridView1.DataSource = drInOrder.Where(x => x.datetime_placing_the_order.Year == dateTimePicker.Value.Year &&
                                                                x.datetime_placing_the_order.Month == dateTimePicker.Value.Month &&
                                                                x.datetime_placing_the_order.Day == dateTimePicker.Value.Day).ToList();
            }
        }
        private void searchDriverDateAndSurname(DateTimePicker dateTimePicker)
        {
            using (Context db = new Context(Form1.connectionString))
            {

                var drInOrder = db.order_driver_car.Select(x => new
                {
                    x.call_sign,
                    x.surname,
                    x.name,
                    x.patronymic,
                    x.mobile_phone,
                    V = x.rented_car.ToString(),
                    x.car_brand,
                    x.car_model,
                    x.colour,
                    x.state_number,
                    x.region_code,
                    x.place_of_departure,
                    x.destination,
                    x.datetime_placing_the_order,
                    x.order_completion_datetime,
                    x.client_mobile_phone
                });
                dataGridView1.DataSource = drInOrder.Where(x => x.datetime_placing_the_order.Year == dateTimePicker.Value.Year &&
                                                                x.datetime_placing_the_order.Month == dateTimePicker.Value.Month &&
                                                                x.datetime_placing_the_order.Day == dateTimePicker.Value.Day && 
                                                                x.surname.Contains(textBox2.Text)).ToList();
            }
        }
        private void fontDatagrid()
        {
            dataGridView1.Columns[0].HeaderText = "Позывной";
            dataGridView1.Columns[1].HeaderText = "Фамилия водителя";
            dataGridView1.Columns[2].HeaderText = "Имя водителя";
            dataGridView1.Columns[3].HeaderText = "Отчество водителя";
            dataGridView1.Columns[4].HeaderText = "Мобильный телефон клиента";
            dataGridView1.Columns[5].HeaderText = "Принадлежность";
            dataGridView1.Columns[6].HeaderText = "Бренд";
            dataGridView1.Columns[7].HeaderText = "Модель";
            dataGridView1.Columns[8].HeaderText = "Цвет";
            dataGridView1.Columns[9].HeaderText = "Гос. номер";
            dataGridView1.Columns[10].HeaderText = "Рег. код";
            dataGridView1.Columns[11].HeaderText = "Адрес подачи";
            dataGridView1.Columns[12].HeaderText = "Адрес назначения";
            dataGridView1.Columns[13].HeaderText = "Дата и время оформления заказа";
            dataGridView1.Columns[14].HeaderText = "Дата и время завершения заказа";
            dataGridView1.Columns[15].HeaderText = "Мобильный телефон клиента";
            foreach (DataGridViewColumn data in dataGridView1.Columns)
                data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
            dataGridView1.EnableHeadersVisualStyles = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            searchDriver();
            fontDatagrid();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                searchDriver(dateTimePicker2);
                fontDatagrid();
            }
            else if (!(string.IsNullOrWhiteSpace(textBox2.Text)))
            {
                searchDriverDateAndSurname(dateTimePicker2);
                fontDatagrid();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Value = DateTime.Now;
            textBox2.Text = "";
            listDriver();
            fontDatagrid();
            
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.ColumnIndex == 5)
                    if (Convert.ToBoolean(this.dataGridView1.Rows[e.RowIndex].Cells[5].Value) == true)
                    {
                        e.Value = "Авто фирмы";
                    }
                    else
                        e.Value = "Личный";
            }
        }
    }
}
