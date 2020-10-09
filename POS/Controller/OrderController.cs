using POS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Controller
{
    class OrderController
    {
        public static List<OrderList> GetAll()
        {
            List<OrderList> orders = null; ;

            using (DAL dal = new DAL())
            {
                var data = dal.ExecuteQuery("spGetOrder").Tables[0];

                orders = DataRowToOrderList(data);

                return orders;
            }
        }

        public static List<Order> DataRowToOrderList(DataTable data)
        {
            List<Order> orders = new List<Order>();

            foreach (DataRow dr in data.AsEnumerable())
            {
                orders.Add(new Order()
                {
                   id = dr.Field<int>("id"),
                   status = dr.Field<int>("status"),
                   totalPrice = dr.Field<double>("totalPrice"),
                   transactionCode = dr.Field<string>("transactionCode"),
                   transactionDate = dr.Field<DateTime>("transactionDate")
                });
            }

            return orders;
        }
    }
}
