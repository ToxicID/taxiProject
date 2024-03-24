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
        }
    }
}
