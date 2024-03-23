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
    public partial class addOrEditCar : Form
    {
        private void bazSetting()
        {
            GosNum1.MaxLength = 1;
            GosNum2.MaxLength = 1;
            GosNum3.MaxLength = 1;
            GosNum4.MaxLength = 1;
            GosNum5.MaxLength = 1;
            GosNum6.MaxLength = 1;
            textBox3.MaxLength = 3;
            numericUpDown1.ReadOnly = true;
            numericUpDown2.ReadOnly = true;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            textBox5.Visible = false;

            using (Context db = new Context(Form1.connectionString))
            {
                List<car_category> cc = db.car_category.ToList();
                comboBox1.DataSource = cc;
                comboBox1.ValueMember = "id_car_category";
                comboBox1.DisplayMember = "name";
            }
        }
        public addOrEditCar(int a)
        {
            InitializeComponent();
            bazSetting();
            if (a == 1) ButIzmen.Visible = false;
            else AddCarBut.Visible = false;
        }

        private void GosNum1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            if (e.KeyChar != 'А' && e.KeyChar != 'В' && e.KeyChar != 'Е'
                && e.KeyChar != 'К' && e.KeyChar != 'М' && e.KeyChar != 'Н' && e.KeyChar != 'О' && e.KeyChar != 'Р' && e.KeyChar != 'К'
                && e.KeyChar != 'С' && e.KeyChar != 'Т' && e.KeyChar != 'У' && e.KeyChar != 'Х' && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void GosNum2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private long indexId;
        private void printCarCat()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var cc = db.car_category.Where(p => p.id_car_category == indexId).FirstOrDefault();
                numericUpDown1.Value = cc.number_of_passengers;
                numericUpDown2.Value = cc.fuel_consumption;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            indexId = comboBox1.SelectedIndex+1;
            printCarCat();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private bool checkCar(string state_number, string regCode)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var car = db.cars;
                if (car.Where(p => p.state_number == state_number && p.region_code == regCode).Count() == 1)
                    return true;
                else return false;
            }
        }

        private void AddCarBut_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(GosNum1.Text) || string.IsNullOrWhiteSpace(GosNum2.Text) || string.IsNullOrWhiteSpace(GosNum3.Text) || string.IsNullOrWhiteSpace(GosNum4.Text)
                || string.IsNullOrWhiteSpace(GosNum5.Text) || string.IsNullOrWhiteSpace(GosNum6.Text))
            {
                MessageBox.Show("Заполните все пустые поля");
                return;
            }
            if(checkBox1.Checked == true)
                if(string.IsNullOrWhiteSpace(textBox5.Text)|| string.IsNullOrWhiteSpace(numericUpDown1.Text) || string.IsNullOrWhiteSpace(numericUpDown2.Text))
                {
                    MessageBox.Show("Заполните все пустые поля");
                    return;
                }

            string stateNumber = $"{GosNum1.Text}{GosNum2.Text}{GosNum3.Text}{GosNum4.Text}{GosNum5.Text}{GosNum6.Text}";

            if(checkCar(stateNumber,textBox3.Text))
            {
                MessageBox.Show("Автомобиль с таким номером уже существует","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

                return;
            }
            using (Context db = new Context(Form1.connectionString))
            {
                try
                {
                    var cc = db.car_category;
                    //Получение или создание id категории авто
                    long idCC;
                    if (checkBox1.Checked == true)
                    {
                        if (cc.Where(p => p.number_of_passengers == numericUpDown1.Value && p.fuel_consumption == numericUpDown2.Value && p.name == comboBox1.Text).Count() != 1)
                        {
                            car_category ccs = new car_category()
                            {
                                name = textBox5.Text,
                                number_of_passengers = (int)numericUpDown1.Value,
                                fuel_consumption = numericUpDown2.Value
                            };
                            db.car_category.Add(ccs);
                            db.SaveChanges();
                            idCC = ccs.id_car_category;
                        }
                        else
                        {
                            MessageBox.Show("Такая категория уже существует", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                    long testId = retIdCC();
                        var findCC = cc.Where(p=>p.id_car_category == testId).FirstOrDefault();
                        idCC = findCC.id_car_category;
                    }
                    bool rentCar = comboBox3.Text == "Арендный" ? true : false;
                    car newCar = new car()
                    {
                        rented_car = rentCar,
                        colour = textBox1.Text,
                        car_brand = textBox2.Text,
                        car_model = textBox4.Text,
                        state_number = stateNumber,
                        region_code = textBox3.Text,
                        technical_condition_car = comboBox2.Text,
                        id_car_category = idCC
                    };
                    db.cars.Add(newCar);
                    db.SaveChanges();
                    MessageBox.Show("Автомобиль был добавлен", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

            }
                catch 
                {

                MessageBox.Show("Произошла ошибка, проверьте введённые данные", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        }
        private long retIdCC()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var cc=db.car_category.Where(p => p.number_of_passengers == numericUpDown1.Value && p.fuel_consumption == numericUpDown2.Value && p.name == comboBox1.Text).FirstOrDefault();
                return cc.id_car_category;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox5.Visible = true;
                comboBox1.Visible = false;

                numericUpDown1.ReadOnly = false;
                numericUpDown1.Value = 1;

                numericUpDown2.ReadOnly = false;
                numericUpDown2.Value = 1;

            }
            else if (checkBox1.Checked == false)
            {
                textBox5.Visible = false;
                comboBox1.Visible = true;
                textBox5.Text = "";
                numericUpDown1.ReadOnly = true;
                numericUpDown2.ReadOnly = true;
                printCarCat();
            }
        }
    }
}
