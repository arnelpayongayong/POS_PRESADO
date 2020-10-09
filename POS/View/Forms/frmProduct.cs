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
using POS.Middleware;

namespace POS.View.Forms
{
    public partial class frmProduct : Form
    {
        List<Product> savedProducts = new List<Product>();
        List<Product> unsavedProducts = new List<Product>();
        List<Distributor> distributors = new List<Distributor>();
        List<Category> categories = new List<Category>();
        Distributor selectedDistributor = new Distributor();

        public frmProduct()
        {
            InitializeComponent();
        }
        List<string> unsavedCategories = new List<string> {};
        private void btnExcel_Click(object sender, EventArgs e)
        {
            unsavedProducts = ExcelController.ExportExcelProduct();
            FillUpDataGridView();
        }
        private void PopulateCategoryList()
        {
            categories = CategoryController.GetAllCategory();
        }

        private void PopulateProductList()
        {
            savedProducts = ProductController.ProductList();
        }
        private void InitializeDataGridView()
        {

            dgvProduct.Font = new Font("Century Gothic", 15);

            dgvProduct.Columns.Add("no", "#");
            dgvProduct.Columns.Add("name", "Name");
            dgvProduct.Columns.Add("quantity", "Quantity");
            dgvProduct.Columns.Add("price", "Price");
            dgvProduct.Columns.Add("unit", "unit");
            dgvProduct.Columns.Add("unitValue", "Unit Value");
            dgvProduct.Columns.Add("category","Category");

            dgvProduct.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvProduct.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            PopulateList();
            PopulateSupplierComboBox();
            PopulateCategoryList();
            PopulateProductList();
        }

        private void FillUpDataGridView()
        {
            ComboBox box = new ComboBox();

            int counter = 0;
            foreach(var p in unsavedProducts)
            {
                dgvProduct.Rows.Add(p.productCode,p.productName,p.productQuantity, p.productPrice,p.productUnit,p.productUnitValue);
                dgvProduct.Rows[counter].Cells[6].Value = p.productCategory.name;
                counter++;
            }
        }



        public void addItems(AutoCompleteStringCollection col)
        {
            foreach (var c in unsavedCategories)
            {
                col.Add(c);
            }
        }

        private void dgvProduct_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var currentRow = dgvProduct;

            if (currentRow.CurrentCellAddress.X == 4)
            {
                TextBox autoText = e.Control as TextBox;
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                    addItems(DataCollection);
                    autoText.AutoCompleteCustomSource = DataCollection;
                }
            }
            else
            {
                TextBox autoText = e.Control as TextBox;

                autoText.AutoCompleteMode = AutoCompleteMode.None;
            }
        }

        private void dgvProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var currentRow = dgvProduct;

            if (currentRow.CurrentCellAddress.X == 4)
            {
                if (currentRow.CurrentCell.Value != null)
                    unsavedCategories.Add(currentRow.CurrentCell.Value.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new frmNewSupplier().ShowDialog();
        }

        private void PopulateList()
        {
            distributors = DistributorController.GetAll();
        }

        private void PopulateSupplierComboBox()
        {
            cmbDistributor.DataSource = distributors;
            cmbDistributor.DisplayMember = "name";
        }

        private void cmbDistributor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int si = 0;
            try
            {
                si = cmbDistributor.SelectedIndex;
            }
            catch (Exception)
            {

                return;
            }
            selectedDistributor = distributors[si];
            lblName.Text = distributors[si].name;
            lblAddress.Text = distributors[si].address;
            lblContact.Text = distributors[si].mobile;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveProducts();
        }
        
        private int ValidateSaving(string category)
        {
            if (CategoryController.isExist(categories, category))
            {
                int counter = 0;
                foreach(var c in categories)
                {
                    if (c.name == category)
                        return counter;

                    counter++;
                }
               
            }

            return CategoryController.Create(new Category()
            {
                name = category
            });



        }
        private void SaveProducts()
        {
            int i = 0;
            foreach(var p in unsavedProducts)
            {
                if(!ProductRequest.Validate(p))
                {
                    MessageBox.Show("Saving product interupted,Please check line " + i);
                    unsavedProducts.RemoveRange(0,i);
                    break;
                }

                if(ProductController.isExist(savedProducts, p.productCode))
                {
                    MessageBox.Show("Saving product interupted,Please use unique product code" + i);
                    break;
                }

                p.productDistributor = selectedDistributor;

                int categoryID = ValidateSaving(p.productCategory.name);
                p.productCategory.id = categoryID;

                ProductController.SaveProduct(p);
                i++;
            }
          
        }
    }
}
