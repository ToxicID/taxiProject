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
        public rateList()
        {
            InitializeComponent();
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
            }
            if (string.IsNullOrWhiteSpace(ChildKres.Text))
                ChildKres.Text = "Недоступно";
            if (string.IsNullOrWhiteSpace(Perevos.Text))
                Perevos.Text = "Недоступно";
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
    }
}
