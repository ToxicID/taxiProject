using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
namespace taxiDesktopProg
{
    public partial class returnFromDirector : Form
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
        public returnFromDirector()
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
            //Выбор водителя
            label6.Visible = false;
            comboBox1.Visible = false;
            startView();
            panel2.Visible = false;
            panel4.Visible = false;
            dataGridView4.Visible = false;
        }
        private void rateList()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var rateLists = db.rates.Select(x => new { V = x.availability.ToString(), x.name, x.boarding, x.cost_per_kilometer, x.cost_downtime, x.child_safety_seat, x.transportation_of_pet }).ToList();
                dataGridView4.DataSource = rateLists;
                dataGridView4.Columns[0].HeaderText = "Доступность";
                dataGridView4.Columns[1].HeaderText = "Название";
                dataGridView4.Columns[2].HeaderText = "Цена за посадку";
                dataGridView4.Columns[3].HeaderText = "Цена за километр";
                dataGridView4.Columns[4].HeaderText = "Цена за ожидаение";
                dataGridView4.Columns[5].HeaderText = "Детское сидение";
                dataGridView4.Columns[6].HeaderText = "Перевозка домашних животных";

                foreach (DataGridViewColumn data in dataGridView4.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView4.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView4.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView4.EnableHeadersVisualStyles = false;
            }
        }
        private void listClientFromBlackList()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var client = db.clients.Where(x => x.blacklist == true).Select(x => new { V = x.mobile_phone }).ToList();
                dataGridView1.DataSource = client;
                dataGridView1.Columns[0].HeaderText = "Номер телефона клиента";

                foreach (DataGridViewColumn data in dataGridView1.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView1.EnableHeadersVisualStyles = false;
            }
        }
        private void personalList()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var disp = db.dispatchers.Select(x => new { V = x.surname, V1 = x.name, V2 = x.patronymic, V3 = x.mobile_phone });
                dataGridView2.DataSource = disp.ToList();
                dataGridView2.Columns[0].HeaderText = "Фамилия диспетчера";
                dataGridView2.Columns[1].HeaderText = "Имя диспетчера";
                dataGridView2.Columns[2].HeaderText = "Отчество диспетчера";
                dataGridView2.Columns[3].HeaderText = "Мобильный телефон диспетчера";
                foreach (DataGridViewColumn data in dataGridView2.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView2.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView2.EnableHeadersVisualStyles = false;


                var driver = db.drivers.Select(x => new
                {
                    V1 = x.call_sign,
                    V2 = x.surname,
                    V3 = x.name,
                    V4 = x.patronymic,
                    V5 = x.date_of_birth,
                    V6 = x.drivers_license_number,
                    V7 = x.mobile_phone
                });
                dataGridView3.DataSource = driver.ToList();
                dataGridView3.Columns[0].HeaderText = "Позывной";
                dataGridView3.Columns[1].HeaderText = "Фамилия диспетчера";
                dataGridView3.Columns[2].HeaderText = "Имя диспетчера";
                dataGridView3.Columns[3].HeaderText = "Отчество диспетчера";
                dataGridView3.Columns[4].HeaderText = "Дата рождения";
                dataGridView3.Columns[5].HeaderText = "Номер водительского удостоверения";
                dataGridView3.Columns[6].HeaderText = "Мобильный телефон водителя";
                foreach (DataGridViewColumn data in dataGridView3.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView3.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView3.EnableHeadersVisualStyles = false;
            }
        }
        private void violationsDriver()
        {
            long driv = Convert.ToInt64(comboBox1.SelectedValue);
            using (Context db = new Context(Form1.connectionString))
            {
                var list = from dr in db.drivers
                           join vio in db.violations on dr.id_driver equals vio.id_driver
                           where dr.id_driver == driv
                           select new
                           {
                               surname = dr.surname,
                               name = dr.name,
                               patronymic = dr.patronymic,
                               dateTimeVio = vio.datetime_the_violation,
                               typeVio = vio.type_of_violation,
                               statusVio = vio.violation_status,
                               measuresVio = vio.measures_taken
                           };
                dataGridView1.DataSource = list.ToList();
                dataGridView1.Columns[0].HeaderText = "Фамилия водителя";
                dataGridView1.Columns[1].HeaderText = "Имя водителя";
                dataGridView1.Columns[2].HeaderText = "Отчество водителя";
                dataGridView1.Columns[3].HeaderText = "Дата и время";
                dataGridView1.Columns[4].HeaderText = "Тип нарушения";
                dataGridView1.Columns[5].HeaderText = "Статус";
                dataGridView1.Columns[6].HeaderText = "Принятые меры";

                foreach (DataGridViewColumn data in dataGridView1.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView1.EnableHeadersVisualStyles = false;
            }

        }
        private void violationsDriver(DateTimePicker dateTimePicker2, DateTimePicker dateTimePicker1)
        {
            long driv = Convert.ToInt64(comboBox1.SelectedValue);
            using (Context db = new Context(Form1.connectionString))
            {
                var list = from dr in db.drivers
                           join vio in db.violations on dr.id_driver equals vio.id_driver
                           where dr.id_driver == driv
                           select new
                           {
                               surname = dr.surname,
                               name = dr.name,
                               patronymic = dr.patronymic,
                               dateTimeVio = vio.datetime_the_violation,
                               typeVio = vio.type_of_violation,
                               statusVio = vio.violation_status,
                               measuresVio = vio.measures_taken
                           };
                dataGridView1.DataSource = list.Where(x => x.dateTimeVio.Year >= dateTimePicker2.Value.Year && x.dateTimeVio.Month >= dateTimePicker2.Value.Month &&
                x.dateTimeVio.Day >= dateTimePicker2.Value.Day && x.dateTimeVio.Year <= dateTimePicker1.Value.Year &&
                x.dateTimeVio.Month <= dateTimePicker1.Value.Month && x.dateTimeVio.Day <= dateTimePicker1.Value.Day).ToList();




                dataGridView1.Columns[0].HeaderText = "Фамилия водителя";
                dataGridView1.Columns[1].HeaderText = "Имя водителя";
                dataGridView1.Columns[2].HeaderText = "Отчество водителя";
                dataGridView1.Columns[3].HeaderText = "Дата и время";
                dataGridView1.Columns[4].HeaderText = "Тип нарушения";
                dataGridView1.Columns[5].HeaderText = "Статус";
                dataGridView1.Columns[6].HeaderText = "Принятые меры";

                foreach (DataGridViewColumn data in dataGridView1.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView1.EnableHeadersVisualStyles = false;
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
                //автоматическое расстояние столбцов
              

                if (comboBox2.SelectedIndex == 0)
                {
                    //Название в клетке
                    excelWorksheet.Cells[1, "A"] = "Клиенты в чёрном списке";
                    //Выбор диапазона
                    var range3 = excelWorksheet.get_Range("A1");
                    //выравнивание
                    range3.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    range3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //размер текста
                    range3.Cells.Font.Size = 14;
                }
                else if (comboBox2.SelectedIndex == 1)
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
                    //Название в клетке
                    excelWorksheet.Cells[1, "A"] = "Нарушения водителей за определённый промежуток времени";
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
                excelWorksheet.Range["A:Z"].EntireColumn.AutoFit();
                // Сохраняем файл Excel и закрываем приложение
                excelWorkbook.SaveAs(saveFileDialog.FileName);
                excelWorkbook.Close();
                excelApp.Quit();
            }
        }
        public void Export(DataGridView dataGridView, DataGridView dataGridView2)
        {
            // Создаем диалоговое окно для сохранения файла
            saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save Excel File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {//Приложение


                Excel.Application excelApp = new Excel.Application() { SheetsInNewWorkbook = 2, Visible = false, DisplayAlerts = false };
                
                //Книга
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                //Страница
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelApp.Worksheets.get_Item(1);
                Excel.Worksheet excelWorksheet2 = (Excel.Worksheet)excelApp.Worksheets.get_Item(2);
                excelWorksheet.Name = "Диспетчеры";
                excelWorksheet2.Name = "Водители";

                //Заполнение диспетчеров

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
                



                //Заполнение водителей

                // Заполняем заголовки столбцов (начиная с A2)
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    excelWorksheet2.Cells[2, i + 1] = dataGridView2.Columns[i].HeaderText;
                }

                // Заполняем ячейки данными из DataGridView (начиная с A3)
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView2.Columns.Count; j++)
                    {
                        excelWorksheet2.Cells[i + 3, j + 1] = dataGridView2.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                //Добавление границ
                excelWorksheet2.Cells[1, 1].CurrentRegion.Borders.LineStyle = Excel.XlLineStyle.xlContinuous; //границы
                //добавление полужирного шрифта для заголовков таблицы
                excelWorksheet2.Rows[1].Font.Bold = true;
                excelWorksheet2.Rows[2].Font.Bold = true;
                //автоматическое расстояние столбцов
                excelWorksheet2.Range["A:Z"].EntireColumn.AutoFit();

                if (comboBox2.SelectedIndex == 3)
                {
                    //Название в клетке
                    excelWorksheet.Cells[1, "A"] = "Список диспетчеров";
                    //Выбор диапазона
                    var range3 = excelWorksheet.get_Range("A1", "D1");
                    //Объединение клеток в 1
                    range3.Merge(Type.Missing);
                    //выравнивание
                    range3.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    range3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //размер текста
                    range3.Cells.Font.Size = 14;


                    //Название в клетке
                    excelWorksheet2.Cells[1, "A"] = "Список водителей";
                    //Выбор диапазона
                    var range2 = excelWorksheet2.get_Range("A1", "G1");
                    //Объединение клеток в 1
                    range2.Merge(Type.Missing);
                    //выравнивание
                    range2.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    range2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //размер текста
                    range2.Cells.Font.Size = 14;
                    var range = excelWorksheet2.get_Range("E2", "E500");
                    range.Cells.Replace("0:00:00", "");
                }
                //автоматическое расстояние столбцов
                excelWorksheet.Range["A:Z"].EntireColumn.AutoFit();
                //автоматическое расстояние столбцов
                excelWorksheet2.Range["A:Z"].EntireColumn.AutoFit();
                // Сохраняем файл Excel и закрываем приложение
                excelWorkbook.SaveAs(saveFileDialog.FileName);
                excelWorkbook.Close();
                excelApp.Quit();
            }


        }
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    panel5.Visible = true;
                    dataGridView1.DataSource = null;
                    dataGridView4.DataSource = null;
                    dataGridView4.Visible = false;
                    dataGridView1.Visible = true;
                    button2.Enabled = true;
                    panel2.Visible = false;
                    panel4.Visible = false;
                    label6.Visible = false;
                    comboBox1.Visible = false;
                    label1.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    dateTimePicker1.Visible = false;
                    dateTimePicker2.Visible = false;
                    dataGridView1.Visible = true;
                    listClientFromBlackList();
                    break;
                case 1:
                    panel5.Visible = true;
                    dataGridView1.DataSource = null;
                    dataGridView4.DataSource = null;
                    dataGridView4.Visible = true;
                    dataGridView1.Visible = false;
                    button2.Enabled = true;
                    panel2.Visible = false;
                    panel4.Visible = false;
                    label6.Visible = false;
                    comboBox1.Visible = false;
                    label1.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    dateTimePicker1.Visible = false;
                    dateTimePicker2.Visible = false;
                    rateList();
                    break;
                case 2:
                    panel5.Visible = true;
                    dataGridView1.DataSource = null; dataGridView4.DataSource = null;
                    dataGridView4.Visible = false;
                    dataGridView1.Visible = true;
                    button2.Enabled = false;
                    MessageBox.Show("Выберите водителя и промежуто времени", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label6.Visible = true;
                    comboBox1.Visible = true;
                    label1.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    dateTimePicker1.Visible = true;
                    dateTimePicker2.Visible = true;
                    panel2.Visible = false;
                    panel4.Visible = false;
                    dataGridView1.Visible = true;
                    break;
                case 3:
                    label6.Visible = false;
                    comboBox1.Visible = false;
                    label1.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    dateTimePicker1.Visible = false;
                    dateTimePicker2.Visible = false;
                    button2.Enabled = true;
                    personalList();
                    panel5.Visible = false;
                    panel2.Visible = true;
                    panel4.Visible = true;
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            violationsDriver();
        }

        private void dateTimePicker2_ValueChanged_1(object sender, EventArgs e)
        {
            violationsDriver(dateTimePicker2, dateTimePicker1);
            button2.Enabled = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    Export(dataGridView1);
                    MessageBox.Show($"Файл был создан по пути: {saveFileDialog.FileName}");
                    button2.Enabled = false;
                    break;
                case 1:
                    Export(dataGridView4);
                    MessageBox.Show($"Файл был создан по пути: {saveFileDialog.FileName}");
                    button2.Enabled = false;
                    break;
                case 2:
                    Export(dataGridView1);
                    MessageBox.Show($"Файл был создан по пути: {saveFileDialog.FileName}");
                    button2.Enabled = false;
                    break;
                case 3:
                    Export(dataGridView2, dataGridView3);
                    MessageBox.Show($"Файл был создан по пути: {saveFileDialog.FileName}");
                    button2.Enabled = false;
                    break;
            }
        }

        private void dataGridView4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
            {
                if (e.Value != null)
                {
                    switch (e.ColumnIndex)
                    {
                        case 0:

                            if (Convert.ToBoolean(this.dataGridView4.Rows[e.RowIndex].Cells[0].Value) == true)
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
            }
        }
    }
}
