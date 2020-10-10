using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    class Order
    {
        public int id { get; set; }
        public DateTime transactionDate { get; set; }
        public int status { get; set; }
        public double totalPrice { get; set; }
        public string transactionCode { get; set; }
        public double change { get; set; }
        public double payment { get; set; }
        public double discount { get; set; }


        public string Status(int status)
        {
            switch (status)
            {
                case 1:
                    return "Pending";
                case 2:
                    return "Completed";
            }

            return "Error";
        }
    }
}
