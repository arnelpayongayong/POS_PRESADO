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

        public static int Update(Order order)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams = {
                        new SqlParameter("@id",order.id),
                        new SqlParameter("@status",order.status),
                        new SqlParameter("@totalPrice",order.totalPrice),
                        new SqlParameter("@change",order.change),
                        new SqlParameter("@payment",order.payment),
                        new SqlParameter("@discount",order.discount)

                    };


                int id = (int)dal.ExecuteQueryScalar("spUpdateOrder", spParams);

                return id;
            }
        }

        public static int Delete(int id)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams = {
                        new SqlParameter("@id",id)
                    };


                dal.ExecuteNonQuery("spDeleteOrder", spParams);

                return id;
            }
        }

        public static List<Order> GetAll()
        {
            List<Order> orders = null; ;

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

        public static Order GetOrder(List<Order> orders,string transactionCode)
        {
            return (orders.Where(o => o.transactionCode == transactionCode).Select(o => o).SingleOrDefault());

        }
    }
}
