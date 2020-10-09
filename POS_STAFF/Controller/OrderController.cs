using POS_STAFF.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_STAFF.Controller
{
    class OrderController
    {
        public static int Create(Order order)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams = {
                        new SqlParameter("@transactionDate",order.transactionDate),
                        new SqlParameter("@status",order.status),
                        new SqlParameter("@totalPrice",order.totalPrice),
                        new SqlParameter("@transationCode",order.transactionCode)
                    };


                int id = (int)dal.ExecuteQueryScalar("spCreateOrder", spParams);

                return id;
            }
        }
    }
}
