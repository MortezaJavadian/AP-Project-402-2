using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backend.Models
{
    public enum Gender { Male, Female }

    public class Customer : User
    {
        public static List<Customer> customers = new List<Customer>();
        public int PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; } // optional
        public Gender gender { get; set; } // optional

        public Customer(string username, string pass, int phone, string firstname, string lastname, string email, string addres) : base (username, pass)
        { 
            this.PhoneNumber = phone;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = email;
            this.Address = addres;
        }

        public static bool IsValidPhonenumber(string phonenumber) // Regex for a true phone number type 
        {
            string pattern = @"^09\d{9}$";
            if (!Regex.IsMatch(phonenumber, pattern))
                throw new Exception("Phone number is not in a true format");
                //throw new Exception("Must be exactly 11 digits and start with 09");
            else
                foreach (var customer in customers)
                    { if (customer.PhoneNumber.ToString() == phonenumber) throw new Exception("This phone number is used before"); }

            return true;
        }
        
        public static bool IsValidName(string Name) // Regex for a true Name (fristname & lastname) type
        {
            string pattern = @"^[A-Za-z]{3,32}$";
            if (!Regex.IsMatch(Name, pattern))
                throw new Exception("Name is not in a true format");
                //throw new Exception("Name must be at least 3 letters and at most 32 letters and only consist of letters and no characters or numbers.");
            return true;
        }
        
        public static bool IsValidEmail(string email) // Regex for a true eamil type
        {
            string pattern = @"^[A-Za-z]{3,32}@[A-Za-z]{3,32}\.[A-Za-z]{2,3}$";
            if (!Regex.IsMatch(email, pattern))
                throw new Exception("Email is not in a true format");
                //throw new Exception("The email must be in the format A@B.C, where A, B can be at least 3 letters and at most 32 letters, and C can be at least 2 letters and at most 3 letters.");
            return true;
        } 
        
        public static bool IsValidPassword(string password) // Regex for a true password of customer type
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,32}$";
            if (!Regex.IsMatch(password, pattern))
                throw new Exception("Password is not in a true format");
                //throw new Exception("The password must be at least 8 and at most 32 characters. It should also contain at least one uppercase letter, one lowercase letter and one number.");
            return true;
        }

        public static void AddToList()
        {
            foreach (var user in User.Users)
            {
                if (user is RestaurantManager)
                    customers.Add((Customer)user);
            }
        }

        public void ChangeEmail(string eamil)
        {
            if(IsValidEmail(eamil))
                Email = eamil;
        }
        
        public void ChangeAddress(string address)
        {
            if (address.Trim() == "" || FirstName == null)
                throw new Exception("Address could not be Empty");
            Address = address.Trim();
        }

        public string Profile_Info()
        {
            string Info = "";
            Info += "Name: " + FirstName + " " + LastName + "\nPhone: " + PhoneNumber.ToString() +
                "\nUser name: " + UserName + "\nEmail" + Email;
            if (Address != null)
                Info += "\nAdress: " + Address;
            if (gender == null)
                return Info;

            if (gender == Gender.Female)
                Info += "\nGender: " + Gender.Female;
            else
                Info += "\nGender: " + Gender.Male;
            return Info;
        }


        public string withoutFilter_search()
        {
            string search = "";
            foreach(var resturan in RestaurantManager.restaurants)
            {
                search += "Name: " + resturan.NameOfRestaurant + "\nAddress: " + resturan.City + " " + resturan.Address;
                if (resturan.Delivery && resturan.Dine_in)
                    search += "\nDistribution: Delivery and Dine_in";
                else if (resturan.Delivery)
                    search += "\nDistribution: Delivery";
                else if (resturan.Dine_in)
                    search += "\nDistribution: Dine_in";
                else
                    search += "There is no distribution consist for restaurant";
                search += "\nScore of restaurant: " + resturan.Score.ToString() + "/5\n\n";
            }

            return search;
        }

    }
}
