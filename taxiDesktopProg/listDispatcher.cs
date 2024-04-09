using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace taxiDesktopProg
{
    public partial class listDispatcher : Form
    {
        private void list()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var disp = db.dispatchers.ToList();
                dataGridView1.DataSource = disp;
                fonts();
            }
        }
        private void fonts()
        {
            dataGridView1.Columns[0].HeaderText = "Логин";
            dataGridView1.Columns[1].HeaderText = "Фамилия";
            dataGridView1.Columns[2].HeaderText = "Имя";
            dataGridView1.Columns[3].HeaderText = "Отчество";
            dataGridView1.Columns[4].HeaderText = "Номер телефона";
            dataGridView1.Columns[5].Visible = false;
        }
        private void search()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var disp = db.dispatchers.Where(x => x.surname.Contains(textBox2.Text)).ToList();
                dataGridView1.DataSource = disp;
            }
            fonts();
            }
        private long? DataGridIndex = null;
        public listDispatcher()
        {
            InitializeComponent();
            list();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            label1.Text = "Поиск диспетчера\nпо фамилии";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {
                e.Value = $"dispatcher_{e.Value}";
            }
        }
        void addOrEditDispatcher_FormClosed(object sender, FormClosedEventArgs e)
        {
            list();
        }
        private void AddDispatcherBut_Click(object sender, EventArgs e)
        {
            addOrEditDispatcher fm = new addOrEditDispatcher();
            fm.FormClosed += new FormClosedEventHandler(addOrEditDispatcher_FormClosed);
            fm.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridIndex = (long)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            catch
            {
                DataGridIndex = null;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addOrEditDispatcher fm = new addOrEditDispatcher(DataGridIndex, "data");
            fm.FormClosed += new FormClosedEventHandler(addOrEditDispatcher_FormClosed);
            fm.Height = 329;
            DataGridIndex = null;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            fm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addOrEditDispatcher fm = new addOrEditDispatcher(DataGridIndex, "pass");
            fm.FormClosed += new FormClosedEventHandler(addOrEditDispatcher_FormClosed);
            DataGridIndex = null;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            fm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                if (DataGridIndex != null)
                {

                    DialogResult result = MessageBox.Show("Удалить данные о этом диспетчере?", "Удаление",
                                                                                       MessageBoxButtons.YesNo,
                                                                                       MessageBoxIcon.Information);
                    if (result == DialogResult.No) return;
                    var delete = db.dispatchers
                                .Where(g => g.id_dispatcher == DataGridIndex)
                                .FirstOrDefault();
                    db.dispatchers.Remove(delete);
                    db.SaveChanges();


                }
            }

            string connectionStrings = addOrEditDispatcher.GetRemoteConnectionString("DESKTOP-HV3NQ7V\\Илья", "");

            SqlConnection con = new SqlConnection(connectionStrings);
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            try
            {

                cmd.CommandText = $"Use Taxi";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"DROP SCHEMA \"dispatcher_{DataGridIndex}\"";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"DROP USER \"dispatcher_{DataGridIndex}\"";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"use master";
                cmd.ExecuteNonQuery();
                cmd.CommandText = $"DROP LOGIN \"dispatcher_{DataGridIndex}\"";
                cmd.ExecuteNonQuery();

                con.Close();
            }

            catch
            {
                MessageBox.Show("Произошла ошибка при удалении", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                list();
                return;
            }

            list();
            MessageBox.Show("Диспетчер был удален","Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DataGridIndex = null;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            search();
            
        }
    }
}
