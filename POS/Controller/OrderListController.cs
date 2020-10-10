using POS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Controller
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

        public static void Update(OrderList orderList)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams = {
                        new SqlParameter("@productID",orderList.product.productID),
                        new SqlParameter("@transactionID",orderList.transactionID),
                        new SqlParameter("@quantity",orderList.quantity),
                    };


                dal.ExecuteNonQuery("spUpdateOrderList", spParams);

                return;
            }
        }

        public static void Delete(OrderList orderList)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams = {
                        new SqlParameter("@productID",orderList.product.productID),
                    };


                dal.ExecuteNonQuery("spDeleteOrderList", spParams);

                return;
            }
        }

        public static List<OrderList> GetAll(int transactionID)
        {
            List<OrderList> ordersList = null; ;

            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams = {
                        new SqlParameter("@transactionID",transactionID),

                };

                var data = dal.ExecuteQuery("spGetOrderList", spParams).Tables[0];

                ordersList = DataRowToOrderList(data);

                return ordersList;
            }
        }

        public static List<OrderList> DataRowToOrderList(DataTable data)
        {
            List<OrderList> orders = new List<OrderList>();

            foreach (DataRow dr in data.AsEnumerable())
            {
                orders.Add(new OrderList()
                {
                    id = dr.Field<int>("id"),
                    product = new Product()
                    {
                        productID = dr.Field<int>("productID"),
                        productName = dr.Field<string>("name"),
                        productCode = dr.Field<string>("code"),
                        productPrice = dr.Field<double>("price"),
                        productUnit = dr.Field<string>("unit"),
                        productUnitValue = dr.Field<double>("unitValue")

                    },
                    quantity = dr.Field<int>("quantity")
                });
            }

            return orders;
        }
    }
}
