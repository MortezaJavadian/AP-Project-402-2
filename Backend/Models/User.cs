using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Backend.Models
{
    public abstract class User
    {
        public static List<User> Users { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public User(string username, string pass)
        {
            UserName = username;
            Password = pass;
        }

        public static bool Exist_UserName(string username)
            => Users.Any(user => user.UserName == username);

        public static User LoginUser(string username, string password)
        {
            foreach (var user in Users)
            {
                if (user.UserName == username)
                {
                    if (user.Password == password)
                        return user;
                    else
                        throw new Exception($"Password for user \"{username}\"");    
                }
            }

            throw new Exception($"There is no user with username \"{username}\" here");    
        }

        public static bool IsValid_UserName(string password)
        {
            string pattern = @"^(?=.*[A-Za-z].*[A-Za-z].*[A-Za-z])[A-Za-z\d]*$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
