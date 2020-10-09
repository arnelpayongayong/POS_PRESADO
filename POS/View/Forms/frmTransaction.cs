using POS.Controller;
using POS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.View.Forms
{
    public partial class frmTransaction : Form
    {
        List<Product> products = new List<Product>();
        List<OrderList> orders = new List<OrderList>();
        string currentTransactionID = "";

        public frmTransaction()
        {
            InitializeComponent();
        }

        private void frmTransaction_Load(object sender, EventArgs e)
        {
            InitializeProductList();
            PopulateProductList();
            FillListView();
            PopulateOrder();
            InitializeOrderList();
        }

        public void PopulateOrder()
        {
            orders = OrderController.GetAll();
            string[] orderSource = orders.Select(o => o.transactionCode).ToArray();

            txtSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
          
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            DataCollection.AddRange(orderSource);
            txtSearch.AutoCompleteCustomSource = DataCollection;
        }

      
        private void InitializeProductList()
        {
              string[][] listViewColumns = new string[][]
              {
                        new string[] {"Product Code","300" },
                        new string[] {"Product Name","200" },
                        new string[] {"Product Price","300" },
                        new string[] {"Product Unit","200" },
                        new string[] {"Category","200" },
               };

            UtilitiesController.InitializeListView(lsvProduct, listViewColumns);
        }
        private void InitializeOrderList()
        {
            string[][] listViewColumns = new string[][]
          {
                new string[] {"Code","100" },
                new string[] {"Name","100" },
                new string[] {"Price","100" },
                new string[] {"Quantity","100" },
                new string[] {"Category","100" },
          };

            UtilitiesController.InitializeListView(lsvOrder, listViewColumns);
        }
        private void FillListView()
        {
            ListViewItem item;

            lsvProduct.Items.Clear();

            foreach (var p in products)
            {
                item = lsvProduct.Items.Add(p.productCode);
                item.SubItems.Add(p.productName);
                item.SubItems.Add(p.productPrice.ToString());
                item.SubItems.Add(p.productUnitValue + " " + p.productUnit);
                item.SubItems.Add(p.productCategory.name);
            }
        }

        private void PopulateProductList()
        {
            products = ProductController.ProductList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            currentTransactionID = txtSearch.Text;
            lblTransactionCode.Text = currentTransactionID;
        }
    }
}
