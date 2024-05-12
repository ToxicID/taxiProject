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
    public partial class listDriver : Form
    {
        private void listDriverDataGrid()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var dr = db.drivers.Select(x=> new { x.id_driver, x.status,x.driver_readiness,x.call_sign,x.surname,x.name,x.patronymic,x.date_of_birth,x.drivers_license_number,x.mobile_phone}).ToList();
                dataGridView1.DataSource = dr;
            }
            fonts();
        }
        private void searchDriver()
        {
            
                using (Context db = new Context(Form1.connectionString))
                {
                    var dr = db.drivers.Where(x => x.surname.Contains(textBox2.Text)).ToList();
                dataGridView1.DataSource = dr;

                }
            
        }
        private void fonts()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Статус";
            dataGridView1.Columns[2].HeaderText = "Готовность";
            dataGridView1.Columns[3].HeaderText = "Позывной";
            dataGridView1.Columns[4].HeaderText = "Фамилия";
            dataGridView1.Columns[5].HeaderText = "Имя";
            dataGridView1.Columns[6].HeaderText = "Отчество";
            dataGridView1.Columns[7].HeaderText = "Дата рождения";
            dataGridView1.Columns[8].HeaderText = "Номер вод. удостоверения";
            dataGridView1.Columns[9].HeaderText = "Номер телефона";
            foreach (DataGridViewColumn data in dataGridView1.Columns)
                data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
            dataGridView1.EnableHeadersVisualStyles = false;
        }
        private long? DataGridIndex = null;
        public listDriver()
        {
            InitializeComponent();
            button1.Enabled = false;
            button3.Enabled = false;
            button5.Enabled = false;
            listDriverDataGrid();
        }
        public listDriver(string name)
        {
            InitializeComponent();
            button1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button2.Visible = false;
            button5.Enabled = false;
            listDriverDataGrid();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                searchInListCarAndDriver();
                fontlistCarAndDriver();
            }
            else
            {
                searchDriver();
                fonts();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addNewDriver fm = new addNewDriver();
            fm.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridIndex = (long)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                button3.Enabled = true;
                button1.Enabled = true;
                button5.Enabled = true;
            }
            catch
            {
                DataGridIndex = null;
                button3.Enabled = false;
                button1.Enabled = false;
                button5.Enabled = false;
            }
        }

        private void listDriver_Click(object sender, EventArgs e)
        {
            DataGridIndex = null;
            button3.Enabled = false;
            button1.Enabled = false;
            button5.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DataGridIndex != null)
            {
                DialogResult result = MessageBox.Show("Изменить данные водителя?", "Изменение",
                                                                                       MessageBoxButtons.YesNo,
                                                                                       MessageBoxIcon.Information);
                    if (result == DialogResult.No) return;
                addNewDriver fm = new addNewDriver(DataGridIndex);
                fm.FormClosed += new FormClosedEventHandler(addNewDriver_FormClosed);
                fm.Show();
                DataGridIndex = null;
                button3.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Необходимо выбрать автомобиль, данные которого нужно изменить", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        void addNewDriver_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                listCarAndDriver();
                fontlistCarAndDriver();
            }
            else
            {

                listDriverDataGrid();
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DataGridIndex != null)
            {
                using (Context db = new Context(Form1.connectionString))
                {
                    DialogResult result = MessageBox.Show("Удалить водителя?", "Удаление",
                                                                                       MessageBoxButtons.YesNo,
                                                                                       MessageBoxIcon.Information);
                    if (result == DialogResult.No) return;
                    var delete = db.drivers
                                .Where(g => g.id_driver== DataGridIndex)
                                .FirstOrDefault();
                    db.drivers.Remove(delete);
                    db.SaveChanges();
                    DataGridIndex = null;
                    button3.Enabled = false;
                    button1.Enabled = false;

                    if (checkBox1.Checked == true)
                    {
                        listCarAndDriver();
                        fontlistCarAndDriver();
                    }
                    else
                    {

                        listDriverDataGrid();

                    }
                    MessageBox.Show("Водитель был удален");
                }
            }
        }
        private void listCarAndDriver()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var dr = from drive in db.drivers
                         join c in db.cars
                         on drive.id_car equals c.id_car
                         join car_cat in db.car_category on c.id_car_category equals car_cat.id_car_category
                         select new
                         {
                             id_driver = drive.id_driver,
                             status = drive.status,
                             driver_readiness = drive.driver_readiness,
                             call_sign = drive.call_sign,
                             surname = drive.surname,
                             name = drive.name,
                             patronymic = drive.patronymic,
                             date_bith = drive.date_of_birth,
                             drivers_license_number = drive.drivers_license_number,
                             mobilePhone = drive.mobile_phone,
                             car_brand = c.car_brand,
                             model_car = c.car_model,
                             color = c.colour,
                             state_number = c.state_number,
                             region_code = c.region_code,
                             car_categoor = car_cat.name + "_" + car_cat.number_of_passengers

                         };
                dataGridView1.DataSource = dr.ToList();
            }
        }
        private void searchInListCarAndDriver()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var dr = from drive in db.drivers
                         join c in db.cars
                         on drive.id_car equals c.id_car
                         join car_cat in db.car_category on c.id_car_category equals car_cat.id_car_category
                         select new
                         {
                             id_driver = drive.id_driver,
                             status = drive.status,
                             driver_readiness = drive.driver_readiness,
                             call_sign = drive.call_sign,
                             surname = drive.surname,
                             name = drive.name,
                             patronymic = drive.patronymic,
                             date_bith = drive.date_of_birth,
                             drivers_license_number = drive.drivers_license_number,
                             mobilePhone = drive.mobile_phone,
                             car_brand = c.car_brand,
                             model_car = c.car_model,
                             color = c.colour,
                             state_number = c.state_number,
                             region_code = c.region_code,
                             car_categoor = car_cat.name + "_" + car_cat.number_of_passengers

                         };
                dataGridView1.DataSource = dr.Where(x => x.surname.Contains(textBox2.Text)).ToList();

            }
        }
        private void fontlistCarAndDriver()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Статус";
            dataGridView1.Columns[2].HeaderText = "Готовность";
            dataGridView1.Columns[3].HeaderText = "Позывной";
            dataGridView1.Columns[4].HeaderText = "Фамилия";
            dataGridView1.Columns[5].HeaderText = "Имя";
            dataGridView1.Columns[6].HeaderText = "Отчество";
            dataGridView1.Columns[7].HeaderText = "Дата рождения";
            dataGridView1.Columns[8].HeaderText = "Номер вод. удостоверения";
            dataGridView1.Columns[9].HeaderText = "Номер телефона";
            dataGridView1.Columns[10].HeaderText = "Бренд";
            dataGridView1.Columns[11].HeaderText = "Модель";
            dataGridView1.Columns[12].HeaderText = "Цвет";
            dataGridView1.Columns[13].HeaderText = "Гос. номер";
            dataGridView1.Columns[14].HeaderText = "Рег. код";
            dataGridView1.Columns[15].HeaderText = "Категория автомобиля_Кол-во мест";
        }
            private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                listCarAndDriver();
                fontlistCarAndDriver();
            }
            else
            {

                listDriverDataGrid();
             
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (DataGridIndex != null)
            {

                DialogResult res = MessageBox.Show("Отвязать автомобиль у этого водителя?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.No)
                {
                    return;
                }
                using (Context db = new Context(Form1.connectionString))
                {
                    var driver = db.drivers.Find(DataGridIndex);
                    driver.id_car = null;
                    db.SaveChanges();
                    DataGridIndex = null;
                    button5.Enabled = false;
                    MessageBox.Show("Автомобиль был отвязан", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (checkBox1.Checked == true)
            {
                listCarAndDriver();
                fontlistCarAndDriver();
            }
            else
            {

                listDriverDataGrid();

            }
        }

       
    }
}
