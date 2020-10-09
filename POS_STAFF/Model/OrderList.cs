using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_STAFF.Model
{
    class OrderList
    {
        public int id { get; set; }
        public Product product { get; set; }
        public int quantity { get; set; }
        public int transactionID { get; set; }
    }
}
