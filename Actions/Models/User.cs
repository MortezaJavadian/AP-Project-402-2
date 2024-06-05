using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Models
{
    internal class User
    {
        public static List<User> Users { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public static bool Exist_UserName(string username)
            => Users.Any(user => user.UserName == username);
    }
}
