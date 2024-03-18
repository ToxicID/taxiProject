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
    public partial class rateList : Form
    {
        public long indexRate;
        private int index;
        public rateList(int index)
        {

            this.index = index;
            InitializeComponent();
            if (index == 1 || index == 2)
            {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                Down.Visible = false;
                Up.Visible = false;
                ChildKres.Text = predvarPrice.ToString();
                Perevos.Text = predvarPrice1.ToString();
            }
            else
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                Down.Visible = true;
                Up.Visible = true;
            }
            comboBox1.Visible = false;
            switch (index)
            {
                case 0:
                    listRate();
                    button4.Visible = false;
                    break;
                case 1:
                    button2.Text = "X";
                    button1.Text = "X";
                    button4.Visible = false;
                    label9.Text = "Доступен";
                    label9.ForeColor = Color.Green;
                    enOrFalseStatusRate = true;
                    status = false;
                    break;
                case 2:
                    listRate();
                    Down.Visible = true;
                    Up.Visible = true;
                    button3.Visible = false ;
                    Names.ReadOnly = true;
                    MinCost.ReadOnly = true;
                    transPoGor.ReadOnly = true;
                    costDowntime.ReadOnly = true;
                    ChildKres.ReadOnly = true;
                    Perevos.ReadOnly = true;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    break;

            }
            

        }
        private void listRate()
        {
            Names.ReadOnly = true;
            MinCost.ReadOnly = true;
            transPoGor.ReadOnly = true;
            costDowntime.ReadOnly = true;
            ChildKres.ReadOnly = true;
            Perevos.ReadOnly = true;
            
            using (Context db = new Context(Form1.connectionString))
            {
                indexRate = db.rates.Where(p => p.availability == true).FirstOrDefault().id_rate;
                List<rate> cp = db.rates.ToList();
                comboBox1.DataSource = cp;
                comboBox1.ValueMember = "id_rate";
                comboBox1.DisplayMember = "name";
            }
            print();
        }
        private void print()
        {//Доделать
            using (Context db = new Context(Form1.connectionString))
            {
                var rate = db.rates.Where(p => p.id_rate == indexRate).FirstOrDefault();
                Names.Text = rate.name.ToString();
                MinCost.Text = rate.boarding.ToString();
                transPoGor.Text = rate.cost_per_kilometer.ToString();
                costDowntime.Text = rate.cost_downtime.ToString();

                ChildKres.Text = rate.child_safety_seat.ToString();
                Perevos.Text = rate.transportation_of_pet.ToString();
                if(rate.availability == true)
                {
                    label9.Text = "Доступен";
                    label9.ForeColor = Color.Green;
                    enOrFalseStatusRate = true;
    }
                else
                {
                    label9.Text = "Недоступен";
                    label9.ForeColor = Color.Red;
                    enOrFalseStatusRate = false;

                }
            }
            if (string.IsNullOrWhiteSpace(ChildKres.Text))
                ChildKres.Text = "Недоступно";
            if (string.IsNullOrWhiteSpace(Perevos.Text))
                Perevos.Text = "Недоступно";
            if (ChildKres.Text == "Недоступно")
                button2.Text = "✓";
            else
                button2.Text = "X";
            if (Perevos.Text == "Недоступно")
                button1.Text = "✓";
            else
                button2.Text = "X";

        }

        private void Up_Click(object sender, EventArgs e)
        {
            try
            {
                comboBox1.SelectedIndex -= 1;
                print();

            }
            catch
            {
                MessageBox.Show("Это самый первый тариф, листайте в другую сторону", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.SelectedIndex = 0;
                print();
                return;
            }
        }

        private void Down_Click(object sender, EventArgs e)
        {
            try
            {
                using (Context db = new Context(Form1.connectionString))
                {
                    comboBox1.SelectedIndex += 1;
                    print();
                }
            }
            catch
            {

                MessageBox.Show("Это самый последний тариф, листайте в другую сторон", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                rate cps = comboBox1.Items[comboBox1.SelectedIndex] as rate;
                indexRate = cps.id_rate;

            }
        }

        private void MinCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (index == 1 || index == 2)
            {
                if (!char.IsDigit(e.KeyChar) & (e.KeyChar != ',') & (e.KeyChar != (char)Keys.Back))
                    e.Handled = true;
            }
        }
        private decimal predvarPrice = 0.0m;
        private decimal predvarPrice1 = 0.0m;
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (button2.Text == "X")
            {
                if (!string.IsNullOrWhiteSpace(ChildKres.Text))
                {

                    predvarPrice = decimal.Parse(ChildKres.Text);
                }
                button2.Text = "✓";
                ChildKres.Text = "Недоступно";

            }
            else if (button2.Text == "✓")
            {
                button2.Text = "X";
                ChildKres.Text = predvarPrice.ToString();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text == "X") 
            {
                if (!string.IsNullOrWhiteSpace(Perevos.Text))
                {

                    predvarPrice1 = decimal.Parse(Perevos.Text);
                }
                button1.Text = "✓";
                Perevos.Text = "Недоступно";
            }
            else if (button1.Text == "✓")
            {
                button1.Text ="X";
                Perevos.Text = predvarPrice1.ToString();

            }
        }

        private string NameRate = "";
        private decimal board = 0.0m;
        private decimal priceKm = 0.0m;
        private decimal priceOjid = 0.0m;
        private decimal? childPrice = 0.0m;
        private bool statusChild;
        private decimal? priceW = 0.0m;
        private bool statusW;
        private void testSt()
        {
            if (string.IsNullOrWhiteSpace(Names.Text) || string.IsNullOrWhiteSpace(MinCost.Text) ||
                    string.IsNullOrWhiteSpace(transPoGor.Text) ||
                    string.IsNullOrWhiteSpace(costDowntime.Text) ||
                    string.IsNullOrWhiteSpace(ChildKres.Text) ||
                    string.IsNullOrWhiteSpace(Perevos.Text))
            {
                MessageBox.Show("Заполните все поля", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (button2.Text == "X")
            {
                statusChild = true;
                childPrice = predvarPrice;
            }
            else if (button2.Text == "✓")
            {
                statusChild = false;
                childPrice = null;
            }
            if (button2.Text == "X")
            {
                statusW = true;
                priceW = predvarPrice1;
                priceW = null;
            }
            else if (button2.Text == "✓")
            {
                statusW = false;
            }
        }
        private void parsing()
        {
            NameRate = Names.Text;
            board = decimal.Parse(MinCost.Text);
            priceKm = decimal.Parse(transPoGor.Text);
            priceOjid = decimal.Parse(costDowntime.Text);
            childPrice = decimal.Parse(ChildKres.Text);
            priceW = decimal.Parse(Perevos.Text);
        }
        private void button3_Click(object sender, EventArgs e)
        {
           
            using (Context db = new Context(Form1.connectionString))
            {
                testSt();
                parsing();


                rate r = new rate()
                {
                    availability = enOrFalseStatusRate,
                    name = Names.Text,
                    boarding = board,
                    cost_per_kilometer = priceKm,
                    cost_downtime = priceOjid,
                    child_safety_seat = childPrice,
                    transportation_of_pet = priceW
                };
                db.rates.Add(r);
                db.SaveChanges();
                MessageBox.Show("Был добавлен новый тариф");
                
                Close();
            }
        }
        private bool status = true;
        private void initialState()
        {
            MinCost.ReadOnly = false;
            transPoGor.ReadOnly = false;
            costDowntime.ReadOnly = false;
            ChildKres.ReadOnly = false;
            Perevos.ReadOnly = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (status)
            {
                initialState();
                status = false;
                button4.Text = "Сохранить";
                Up.Enabled = false;
                Down.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                testSt();
                parsing();
                using (Context db = new Context(Form1.connectionString))
                {
                    rate c = db.rates.Find(indexRate);
                    c.availability = enOrFalseStatusRate;
                    c.boarding = board;
                    c.cost_per_kilometer = priceKm;
                    c.cost_downtime = priceOjid;
                    c.child_safety_seat = childPrice;
                    c.transportation_of_pet = priceW;
                    db.SaveChanges();
                    button4.Text = "Изменить";
                    status = true;
                    MessageBox.Show("Тариф \"" + c.name + "\" был изменён");
                }
                initialState();
                Up.Enabled = true;
                Down.Enabled = true;
                status = true;
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }
        private bool enOrFalseStatusRate;
        private void label9_Click(object sender, EventArgs e)
        {
            if (!status)
            {
                if (enOrFalseStatusRate)
                {
                    label9.Text = "Недоступен";
                    label9.ForeColor = Color.Red;
                    enOrFalseStatusRate = false;
                }
                else
                {
                    label9.Text = "Доступен";
                    label9.ForeColor = Color.Green;
                    enOrFalseStatusRate = true;
                }
            }
        }
    }
}
