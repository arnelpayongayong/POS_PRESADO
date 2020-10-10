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
        List<Order> orders = new List<Order>();
        List<OrderList> orderLists = new List<OrderList>();
        Order currentOrder = new Order();
        double currentPrice = 0;
        double currentChange = 0;
        double currentPayment = 0;
        double currentDiscount = 0;
        string currentTransactionCode = "";

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
            currentTransactionCode = txtSearch.Text;
            lblTransactionCode.Text = currentTransactionCode;

            PopulateOrderList();
            FillOrderListView();

        }

        private void PopulateOrderList()
        {
            currentOrder = OrderController.GetOrder(orders,currentTransactionCode);
            orderLists = OrderListController.GetAll(currentOrder.id);
        }

        private void FillOrderListView()
        {
            ListViewItem item;

            lsvOrder.Items.Clear();

            foreach (var p in orderLists)
            {
                item = lsvOrder.Items.Add(p.product.productCode);
                item.SubItems.Add(p.product.productName + " " + p.product.productUnitValue + " " + p.product.productUnit);
                item.SubItems.Add(p.product.productPrice.ToString());
                item.SubItems.Add(p.quantity.ToString());
                currentPrice += p.product.productPrice * p.quantity;
            }

            txtTotalPrice.Text = currentPrice.ToString();
        }

      

        private void txtPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            
        }

        private void txtPayment_TextChanged(object sender, EventArgs e)
        {
            if (txtPayment.Text.Length == 0) 
            {
                txtChange.Clear();
                return;
            }

            currentPayment = double.Parse(txtPayment.Text);
            currentChange = currentPayment - currentPrice;
            txtChange.Text = currentChange.ToString();
        }

        private void PrintReceipt()
        {
            printPreviewDialog1.Document = printDocument1;
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 1000);

            printPreviewDialog1.ShowDialog();
        }

        private void btnCompleteOrder_Click(object sender, EventArgs e)
        {
            CompleteOrder();
            UpdateProductQuantity();
            PrintReceipt();
            OrderClear();
        }
        private void CompleteOrder()
        {
            currentOrder.status = 2;
            currentOrder.payment = currentPayment;
            currentOrder.totalPrice = currentPrice;
            currentOrder.change = currentChange;
            currentOrder.discount = currentDiscount;

            OrderController.Update(currentOrder);
        }
        private void UpdateProductQuantity()
        {
            foreach(var p in orderLists)
            {
                p.product.productQuantity = p.quantity;
                ProductController.UpdateProductQuantity(p.product);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ReceiptHeader(e);
            ReceiptBody(e);
            ReceiptFooter(e);
            //for (int i = 0; i < 300; i += 50)
            //{
            //    e.Graphics.DrawString(i.ToString(), new Font("Century Gothic", 14), Brushes.Black, new Point(i, 100));
            //}

            //for (int i = 0; i < 1000; i += 50)
            //{
            //    e.Graphics.DrawString(i.ToString(), new Font("Century Gothic", 14), Brushes.Black, new Point(100, i));
            //}

            //e.Graphics.DrawString(i.ToString(), new Font("Century Gothic", 14), Brushes.Black, new Point(100, 100));
        }

        private void ReceiptHeader(System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // Draw the text and the surrounding rectangle.
            e.Graphics.DrawString("Presado Market", new Font("Fake Receipt", 8), Brushes.Black, (float)142.5, 20, stringFormat);
            e.Graphics.DrawString("Sample Street,Daet City", new Font("Fake Receipt", 8), Brushes.Black, (float)142.5, 40,stringFormat);
            e.Graphics.DrawString("Owned and Operated by:", new Font("Fake Receipt", 8), Brushes.Black, (float)142.5, 60,stringFormat);
            e.Graphics.DrawString("Presado Family Inc.", new Font("Fake Receipt", 8), Brushes.Black, (float)142.5, 80, stringFormat);
            e.Graphics.DrawString("Vat Reg. TIN ###-###-###-###", new Font("Fake Receipt", 8), Brushes.Black, (float)142.5, 100, stringFormat);
            e.Graphics.DrawString("PRESADO POS 2400 S/N: CP-##########", new Font("Fake Receipt", 8), Brushes.Black, (float)142.5, 120, stringFormat);


            StringFormat stringFormatLeft = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;

            e.Graphics.DrawString("Name :", new Font("Fake Receipt", 8), Brushes.Black, (float)10, 150, stringFormatLeft);
            e.Graphics.DrawString("Address :", new Font("Fake Receipt", 8), Brushes.Black, (float)10, 170, stringFormatLeft);
            e.Graphics.DrawString("TIN :", new Font("Fake Receipt", 8), Brushes.Black, (float)10, 190, stringFormatLeft);
        }

        private void ReceiptBody(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int totalItem = 0;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;

            e.Graphics.DrawString("------------------------------------", new Font("Fake Receipt", 8), Brushes.Black, (float)10, 220, stringFormat);
            e.Graphics.DrawString("QTY DESCRIPTION       PRICE   AMOUNT", new Font("Fake Receipt", 8), Brushes.Black, (float)10, 235, stringFormat);
            int spaceCounter = 10;

            foreach(var p in orderLists)
            {
                string productDescription = p.product.productName + " " + p.product.productUnitValue + p.product.productUnit ;
                string amount = (p.product.productPrice * p.quantity).ToString();
                totalItem += p.quantity;

                e.Graphics.DrawString(p.quantity.ToString(), new Font("Fake Receipt", 8), Brushes.Black, (float)10, 235+spaceCounter, stringFormat);
                e.Graphics.DrawString(productDescription.ToUpper(), new Font("Fake Receipt", 8), Brushes.Black, (float)40, 235 + spaceCounter, stringFormat);
                e.Graphics.DrawString(p.product.productPrice.ToString(), new Font("Fake Receipt", 8), Brushes.Black, (float)180, 235 + spaceCounter, stringFormat);
                e.Graphics.DrawString(amount, new Font("Fake Receipt", 8), Brushes.Black, (float)230, 235 + spaceCounter, stringFormat);
               
                spaceCounter += 15;
            }

            int newHeight = 235 + spaceCounter + 20;
            e.Graphics.DrawString("------------------------------------", new Font("Fake Receipt", 8), Brushes.Black, (float)10, newHeight, stringFormat);

            e.Graphics.DrawString(totalItem + " ITEMS(s)", new Font("Fake Receipt", 8), Brushes.Black, (float)10, newHeight+20, stringFormat);

        }

        private void ReceiptFooter(System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            int length = orderLists.Count() * 15 + 210 + 200;

            e.Graphics.DrawString("TOTAL:       " + String.Format("{0:0.00}", currentPrice) + "PHP", new Font("Fake Receipt", 12), Brushes.Black, (float)130,length , stringFormat);
            e.Graphics.DrawString("CHANGE:      " + String.Format("{0:0.00}", currentChange)+ "PHP", new Font("Fake Receipt", 12), Brushes.Black, (float)130, length+30, stringFormat);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OrderController.Delete(currentOrder.id);
            MessageBox.Show("Order #" + currentOrder.transactionCode + " is deleted");
            OrderClear();
            PopulateOrder();

        }

        private void OrderClear()
        {
            txtSearch.Clear();
            lsvOrder.Items.Clear();
            txtTotalPrice.Clear();
            txtChange.Clear();
            txtPayment.Clear();
            currentChange = 0;
            currentPayment = 0;
            currentDiscount = 0;
            currentPrice = 0;
            currentOrder = null;
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (txtDiscount.Text.Length == 0)
            {
                currentDiscount = 0;
            }

            currentDiscount = double.Parse(txtDiscount.Text);
        }
    }
}
