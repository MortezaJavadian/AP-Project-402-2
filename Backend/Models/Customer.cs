using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Customer : User
    {
        enum Gender
        {
            male,
            female,
        }
        public int phoneNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Email { get; set; }
        public string address { get; set; } // optional
        //public Gender gender { get; set; } // optional
        public static List<Customer> customers = new List<Customer>();  

        public Customer(string username, string pass, int phone, string firstname, string lastname, string email, string addres) : base (username, pass)
        { 
            this.phoneNumber = phone;
            this.firstName = firstname;
            this.lastName = lastname;
            this.Email = email;
            this.address = addres;
        }

        public static bool IsValidPhonenumber(string phonenumber)
        {
            string pattern = @"^09\d{9}$";
            return Regex.IsMatch(phonenumber, pattern);
        }
        
        public static bool IsValidName(string Name)
        {
            string pattern = @"^[A-Za-z]{3,32}$";
            return Regex.IsMatch(Name, pattern);
        }
        
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[A-Za-z]{3,32}@[A-Za-z]{3,32}\.[A-Za-z]{2,3}$";
            return Regex.IsMatch(email, pattern);
        } 
        
        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,32}$";
            return Regex.IsMatch(password, pattern);
        }

        public static void AddToList()
        {
            foreach (var user in User.Users)
            {
                if (user is RestaurantManager)
                    customers.Add((Customer)user);
            }
        }
    }
}
