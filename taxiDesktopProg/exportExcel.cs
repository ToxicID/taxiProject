using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;
namespace taxiDesktopProg
{
    internal class exportExcel
    {
        static System.Windows.Forms.SaveFileDialog saveFileDialog;
        returnInformation rI;
     public exportExcel(returnInformation rI)
        {
            this.rI = rI;
        }
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


                if (rI.comboBox2.SelectedIndex == 1)
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
                else if (rI.comboBox2.SelectedIndex == 2)
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
                else if (rI.comboBox2.SelectedIndex == 0)
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
                else if (rI.comboBox2.SelectedIndex == 3)
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
                MessageBox.Show($"Файл был создан по пути: {saveFileDialog.FileName}");
            }
        }
    }
}
