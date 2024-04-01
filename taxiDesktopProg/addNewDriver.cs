using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace taxiDesktopProg
{
    public partial class addNewDriver : Form
    {
        public addNewDriver()
        {
            InitializeComponent();
            label8.Text = "Номер \nводительского\nудостоверения";
            ButIzmen.Visible = false;
        }
        private long? idDriver;
        private string oldCall_sign;
        public addNewDriver(long? idDriver)
        {
            InitializeComponent();
            AddDriverBut.Visible = false;
            dateTimePicker1.Enabled = false;
            label8.Text = "Номер \nводительского\nудостоверения";
            this.idDriver = idDriver;
            using (Context db = new Context(Form1.connectionString))
            {
                var dr = db.drivers.Where(p => p.id_driver == idDriver).FirstOrDefault();
                textBox3.Text = dr.call_sign;
                oldCall_sign = dr.call_sign;
                textBox1.Text = dr.surname;
                textBox2.Text = dr.name;
                textBox4.Text = dr.patronymic;
                if(dr.patronymic == "" || dr.patronymic == "Отсутствует" || dr.patronymic == null)
                        checkBox1.Checked = true;
                dateTimePicker1.Value = dr.date_of_birth;
                maskedTextBox1.Text = dr.drivers_license_number;
                textBox5.Text = dr.mobile_phone;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
            
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox5.MaxLength = 11;
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private string preliminaryPatronymic = "";
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                preliminaryPatronymic = textBox4.Text;
                textBox4.ReadOnly = true;
                textBox4.Text = "Отсутствует";
            }
            else
            {
                textBox4.ReadOnly = false;
                textBox4.Text = preliminaryPatronymic;
            }
        }

        private void AddCarBut_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Некорректный ввод даты рождения.\nОн не мог родиться в будующем", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker1.Value = DateTime.Now;
                return;
            }
            if ((DateTime.Now.Year - dateTimePicker1.Value.Year) < 18)
            {
                MessageBox.Show("Водитель должен быть совершенолетним", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker1.Value = DateTime.Now;
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || 
               string.IsNullOrWhiteSpace(maskedTextBox1.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Заполните пустые поля","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (textBox5.Text.Length != 11 || textBox5.Text[0] == '0')
            {
                MessageBox.Show("Поле номер телефона клиента заполнено некорректно:\n" +
                                "1. Проверьте, возможно введено не 11 символов\n" +
                                "2. Проверьте, возможно номер телефона начинатеся с \'0\'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text))
                textBox4.Text = "Отсутствует";

            using (Context db = new Context(Form1.connectionString))
            {
                var drOld = db.drivers;
               
                 if(drOld.Where(p=>p.call_sign == textBox3.Text).Count() >= 1)
                {
                    MessageBox.Show("Водитель с таким позывным уже существуюет в базе данных", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if(drOld.Where(p => p.drivers_license_number == maskedTextBox1.Text).Count() >= 1)
                {
                    MessageBox.Show("Водитель с таким номером водительского удостоверения уже существуюет в базе данных", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (drOld.Where(p => p.mobile_phone == textBox5.Text).Count() >= 1)
                {
                    MessageBox.Show("Водитель с номером телефона уже существуюет в базе данных", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                driver drNew = new driver()
                {
                    status = "Занят",
                    driver_readiness = "Не готов",
                    call_sign = textBox3.Text,
                    surname = textBox1.Text,
                    name = textBox2.Text,
                    patronymic = textBox4.Text,
                    date_of_birth = dateTimePicker1.Value,
                    drivers_license_number = maskedTextBox1.Text,
                    mobile_phone = textBox5.Text,
                    id_car = null
                };
                db.drivers.Add(drNew);
                db.SaveChanges();
                MessageBox.Show("Водитель добавлен", "Successfully",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Close();
            }
        }

        private void ButIzmen_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Некорректный ввод даты рождения.\nОн не мог родиться в будующем", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker1.Value = DateTime.Now;
                return;
            }
            if ((DateTime.Now.Year - dateTimePicker1.Value.Year) < 18)
            {
                MessageBox.Show("Водитель должен быть совершенолетним", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker1.Value = DateTime.Now;
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) ||
               string.IsNullOrWhiteSpace(maskedTextBox1.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Заполните пустые поля", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox5.Text.Length != 11 || textBox5.Text[0] == '0')
            {
                MessageBox.Show("Поле номер телефона клиента заполнено некорректно:\n" +
                                "1. Проверьте, возможно введено не 11 символов\n" +
                                "2. Проверьте, возможно номер телефона начинатеся с \'0\'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text))
                textBox4.Text = "Отсутствует";
            using (Context db = new Context(Form1.connectionString))
            {
                var drAll = db.drivers;
                var dr = db.drivers.Where(p => p.id_driver == idDriver).FirstOrDefault();
                if (textBox3.Text != oldCall_sign)
                {
                    if (drAll.Where(p => p.call_sign == textBox3.Text).Count() >= 1)
                    {
                        MessageBox.Show("Водитель с таким позывным уже существуюет в базе данных", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                dr.call_sign = textBox3.Text;
                    dr.surname = textBox1.Text;
                dr.name = textBox2.Text;
                dr.patronymic = textBox4.Text;
                dr.date_of_birth = dateTimePicker1.Value;
                dr.drivers_license_number = maskedTextBox1.Text;
                dr.mobile_phone = textBox5.Text;
                db.SaveChanges();
                MessageBox.Show("Данные водителя изменены", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }
    }
}
