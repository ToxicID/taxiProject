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
using static System.Net.Mime.MediaTypeNames;

namespace taxiDesktopProg
{
    public partial class listCars : Form
    {
        private long? DataGridIndex = null;
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
            RemoveCar.Enabled = false;
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
      private void lists()
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
                var carNew = car.Where(x => x.state_number.Contains(textBox2.Text)).ToList();
                dataGridView1.DataSource = carNew;
            }
            }
        private void fonts()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Принадлежность";
            dataGridView1.Columns[2].HeaderText = "Цвет";
            dataGridView1.Columns[3].HeaderText = "Бренд";
            dataGridView1.Columns[4].HeaderText = "Модель";
            dataGridView1.Columns[5].HeaderText = "Гос. номер";
            dataGridView1.Columns[6].HeaderText = "Рег. код";
            dataGridView1.Columns[7].HeaderText = "Тех. состояние";
            dataGridView1.Columns[8].HeaderText = "Категория автомобиля";

            foreach (DataGridViewColumn data in dataGridView1.Columns)
                data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            //lists();
            //fonts();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox2.Text.ToUpper()))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }

        }

        //Вызов формы добавления и создание события закрытия формы
        private void AddCarBut_Click(object sender, EventArgs e)
        {
            addOrEditCar fm = new addOrEditCar(1);
            fm.FormClosed += new FormClosedEventHandler(addOrEditCar_FormClosed);
            fm.Show();
        }
        void addOrEditCar_FormClosed(object sender, FormClosedEventArgs e)
        {
            print(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridIndex = (long)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                RemoveCar.Enabled = true;
            }
            catch
            {
                DataGridIndex = null;
                RemoveCar.Enabled = false;
            }
        }

        private void listCars_Click(object sender, EventArgs e)
        {
            DataGridIndex = null;
            RemoveCar.Enabled = false;
        }

        private void RemoveCar_Click(object sender, EventArgs e)
        {
            if (DataGridIndex != null)
            {
                using (Context db = new Context(Form1.connectionString))
                {
                    DialogResult result = MessageBox.Show("Удалить данный автомобиль?", "Удаление",
                                                                                       MessageBoxButtons.YesNo,
                                                                                       MessageBoxIcon.Information);
                    if (result == DialogResult.No) return;
                    var delete = db.cars
                                .Where(g => g.id_car == DataGridIndex)
                                .FirstOrDefault();
                    db.cars.Remove(delete);
                    db.SaveChanges();
                    DataGridIndex = null;
                    RemoveCar.Enabled = false;
                    print(dataGridView1);
                    MessageBox.Show("Автомобиль был удален");
                }
            }
        }
    }
}
