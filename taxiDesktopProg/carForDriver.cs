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
    public partial class carForDriver : Form
    {
        public carForDriver()
        {
            InitializeComponent();
            listDriver();
            listCar();

        }
        private long? DataGrid1IndexDriver = null;
        private long? DataGrid2IndexCar = null;
        private void listCar()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var car = from c in db.cars
                          join car_cat in db.car_category on c.id_car_category equals car_cat.id_car_category
                          where c.technical_condition_car == "Исправлено" && c.rented_car == true
                          select new
                          {
                              id_car = c.id_car,
                              color = c.colour,
                              car_brand = c.car_brand,
                              model_car = c.car_model,
                              state_number = c.state_number,
                              region_code = c.region_code,
                              car_categoor = car_cat.name + "_" + car_cat.number_of_passengers
                          };

                dataGridView2.DataSource = car.ToList();
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].HeaderText = "Цвет";
                dataGridView2.Columns[2].HeaderText = "Бренд";
                dataGridView2.Columns[3].HeaderText = "Модель";
                dataGridView2.Columns[4].HeaderText = "Гос. номер";
                dataGridView2.Columns[5].HeaderText = "Рег. код";
                dataGridView2.Columns[6].HeaderText = "Категория автомобиля_Кол-во мест";

                foreach (DataGridViewColumn data in dataGridView2.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView2.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView2.EnableHeadersVisualStyles = false;
            }
        }
        private void listDriver()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var dr = db.drivers.ToList();
#pragma warning disable CS0472 // Результат значения всегда одинаковый, так как значение этого типа никогда не равно NULL
                var driverWithFirmCars = from drive in db.drivers
                                         join car in db.cars 
                                            on drive.id_car equals car.id_car into grouping
                                         from p in grouping.DefaultIfEmpty()
                                         
                                         select new
                                         {
                                             id_driver = drive.id_driver,
                                             rental = p.rented_car != null ? p.rented_car : true,
                                             
                                             call_sign = drive.call_sign,
                                             surname = drive.surname,
                                             name = drive.name,
                                             patronymic = drive.patronymic,
                                             drivers_license_number = drive.drivers_license_number,
                                             mob_phone = drive.mobile_phone
                                         };
#pragma warning restore CS0472 // Результат значения всегда одинаковый, так как значение этого типа никогда не равно NULL

                dataGridView1.DataSource = driverWithFirmCars.Where(x=>x.rental != false).ToList();
            }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "Позывной";
            dataGridView1.Columns[3].HeaderText = "Фамилия";
            dataGridView1.Columns[4].HeaderText = "Имя";
            dataGridView1.Columns[5].HeaderText = "Отчество";
            dataGridView1.Columns[6].HeaderText = "Номер вод. удостоверения";
            dataGridView1.Columns[7].HeaderText = "Номер телефона";
            
            foreach (DataGridViewColumn data in dataGridView1.Columns)
                data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
            dataGridView1.EnableHeadersVisualStyles = false;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGrid1IndexDriver = (long)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                checkDriver = true;
            }
            catch
            {
                DataGrid1IndexDriver = null;
                checkDriver = false;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGrid2IndexCar = (long)dataGridView2.Rows[e.RowIndex].Cells[0].Value;
                checkCar = true;
            }
            catch
            {
                DataGrid2IndexCar = null;
                checkCar = false;
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool checkDriver = false;
        bool checkCar = false;
        private void AddDispatcherBut_Click(object sender, EventArgs e)
        {
            if (checkDriver == false)
            {
                MessageBox.Show("Ошибка.\nНеобходимо выбрать водителя", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (checkCar == false)
            {
                MessageBox.Show("Ошибка.\nНеобходимо выбрать автомобиль", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Назначить выбранный автомобиль водителю?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.No)
                {
                    DataGrid1IndexDriver = null;
                    DataGrid2IndexCar = null;
                    checkCar = false;
                    checkDriver = false;
                    return;
                }
                using (Context db = new Context(Form1.connectionString))
                {
                    //поиск авто
                    var car = db.cars.Where(p => p.id_car == DataGrid2IndexCar).FirstOrDefault();
                    //получение количества человек за этим автомобилем.
                    //Если количество больше равно 3, то на этом автомобиле максимум человек, и назначить водителя сюда нельзя
                    //Иначе назначаем
                    var driverListWithThisCar = db.drivers.Where(p => p.id_car == car.id_car);
                    int count = 0;
                    foreach (var z in driverListWithThisCar)
                        count++;

                    if(count >= 3)
                    {
                        MessageBox.Show("На этом автмобиле уже максимальное число водителей.\nВыберите другой автомобиль", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        checkCar = false;
                        DataGrid2IndexCar = null;
                        return;
                    }

                    //поиск водителя
                    var driver = db.drivers.Where(p => p.id_driver == DataGrid1IndexDriver).FirstOrDefault();
                    
                    //Проверка на уже наличие авто.
                    //Если выбрать "Да", авто отвяжется от текущего водителя.
                    if(driver.id_car != null)
                    {
                        DialogResult resut = MessageBox.Show("Отвязать автомобиль, который уже привязан?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if(resut == DialogResult.Yes)
                        {
                            driver.id_car = null;
                            db.SaveChanges();
                           
                        }
                    }
                    driver.id_car = car.id_car;
                    db.SaveChanges();
                    if(string.IsNullOrWhiteSpace(driver.patronymic) || driver.patronymic == "Отсутствует")
                        MessageBox.Show($"Автомобиль был назначен водителю:{driver.surname} {driver.name.Substring(0, 1)}.");
                    else
                    MessageBox.Show($"Автомобиль был назначен водителю:{driver.surname} {driver.name.Substring(0,1)}. {driver.patronymic.Substring(0, 1)}.");
                }
            }
        }

        private void carForDriver_Click(object sender, EventArgs e)
        {
            DataGrid1IndexDriver = null;
            DataGrid2IndexCar = null;
            checkCar = false;
            checkDriver = false;
        }
    }
}
