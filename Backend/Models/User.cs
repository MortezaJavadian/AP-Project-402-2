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

        public static bool IsValid_UserName(string userName) // Regex for a true username type
        {
            string pattern = @"^(?=.*[A-Za-z].*[A-Za-z].*[A-Za-z])[A-Za-z\d]*$";
            if (!Regex.IsMatch(userName, pattern))
                throw new Exception("Username is not in a true format");
                //throw new Exception("The username must contain at least three letters, which can include numbers, uppercase or lowercase letters.");
            else 
                foreach(var user in Users)
                    { if (user.UserName == userName) throw new Exception("This username is used before"); }

            return true;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}
