using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;
namespace taxiDesktopProg
{
    public partial class returnInformation : Form
    {
        public class driverses
        {
            public string FullName { get; set; }
            public long id_driverses { get; set; }
            public List<driverses> _driverses = new List<driverses>();
            public void listDriver(List<driver> dr)
            {
                foreach (var d in dr)
                {
                    if (string.IsNullOrWhiteSpace(d.patronymic) || d.patronymic == "Отсутствует")
                    {
                        _driverses.Add(new driverses(id_driverses = d.id_driver,
                        FullName = d.surname + " " + d.name.Substring(0, 1) + ". "));
                    }
                    else
                    {
                        _driverses.Add(new driverses(id_driverses = d.id_driver,
                        FullName = d.surname + " " + d.name.Substring(0, 1) + ". " + d.patronymic.Substring(0, 1) + "."));
                    }
                }
            }
            public driverses(long id, string _name)
            {
                id_driverses = id;
                FullName = _name;

            }
            public driverses()
            {
            }
        }
        private void startView()
        {

            using (Context db = new Context(Form1.connectionString))
            {

                var dr = db.drivers;
                driverses d = new driverses();
                d.listDriver(dr.ToList());

                var bindingSource1 = new BindingSource();
                bindingSource1.DataSource = d._driverses;

                comboBox1.DataSource = bindingSource1.DataSource;
                comboBox1.DisplayMember = "FullName";
                comboBox1.ValueMember = "id_driverses";

            }
        }
        public returnInformation()
        {
            InitializeComponent();
            button2.Enabled = false;
            //Выбор промежутка
            label1.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            //Выбор водителя
            label6.Visible = false;
            comboBox1.Visible = false;

        }
        private void driverInOrder()
        {
            long dr = Convert.ToInt64(comboBox1.SelectedValue);
            using (Context db = new Context(Form1.connectionString))
            {

                var drInOrder = db.order_driver_car.Where(x => x.id_driver == dr).Select(x => new
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
                dataGridView2.DataSource = drInOrder.ToList();
                dataGridView2.Columns[0].HeaderText = "Позывной";
                dataGridView2.Columns[1].HeaderText = "Фамилия водителя";
                dataGridView2.Columns[2].HeaderText = "Имя водителя";
                dataGridView2.Columns[3].HeaderText = "Отчество водителя";
                dataGridView2.Columns[4].HeaderText = "Мобильный телефон клиента";
                dataGridView2.Columns[5].HeaderText = "Принадлежность";
                dataGridView2.Columns[6].HeaderText = "Бренд";
                dataGridView2.Columns[7].HeaderText = "Модель";
                dataGridView2.Columns[8].HeaderText = "Цвет";
                dataGridView2.Columns[9].HeaderText = "Гос. номер";
                dataGridView2.Columns[10].HeaderText = "Рег. код";
                dataGridView2.Columns[11].HeaderText = "Адрес подачи";
                dataGridView2.Columns[12].HeaderText = "Адрес назначения";
                dataGridView2.Columns[13].HeaderText = "Дата и время оформления заказа";
                dataGridView2.Columns[14].HeaderText = "Дата и время завершения заказа";
                dataGridView2.Columns[15].HeaderText = "Мобильный телефон клиента";
                foreach (DataGridViewColumn data in dataGridView2.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView2.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView2.EnableHeadersVisualStyles = false;
            }
        }
        private void driverInOrder(DateTimePicker dateTimePicker2, DateTimePicker dateTimePicker1)
        {
            long dr = Convert.ToInt64(comboBox1.SelectedValue);
            using (Context db = new Context(Form1.connectionString))
            {
                var drInOrder = db.order_driver_car.Where(x => x.id_driver == dr).Select(x => new
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

                dataGridView2.DataSource = drInOrder.Where(x =>
               x.datetime_placing_the_order.Year >= dateTimePicker2.Value.Year &&
            (x.datetime_placing_the_order.Year > dateTimePicker2.Value.Year ||
            x.datetime_placing_the_order.Month >= dateTimePicker2.Value.Month) &&
            (x.datetime_placing_the_order.Year > dateTimePicker2.Value.Year ||
            x.datetime_placing_the_order.Month > dateTimePicker2.Value.Month ||
            x.datetime_placing_the_order.Day >= dateTimePicker2.Value.Day) &&
            x.datetime_placing_the_order.Year <= dateTimePicker1.Value.Year &&
            (x.datetime_placing_the_order.Year < dateTimePicker1.Value.Year ||
            x.datetime_placing_the_order.Month <= dateTimePicker1.Value.Month) &&
            (x.datetime_placing_the_order.Year < dateTimePicker1.Value.Year ||
            x.datetime_placing_the_order.Month < dateTimePicker1.Value.Month ||
            x.datetime_placing_the_order.Day <= dateTimePicker1.Value.Day)).Distinct().ToList();
                dataGridView2.Columns[0].HeaderText = "Позывной";
                dataGridView2.Columns[1].HeaderText = "Фамилия водителя";
                dataGridView2.Columns[2].HeaderText = "Имя водителя";
                dataGridView2.Columns[3].HeaderText = "Отчество водителя";
                dataGridView2.Columns[4].HeaderText = "Мобильный телефон клиента";
                dataGridView2.Columns[5].HeaderText = "Принадлежность";
                dataGridView2.Columns[6].HeaderText = "Бренд";
                dataGridView2.Columns[7].HeaderText = "Модель";
                dataGridView2.Columns[8].HeaderText = "Цвет";
                dataGridView2.Columns[9].HeaderText = "Гос. номер";
                dataGridView2.Columns[10].HeaderText = "Рег. код";
                dataGridView2.Columns[11].HeaderText = "Адрес подачи";
                dataGridView2.Columns[12].HeaderText = "Адрес назначения";
                dataGridView2.Columns[13].HeaderText = "Дата и время оформления заказа";
                dataGridView2.Columns[14].HeaderText = "Дата и время завершения заказа";
                dataGridView2.Columns[15].HeaderText = "Мобильный телефон клиента";
                foreach (DataGridViewColumn data in dataGridView2.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView2.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView2.EnableHeadersVisualStyles = false;
            }
        }
        private void rateList()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var rateLists = db.rates.Select(x => new { V = x.availability.ToString(), x.name, x.boarding, x.cost_per_kilometer, x.cost_downtime, x.child_safety_seat, x.transportation_of_pet }).ToList();
                dataGridView1.DataSource = rateLists;
                dataGridView1.Columns[0].HeaderText = "Доступность";
                dataGridView1.Columns[1].HeaderText = "Название";
                dataGridView1.Columns[2].HeaderText = "Цена за посадку";
                dataGridView1.Columns[3].HeaderText = "Цена за километр";
                dataGridView1.Columns[4].HeaderText = "Цена за ожидаение";
                dataGridView1.Columns[5].HeaderText = "Детское сидение";
                dataGridView1.Columns[6].HeaderText = "Перевозка домашних животных";

                foreach (DataGridViewColumn data in dataGridView1.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView1.EnableHeadersVisualStyles = false;
            }
        }
        private void carList()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var car = from c in db.cars
                          join car_cat in db.car_category on c.id_car_category equals car_cat.id_car_category
                          select new
                          {
                              rented_car = c.rented_car.ToString(),
                              car_brand = c.car_brand,
                              model_car = c.car_model,
                              color = c.colour,
                              state_number = c.state_number,
                              region_code = c.region_code,
                              technical_condition_car = c.technical_condition_car,
                              car_categoor = car_cat.name + "_" + car_cat.number_of_passengers
                          };

                dataGridView1.DataSource = car.ToList();
                dataGridView1.Columns[0].HeaderText = "Принадлежность";
                dataGridView1.Columns[1].HeaderText = "Бренд";
                dataGridView1.Columns[2].HeaderText = "Модель";
                dataGridView1.Columns[3].HeaderText = "Цвет";
                dataGridView1.Columns[4].HeaderText = "Гос. номер";
                dataGridView1.Columns[5].HeaderText = "Рег. код";
                dataGridView1.Columns[6].HeaderText = "Тех. состояние";
                dataGridView1.Columns[7].HeaderText = "Категория автомобиля_Кол-во мест";

                foreach (DataGridViewColumn data in dataGridView1.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView1.EnableHeadersVisualStyles = false;
            }
        }
        private void orderList()
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
                               status = ord.status,
                               reason_cancellation = ord.reason_cancellation,
                               place = ad1.city + " " + ad1.street + " Д" + ad1.house + " " + ad1.enrance,
                               place2 = ad2.city + " " + ad2.street + " Д" + ad2.house + " " + ad2.enrance,
                               order_cost = ord.order_cost,
                               payment_method = ord.payment_method,
                               datetime_placing = ord.datetime_placing_the_order,
                               datetime_complete_order = ord.order_completion_datetime,
                               id_driver = driv.call_sign,
                               id_client = ord.client.mobile_phone,
                               id_rate = ord.rate.name,

                           };
                dataGridView2.DataSource = list.Distinct().ToList();
                dataGridView2.Columns[0].HeaderText = "Статус";
                dataGridView2.Columns[1].HeaderText = "Причина отмены";
                dataGridView2.Columns[2].HeaderText = "Адрес подачи";
                dataGridView2.Columns[3].HeaderText = "Адрес назначения";
                dataGridView2.Columns[4].HeaderText = "Стоимость";
                dataGridView2.Columns[5].HeaderText = "Способ оплаты";
                dataGridView2.Columns[6].HeaderText = "Дата и время оформления заказа";
                dataGridView2.Columns[7].HeaderText = "Дата и время завершения заказа";
                dataGridView2.Columns[8].HeaderText = "Позывной";
                dataGridView2.Columns[9].HeaderText = "Телефона";
                dataGridView2.Columns[10].HeaderText = "Тариф";
                foreach (DataGridViewColumn data in dataGridView2.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView2.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView2.EnableHeadersVisualStyles = false;
            }
        }
        private void orderList(DateTimePicker dateTimePicker2, DateTimePicker dateTimePicker1)
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
                               status = ord.status,
                               reason_cancellation = ord.reason_cancellation,
                               place = ad1.city + " " + ad1.street + " Д" + ad1.house + " " + ad1.enrance,
                               place2 = ad2.city + " " + ad2.street + " Д" + ad2.house + " " + ad2.enrance,
                               order_cost = ord.order_cost,
                               payment_method = ord.payment_method,
                               datetime_placing = ord.datetime_placing_the_order,
                               datetime_complete_order = ord.order_completion_datetime,
                               id_driver = driv.call_sign,
                               id_client = ord.client.mobile_phone,
                               id_rate = ord.rate.name,

                           };
                dataGridView2.DataSource = list.Where(x =>
                x.datetime_placing.Year >= dateTimePicker2.Value.Year &&
            (x.datetime_placing.Year > dateTimePicker2.Value.Year ||
            x.datetime_placing.Month >= dateTimePicker2.Value.Month) &&
            (x.datetime_placing.Year > dateTimePicker2.Value.Year ||
            x.datetime_placing.Month > dateTimePicker2.Value.Month ||
            x.datetime_placing.Day >= dateTimePicker2.Value.Day) &&
            x.datetime_placing.Year <= dateTimePicker1.Value.Year &&
            (x.datetime_placing.Year < dateTimePicker1.Value.Year ||
            x.datetime_placing.Month <= dateTimePicker1.Value.Month) &&
            (x.datetime_placing.Year < dateTimePicker1.Value.Year ||
            x.datetime_placing.Month < dateTimePicker1.Value.Month ||
            x.datetime_placing.Day <= dateTimePicker1.Value.Day)).Distinct().ToList();

                dataGridView2.Columns[0].HeaderText = "Статус";
                dataGridView2.Columns[1].HeaderText = "Причина отмены";
                dataGridView2.Columns[2].HeaderText = "Адрес подачи";
                dataGridView2.Columns[3].HeaderText = "Адрес назначения";
                dataGridView2.Columns[4].HeaderText = "Стоимость";
                dataGridView2.Columns[5].HeaderText = "Способ оплаты";
                dataGridView2.Columns[6].HeaderText = "Дата и время оформления заказа";
                dataGridView2.Columns[7].HeaderText = "Дата и время завершения заказа";
                dataGridView2.Columns[8].HeaderText = "Позывной";
                dataGridView2.Columns[9].HeaderText = "Телефона";
                dataGridView2.Columns[10].HeaderText = "Тариф";
                foreach (DataGridViewColumn data in dataGridView2.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView2.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView2.EnableHeadersVisualStyles = false;
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    dataGridView2.DataSource = null;
                    dataGridView1.Visible = false;
                    dataGridView2.Visible = false;

                    startView();
                    label6.Visible = true;
                    comboBox1.Visible = true;
                    label1.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    dateTimePicker1.Visible = true;
                    dateTimePicker2.Visible = true;
                    MessageBox.Show("Выберите водителя и промежуто времени", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1:
                    button2.Enabled = true;
                    label6.Visible = false;
                    comboBox1.Visible = false;
                    label1.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    dateTimePicker1.Visible = false;
                    dateTimePicker2.Visible = false;
                    rateList();
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = false;
                    break;
                case 2:
                    button2.Enabled = true;
                    label6.Visible = false;
                    comboBox1.Visible = false;
                    label1.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    dateTimePicker1.Visible = false;
                    dateTimePicker2.Visible = false;
                    carList();
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = false;
                    break;
                case 3:
                    label6.Visible = false;
                    comboBox1.Visible = false;
                    orderList();
                    dataGridView1.Visible = false;
                    dataGridView2.Visible = true;
                    label1.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    dateTimePicker1.Visible = true;
                    dateTimePicker2.Visible = true;
                    MessageBox.Show("Выберите промежуто времени", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            driverInOrder();
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                driverInOrder(dateTimePicker2, dateTimePicker1);
                button2.Enabled = true;
                dataGridView1.Visible = false;
                dataGridView2.Visible = true;
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                orderList(dateTimePicker2, dateTimePicker1);
                button2.Enabled = true;
                dataGridView1.Visible = false;
                dataGridView2.Visible = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                driverInOrder(dateTimePicker2, dateTimePicker1);
                button2.Enabled = true;
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                orderList(dateTimePicker2, dateTimePicker1);
                button2.Enabled = true;
            }
        }
        static System.Windows.Forms.SaveFileDialog saveFileDialog;
        public void Export(DataGridView dataGridView)
        {
            // Создаем диалоговое окно для сохранения файла
            saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save Excel File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {//Приложение


                Excel.Application excelApp = new Excel.Application();
                //Скрытие окна Excel и окна для подтверждения о перезаписи
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;
                //Книга
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                //Страница
                Excel.Worksheet excelWorksheet = excelWorkbook.Sheets[1];

                // Заполняем заголовки столбцов (начиная с A2)
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    excelWorksheet.Cells[2, i + 1] = dataGridView.Columns[i].HeaderText;
                }

                // Заполняем ячейки данными из DataGridView (начиная с A3)
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        excelWorksheet.Cells[i + 3, j + 1] = dataGridView.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                //Добавление границ
                excelWorksheet.Cells[1, 1].CurrentRegion.Borders.LineStyle = Excel.XlLineStyle.xlContinuous; //границы
                //добавление полужирного шрифта для заголовков таблицы
                excelWorksheet.Rows[1].Font.Bold = true;
                excelWorksheet.Rows[2].Font.Bold = true;


                if (comboBox2.SelectedIndex == 1)
                {
                    excelWorkbook.Worksheets[1].Cells.Replace("ЛОЖЬ", "Недоступен");
                    excelWorkbook.Worksheets[1].Cells.Replace("ИСТИНА", "Доступен");
                    //Название в клетке
                    excelWorksheet.Cells[1, "A"] = "Список тарифов";
                    //Выбор диапазона
                    var range3 = excelWorksheet.get_Range("A1", "G1");
                    //Объединение клеток в 1
                    range3.Merge(Type.Missing);
                    //выравнивание
                    range3.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    range3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //размер текста
                    range3.Cells.Font.Size = 14;
                }
                else if (comboBox2.SelectedIndex == 2)
                {
                    excelWorkbook.Worksheets[1].Cells.Replace("ЛОЖЬ", "Личный");
                    excelWorkbook.Worksheets[1].Cells.Replace("ИСТИНА", "Авто фирмы");

                    //Название в клетке
                    excelWorksheet.Cells[1, "A"] = "Список автомобилей";
                    //Выбор диапазона
                    var range3 = excelWorksheet.get_Range("A1", "H1");
                    //Объединение клеток в 1
                    range3.Merge(Type.Missing);
                    //выравнивание
                    range3.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    range3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //размер текста
                    range3.Cells.Font.Size = 14;
                }
                else if (comboBox2.SelectedIndex == 0)
                {
                    //Название в клетке
                    excelWorksheet.Cells[1, "A"] = "История заказов водителей за определённый промежуток времени";
                    //Выбор диапазона
                    var range3 = excelWorksheet.get_Range("A1", "P1");
                    //Объединение клеток в 1
                    range3.Merge(Type.Missing);
                    //выравнивание
                    range3.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    range3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //размер текста
                    range3.Cells.Font.Size = 14;

                    var range4 = excelWorksheet.get_Range("F3", "F500");
                    range4.Cells.Replace("ЛОЖЬ", "Личный");
                    range4.Cells.Replace("ИСТИНА", "Авто фирмы");
                }
                else if (comboBox2.SelectedIndex == 3)
                {
                    //Название в клетке
                    excelWorksheet.Cells[1, "A"] = "Заказы за промежуток времени";
                    //Выбор диапазона
                    var range3 = excelWorksheet.get_Range("A1", "K1");
                    //Объединение клеток в 1
                    range3.Merge(Type.Missing);
                    //выравнивание
                    range3.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    range3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //размер текста
                    range3.Cells.Font.Size = 14;
                }
                //автоматическое расстояние столбцов
                excelWorksheet.Range["A:Z"].EntireColumn.AutoFit();
                // Сохраняем файл Excel и закрываем приложение
                excelWorkbook.SaveAs(saveFileDialog.FileName);
                excelWorkbook.Close();
                excelApp.Quit();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    Export(dataGridView2);
                    MessageBox.Show($"Файл был создан по пути: {saveFileDialog.FileName}");
                    button2.Enabled = false;
                    break;
                case 1:
                    Export(dataGridView1);
                    MessageBox.Show($"Файл был создан по пути: {saveFileDialog.FileName}");
                    button2.Enabled = false;
                    break;
                case 2:
                    Export(dataGridView1);
                    MessageBox.Show($"Файл был создан по пути: {saveFileDialog.FileName}");
                    button2.Enabled = false;
                    break;
                case 3:
                    Export(dataGridView2);
                    MessageBox.Show($"Файл был создан по пути: {saveFileDialog.FileName}");
                    button2.Enabled = false;
                    break;
            }
        }


        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
            {

                switch (e.ColumnIndex)
                {
                    case 0:
                        if (Convert.ToBoolean(this.dataGridView1.Rows[e.RowIndex].Cells[0].Value) == true)
                        {
                            e.Value = "Доступен";
                        }
                        else
                        {
                            e.Value = "Недоступен";
                        }
                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        if (e.Value != null)
                        {
                            e.Value = Math.Round(double.Parse(e.Value.ToString()), 0);
                        }
                        break;

                }
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                if (e.ColumnIndex == 0)
                    if (Convert.ToBoolean(this.dataGridView1.Rows[e.RowIndex].Cells[0].Value) == true)
                    {
                        e.Value = "Авто фирмы";
                    }
                    else
                        e.Value = "Личный";
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (comboBox2.SelectedIndex == 3)
            {
                if (e.ColumnIndex == 4)
                {
                    if (e.Value != null)
                    {
                        e.Value = Math.Round(double.Parse(e.Value.ToString()), 0);
                    }
                }
            }
            else if (comboBox2.SelectedIndex == 0)
            {
                if (e.ColumnIndex == 5)
                    if (Convert.ToBoolean(this.dataGridView2.Rows[e.RowIndex].Cells[5].Value) == true)
                    {
                        e.Value = "Авто фирмы";
                    }
                    else
                        e.Value = "Личный";
            }
        }
    }
}
