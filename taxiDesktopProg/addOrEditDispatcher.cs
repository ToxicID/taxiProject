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
    public partial class addOrEditDispatcher : Form
    {
        public addOrEditDispatcher()
        {
            InitializeComponent();
            ButIzmen.Visible = false;
            button1.Visible = false;
        }
        private long? idDispEdit;
        private void listDataDisp()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var disp = db.dispatchers.Where(p => p.id_dispatcher == idDispEdit).FirstOrDefault();
                textBox1.Text = disp.surname;
                textBox2.Text = disp.name;
                textBox3.Text = disp.patronymic;
                textBoxMobule.Text = disp.mobile_phone;
                if(textBox3.Text == "Отсутствует" || string.IsNullOrWhiteSpace(textBox3.Text))
                        checkBox1.Checked = true;
                    

            }
        }
        public addOrEditDispatcher(long? idDisp,string whyEdit)
        {
            InitializeComponent();
            idDispEdit = idDisp;
            AddDispatcherBut.Visible = false;
            if (whyEdit == "data")
            {
                button1.Visible = false;
                this.Height = 429;
                label8.Visible = false;
                label9.Visible = false;
                textBoxPas.Visible = false;
                textBoxRepitPas.Visible = false;
                listDataDisp();

            }
            else if (whyEdit == "pass")
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBoxMobule.ReadOnly = true;
                ButIzmen.Visible = false;
                listDataDisp();
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxMobule_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxMobule.MaxLength = 11;
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void AddDriverBut_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBoxMobule.Text))
            {
                MessageBox.Show("Заполните все обязательные поля!\nНеобязательное поле только отчество диспетчера", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBoxMobule.Text.Length != 11 || textBoxMobule.Text[0] == '0')
            {
                MessageBox.Show("Неполностью или неправильно заполнено поле \"Номер телефона\"");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxPas.Text) || string.IsNullOrWhiteSpace(textBoxRepitPas.Text))
            {
                MessageBox.Show("Заполните поля с паролями", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxPas.Text != textBoxRepitPas.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "Отсутствует";
                checkBox1.Checked = true;
            }
            using (Context db = new Context(Form1.connectionString))
            {
                var dispAll = db.dispatchers;
                if(dispAll.Where(p=>p.mobile_phone == textBoxMobule.Text).Count() == 1)
                {
                    MessageBox.Show("Диспетчер с таким номером телефона уже есть в базе данных","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                if(dispAll.Where(p => p.surname == textBox1.Text && p.name == textBox2.Text && p.patronymic == textBox3.Text).Count() == 1)
                {
                    MessageBox.Show("Диспетчер с таким ФИО уже есть в базе данных", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                idNewDisp = db.Database.SqlQuery<long>("select max(id_dispatcher) + 1 from dispatcher").FirstOrDefault();
                dispatcher d = new dispatcher()
                {
                    id_dispatcher = idNewDisp,
                    surname = textBox1.Text,
                    name = textBox2.Text,
                    patronymic = textBox3.Text,
                    mobile_phone = textBoxMobule.Text,

                };
                db.dispatchers.Add(d);
               



                conStr = GetRemoteConnectionString("DESKTOP-HV3NQ7V\\Илья", "");
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            try
            {
                cmd.CommandText = $"EXEC sp_addlogin 'dispatcher_{idNewDisp}','{textBoxPas.Text}','Taxi'";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"USE Taxi";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"EXEC sp_adduser 'dispatcher_{idNewDisp}','dispatcher_{idNewDisp}'";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"EXEC sp_addrolemember 'dispatcher','dispatcher_{idNewDisp}'";
                cmd.ExecuteNonQuery();

                con.Close();

            }

            catch
            {
                MessageBox.Show("Такой логин занят");
                return;
            }
            MessageBox.Show($"Диспетчер зарегистрирован.\nЛогин: dispatcher_{idNewDisp}\nПароль: {textBoxPas.Text}", "Successfully",MessageBoxButtons.OK,MessageBoxIcon.Information);
                db.SaveChanges();
                this.Close();
            }
        }
        private string conStr;
        private long idNewDisp;
        public static string GetRemoteConnectionString(string login, string password)
        {
            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder()
            {
                DataSource = $"DESKTOP-HV3NQ7V",
                InitialCatalog = "Taxi", //Database
                IntegratedSecurity = true,
                MultipleActiveResultSets = true,
                ApplicationName = "EntityFramework",
                UserID = login,
                Password = password

            };
            return sqlString.ToString();
        }
        private void ButIzmen_Click(object sender, EventArgs e)
        {
            using (Context db = new Context(Form1.connectionString))
            {
                var disp = db.dispatchers.Where(p => p.id_dispatcher == idDispEdit).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBoxMobule.Text))
                {
                    MessageBox.Show("Заполните все обязательные поля!\nНеобязательное поле только отчество диспетчера", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (textBoxMobule.Text.Length != 11 || textBoxMobule.Text[0] == '0')
                {
                    MessageBox.Show("Неполностью или неправильно заполнено поле \"Номер телефона\"");
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    textBox3.Text = "Отсутствует";
                    checkBox1.Checked = true;
                }
                disp.surname = textBox1.Text;
                disp.name = textBox2.Text;
                disp.patronymic = textBox3.Text;
                disp.mobile_phone = textBoxMobule.Text;
                db.SaveChanges();
                MessageBox.Show($"Строка обновлена", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                if (string.IsNullOrWhiteSpace(textBoxPas.Text) || string.IsNullOrWhiteSpace(textBoxRepitPas.Text))
            {
                MessageBox.Show("Заполните поля с паролями", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxPas.Text != textBoxRepitPas.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            conStr = GetRemoteConnectionString("DESKTOP-HV3NQ7V\\Илья", "");
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            try
            {

                cmd.CommandText = $"USE Taxi";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"ALTER LOGIN dispatcher_{idDispEdit} WITH PASSWORD = '{textBoxPas.Text}'";
                cmd.ExecuteNonQuery();

                con.Close();

            }

            catch
            {
                MessageBox.Show("Что-то пошло не так. Попробуйте ещё раз","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Пароль обновлён.\nЛогин: dispatcher_{idDispEdit}\nПароль: {textBoxPas.Text}", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        private string preliminaryPatronymic = "";
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                preliminaryPatronymic = textBox3.Text;
                textBox3.ReadOnly = true;
                textBox3.Text = "Отсутствует";
            }
            else
            {
                textBox3.ReadOnly = false;
                textBox3.Text = preliminaryPatronymic;
            }
        }
    }
}
