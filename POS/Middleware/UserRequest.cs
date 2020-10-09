using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Model;
namespace POS.Middleware
{
    class UserRequest
    {
        public static bool Validate(User user)
        {
            if (user.firstname.Length > 50 | user.firstname.Length <= 0) return false;
            if (user.middlename.Length > 50 | user.middlename.Length <= 0) return false;
            if (user.lastname.Length > 50 | user.lastname.Length <= 0) return false;
            if (user.address.Length > 50 | user.address.Length <= 0) return false;
            if (user.contact.Length > 11 | user.contact.Length <= 0) return false;
            if (user.username.Length > 50 | user.username.Length <= 0) return false;
            if (user.password.Length > 11 | user.password.Length <= 0) return false;

            return true;
        }

    }
}
