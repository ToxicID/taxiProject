using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taxiDesktopProg
{
    public partial class violationsTable : Form
    {
        private void listTable()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var list = from dr in db.drivers
                           join vio in db.violations on dr.id_driver equals vio.id_driver
                           select new
                           {
                               id_Vio = vio.id_violations,
                               FullNameDriver = dr.surname + " " + dr.name.Substring(0, 1) + ". " + dr.patronymic.Substring(0, 1) + ".",
                               dateTimeVio = vio.datetime_the_violation,
                               typeVio = vio.type_of_violation,
                               statusVio = vio.violation_status,
                               measuresVio = vio.measures_taken
                           };
                dataGridView1.DataSource = list.ToList();
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "ФИО";
                dataGridView1.Columns[2].HeaderText = "Дата и время";
                dataGridView1.Columns[3].HeaderText = "Тип нарушения";
                dataGridView1.Columns[4].HeaderText = "Статус";
                dataGridView1.Columns[5].HeaderText = "Принятые меры";

                foreach (DataGridViewColumn data in dataGridView1.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView1.EnableHeadersVisualStyles = false;
            }
        }
        public violationsTable()
        {
            InitializeComponent();
            listTable();
            button1.Enabled = false;
        }

        private void ButIzmen_Click(object sender, EventArgs e)
        {
            addOrEditStatusViolations fm = new addOrEditStatusViolations(0);
            fm.FormClosed += new FormClosedEventHandler(addOrEditStatusViolations_FormClosed);
           
            fm.Show();
        }
        void addOrEditStatusViolations_FormClosed(object sender, FormClosedEventArgs e)
        {
            listTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                if (DataGridIndex != null)
                {
                    DialogResult result = MessageBox.Show("Изменить нарушение", "Изменение",
                                                                                       MessageBoxButtons.YesNo,
                                                                                       MessageBoxIcon.Information);
                    if (result == DialogResult.No) return;
                    var vio = db.violations.Where(p => p.id_violations == DataGridIndex).FirstOrDefault();
                    if (vio.violation_status == "Рассмотрен")
                    {
                        MessageBox.Show("Данное нарушение уже рассмотрено", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    addOrEditStatusViolations fm = new addOrEditStatusViolations(DataGridIndex);
                    fm.FormClosed += new FormClosedEventHandler(addOrEditStatusViolations_FormClosed);
                    fm.Show();
                    DataGridIndex = null;
                    button1.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Произвести изменение, необходимо его выбрать и нажать на кнопку", "Ошибка", MessageBoxButtons.OK,
                                                                                                  MessageBoxIcon.Information);
                    return;
                }

            }
        }
        private long? DataGridIndex = null;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                DataGridIndex = (long)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                button1.Enabled = true;
            }
            catch
            {
                DataGridIndex = null;
                button1.Enabled = false;
            }
        }

        private void violationsTable_Click(object sender, EventArgs e)
        {
            DataGridIndex = null;
            button1.Enabled = false;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                if (DataGridIndex != null)
                {
                    DialogResult result = MessageBox.Show("Изменить нарушение", "Изменение",
                                                                                       MessageBoxButtons.YesNo,
                                                                                       MessageBoxIcon.Information);
                    if (result == DialogResult.No) return;
                    var vio = db.violations.Where(p => p.id_violations == DataGridIndex).FirstOrDefault();
                    if (vio.violation_status == "Рассмотрен")
                    {
                        MessageBox.Show("Данное нарушение уже рассмотрено", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    addOrEditStatusViolations fm = new addOrEditStatusViolations(DataGridIndex);
                    fm.FormClosed += new FormClosedEventHandler(addOrEditStatusViolations_FormClosed);
                    fm.Show();
                    DataGridIndex = null;
                    button1.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Произвести изменение, необходимо выбрать нарушение и нажать на кнопку", "Ошибка", MessageBoxButtons.OK,
                                                                                                  MessageBoxIcon.Information);
                    return;
                }

            }
        }
    }
}
