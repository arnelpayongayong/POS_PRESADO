using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Controller;
using POS.Model;
using POS.View.Forms;

namespace POS.View.UserControlList
{
    public partial class uscEmployee : UserControl
    {
        List<User> users = new List<User>();
        public uscEmployee()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

            if(DialogResult.OK == new Forms.frmNewUser().ShowDialog())
            {
                PopulateList();
                UpdateListView();
            }
        }
        private void InitializeListView()
        {
            string[][] listViewColumns = new string[][]
            {
                new string[] {"Fullname","300" },
                new string[] {"Mobile No.","200" },
                new string[] {"Address","300" },
                new string[] {"Position","200" },
            };
            lsvEmployee = UtilitiesController.InitializeListView(lsvEmployee, listViewColumns);
        }

        private void PopulateList()
        {
            var getAllMethod = UserController.GetAll();
            users = UserController.GetAllActiveUser(getAllMethod);
        }
        private void UpdateListView()
        {
            ListViewItem item;

            lsvEmployee.Items.Clear();

            foreach(var u in users)
            {
                item = lsvEmployee.Items.Add(u.firstname + " " + u.middlename[0] + ". " + u.lastname);
                item.SubItems.Add(u.contact);
                item.SubItems.Add(u.address);
                item.SubItems.Add(u.position);
            }
        }
        private void btnAssignedHistory_Click(object sender, EventArgs e)
        {
            new Forms.frmCashierAssignedHistory().ShowDialog();
        }

        private void uscEmployee_Load(object sender, EventArgs e)
        {
            InitializeListView();
            PopulateList();
            UpdateListView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedIndex = 0;

            try
            {
                selectedIndex = lsvEmployee.SelectedItems[0].Index;
            }
            catch
            {
                MessageBox.Show("Please select you wish to delete");
                return;
            }

            string message = "Do you really want to delete " + users[selectedIndex].lastname;

            if(MessageBox.Show(message, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK )
                UserController.Delete(users[selectedIndex].id);

            PopulateList();
            UpdateListView();
         
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int selectedIndex = 0;

            try
            {
                selectedIndex = lsvEmployee.SelectedItems[0].Index;
            }
            catch
            {
                MessageBox.Show("Please select you wish to edit");
                return;
            }

            if (DialogResult.OK == new Forms.frmNewUser(users[selectedIndex]).ShowDialog())
            {
                PopulateList();
                UpdateListView();
            }
        }
    }
}
