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
        public static List<RestaurantManager> restaurantManagers {  get; set; } = new List<RestaurantManager>();
        public static List<Customer> customers { get; set; } = new List<Customer>();
        public static List<Admin> admins { get; set; } = new List<Admin>();
        public string UserName { get; set; }
        public string Password { get; set; }

        public User(string username, string pass)
        {
            UserName = username;
            Password = pass;
        }

        public static bool Exist_UserName(string username)
        {
            User user = admins.Where(admin => admin.UserName == username).FirstOrDefault();
            if(user == null)
                user = restaurantManagers.Where(manager => manager.UserName == username).FirstOrDefault();
            if(user == null)
                user = customers.Where(customer => customer.UserName == username).FirstOrDefault();
            if (user == null)
                return false;
            return true;
        }
        public static User LoginUser(string username, string password)
        {
            if (username == "" || password == "")
                throw new Exception("Fields can not be empty");

            foreach (var admin in admins)
            {
                if (admin.UserName == username && admin.Password == password)
                    return admin;
            }

            foreach (var manager in restaurantManagers)
            {
                if (manager.UserName == username && manager.Password == password)
                    return manager;
            }

            foreach (var customer in customers)
            {
                if (customer.UserName == username && customer.Password == password)
                    return customer;
            }

            throw new Exception("Username or Password is not valid");
        }

        // Regex for a true username type
        public static (bool Valid, string Message) IsValid_UserName(string userName)
        {
            if (userName == "")
                return (false, "Username can not be empty");

            string pattern = @"^(?=.*[A-Za-z].*[A-Za-z].*[A-Za-z])[A-Za-z\d]*$";
            if (!Regex.IsMatch(userName, pattern))
                return (false, "Username is not in a true format");
            else
            {
                foreach (var admin in admins)
                { if (admin.UserName == userName) return (false, "Username is used as an admin"); }
                foreach (var manager in restaurantManagers)
                { if (manager.UserName == userName) return (false, "Username is used as a manager"); }
                foreach (var customer in customers)
                { if (customer.UserName == userName) return (false, "Username is used as a customer"); }
            }

            return (true, "Username is valid");
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}
