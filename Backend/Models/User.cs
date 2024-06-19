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
        public static List<User> Users = new List<User>();
<<<<<<< HEAD

=======
>>>>>>> Morteza
        public string UserName { get; set; }
        public string Password { get; set; }

        public User(string username, string pass)
        {
            UserName = username;
            Password = pass;

            Users.Add(this);
        }

        public static bool Exist_UserName(string username)
            => Users.Any(user => user.UserName == username);

        public static User LoginUser(string username, string password)
        {
            if (username == "" || password == "")
                throw new Exception("Fields can not be empty");

            foreach (var user in Users)
            {
                if (user.UserName == username && user.Password == password)
                    return user;
            }

            throw new Exception("Username or Password is not valid");
        }

<<<<<<< HEAD
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
=======
        // Regex for a true username type
        public static (bool Valid, string Message) IsValid_UserName(string userName)
        {
            if (userName == "")
                return (false, "Username can not be empty");

            string pattern = @"^(?=.*[A-Za-z].*[A-Za-z].*[A-Za-z])[A-Za-z\d]*$";
            if (!Regex.IsMatch(userName, pattern))
                return (false, "Username is not in a true format");
            else 
                foreach(var user in Users)
                    { if (user.UserName == userName) return (false, "Username is used before"); }

            return (true, "Username is valid");
>>>>>>> Morteza
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}
