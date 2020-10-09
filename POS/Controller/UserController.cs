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
    class UserController
    {
        public static User Create(User user)
        {

            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams =
                {
                        new SqlParameter("@username",user.firstname),
                        new SqlParameter("@password",user.middlename),
                        new SqlParameter("@firstname",user.firstname),
                        new SqlParameter("@middlename",user.middlename),
                        new SqlParameter("@lastname",user.lastname),
                        new SqlParameter("@phoneNumber",user.contact),
                        new SqlParameter("@address",user.address),
                        new SqlParameter("@position",user.position),
                    };


                dal.ExecuteQueryScalar("spCreateUser", spParams);

                return null;
            }
        }

        public static User Update(User user)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams =
                {
                        new SqlParameter("@id",user.id),
                        new SqlParameter("@username",user.username),
                        new SqlParameter("@password",user.password),
                        new SqlParameter("@firstname",user.firstname),
                        new SqlParameter("@middlename",user.middlename),
                        new SqlParameter("@lastname",user.lastname),
                        new SqlParameter("@phoneNumber",user.contact),
                        new SqlParameter("@address",user.address),
                        new SqlParameter("@position",user.position),
                    };


                dal.ExecuteNonQuery("spUpdateUser", spParams);

                return null;
            }
        }
        //SPECIFIC USER
        public static User Get(User user)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams =
                {
                        new SqlParameter("@id",user.id),
                        new SqlParameter("@firstname",user.firstname),
                        new SqlParameter("@middlename",user.middlename),
                        new SqlParameter("@lastname",user.lastname),
                        new SqlParameter("@contact",user.contact),
                        new SqlParameter("@address",user.address),
                    };


                var data = dal.ExecuteQuery("spDeleteUser", spParams).Tables[0];

                return DataRowToUserList(data)[0];
            }
        }

        public static void Delete(int id)
        {
            using (DAL dal = new DAL())
            {
                SqlParameter[] spParams =
                {
                    new SqlParameter("@id",id),
                };


                dal.ExecuteNonQuery("spDeleteUser", spParams);

                return;
            }
        }
        //ALL USER
        public static List<User> GetAll()
        {
            List<User> users = null; ;

            using (DAL dal = new DAL())
            {
                var data = dal.ExecuteQuery("spGetAllUser").Tables[0];

                users = DataRowToUserList(data);

                return users;
            }
        }

        public static List<User> GetAllActiveUser(List<User> users)
        {
            return users.Where(c => c.isActive == 1).Select(u => u).ToList();
        }

        public static List<User> DataRowToUserList(DataTable data)
        {
            List<User> users = new List<User>();

            foreach (DataRow dr in data.AsEnumerable())
            {
                users.Add(new User()
                {
                    id = dr.Field<int>("id"),
                    firstname = dr.Field<string>("firstname"),
                    middlename = dr.Field<string>("middlename"),
                    username = dr.Field<string>("username"),
                    password = dr.Field<string>("password"),
                    lastname = dr.Field<string>("lastname"),
                    address = dr.Field<string>("address"),
                    contact = dr.Field<string>("phoneNumber"),
                    isActive = dr.Field<int>("isActive"),
                    position = dr.Field<string>("position")
                });
            }

            return users;
        }

    }
}
