using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Model;
using POS.Controller;

namespace POS.View
{
    public partial class uscProduct : UserControl
    {
        List<Product> products = new List<Product>();
        public uscProduct()
        {
            InitializeComponent();
        }

        private void uscProduct_Load(object sender, EventArgs e)
        {
            InitListView();
            PopulateProductList();
            FillUpDataGridView();
        }

        private void InitListView()
        {
            dgvProduct.Font = new Font("Century Gothic", 11);

            dgvProduct.Columns.Add("no", "#");
            dgvProduct.Columns.Add("name", "Name");
            dgvProduct.Columns.Add("quantity", "Quantity");
            dgvProduct.Columns.Add("price", "Price");
            dgvProduct.Columns.Add("unit", "unit");
            dgvProduct.Columns.Add("unitValue", "Unit Value");
            dgvProduct.Columns.Add("expiration", "Expiration Date");
            dgvProduct.Columns.Add("category", "Category");
            dgvProduct.Columns.Add("distributor", "Distributor");

            dgvProduct.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvProduct.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvProduct.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if(new Forms.frmProduct().ShowDialog() == DialogResult.OK)
            {
                PopulateProductList();
                FillUpDataGridView();
            }
        }
        private void PopulateProductList()
        {
            products = ProductController.ProductList();
        }

        private void FillUpDataGridView()
        {
            ComboBox box = new ComboBox();

            int counter = 0;
            foreach (var p in products)
            {
                dgvProduct.Rows.Add(p.productCode, p.productName, p.productQuantity, p.productPrice, p.productUnit, p.productUnitValue,p.productExpirationDate.ToShortDateString());
                dgvProduct.Rows[counter].Cells[7].Value = p.productCategory.name;
                dgvProduct.Rows[counter].Cells[8].Value = p.productDistributor.name;
                counter++;
            }
        }
    }
}
