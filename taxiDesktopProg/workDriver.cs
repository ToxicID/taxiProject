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
    public partial class workDriver : Form
    {
        private void listWorkDriver()
        {
            using (Context db = new Context(Form1.connectionString))
            {
                DateTime now = DateTime.Now;
            
                var list = from dr in db.drivers
                           join work in db.work_schedule on dr.id_driver equals work.id_driver
                           select new
                           {
                               id_work = work.id_work_schedule,
                               FullNameDriver = dr.surname + " " + dr.name.Substring(0, 1) + ". " + dr.patronymic.Substring(0, 1) + ".",
                               dateWorkFrom = work.date_of_work_from,
                               dateWorkBefore = work.date_of_work_before,
                               fromWorkTime = work.work_schedule_from.ToString(),
                               beforeWorkTime = work.work_schedule_before
                               
                           };
                dataGridView1.DataSource = list.ToList();
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "ФИО";
                dataGridView1.Columns[2].HeaderText = "Дата начала работы";
                dataGridView1.Columns[3].HeaderText = "Дата окончания работы";
                dataGridView1.Columns[4].HeaderText = "Время начала работы";
                dataGridView1.Columns[5].HeaderText = "Принятые время окончания работы";

                foreach (DataGridViewColumn data in dataGridView1.Columns)
                    data.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView1.EnableHeadersVisualStyles = false;
            }
        }
        public workDriver()
        {
            InitializeComponent();
            listWorkDriver();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }
    }
}
