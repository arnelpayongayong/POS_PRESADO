using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    class Product
    {
        public int productID { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public int productQuantity { get; set; }
        public double productPrice { get; set; }
        public string productUnit { get; set; }
        public double productUnitValue { get; set; }
        public DateTime productExpirationDate { get; set; }
        public int productMinimunQuantity { get; set; }

        public Category productCategory { get; set; }
        public Distributor productDistributor { get; set; }


        public Product()
        {
            productCategory = new Category() { name = "" };
            productDistributor = new Distributor() { name = "" };

        }
    }
}
