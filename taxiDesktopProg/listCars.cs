using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taxiDesktopProg
{
    public partial class listCars : Form
    {
        private void print(DataGridView dataName)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var car = from c in db.cars
                          join car_cat in db.car_category on c.id_car_category equals car_cat.id_car_category
                          select new
                          {
                              id_car = c.id_car,
                              rented_car = c.rented_car.ToString(),
                              color = c.colour,
                              car_brand = c.car_brand,
                              model_car = c.car_model,
                              state_number = c.state_number,
                              region_code = c.region_code,
                              technical_condition_car = c.technical_condition_car,
                              car_categoor = car_cat.name
                          };

                dataName.DataSource = car.ToList();
                dataName.Columns[0].Visible = false;
                dataName.Columns[1].HeaderText = "Принадлежность";
                dataName.Columns[2].HeaderText = "Цвет";
                dataName.Columns[3].HeaderText = "Бренд";
                dataName.Columns[4].HeaderText = "Модель";
                dataName.Columns[5].HeaderText = "Гос. номер";
                dataName.Columns[6].HeaderText = "Рег. код";
                dataName.Columns[7].HeaderText = "Тех. состояние";
                dataName.Columns[8].HeaderText = "Категория автомобиля";

                foreach (DataGridViewColumn data in dataName.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataName.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataName.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataName.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataName.EnableHeadersVisualStyles = false;

            }
        }
        public listCars()
        {
            InitializeComponent();
            print(dataGridView1);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
                if (Convert.ToBoolean(this.dataGridView1.Rows[e.RowIndex].Cells[1].Value) == true)
                {
                    e.Value = "Арендный";
                }
            else
                    e.Value = "Личный";
            else if(e.ColumnIndex == 7)
                if(e.Value.ToString() == "Исправлено")
                    e.CellStyle.ForeColor = Color.Green;
                else
                    e.CellStyle.ForeColor = Color.Red;
        }
    }
}
