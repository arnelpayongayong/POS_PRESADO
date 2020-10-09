using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_STAFF.Controller;
using POS_STAFF.Model;
using POS_STAFF.View;
using POS_STAFF.View.Forms;

namespace POS_STAFF.View
{
    public partial class POS : Form
    {
        List<Product> products = new List<Product>();
        List<Product> pendingOrder = new List<Product>();
        double totalPrice = 0;
        public POS()
        {
            InitializeComponent();
        }

        private void POS_Load(object sender, EventArgs e)
        {
            InitializeListView();
            PopulateListView();
            FillListView();
            FillComboBox();
            InitializeListViewPending();
        }

        private void InitializeListView()
        {
           string[][] listViewColumns = new string[][]
          {
                new string[] {"Product Code","300" },
                new string[] {"Product Name","200" },
                new string[] {"Product Price","300" },
                new string[] {"Product Unit","200" },
                new string[] {"Category","200" },
          };

            UtilitiesController.InitializeListView(lsvProducts, listViewColumns);
        }

        private void InitializeListViewPending()
        {
            string[][] listViewColumns = new string[][]
           {
                new string[] {"Code","100" },
                new string[] {"Name","100" },
                new string[] {"Price","100" },
                new string[] {"Quantity","100" },
                new string[] {"Category","100" },
           };

            UtilitiesController.InitializeListView(lsvPendingOrder, listViewColumns);
        }

        private void PopulateListView()
        {
            products = ProductController.ProductList();
        }
        private void FillPendingListView()
        {
            ListViewItem item;

            lsvPendingOrder.Items.Clear();

            foreach (var p in pendingOrder)
            {
                item = lsvPendingOrder.Items.Add(p.productCode);
                item.SubItems.Add(p.productName);
                item.SubItems.Add(p.productPrice.ToString());
                item.SubItems.Add(p.productQuantity.ToString());
                item.SubItems.Add(p.productCategory.name);
            }
        }

        private void FillListView()
        {
            ListViewItem item;

            lsvProducts.Items.Clear();

            foreach(var p in products)
            {
                item = lsvProducts.Items.Add(p.productCode);
                item.SubItems.Add(p.productName);
                item.SubItems.Add(p.productPrice.ToString());
                item.SubItems.Add(p.productUnitValue + " " + p.productUnit);
                item.SubItems.Add(p.productCategory.name);
            }
        }

        private void lsvProducts_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                AddToPendingOrder();
                return;
            }

        }
        private void FillComboBox()
        {
            cmbCategory.DataSource = CategoryController.GetAllCategory();
            cmbCategory.DisplayMember = "name";
        }
        private void AddToPendingOrder()
        {
            int si = 0;

            try
            {
                si = lsvProducts.SelectedItems[0].Index;
            }
            catch (Exception)
            {

                MessageBox.Show("Please select item");
                return;
            }

            AddToPendingOrderList(si);
            FillPendingListView();
        }

        private void lsvProducts_DoubleClick(object sender, EventArgs e)
        {
            AddToPendingOrder();
        }

        private unsafe int IsAlreadyOnPendingOrder(int id, bool* pointer)
        {
           int i = 0;
           
            foreach(var p in pendingOrder)
            {
                if (p.productID == id)
                {
                    *pointer = true;
                    return i;
                }

                i++;
            }

           return i;
        }
        private unsafe void AddToPendingOrderList(int selectedIndex)
        {
            Product selectedProduct = products[selectedIndex];
            bool sample = false;
            bool* isExist = &sample;

            int index = IsAlreadyOnPendingOrder(selectedProduct.productID,isExist);

            if(*isExist == false)
            {
               
                selectedProduct.productQuantity = 1;
                totalPrice += selectedProduct.productPrice;
                pendingOrder.Add(selectedProduct);
            }
            else
            {
                pendingOrder[index].productQuantity++;
                totalPrice += pendingOrder[index].productPrice;
            }

            lblTotalPrice.Text = totalPrice.ToString() + " PHP";
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            string transactionCode = totalPrice + DateTime.Now.ToString("ssmmMMdd");

            int id = SaveOrder(transactionCode);
            SaveOrderList(id);
            new PrintCode(transactionCode).ShowDialog();
        }

        private unsafe int SaveOrder(string transactionCode)
        {
            Order order = new Order()
            {
                transactionDate = DateTime.Now,
                status = 1,
                totalPrice = totalPrice,
                transactionCode = transactionCode
            };

            return OrderController.Create(order);
        }

        private void SaveOrderList(int id)
        {
            foreach(var p in pendingOrder)
            {
                OrderList orderList = new OrderList()
                {
                    transactionID = id,
                    product = p,
                    quantity = p.productQuantity
                };

                OrderListController.Create(orderList);
            }
        }
    }
}
