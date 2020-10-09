using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.View
{
    public partial class uscProduct : UserControl
    {
        public uscProduct()
        {
            InitializeComponent();
        }

        private void uscProduct_Load(object sender, EventArgs e)
        {
            InitListView();
        }

        private void InitListView()
        {
            dgvProduct.Columns.Add("no","#");
            dgvProduct.Columns.Add("name","Name");
            dgvProduct.Columns.Add("quantity","Quantity");
            dgvProduct.Columns.Add("price","Price");
            dgvProduct.Columns.Add("category","Category");
            dgvProduct.Columns.Add("distributor","Distributor");

            dgvProduct.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvProduct.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            new Forms.frmProduct().ShowDialog();
        }
    }
}
