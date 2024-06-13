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
    public enum Gender { Male, Female, Unknown }

    public class Customer : User
    {
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
            this.gender = Gender.Unknown;
        }

        public static void IsValidPhonenumber(string phonenumber) // Regex for a true phone number type 
        {
            string pattern = @"^09\d{9}$";
            if (!Regex.IsMatch(phonenumber, pattern))
                throw new Exception("Phone Number is not in a true format");
            else
            {
                foreach (var user in User.Users)
                {
                    if (user is Customer)
                    {
                        Customer customer = user as Customer;
                        if (customer.PhoneNumber.ToString() == phonenumber) 
                            throw new Exception("This phone number is used before");
                    }
                }
            }
        }
        
        public static void IsValidName(string Name) // Regex for a true Name (fristname & lastname) type
        {
            string pattern = @"^[A-Za-z]{3,32}$";
            if (!Regex.IsMatch(Name, pattern))
                throw new Exception("Name is not in a true format");
        }
        
        public static void IsValidEmail(string email) // Regex for a true eamil type
        {
            string pattern = @"^[A-Za-z]{3,32}@[A-Za-z]{3,32}\.[A-Za-z]{2,3}$";
            if (!Regex.IsMatch(email, pattern))
                throw new Exception("Email is not in a true format");
        } 
        
        public static bool IsValidPassword(string password) // Regex for a true password of customer type
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,32}$";
            if (!Regex.IsMatch(password, pattern))
                throw new Exception("Password is not in a true format");
            return true;
        }

        public void ChangeEmail(string eamil)
        {
            IsValidEmail(eamil);
            Email = eamil;
        }
        
        public void ChangeAddress(string address)
        {
            if (address.Trim() == "" || address == null)
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
            if (gender == Gender.Unknown)
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
            foreach (var user in User.Users)
            {
                if (user is RestaurantManager)
                {
                    RestaurantManager resturan = user as RestaurantManager;
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
            }

            return search;
        }
    }
}
