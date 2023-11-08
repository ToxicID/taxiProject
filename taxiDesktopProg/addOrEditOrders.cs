using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taxiDesktopProg
{
    public partial class addOrEditOrders : Form
    {
        public addOrEditOrders()
        {
            InitializeComponent();
            using (Context db = new Context(Form1.connectionString))
            {
                findAddressCity(textBoxCity1);
                findAddressStreet(textBoxStreet1);
                findAddressHouse(textBoxHouse1);

                findAddressCity(textBoxCity2);
                findAddressStreet(textBoxStreet2);
                findAddressHouse(textBoxHouse2);
               
                    
                    List<rate> cp = db.rates.ToList();
                    comboBox2.DataSource = cp;
                    comboBox2.ValueMember = "id_rate";
                    comboBox2.DisplayMember = "name";
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
        private void findAddressStreet(TextBox text)
        {
            AutoCompleteStringCollection textComplete = new AutoCompleteStringCollection();

            using (Context db = new Context(Form1.connectionString))
            {
                var city = db.addresses;

                foreach (address City in city)
                {
                    textComplete.Add(City.street);
                }

                text.AutoCompleteCustomSource = textComplete;
                text.AutoCompleteMode = AutoCompleteMode.Suggest;
                text.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }
        private void findAddressHouse(TextBox text)
        {
            AutoCompleteStringCollection textComplete = new AutoCompleteStringCollection();

            using (Context db = new Context(Form1.connectionString))
            {
                var city = db.addresses;

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
    }
}
