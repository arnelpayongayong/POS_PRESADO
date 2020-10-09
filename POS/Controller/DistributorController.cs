using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Model;

namespace POS.Controller
{
    class DistributorController
    {
        public static void Create(Distributor distributor)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams =
                {
                        new SqlParameter("@name",distributor.name),
                        new SqlParameter("@address",distributor.address),
                        new SqlParameter("@contact",distributor.mobile),
                    };


                dal.ExecuteQueryScalar("spCreateDistributor", spParams);

                return;
            }

        }
        public static void Update(Distributor distributor)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams =
                {
                        new SqlParameter("@name",distributor.id),
                        new SqlParameter("@name",distributor.name),
                        new SqlParameter("@address",distributor.address),
                        new SqlParameter("@contact",distributor.mobile),
                };


                dal.ExecuteQueryScalar("spUpdateDistributor", spParams);

                return;
            }
        }
        public static void Delete(int id)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams =
                {
                        new SqlParameter("@id",id)
                };


                dal.ExecuteQueryScalar("spDeleteDistributor", spParams);

                return;
            }
        }
        public static List<Distributor> GetAll()
        {
            List<Distributor> distributors = new List<Distributor>();

            using (DAL dal = new DAL())
            {
                var data = dal.ExecuteQuery("spGetAllDistributor").Tables[0];

                distributors = DataRowToDistributorList(data);

                return distributors;
            }

        }

        public static List<Distributor> DataRowToDistributorList(DataTable data)
        {
            List<Distributor> distributors = new List<Distributor>();

            foreach (DataRow dr in data.AsEnumerable())
            {
                distributors.Add(new Distributor()
                {
                    id = dr.Field<int>("id"),
                    name= dr.Field<string>("name"),
                    address = dr.Field<string>("address"),
                    mobile = dr.Field<string>("contact"),
                    isActive = dr.Field<int>("isActive")

                });
            }

            return distributors;
        }
    }
}
