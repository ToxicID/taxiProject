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
    public partial class listClient : Form
    {
        private void list()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var cl = db.clients.ToList();
                dataGridView1.DataSource = cl;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Номер телефона";
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;

                foreach (DataGridViewColumn data in dataGridView1.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView1.EnableHeadersVisualStyles = false;
            }
        }
        public listClient()
        {
            InitializeComponent();
            list();
            button2.Visible = false;
            button3.Visible = false;
            button3.Enabled = false;
            if (Form1.mode == 2) {
                button2.Visible = true;
                button3.Visible = true;
            }


        }

        

        private void dataGridView1_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            String s = dataGridView1[2, e.RowIndex].Value.ToString();
            if (bool.Parse(s) == true)
            {
                e.CellStyle.ForeColor = Color.Red;
            }
        }
        private long? DataGridIndex = null;
        private void button1_Click(object sender, EventArgs e)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                if (DataGridIndex != null)
                {
                    DialogResult result = MessageBox.Show("Посмотреть заказы данного клиента?", "Просмотр",
                                                                                       MessageBoxButtons.YesNo,
                                                                                       MessageBoxIcon.Information);
                    if (result == DialogResult.No) return;
                    OrderList fm = new OrderList(DataGridIndex);
                    fm.Show();
                    DataGridIndex = null;

                }
                else
                {
                    MessageBox.Show("Чтобы посмотреть заказы, необходимо выбрать клиента клиента","Ошибка",MessageBoxButtons.OK,
                                                                                                  MessageBoxIcon.Information);
                    return;
                }
           
            }
        }

        private void listClient_Load(object sender, EventArgs e)
        {

        }

        private void listClient_Click(object sender, EventArgs e)
        {
            DataGridIndex = null;
            button3.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridIndex = (long)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                button3.Enabled = true;
            }
            catch
            {
                DataGridIndex = null;
                button3.Enabled = false;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                if (DataGridIndex != null)
                {
                    DialogResult result = MessageBox.Show("Посмотреть заказы данного клиента?", "Просмотр",
                                                                                       MessageBoxButtons.YesNo,
                                                                                       MessageBoxIcon.Information);
                    if (result == DialogResult.No) return;
                    OrderList fm = new OrderList(DataGridIndex);
                    fm.Show();
                    DataGridIndex = null;
                    button3.Enabled = false;

                }
                else
                {
                    MessageBox.Show("Чтобы посмотреть заказы, необходимо выбрать клиента клиента", "Ошибка", MessageBoxButtons.OK,
                                                                                                  MessageBoxIcon.Information);
                    return;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                if (DataGridIndex != null)
                {
                    DialogResult result = MessageBox.Show("Убрать из чёрного списка данного клиента?", "Действие",
                                                                                       MessageBoxButtons.YesNo,
                                                                                       MessageBoxIcon.Information);
                    if (result == DialogResult.No) return;
                    var cl = db.clients.Where(p => p.id_client == DataGridIndex).FirstOrDefault();
                    if(cl.blacklist == true)
                    {
                        cl.blacklist = false;
                        db.SaveChanges();
                        MessageBox.Show("Клиент был убран из чёрного списка", "Successfully",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        list();
                    }
                    else
                    {
                        MessageBox.Show("Клиент не находится в чёрном списке", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        list();
                    }

                    DataGridIndex = null;
                    button3.Enabled = false;

                }
                else
                {
                    MessageBox.Show("Чтобы посмотреть заказы, необходимо выбрать клиента клиента", "Ошибка", MessageBoxButtons.OK,
                                                                                                  MessageBoxIcon.Information);
                    return;
                }

            }
        }
    }
}
