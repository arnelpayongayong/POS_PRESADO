using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    public class User
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string position { get; set; }

        public int isActive { get; set; }
    }
}
