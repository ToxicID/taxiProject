using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace taxiDesktopProg
{
    public partial class workDriver : Form
    {
  
        private void listWorkDriver()
        {
           
            using (Context db = new Context(auth.connectionString))
            {
                DateTime now = DateTime.Now;
            
                 var list = from dr in db.drivers
                           join work in db.work_schedule on dr.id_driver equals work.id_driver
                           select new
                           {
                               id_work = work.id_work_schedule,
                               idDriver = dr.id_driver,
                               FullNameDriver = dr.surname + " " + dr.name + " " + dr.patronymic,
                               dateWorkFrom = work.date_of_work_from,
                               dateWorkBefore = work.date_of_work_before,
                               fromWorkTime = work.work_schedule_from.ToString(),
                               beforeWorkTime = work.work_schedule_before
                               
                           };
                
                dataGridView1.DataSource = list.ToList();
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].HeaderText = "ФИО";
                dataGridView1.Columns[3].HeaderText = "Дата начала работы";
                dataGridView1.Columns[4].HeaderText = "Дата окончания работы";
                dataGridView1.Columns[5].HeaderText = "Время начала работы";
                dataGridView1.Columns[6].HeaderText = "Время окончания работы";

                foreach (DataGridViewColumn data in dataGridView1.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView1.EnableHeadersVisualStyles = false;
            }
            endWorkDriver.Enabled = false;

            label1.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            button2.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;

        }
        public workDriver()
        {
            InitializeComponent();
            listWorkDriver();
            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 2)
            {
                string[] fullname = e.Value.ToString().Split(' ');
                if (fullname[2] == " " || fullname[2] == "Отсутствует")
                {
                    e.Value = fullname[0] + " " + fullname[1].Substring(0,1) + ".";
                }
                else
                {
                    e.Value = fullname[0] + " " + fullname[1].Substring(0, 1) + ". " + fullname[2].Substring(0, 1) + ".";
                }
            }
        }

        private void AddCarBut_Click(object sender, EventArgs e)
        {
            startOrEndWorkDriverForm fm = new startOrEndWorkDriverForm();
            fm.FormClosed += new FormClosedEventHandler(startOrEndWorkDriverForm_FormClosed);
            fm.Show();
        }
        void startOrEndWorkDriverForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            listWorkDriver();
        }

        private void endWorkDriver_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Завершить смену {fullnameDriver}?","Завершить", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.No) return;
            using (Context db = new Context(auth.connectionString))
            {
                var wc = db.work_schedule.Where(p => p.id_work_schedule == DatagridIndex).FirstOrDefault();
                var dr = db.drivers.Where(p => p.id_driver == id_driver).FirstOrDefault();
                if (dr.driver_readiness == "Не готов")
                {
                    MessageBox.Show($"Смена у водителя {fullnameDriver} уже завершена", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                wc.date_of_work_before = DateTime.Now;
                wc.work_schedule_before = DateTime.Now.TimeOfDay;
                dr.driver_readiness = "Не готов";
                dr.status = "Занят";
                db.SaveChanges();
                MessageBox.Show($"Смена {fullnameDriver} была завершена", "Successfully", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            listWorkDriver();
        }
        private long? DatagridIndex = null;
        private long? id_driver = null;
        private string fullnameDriver = null;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DatagridIndex = (long)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                id_driver = (long)dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                fullnameDriver = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

                string[] fullname = fullnameDriver.Split(' ');
                if (fullname[2] == " " || fullname[2] == "Отсутствует")
                {
                    fullnameDriver = fullname[0] + " " + fullname[1];
                }
                else
                {
                    fullnameDriver = fullname[0] + " " + fullname[1] + " "+fullname[2];
                }

                endWorkDriver.Enabled = true;
            }
            catch
            {
                DatagridIndex = null;
                id_driver = null;
                fullnameDriver = null;
                endWorkDriver.Enabled = false;
            }
        }

        private void workDriver_Click(object sender, EventArgs e)
        {
            DatagridIndex = null;
            id_driver = null;
            fullnameDriver = null;
            endWorkDriver.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            button2.Visible = true;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
                
        }
        static SaveFileDialog saveFileDialog;
        public static void Export(DataGridView dataGridView)
        {
            // Создаем диалоговое окно для сохранения файла
             saveFileDialog = new SaveFileDialog();
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
                //Название в клетке
                excelWorksheet.Cells[1, "A"] = "График смены водителей";
                //Выбор диапазона
                var range3 = excelWorksheet.get_Range("A1", "G1");
                //Объединение клеток в 1
                range3.Merge(Type.Missing);
                //выравнивание
                range3.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                range3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //размер текста
                range3.Cells.Font.Size = 14;
                //Убираем время из поля с датой
                var range = excelWorksheet.get_Range("D3","D500");
                range.Cells.Replace("0:00:00", "");

                var range1 = excelWorksheet.get_Range("E3", "E500");
                range1.Cells.Replace("0:00:00", "");
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
            using (Context db = new Context(auth.connectionString))
            {
                var list = from dr in db.drivers
                           join work in db.work_schedule on dr.id_driver equals work.id_driver
                           select new
                           {
                               surname = dr.surname,
                               Name = dr.name,
                               patronymic = dr.patronymic,
                               dateWorkFrom = work.date_of_work_from,
                               dateWorkBefore = work.date_of_work_before,
                               fromWorkTime = work.work_schedule_from.ToString(),
                               beforeWorkTime = work.work_schedule_before

                           };
                var print = list.Where(x => x.dateWorkFrom >= dateTimePicker2.Value && x.dateWorkBefore <= dateTimePicker1.Value).Select(x =>
                 new { x.surname, x.Name, x.patronymic, V = x.dateWorkFrom, x.dateWorkBefore, x.fromWorkTime, x.beforeWorkTime 
                });
                
                dgv.DataSource = print.ToList();
                dgv.Columns[0].HeaderText = "Фамилия водителя";
                dgv.Columns[1].HeaderText = "Имя водителя";
                dgv.Columns[2].HeaderText = "Отчество водителя";
                dgv.Columns[3].HeaderText = "Дата начала работы";
                dgv.Columns[4].HeaderText = "Дата окончания работы";
                dgv.Columns[5].HeaderText = "Время начала работы";
                dgv.Columns[6].HeaderText = "Время окончания работы";
                foreach (DataGridViewColumn data in dgv.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dgv.EnableHeadersVisualStyles = false;

                Export(dgv);
                label1.Visible = false ;
                label3.Visible = false;
                label4.Visible = false;
                button2.Visible = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                MessageBox.Show($"Файл был создан по пути: {saveFileDialog.FileName}");
            }
            
        }
    }
}
