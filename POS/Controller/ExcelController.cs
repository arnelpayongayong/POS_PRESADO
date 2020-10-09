using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using POS.Model;

namespace POS.Controller
{
    class ExcelController
    {

        public static List<Product> ExportExcelProduct()
        {
            List<Product> products = new List<Product>();
            string path = OpenFile();

            if (path == "") return null;


            FileInfo fileInfo = new FileInfo(path);

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

            // get number of rows and columns in the sheet
            int rows = worksheet.Dimension.Rows; // 20
            int columns = worksheet.Dimension.Columns; // 7
            // loop through the worksheet rows and columns
            for (int i = 1; i <= rows; i++)
            {

                try
                {
                    if (worksheet.Cells["B" + i].Value == null || worksheet.Cells["C" + i].Value == null || worksheet.Cells["D" + i].Value == null|| worksheet.Cells["E" + i].Value == null)
                        continue;
                    products.Add(new Product()
                    {
                        productCode = worksheet.Cells["B" + i].Value.ToString(),
                        productName = worksheet.Cells["C" + i].Value.ToString(),
                        productQuantity = int.Parse(worksheet.Cells["D" + i].Value.ToString()),
                        productPrice = float.Parse(worksheet.Cells["E" + i].Value.ToString()),
                        productUnit = worksheet.Cells["F" + i].Value.ToString(),
                        productUnitValue = float.Parse(worksheet.Cells["G" + i].Value.ToString()),
                        productExpirationDate = DateTime.FromOADate((double)worksheet.Cells["H" + i].Value),
                        productMinimunQuantity = int.Parse(worksheet.Cells["I" + i].Value.ToString()),
                        productCategory = new Category() { name = worksheet.Cells["J" + i].Value.ToString(), },
                    }); ;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }

            return products;
        }

        public static string OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "excel files (*.xls)|*.xlsx*";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return "";
            }

            return openFileDialog.FileName;
        }
    }
}
