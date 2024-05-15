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
using static taxiDesktopProg.addOrEditStatusViolations;

namespace taxiDesktopProg
{

    public partial class addOrEditStatusViolations : Form
    {
        //Вспомогательный класс, для записи значений в combobox3
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
        private string statusmeasuresNotReady = "Находится в процессе рассмотрения";
        private string statusmeasuresReady = "Рассмотрен";
        public addOrEditStatusViolations(int a)
        {
            InitializeComponent();
            startView();
            label5.Text = "Добавление нарушения";
            ButEdit.Visible = false;
        }

        private long? idVio;

        public addOrEditStatusViolations(long? id)
        {
            InitializeComponent();
            idVio = id;
            startView();
            //Если с момента добавления нарушения прошло 24 часа, диспетчер не может изменять
            // ничего кроме принятых мер
            using (Context db = new Context(auth.connectionString))
            {
                var vio = db.violations.Where(p=>p.id_violations == idVio).FirstOrDefault();
                var dtVio = vio.datetime_of_recording_violation;
                if (DateTime.Now.Subtract(dtVio).TotalHours >= 24)
                {
                    dateTimePicker1.Enabled = false;
                    richTextBox1.ReadOnly = true;
                }
                
                comboBox3.SelectedValue = vio.id_driver;
                richTextBox1.Text = vio.type_of_violation;
                dateTimePicker1.Value = vio.datetime_the_violation;
                richTextBox2.Text = vio.measures_taken;
            }
                
            label5.Text = "Изменение нарушения";
            ButAdd.Visible = false;
            checkBox1.Checked = true;
            checkBox1.Visible = false;
            comboBox3.Enabled = false;

        }
        private void startView()
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            
            using (Context db = new Context(auth.connectionString))
            {

                var dr = db.drivers;
                driverses d = new driverses();
                d.listDriver(dr.ToList());

                var bindingSource1 = new BindingSource();
                bindingSource1.DataSource = d._driverses;

                comboBox3.DataSource = bindingSource1.DataSource;
                comboBox3.DisplayMember = "FullName";
                comboBox3.ValueMember = "id_driverses";
                richTextBox2.ReadOnly = true;
            }
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                richTextBox2.ReadOnly = false;
            else
                richTextBox2.ReadOnly = true;
        }
        private long idDriver;
        
        private void ButAdd_Click(object sender, EventArgs e)
        {
          
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                MessageBox.Show("Заполните поле \"Тип нарушения\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (checkBox1.Checked)
                if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    MessageBox.Show("Заполните поле \"Меры\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
          
            using (Context db = new Context(auth.connectionString))
            {
                try
                {
                    idDriver = long.Parse(comboBox3.SelectedValue.ToString());
                    violation v = new violation()
                    {
                        id_driver = idDriver,
                        datetime_the_violation = dateTimePicker1.Value,
                        datetime_of_recording_violation = DateTime.Now,
                        type_of_violation = richTextBox1.Text,
                        measures_taken = checkBox1.Checked ? richTextBox2.Text : "",
                        violation_status = checkBox1.Checked ? statusmeasuresReady : statusmeasuresNotReady
                    };
                    db.violations.Add(v);
                    db.SaveChanges();
                    MessageBox.Show("Нарушение было добавлено", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("Что-то пошло не так, попробуйте перезапустить окно, и повторить операцию", "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

            }
        }

        private void ButEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                MessageBox.Show("Заполните поле \"Тип нарушения\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (checkBox1.Checked)
                if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    MessageBox.Show("Заполните поле \"Меры\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
           
            using (Context db = new Context(auth.connectionString))
            {
                var vio = db.violations.Where(p => p.id_violations == idVio).FirstOrDefault();
                vio.datetime_the_violation = dateTimePicker1.Value;
                vio.type_of_violation = richTextBox1.Text;
                vio.measures_taken = richTextBox2.Text;
                vio.violation_status = statusmeasuresReady;
                db.SaveChanges();
                MessageBox.Show("Нарушение было успешно изменено", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Нельзя добавить нарушение, которое произойдёт в будующем", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker1.Value = DateTime.Now;
                return;
            }
        }
    }
}
