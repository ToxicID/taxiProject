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
    public partial class listDriver : Form
    {
        private void listDriverDataGrid()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var dr = db.drivers.ToList();
                dataGridView1.DataSource = dr;
            }
            fonts();
        }
        private void searchDriver()
        {
            
                using (Context db = new Context(Form1.connectionString))
                {
                    var dr = db.drivers.Where(x => x.surname.Contains(textBox2.Text)).ToList();
                dataGridView1.DataSource = dr;

                }
            
        }
        private void fonts()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Статус";
            dataGridView1.Columns[2].HeaderText = "Готовность";
            dataGridView1.Columns[3].HeaderText = "Позывной";
            dataGridView1.Columns[4].HeaderText = "Фамилия";
            dataGridView1.Columns[5].HeaderText = "Имя";
            dataGridView1.Columns[6].HeaderText = "Отчество";
            dataGridView1.Columns[7].HeaderText = "Дата рождения";
            dataGridView1.Columns[8].HeaderText = "Номер вод. удостоверения";
            dataGridView1.Columns[9].HeaderText = "Номер телефона";
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            foreach (DataGridViewColumn data in dataGridView1.Columns)
                data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
            dataGridView1.EnableHeadersVisualStyles = false;
        }
        private long? DataGridIndex = null;
        public listDriver()
        {
            InitializeComponent();
            button1.Enabled = false;
            button3.Enabled = false;
            listDriverDataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            searchDriver();
            fonts();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addNewDriver fm = new addNewDriver();
            fm.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridIndex = (long)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                button3.Enabled = true;
                button1.Enabled = true;
            }
            catch
            {
                DataGridIndex = null;
                button3.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void listDriver_Click(object sender, EventArgs e)
        {
            DataGridIndex = null;
            button3.Enabled = false;
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DataGridIndex != null)
            {
                addNewDriver fm = new addNewDriver(DataGridIndex);
                fm.FormClosed += new FormClosedEventHandler(addNewDriver_FormClosed);
                fm.Show();
                DataGridIndex = null;
                button3.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Необходимо выбрать автомобиль, данные которого нужно изменить", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        void addNewDriver_FormClosed(object sender, FormClosedEventArgs e)
        {
            listDriverDataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DataGridIndex != null)
            {
                using (Context db = new Context(Form1.connectionString))
                {
                    DialogResult result = MessageBox.Show("Удалить водителя?", "Удаление",
                                                                                       MessageBoxButtons.YesNo,
                                                                                       MessageBoxIcon.Information);
                    if (result == DialogResult.No) return;
                    var delete = db.drivers
                                .Where(g => g.id_driver== DataGridIndex)
                                .FirstOrDefault();
                    db.drivers.Remove(delete);
                    db.SaveChanges();
                    DataGridIndex = null;
                    button3.Enabled = false;
                    button1.Enabled = false;
                    listDriverDataGrid();
                    MessageBox.Show("Водитель был удален");
                }
            }
        }
    }
}
