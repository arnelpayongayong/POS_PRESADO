using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Controller;
using POS.Model;
using POS.Middleware;

namespace POS.View.Forms
{
    public partial class frmNewUser : Form
    {
        User user = new User();
        public frmNewUser(Model.User user = null)
        {
            InitializeComponent();

            lblStatus.Text = (user == null) ? "New User" : "Update User";
            this.user = user;
            FillUp();
        }

        private void FillUp()
        {
            if (user == null) return;

            txtFirstname.Text = user.firstname;
            txtMiddlename.Text = user.middlename;
            txtLastname.Text = user.lastname;
            txtMobile.Text = user.contact;
            txtUsername.Text = user.username;
            txtPassword.Text = user.password;
            cmbPosition.Text = user.position;
            txtAddress.Text = user.address;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            User user = new User()
            {
                id = (this.user == null)?0:this.user.id,
                firstname = txtFirstname.Text,
                middlename = txtMiddlename.Text,
                lastname = txtLastname.Text,
                address = txtAddress.Text,
                contact = txtMobile.Text,
                username = txtUsername.Text,
                password = txtPassword.Text,
                position = cmbPosition.SelectedItem.ToString()
            };

            if (!UserRequest.Validate(user))
            {
                MessageBox.Show("Unsucessfully to save please check all the data");
                return;
            }

            SaveOrUpdate(user);



        }
        

        private void SaveOrUpdate(User user)
        {
            try
            {
                if (user.id == 0)
                {
                    
                    UserController.Create(user);
                }
                else
                {

                    UserController.Update(user);
                   
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please use different username");
                return;
            }

            string message = (user==null)?"Sucessfully Created":"Sucessfully Updated";

            MessageBox.Show(message);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;


        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
