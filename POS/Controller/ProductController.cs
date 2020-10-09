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
    class ProductController
    {
        public static void SaveProduct(Product product)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams = {
                        new SqlParameter("@code",product.productCode),
                        new SqlParameter("@name",product.productName),
                        new SqlParameter("@quantity",product.productQuantity),
                        new SqlParameter("@price",product.productPrice),
                        new SqlParameter("@unit",product.productUnit),
                        new SqlParameter("@unitValue",product.productUnitValue),
                        new SqlParameter("@expirationDate",product.productExpirationDate),
                        new SqlParameter("@minimumQuantity",product.productMinimunQuantity),
                        new SqlParameter("@categoryID",product.productCategory.id),
                        new SqlParameter("@distributorID",product.productDistributor.id)

                    };


                dal.ExecuteQueryScalar("spSaveProduct", spParams);

                return;
            }
        }

        public static List<Model.Product> ProductList()
        {
            List<Model.Product> products = null; ;

            using (DAL dal = new DAL())
            {
                var data = dal.ExecuteQuery("spGetProducts").Tables[0];

                products = new List<Model.Product>();

                foreach (DataRow dr in data.AsEnumerable())
                {


                    products.Add(new Model.Product()
                    {
                        productID = dr.Field<int>("id"),
                        productName = dr.Field<string>("name"),
                        productQuantity = dr.Field<int>("quantity"),
                        productPrice = (float)dr.Field<double>("price"),
                        productUnit = dr.Field<string>("unit").ToLower(),
                        productCode = dr.Field<string>("code")
                    });
                }

                return products;
            }
        }

        public static bool isExist(List<Product> products,string code)
        {
            int count = products.Where(p => p.productCode == code).Select(p => p).Count();

            if (count > 0)
                return true;

            return false;
        }
    }
}
