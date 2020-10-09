using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.View;
using POS.View.UserControlList;
namespace POS
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            InitView();
        }

        private void InitView()
        {
            panelControl.Controls.Add(new uscDashboard());
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            panelControl.Controls.Clear();
            panelControl.Controls.Add(new uscProduct());
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            panelControl.Controls.Clear();
            panelControl.Controls.Add(new uscDashboard());
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            panelControl.Controls.Clear();
            panelControl.Controls.Add(new uscEmployee());
        }
    }
}
