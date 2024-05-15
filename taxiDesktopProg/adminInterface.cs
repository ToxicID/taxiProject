using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taxiDesktopProg
{
    internal class adminInterface
    {
        Form_for_Admin form_For_Admin;
        public adminInterface(Form_for_Admin fm)
        {
            form_For_Admin = fm;
        }
        public void customizeDesing()
        {
            form_For_Admin.panelDriver.Visible = false;
            form_For_Admin.panelProfiles.Visible = false;
            form_For_Admin.panelRate.Visible = false;

        }
        public void hidesubMenu()
        {
            if (form_For_Admin.panelDriver.Visible == true)
                form_For_Admin.panelDriver.Visible = false;
            if (form_For_Admin.panelProfiles.Visible == true)
                form_For_Admin.panelProfiles.Visible = false;
            if (form_For_Admin.panelRate.Visible == true)
                form_For_Admin.panelRate.Visible = false;
        }
        public void showMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hidesubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        public Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            form_For_Admin.panelGlavWin.Controls.Add(childForm);
            form_For_Admin.panelGlavWin.Tag = childForm;
            childForm.BringToFront();
            form_For_Admin.dataGridView1.Visible = false;
            childForm.Show();
        }
    }
}
