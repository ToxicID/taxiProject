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

namespace taxiDesktopProg
{
    public partial class Form1 : Form
    {
        public static string connectionString;
        public Form1()
        {
            InitializeComponent();
        }
        public static string GetConnection(string login, string pass)
        {
            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder()
            {
                DataSource = $"DESKTOP-HV3NQ7V",
                InitialCatalog = "Taxi",
                IntegratedSecurity = false, 
                MultipleActiveResultSets = true,
                ApplicationName = "EntityFramework",
                UserID = login,
                Password = pass
            };
            return sqlString.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox1.Visible = true;
            PassTextBox.UseSystemPasswordChar = true;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox1.Visible = false;
            PassTextBox.UseSystemPasswordChar = false;
        }
        public static int mode;
        private void button1_Click(object sender, EventArgs e)
        {
            connectionString = GetConnection(LoginTextBox.Text, PassTextBox.Text);
            using (Context db = new Context(connectionString))
            {
                try
                {
                   
                     if (db.Database.SqlQuery<int>("SELECT is_member('dispatcher')").FirstOrDefault() == 1)
                        mode = 1;
                    else if(db.Database.SqlQuery<int>("SELECT is_member('sysadmin')").FirstOrDefault() == 1)
                        mode = 2;

                }
                catch
                {

                    MessageBox.Show("При авторизации произошла ошибка, проверьте данные и повторите заново", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                switch (mode)
                {
                    case 1:
                        string[] id_dis = LoginTextBox.Text.Split('_');
                        long id_dispatcher = long.Parse(id_dis[1]);
                        Form fm1 = new Form_for_Dispatcher(id_dispatcher, this);
                        fm1.Show();
                        this.Hide();
                        LoginTextBox.Text = "";
                        PassTextBox.Text = "";
                        break;

                    case 2:
                        Form fm = new Form_for_Admin(this);
                        fm.Show();
                        this.Hide();
                        LoginTextBox.Text = "";
                        PassTextBox.Text = "";
                        break;
                   
                    
                }
            }
        }
    }
}
