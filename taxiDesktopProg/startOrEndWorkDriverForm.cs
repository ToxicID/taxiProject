using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace taxiDesktopProg
{
    public partial class startOrEndWorkDriverForm : Form
    {
        private void comboboxListFioDriver()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var driver = db.drivers.ToList();

                comboBox1.DataSource = driver;
                comboBox1.ValueMember = "id_driver";
                comboBox1.DisplayMember = "surname";
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
            }
        }
        public startOrEndWorkDriverForm()
        {
            InitializeComponent();
            comboboxListFioDriver();
        }
        private long idDriver;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                driver dr = comboBox1.Items[comboBox1.SelectedIndex] as driver;
            textBox1.Text = dr.name;
            textBox2.Text = dr.patronymic != null ? dr.patronymic:"Отсутствует";
            textBox3.Text = dr.call_sign;
            idDriver = dr.id_driver;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                DialogResult result = MessageBox.Show("Начать смену данного водителя", "Начало смены",
                                                                                         MessageBoxButtons.YesNo,
                                                                                         MessageBoxIcon.Information);
                if (result == DialogResult.No) return;
                var dr = db.drivers.Where(p=>p.id_driver == idDriver).FirstOrDefault();
                if(dr.driver_readiness == "Готов")
                {
                    MessageBox.Show("Водитель уже работает","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                work_schedule ws = new work_schedule()
                {
                    date_of_work_from = DateTime.Now,
                    work_schedule_from= DateTime.Now.TimeOfDay,
                    work_schedule_before = null,
                    date_of_work_before = null,
                    id_driver = dr.id_driver,
                };
                dr.driver_readiness = "Готов";
                dr.status = "Свободен";
                db.work_schedule.Add(ws);
                db.SaveChanges();
                if (dr.patronymic == "" || dr.patronymic == null || dr.patronymic == "Отсутствует")
                {
                    MessageBox.Show($"Смена {dr.surname} {dr.name.Substring(0, 1)}. началась", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    MessageBox.Show($"Смена {dr.surname} {dr.name.Substring(0, 1)}. {dr.patronymic.Substring(0, 1)}. началась", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
        }
    }
}
