using POS_STAFF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_STAFF.Controller
{
    class CategoryController
    {
        public static int Create(Category category)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams = {
                        new SqlParameter("@name",category.name),
                    };


                int id = (int)dal.ExecuteQueryScalar("spCreateCategory", spParams);

                return id;
            }
        }

        public static List<Category> GetAllCategory()
        {
            List<Category> categories = null; ;

            using (DAL dal = new DAL())
            {
                var data = dal.ExecuteQuery("spGetAllCategory").Tables[0];

                categories = new List<Category>();

                foreach (DataRow dr in data.AsEnumerable())
                {


                    categories.Add(new Category()
                    {
                        name = dr.Field<string>("name").ToUpper(),
                        id = dr.Field<int>("id")
                    });
                }

                return categories;
            }
        }
        public static bool isExist(List<Category> categories, string name)
        {
            if (categories.Where(c => c.name == name).Select(c => c).Count() >= 1)
                return true;

            return false;
        }
    }
}
