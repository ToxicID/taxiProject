
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using GMap.NET.WindowsPresentation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Device.Location;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace taxiDesktopProg
{
    public partial class addOrEditOrders : Form
    {
        forOrders fO;
        //ID для редактирования заказа
        private long? idOrder = 0;
        //Конструктор для добавления
        public addOrEditOrders()
        {
            InitializeComponent();
            this.Width = 820;
            gmap.Visible = false;
            fO = new forOrders(this);
            label16.Text = "Перевозка" + Environment.NewLine + "домашних животных";
            using (Context db = new Context(auth.connectionString))
            {
                fO.findAddressCity(textBoxCity1);
                fO.findAddressCity(textBoxCity2);
                fO.findClientMobile(textBox1);

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
            forOrders fO = new forOrders(this);
            label16.Text = "Перевозка" + Environment.NewLine + "домашних животных";
            using (Context db = new Context(auth.connectionString))
            {
                var cp = db.rates.Where(p => p.availability == true).ToList();

                
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
                var idRate = cp.Where(p => p.name == order.rate.name).FirstOrDefault();
                comboBox2.SelectedValue = idRate.id_rate;
                textBox1.Text = order.client.mobile_phone;
                comboBox1.Text = order.payment_method;
                priceBox.Text = Math.Round(order.order_cost,0).ToString();
                if (index == 2)
                {
                        checkBox1.Checked = true;
                        dateTimePicker1.Value = order.datetime_placing_the_order;
                        dateTimePicker2.Value = order.datetime_placing_the_order;
                }
                array1 = new double[2];
                Coords($"{textBoxCity1.Text},{textBoxStreet1.Text},{textBoxHouse1.Text}", out array1); //!!!!!
                array2 = new double[2];
                Coords($"{textBoxCity2.Text},{textBoxStreet2.Text},{textBoxHouse2.Text}", out array2); //!!!!!
                gmap.Visible = true;
                this.Width = 1350;
                fO.LoadMap();
                // Добавление маркеров
                fO.AddMarker(new PointLatLng(array1[0], array1[1]), new PointLatLng(array2[0], array2[1]), "Пункт отправления", "Пункт назначения");

                // Построение маршрута
                fO.DrawRoute(new PointLatLng(array1[0], array1[1]), new PointLatLng(array2[0], array2[1]));

                // Обновление карты
                gmap.ZoomAndCenterMarkers("markers");


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
                boardingBox.Text = Math.Round(cp.cost_downtime).ToString();
                costDowntimeBox.Text = Math.Round(cp.cost_downtime).ToString();
                costPerKilometerBox.Text = Math.Round(cp.cost_per_kilometer).ToString();
                if (cp.child_safety_seat != null)
                    childSafetySeatBox.Text = Math.Round((decimal)cp.child_safety_seat).ToString();
                else
                    childSafetySeatBox.Text = "Недоступно";
                if (cp.transportation_of_pet != null)
                    transportationOfPetBox.Text = Math.Round((decimal)cp.transportation_of_pet).ToString();
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
                    array[0] = double.Parse(lat);
                    array[1] = double.Parse(lon);

                }
                else
                {
                    MessageBox.Show("Введены некорректные данные");
                }
            }

            return array;
            
        }
      
       
        private decimal? priceOrder = 0;
        double[] array1;
        double[] array2;
        private void calculatingPrice()
        {
            if (string.IsNullOrWhiteSpace(textBoxCity1.Text) ||
               string.IsNullOrWhiteSpace(textBoxCity2.Text) ||
               string.IsNullOrWhiteSpace(textBoxStreet1.Text) ||
               string.IsNullOrWhiteSpace(textBoxStreet2.Text) ||
               string.IsNullOrWhiteSpace(textBoxHouse1.Text) ||
               string.IsNullOrWhiteSpace(textBoxHouse2.Text))
            {
                MessageBox.Show("Заполните все поля", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                array1 = new double[2];
                Coords($"{textBoxCity1.Text},{textBoxStreet1.Text},{textBoxHouse1.Text}", out array1); //!!!!!
                 array2 = new double[2];
                Coords($"{textBoxCity2.Text},{textBoxStreet2.Text},{textBoxHouse2.Text}", out array2); //!!!!!
                var dist1 = new GeoCoordinate(array1[0], array1[1]);
                var dist2 = new GeoCoordinate(array2[0], array2[1]);
                var distances = fO.Distance(dist1.Latitude,dist1.Longitude,dist2.Latitude,dist2.Longitude);
                rate cp = comboBox2.Items[comboBox2.SelectedIndex] as rate;

                priceOrder = cp.boarding + cp.cost_per_kilometer * (decimal)distances;


                if (childSafetySeatCheck.Checked == true && cp.child_safety_seat != null)
                    priceOrder += cp.child_safety_seat;
                if (transportationOfPetCheck.Checked == true && cp.transportation_of_pet != null)
                    priceOrder += cp.transportation_of_pet;

                priceBox.Text = Math.Round((double)priceOrder, 0).ToString();
                PlaceOrder.Enabled = true;
                buttonEdit.Enabled = true;

                gmap.Visible = true;
                this.Width = 1350;
                fO.array1 = array1;
                fO.array2 = array2 ;
                fO.LoadMap();
                // Добавление маркеров
                fO.AddMarker(new PointLatLng(array1[0], array1[1]), new PointLatLng(array2[0], array2[1]), "Пункт отправления", "Пункт назначения");

                // Построение маршрута
                fO.DrawRoute(new PointLatLng(array1[0], array1[1]), new PointLatLng(array2[0], array2[1]));

                // Обновление карты
                gmap.ZoomAndCenterMarkers("markers");
            }
            catch
            {
                MessageBox.Show("Произошла ошибка в вычислениях", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        //Рассчёт цены 
        private void estimatedСost_Click(object sender, EventArgs e)
        {
            
            calculatingPrice();
            
        }
        
       
       
        //Оформление заказа
        private void addOrder()
        {
            Check();
            using (Context db = new Context(auth.connectionString))
            {
                var idClient = fO.checkClient(textBox1.Text);
                var idAdddress1 = fO.checkAddress(textBoxCity1.Text, textBoxStreet1.Text, textBoxHouse1.Text, textBox4.Text);
                var idAdddress2 = fO.checkAddress(textBoxCity2.Text, textBoxStreet2.Text, textBoxHouse2.Text, textBox6.Text);

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
        private void button1_Click(object sender, EventArgs e)
        {

            addOrder();

        }
        //Автозаполнение после смены фокуса с города, улицы для дальнейших полей
        private void textBoxCity1_Leave(object sender, EventArgs e)
        {
            if (idOrder == 0)
            {
                textBoxStreet1.Clear();
                textBox4.Clear();
                textBoxHouse1.Clear();
                fO.findAddressStreet(textBoxStreet1, textBoxCity1.Text);
            }
        }

        private void textBoxCity2_Leave(object sender, EventArgs e)
        {
            if (idOrder == 0)
            {
                textBoxStreet2.Clear();
                textBox6.Clear();
                textBoxHouse2.Clear();
                fO.findAddressStreet(textBoxStreet2, textBoxCity2.Text);
            }
        }

        private void textBoxStreet1_Leave(object sender, EventArgs e)
        {
            if (idOrder == 0)
            {
                textBox4.Clear();
                textBoxHouse1.Clear();
                fO.findAddressHouse(textBoxHouse1, textBoxCity1.Text, textBoxStreet1.Text);
            }
        }

        private void textBoxStreet2_Leave(object sender, EventArgs e)
        {
            if (idOrder == 0)
            {
                textBox6.Clear();
                textBoxHouse2.Clear();
                fO.findAddressHouse(textBoxHouse2, textBoxCity2.Text, textBoxStreet2.Text);
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
        private void editOrder(long? idOrder)
        {
            using (Context db = new Context(auth.connectionString))
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

                var idClient = fO.checkClient(textBox1.Text);

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
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            editOrder(idOrder);
        }
 
      
    }
}
