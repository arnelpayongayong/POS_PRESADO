using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Model;
using POS.Controller;
using System.Management;

namespace POS.View.Forms
{
    public partial class frmNewSupplier : Form
    {
        public frmNewSupplier()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Distributor distributor = new Distributor()
            {
                name = txtName.Text,
                address = txtAddress.Text,
                mobile = txtContact.Text
            };

            DistributorController.Create(distributor);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
