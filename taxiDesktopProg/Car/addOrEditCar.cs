using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            textBox6.Visible = false;
            comboBox4.Visible = false;
            maskedTextBox1.Visible = false;
            button1.Visible = false;
           
            using (Context db = new Context(Form1.connectionString))
            {
                List<car_category> cc = db.car_category.ToList();
                comboBox1.DataSource = cc;
                comboBox1.ValueMember = "id_car_category";
                comboBox1.DisplayMember = "name";
            }
     
        }
        public addOrEditCar()
        {
            InitializeComponent();
            bazSetting();
            label14.Text = "Номер \nводительского\nудостоверения";
            ButIzmen.Visible = false;
           AddCarBut.Visible = true;
        }
        private void UpdateCar(long? id)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                car c = db.cars.Find(id);
                string chars = c.state_number;
                char[] gosnum = chars.ToCharArray();
                if (string.IsNullOrWhiteSpace(gosnum[0].ToString())) GosNum1.Text = "";
                else GosNum1.Text = gosnum[0].ToString();

                if (string.IsNullOrWhiteSpace(gosnum[1].ToString())) GosNum2.Text = "";
                else GosNum2.Text = gosnum[1].ToString();

                if (string.IsNullOrWhiteSpace(gosnum[2].ToString())) GosNum3.Text = "";
                else GosNum3.Text = gosnum[2].ToString();

                if (string.IsNullOrWhiteSpace(gosnum[3].ToString())) GosNum4.Text = "";
                else GosNum4.Text = gosnum[3].ToString();

                if (string.IsNullOrWhiteSpace(gosnum[4].ToString())) GosNum5.Text = "";
                else GosNum5.Text = gosnum[4].ToString();

                if (string.IsNullOrWhiteSpace(gosnum[5].ToString())) GosNum6.Text = "";
                else GosNum6.Text = gosnum[5].ToString();

                textBox1.Text = c.colour;
                textBox2.Text = c.car_brand;
                textBox4.Text = c.car_model;
                textBox3.Text = c.region_code;
                comboBox2.Text = c.technical_condition_car;

        
                comboBox1.SelectedValue = c.car_category.id_car_category;
                if (c.rented_car)
                    comboBox3.Text = "Авто фирмы";
                else
                    comboBox3.Text = "Личный";

            }
        }
        long? idCar;
        public addOrEditCar(long? id)
        {
            InitializeComponent();
            bazSetting();
            UpdateCar(id);
            ButIzmen.Visible = true;
            AddCarBut.Visible = false;
            comboBox3.Enabled = false;
            GosNum1.ReadOnly = true;
            GosNum2.ReadOnly = true;
            GosNum3.ReadOnly = true;
            GosNum4.ReadOnly = true;
            GosNum5.ReadOnly = true;
            GosNum6.ReadOnly = true;
            textBox3.ReadOnly = true;
            idCar = id;
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
            //indexId = Convert.ToInt64(comboBox1.SelectedValue);
            if (comboBox1.SelectedItem != null)
            {
                car_category selectedCategory = (car_category)comboBox1.SelectedItem;
                 indexId = selectedCategory.id_car_category;
            }
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

        private void testPol()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) ||
               string.IsNullOrWhiteSpace(GosNum1.Text) || string.IsNullOrWhiteSpace(GosNum2.Text) || string.IsNullOrWhiteSpace(GosNum3.Text) || string.IsNullOrWhiteSpace(GosNum4.Text)
               || string.IsNullOrWhiteSpace(GosNum5.Text) || string.IsNullOrWhiteSpace(GosNum6.Text))
            {
                MessageBox.Show("Заполните все пустые поля");
                return;
            }
            if (checkBox1.Checked == true)
                if (string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(numericUpDown1.Text) || string.IsNullOrWhiteSpace(numericUpDown2.Text))
                {
                    MessageBox.Show("Заполните все пустые поля");
                    return;
                }
           
        }
        private void AddCarBut_Click(object sender, EventArgs e)
        {
            testPol();
            string stateNumber = $"{GosNum1.Text}{GosNum2.Text}{GosNum3.Text}{GosNum4.Text}{GosNum5.Text}{GosNum6.Text}";
            if (checkCar(stateNumber, textBox3.Text))
            {
                MessageBox.Show("Автомобиль с таким номером уже существует", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    bool rentCar = comboBox3.Text == "Авто фирмы" ? true : false;
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
                    if (newCar.rented_car == false)
                    {
                        startView();
                        this.Height = 695;
                        enableNaznachDriver();
                        MessageBox.Show("Назначьте водителя", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        id_carNewCar = newCar.id_car;
                        AddCarBut.Enabled = false;
                        ButIzmen.Enabled = false;
                        comboBox2.Enabled = false;
                        checkBox1.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Автомобиль был добавлен", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
            }
                catch 
                {

                MessageBox.Show("Произошла ошибка, проверьте введённые данные", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        }
        private void enableNaznachDriver()
        {
            label12.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            textBox6.Visible = true;
            comboBox4.Visible = true;
            maskedTextBox1.Visible = true;
            button1.Visible = true;
        }
        long id_carNewCar;
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

                comboBox4.DataSource = bindingSource1.DataSource;
                comboBox4.DisplayMember = "FullName";
                comboBox4.ValueMember = "id_driverses";
                maskedTextBox1.ReadOnly = true;
                textBox6.ReadOnly = true;
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

        private void ButIzmen_Click(object sender, EventArgs e)
        {
            testPol();
            string stateNumber = $"{GosNum1.Text}{GosNum2.Text}{GosNum3.Text}{GosNum4.Text}{GosNum5.Text}{GosNum6.Text}";
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
                        var findCC = cc.Where(p => p.id_car_category == testId).FirstOrDefault();
                        idCC = findCC.id_car_category;
                    }
                    bool rentCar = comboBox3.Text == "Авто фирмы" ? true : false;
                   
                        car car = db.cars.Find(idCar);
                        car.rented_car = rentCar;
                        car.colour = textBox1.Text;
                        car.car_brand = textBox2.Text;
                        car.car_model = textBox4.Text;
                        car.state_number = stateNumber;
                        car.region_code = textBox3.Text;
                        car.technical_condition_car = comboBox2.Text;
                        car.id_car_category = idCC;
                        db.SaveChanges();
                        MessageBox.Show("Данные о автомобиле были обновлены", "Успешно",MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                }
                catch
                {

                    MessageBox.Show("Произошла ошибка, проверьте введённые данные", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        long id_driverForAuto;
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (Context db = new Context(Form1.connectionString)) {
                long idDriver;
                if (comboBox4.SelectedItem != null)
                {
                    driverses selectedCategory = (driverses)comboBox4.SelectedItem;
                    idDriver = selectedCategory.id_driverses;
                
                var dr = db.drivers.Where(x => x.id_driver == idDriver).FirstOrDefault();
            
            textBox6.Text = dr.call_sign;
            maskedTextBox1.Text = dr.drivers_license_number;
            id_driverForAuto = dr.id_driver;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Назначить этого водителя на выбранный автомобиль?","Infromation",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if(result == DialogResult.Yes)
            {
                using (Context db = new Context(Form1.connectionString))
                {
                    var driver = db.drivers.Where(x => x.id_driver == id_driverForAuto).FirstOrDefault();
                    driver.id_car = id_carNewCar;
                    db.SaveChanges();
                    MessageBox.Show("Автомобиль был назначен", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
