using POS_STAFF.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_STAFF.Controller
{
    class OrderListController
    {
        public static void Create(OrderList orderList)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams = {
                        new SqlParameter("@productID",orderList.product.productID),
                        new SqlParameter("@transactionID",orderList.transactionID),
                        new SqlParameter("@quantity",orderList.quantity)

                    };


                dal.ExecuteNonQuery("spCreateOrderList", spParams);

                return;
            }
        }
    }
}
