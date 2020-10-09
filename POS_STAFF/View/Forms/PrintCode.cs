using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_STAFF.View.Forms
{
    public partial class PrintCode : Form
    {
        string transactionCode = "";
        public PrintCode(string transactionCode)
        {
            InitializeComponent();
            this.transactionCode = transactionCode;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            printPreviewDialog1.Document = printDocument1;
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            printDocument1.Print();
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Transaction Code", new Font("Century Gothic", 14), Brushes.Black, new Point(100, 100));
            e.Graphics.DrawString(transactionCode, new Font("Century Gothic", 14), Brushes.Black, new Point(200, 100));
        }

        private void PrintCode_Load(object sender, EventArgs e)
        {
            lblTransactionCode.Text = transactionCode;
        }
    }
}
