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
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 100);
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            e.Graphics.DrawString("Transaction Code", new Font("Century Gothic", 14), Brushes.Black, (float)140.5,20,stringFormat);
            e.Graphics.DrawString(transactionCode, new Font("Century Gothic", 14), Brushes.Black, (float)140.5, 50, stringFormat);
            e.Graphics.DrawString("DATE/TIME" + DateTime.Now.ToString(), new Font("Century Gothic", 10), Brushes.Black, (float)140.5, 70, stringFormat);
        }

        private void PrintCode_Load(object sender, EventArgs e)
        {
            lblTransactionCode.Text = transactionCode;
        }
    }
}
