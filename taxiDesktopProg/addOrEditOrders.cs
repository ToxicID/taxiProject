using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace taxiDesktopProg
{
    public partial class addOrEditOrders : Form
    {
        //ID для редактирования заказа
        private long? idOrder = 0;
        //Конструктор для добавления
        public addOrEditOrders()
        {
            InitializeComponent();
            label16.Text = "Перевозка" + Environment.NewLine + "домашних животных";
            using (Context db = new Context(Form1.connectionString))
            {
                findAddressCity(textBoxCity1);
                findAddressCity(textBoxCity2);
                findClientMobile(textBox1);

                List<rate> cp = db.rates.Where(p=>p.availability == true).ToList();
                
                    comboBox2.DataSource = cp;
                    comboBox2.ValueMember = "id_rate";
                    comboBox2.DisplayMember = "name";
                boardingBox.ReadOnly = true;
                costDowntimeBox.ReadOnly = true;
                costPerKilometerBox.ReadOnly = true;
                childSafetySeatBox.ReadOnly = true;
                transportationOfPetBox.ReadOnly = true;
                groupBox2.Visible = false;
                buttonEdit.Visible = false;
             
                }
        }
        //Конструктор для редактирования
        public addOrEditOrders(long? id, int index)
        {
            idOrder = id;
            InitializeComponent();
            label16.Text = "Перевозка" + Environment.NewLine + "домашних животных";
            using (Context db = new Context(Form1.connectionString))
            {
                List<rate> cp = db.rates.Where(p => p.availability == true).ToList();

                
                boardingBox.ReadOnly = true;
                costDowntimeBox.ReadOnly = true;
                costPerKilometerBox.ReadOnly = true;
                childSafetySeatBox.ReadOnly = true;
                transportationOfPetBox.ReadOnly = true;
                groupBox2.Visible = false;
                textBoxCity1.ReadOnly = true;
                textBoxCity2.ReadOnly = true;
                textBoxStreet1.ReadOnly = true;
                textBoxStreet2.ReadOnly = true;
                textBoxHouse1.ReadOnly = true;
                textBoxHouse2.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox6.ReadOnly = true;
                PlaceOrder.Visible = false;
                buttonEdit.Enabled = false;

                var order = db.orders.Where(p => p.id_order == idOrder).FirstOrDefault();
                textBoxCity1.Text = order.address1.city;
                textBoxStreet1.Text = order.address1.street;
                textBoxHouse1.Text = order.address1.house;
                textBox4.Text = order.address1.enrance;

                textBoxCity2.Text = order.address.city;
                textBoxStreet2.Text = order.address.street;
                textBoxHouse2.Text = order.address.house;
                textBox6.Text = order.address.enrance;

                comboBox2.DataSource = cp;
                comboBox2.ValueMember = "id_rate";
                comboBox2.DisplayMember = "name";

                comboBox2.SelectedIndex = (int)order.id_rate-1;
                textBox1.Text = order.client.mobile_phone;
                comboBox1.Text = order.payment_method;
                priceBox.Text = order.order_cost.ToString();
                if (index == 2)
                {
                        checkBox1.Checked = true;
                        dateTimePicker1.Value = order.datetime_placing_the_order;
                        dateTimePicker2.Value = order.datetime_placing_the_order;
                    
                }
              

            }
        }
        
            private void findClientMobile(TextBox text)
        {
            AutoCompleteStringCollection textComplete = new AutoCompleteStringCollection();

            using (Context db = new Context(Form1.connectionString))
            {
                var Client = db.clients;

                foreach (client cl in Client)
                {
                    textComplete.Add(cl.mobile_phone);
                }

                text.AutoCompleteCustomSource = textComplete;
                text.AutoCompleteMode = AutoCompleteMode.Suggest;
                text.AutoCompleteSource = AutoCompleteSource.CustomSource;

            }
        }
        //следующие три метода осуществляют автозаполнение Города, Улицы и Дома адреса
        private void findAddressCity(TextBox text)
        {
            AutoCompleteStringCollection textComplete = new AutoCompleteStringCollection();

            using (Context db = new Context(Form1.connectionString))
            {
                var city = db.addresses;

                foreach (address City in city)
                {
                    textComplete.Add(City.city);
                }

                text.AutoCompleteCustomSource = textComplete;
                text.AutoCompleteMode = AutoCompleteMode.Suggest;
                text.AutoCompleteSource = AutoCompleteSource.CustomSource;
                
            }
        }
        private void findAddressStreet(TextBox text,string cityText)
        {
            AutoCompleteStringCollection textComplete = new AutoCompleteStringCollection();

            using (Context db = new Context(Form1.connectionString))
            {
                var city = db.addresses.Where(p=>p.city == cityText);

                foreach (address City in city)
                {
                    textComplete.Add(City.street);
                }

                text.AutoCompleteCustomSource = textComplete;
                text.AutoCompleteMode = AutoCompleteMode.Suggest;
                text.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }
        private void findAddressHouse(TextBox text,string cityText, string streetText)
        {
            AutoCompleteStringCollection textComplete = new AutoCompleteStringCollection();

            using (Context db = new Context(Form1.connectionString))
            {
                var city = db.addresses.Where(p=> p.city == cityText && p.street == streetText);

                foreach (address City in city)
                {
                    textComplete.Add(City.house);
                }

                text.AutoCompleteCustomSource = textComplete;
                text.AutoCompleteMode = AutoCompleteMode.Suggest;
                text.AutoCompleteSource = AutoCompleteSource.CustomSource;
                
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
        //Ограничение на ввод номера телефона клиента
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox1.MaxLength = 11;
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        //Вывод тарифа с ценами
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            rate cp = comboBox2.Items[comboBox2.SelectedIndex] as rate;
            boardingBox.Text = cp.cost_downtime.ToString();
            costDowntimeBox.Text = cp.cost_downtime.ToString();
            costPerKilometerBox.Text = cp.cost_per_kilometer.ToString();
            if (cp.child_safety_seat != null)
                childSafetySeatBox.Text = cp.child_safety_seat.ToString();
            else
                childSafetySeatBox.Text = "Недоступно";
            if (cp.transportation_of_pet != null)
                transportationOfPetBox.Text = cp.transportation_of_pet.ToString();
            else
                transportationOfPetBox.Text = "Недоступно";
        }

       
        //Вычисление координат адреса
        private double[] Coords(string address, out double[] array)
        {

            string url = $"https://nominatim.openstreetmap.org/search?format=json&q={WebUtility.UrlEncode(address)}";
            array = new double[2];
            using (WebClient httpClient = new WebClient())
            {
                string userAgentString = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Win64; x64; Trident/4.0; Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1) ; .NET CLR 2.0.50727; SLCC2; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Tablet PC 2.0; .NET4.0C; .NET4.0E)";
                httpClient.Headers["User-Agent"] = userAgentString;
                string json = httpClient.DownloadString(url);
                var data = JArray.Parse(json);

                if (data.Count > 0)
                {
                    var lat = data[0]["lat"].ToString();
                    var lon = data[0]["lon"].ToString();
                    lat = lat.Replace(".", ",");
                    lon = lon.Replace(".", ",");
                    array[0] = Math.Round(double.Parse(lat), 6);
                    array[1] = Math.Round(double.Parse(lon), 6);

                }
                else
                {
                    MessageBox.Show("Введены некорректные данные");
                }
            }
            return array;
        }
        private decimal? priceOrder = 0;
        //Рассчёт цены 
        private void estimatedСost_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCity1.Text) ||
               string.IsNullOrWhiteSpace(textBoxCity2.Text) ||
               string.IsNullOrWhiteSpace(textBoxStreet1.Text) ||
               string.IsNullOrWhiteSpace(textBoxStreet2.Text) ||
               string.IsNullOrWhiteSpace(textBoxHouse1.Text)||
               string.IsNullOrWhiteSpace(textBoxHouse2.Text))
            {
                MessageBox.Show("Заполните все поля", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                double[] array1 = new double[2];
                Coords($"{textBoxCity1.Text},{textBoxStreet1.Text},{textBoxHouse1.Text}", out array1); //!!!!!
                double[] array2 = new double[2];
                Coords($"{textBoxCity2.Text},{textBoxStreet2.Text},{textBoxHouse2.Text}", out array2); //!!!!!
                var dist1 = new GeoCoordinate(array1[0], array1[1]);
                var dist2 = new GeoCoordinate(array2[0], array2[1]);

                var distances = (dist1.GetDistanceTo(dist2) / 1000);
                
                rate cp = comboBox2.Items[comboBox2.SelectedIndex] as rate;
               
                    priceOrder = cp.boarding + cp.cost_per_kilometer * (decimal)distances;


                if (childSafetySeatCheck.Checked == true && cp.child_safety_seat != null)
                    priceOrder += cp.child_safety_seat;
                if(transportationOfPetCheck.Checked == true && cp.transportation_of_pet!=null)
                    priceOrder += cp.transportation_of_pet;

                priceBox.Text = Math.Round((double)priceOrder,2).ToString();
                PlaceOrder.Enabled = true;
                buttonEdit.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Произошла ошибка в вычислениях", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        //Проверка клиента, если нет такого в таблице, то добавляется, если есть, просто передаётся id
        private long checkClient()
        {
            long idClient;
            using (Context db = new Context(Form1.connectionString))
            {
                var client = db.clients;
                
                if (client.Where(p => p.mobile_phone == textBox1.Text).Count() != 1)
                {
                    client cl = new client
                    {
                        mobile_phone = textBox1.Text,
                        blacklist = false
                    };
                    db.clients.Add(cl);
                    db.SaveChanges();
                    idClient = cl.id_client;
                    return idClient;
                }
                else
                {
                   var findClient = client.Where(p => p.mobile_phone == textBox1.Text).FirstOrDefault();
                    idClient = findClient.id_client;
                    return idClient;

                }
            }
        }
        //Проверка адреса, если нет такого адреса в списках, добавляется, если есть то просто возращает его id
        private long checkAddress(string cityAd, string streetAd,string houseAd,string enranceAd)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var address = db.addresses;
                long idAddress;
                string newEnranced = "";
                if (string.IsNullOrWhiteSpace(enranceAd))
                    newEnranced = "";
                else
                    newEnranced = $"П{enranceAd}";

                if (address.Where(p => p.city == cityAd &&
                                 p.street == streetAd &&
                                 p.house == houseAd &&
                                 p.enrance == newEnranced).Count() != 1)
                {
                   
                    address ad = new address
                    {
                        city = cityAd,
                        street = streetAd,
                        house = houseAd,
                        enrance = newEnranced
                    };
                    db.addresses.Add(ad);
                    db.SaveChanges();
                    idAddress = ad.id_address;
                    return idAddress;
                }
                else
                {
                    var findAddress = address.Where(p => p.city == cityAd &&
                                 p.street == streetAd &&
                                 p.house == houseAd &&
                                 p.enrance == newEnranced).FirstOrDefault();
                    idAddress = findAddress.id_address;
                    return idAddress;
                    
                }
            }
        }
        //Оформление заказа
        private void button1_Click(object sender, EventArgs e)
        {

            Check();
            using (Context db = new Context(Form1.connectionString))
            {
                var idClient = checkClient();
                var idAdddress1 = checkAddress(textBoxCity1.Text,textBoxStreet1.Text,textBoxHouse1.Text,textBox4.Text);
                var idAdddress2 = checkAddress(textBoxCity2.Text, textBoxStreet2.Text, textBoxHouse2.Text, textBox6.Text);
            
                rate cp = comboBox2.Items[comboBox2.SelectedIndex] as rate;
                DateTime dateAndTime = DateTime.Now ;
                if(checkBox1.Checked == true)
                {
                    DateTime dateTime = DateTime.Now;
                    DateTime dt = dateTimePicker2.Value.Date + dateTimePicker1.Value.TimeOfDay;
                    if (dateTime < dt)
                        dateAndTime = dt;
                    else
                    {
                        MessageBox.Show("Исправьте дату и время для предзаказа");
                        return;
                    }
                }
                order o = new order
                {
                    status = "В ожидании",
                    place_of_departure = idAdddress1,
                    destination = idAdddress2,
                    order_cost = (decimal)priceOrder,
                    payment_method = comboBox1.Text,
                    datetime_placing_the_order = dateAndTime,
                    order_completion_datetime = null,
                    id_driver = null,
                    id_client = idClient,
                    id_rate = cp.id_rate,
                    id_dispatcher = Form_for_Dispatcher.id


                };
                db.orders.Add(o);
                db.SaveChanges();
                
            }
            
            MessageBox.Show("Заказ оформлен");
            Close();

        }
        //Автозаполнение после смены фокуса с города, улицы для дальнейших полей
        private void textBoxCity1_Leave(object sender, EventArgs e)
        {
            if (idOrder == 0)
            {
                textBoxStreet1.Clear();
                textBox4.Clear();
                textBoxHouse1.Clear();
                findAddressStreet(textBoxStreet1, textBoxCity1.Text);
            }
        }

        private void textBoxCity2_Leave(object sender, EventArgs e)
        {
            if (idOrder == 0)
            {
                textBoxStreet2.Clear();
                textBox6.Clear();
                textBoxHouse2.Clear();
                findAddressStreet(textBoxStreet2, textBoxCity2.Text);
            }
        }

        private void textBoxStreet1_Leave(object sender, EventArgs e)
        {
            if (idOrder == 0)
            {
                textBox4.Clear();
                textBoxHouse1.Clear();
                findAddressHouse(textBoxHouse1, textBoxCity1.Text, textBoxStreet1.Text);
            }
        }

        private void textBoxStreet2_Leave(object sender, EventArgs e)
        {
            if (idOrder == 0)
            {
                textBox6.Clear();
                textBoxHouse2.Clear();
                findAddressHouse(textBoxHouse2, textBoxCity2.Text, textBoxStreet2.Text);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                groupBox2.Visible = true;
            else
                groupBox2.Visible = false;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now.AddDays(3);
            if (!(dateTimePicker2.Value.Date <= dt.Date && dateTimePicker2.Value.Date >= DateTime.Now.Date))
            {
                MessageBox.Show("Выбранная дата не может являться предзаказом");
                dateTimePicker2.Value = DateTime.Now;
                return;
            }

        }
        private void Check()
        {
            if (string.IsNullOrWhiteSpace(textBoxCity1.Text) ||
               string.IsNullOrWhiteSpace(textBoxCity2.Text) ||
               string.IsNullOrWhiteSpace(textBoxStreet1.Text) ||
               string.IsNullOrWhiteSpace(textBoxStreet2.Text) ||
               string.IsNullOrWhiteSpace(textBoxHouse1.Text) ||
               string.IsNullOrWhiteSpace(textBoxHouse2.Text) ||
               string.IsNullOrWhiteSpace(comboBox1.Text) ||
               string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Заполните все поля", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox1.Text.Length != 11 || textBox1.Text[0] == '0')
            {
                MessageBox.Show("Поле номер телефона клиента заполнено некорректно:\n" +
                                "1. Проверьте, возможно введено не 11 символов\n" +
                                "2. Проверьте, возможно номер телефона начинатеся с \'0\'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var orderView = db.orders.Where(p => p.id_order == idOrder).FirstOrDefault();
                Check();
                rate cp = comboBox2.Items[comboBox2.SelectedIndex] as rate;
                DateTime dateAndTime = DateTime.Now;
                if (checkBox1.Checked == true)
                {
                    DateTime dateTime = DateTime.Now;
                    DateTime dt = dateTimePicker2.Value.Date + dateTimePicker1.Value.TimeOfDay;
                    if (dateTime < dt)
                        dateAndTime = dt;
                    else
                    {
                        MessageBox.Show("Исправьте дату и время для предзаказа");
                        return;
                    }
                }

                var idClient = checkClient();

                orderView.id_client = idClient;
                orderView.order_cost = (decimal)priceOrder;
                orderView.payment_method = comboBox1.Text;
                orderView.datetime_placing_the_order = dateAndTime;
                orderView.id_rate = cp.id_rate;
                db.SaveChanges();
                MessageBox.Show("Заказ изменён");
                Close();
            }
        }
    }
}
