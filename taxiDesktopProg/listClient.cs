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
               
           
            }
        }
        public listClient()
        {
            InitializeComponent();
            list();
            
            
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
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridIndex = (long)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            }
            catch
            {
                DataGridIndex = null;
            }
        }
    }
}
