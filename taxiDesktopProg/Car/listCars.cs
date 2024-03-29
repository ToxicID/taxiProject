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

                dataGridView1.DataSource = car.ToList();
                fonts();

            }
        }
        public listCars()
        {
            InitializeComponent();
            print(dataGridView1);
            RemoveCar.Enabled = false;
            button2.Enabled = false;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
                if (Convert.ToBoolean(this.dataGridView1.Rows[e.RowIndex].Cells[1].Value) == true)
                {
                    e.Value = "Авто фирмы";
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
            lists();
            fonts();


        }

        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        //Вызов формы добавления и создание события закрытия формы
        private void AddCarBut_Click(object sender, EventArgs e)
        {
            addOrEditCar fm = new addOrEditCar();
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
                button2.Enabled = true;
            }
            catch
            {
                DataGridIndex = null;
                RemoveCar.Enabled = false;
                button2.Enabled = false ;
            }
        }

        private void listCars_Click(object sender, EventArgs e)
        {
            DataGridIndex = null;
            RemoveCar.Enabled = false;
            button2.Enabled = false;
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(DataGridIndex != null)
            {
                addOrEditCar fm = new addOrEditCar(DataGridIndex);
                fm.FormClosed += new FormClosedEventHandler(addOrEditCar_FormClosed);
                fm.Show();
                RemoveCar.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                MessageBox.Show("Необходимо выбрать автомобиль, данные которого нужно изменить","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (DataGridIndex != null)
            {
                addOrEditCar fm = new addOrEditCar(DataGridIndex);
                fm.FormClosed += new FormClosedEventHandler(addOrEditCar_FormClosed);
                fm.Show();
                RemoveCar.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                MessageBox.Show("Необходимо выбрать автомобиль, данные которого нужно изменить", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
