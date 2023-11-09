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
        public addOrEditOrders()
        {
            InitializeComponent();
            label16.Text = "Перевозка" + Environment.NewLine + "домашних животных";
            using (Context db = new Context(Form1.connectionString))
            {
                findAddressCity(textBoxCity1);
                findAddressCity(textBoxCity2);
               
                    
                    List<rate> cp = db.rates.Where(p=>p.availability == true).ToList();
                
                    comboBox2.DataSource = cp;
                    comboBox2.ValueMember = "id_rate";
                    comboBox2.DisplayMember = "name";
                boardingBox.ReadOnly = true;
                costDowntimeBox.ReadOnly = true;
                costPerKilometerBox.ReadOnly = true;
                childSafetySeatBox.ReadOnly = true;
                transportationOfPetBox.ReadOnly = true;

                }
        }

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
        
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox1.MaxLength = 11;
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

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
            }
            catch
            {
                MessageBox.Show("Произошла ошибка в вычислениях", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
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
        private long checkAddress(string cityAd, string streetAd,string houseAd,string enranceAd)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var address = db.addresses;
                long idAddress;
                if (address.Where(p => p.city == cityAd &&
                                 p.street == streetAd &&
                                 p.house == houseAd &&
                                 p.enrance == enranceAd).Count() != 1)
                {
                    address ad = new address
                    {
                        city = cityAd,
                        street = streetAd,
                        house = houseAd,
                        enrance = enranceAd
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
                                 p.enrance == enranceAd).FirstOrDefault();
                    idAddress = findAddress.id_address;
                    return idAddress;
                    
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
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
            using (Context db = new Context(Form1.connectionString))
            {
                var idClient = checkClient();
                if(idClient == 0)
                {
                    MessageBox.Show("ErrorКлиент");
                    return;
                }
                var idAdddress1 = checkAddress(textBoxCity1.Text,textBoxStreet1.Text,textBoxHouse1.Text,textBox4.Text);
                var idAdddress2 = checkAddress(textBoxCity2.Text, textBoxStreet2.Text, textBoxHouse2.Text, textBox6.Text);
                if (idAdddress1 == 0)
                {
                    MessageBox.Show("ErrorAddress1");
                    return;
                }
                if (idAdddress2 == 0)
                {
                    MessageBox.Show("ErrorAddress2");
                    return;
                }
                rate cp = comboBox2.Items[comboBox2.SelectedIndex] as rate;
                order o = new order
                {
                    status = "В ожидании",
                    place_of_departure = idAdddress1,
                    destination = idAdddress2,
                    order_cost = (decimal)priceOrder,
                    payment_method = comboBox1.Text,
                    datetime_placing_the_order = DateTime.Now,
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
            findAddressStreet(textBoxStreet1, textBoxCity1.Text);
        }

        private void textBoxCity2_Leave(object sender, EventArgs e)
        {

            findAddressStreet(textBoxStreet2,textBoxCity2.Text);
        }

        private void textBoxStreet1_Leave(object sender, EventArgs e)
        {
            findAddressHouse(textBoxHouse1, textBoxCity1.Text,textBoxStreet1.Text);
        }

        private void textBoxStreet2_Leave(object sender, EventArgs e)
        {
            findAddressHouse(textBoxHouse2, textBoxCity2.Text, textBoxStreet2.Text);
        }
    }
}
