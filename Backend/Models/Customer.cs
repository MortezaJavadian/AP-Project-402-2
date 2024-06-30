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
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; } // optional
        public Gender gender { get; set; } // optional

        public Customer(string username, string pass, string phone, string firstname, string lastname, string email) : base (username, pass)
        { 
            this.PhoneNumber = phone;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = email;
            this.Address = "";
            this.gender = Gender.Unknown;
        }

        // Regex for a true phone number type
        public static (bool Valid, string Message) IsValidPhonenumber(string phonenumber) 
        {
            if (phonenumber == "")
                return (false, "Phone Number can not be empty");

            string pattern = @"^09\d{9}$";
            if (!Regex.IsMatch(phonenumber, pattern))
                return (false, "Phone Number is not in a true format");
            else
            {
                foreach (var user in User.Users)
                {
                    if (user is Customer)
                    {
                        Customer customer = user as Customer;
                        if (customer.PhoneNumber == phonenumber) 
                            return (false, "Phone Number is used before");
                    }
                }
            }

            return (true, "Phone Number is valid");
        }

        // Regex for a true Name (fristname & lastname) type
        public static (bool Valid, string Message) IsValidName(string Name)
        {
            if (Name == "")
                return (false, "name can not be empty");

            string pattern = @"^[A-Za-z]{3,32}$";
            if (!Regex.IsMatch(Name, pattern))
                return (false, "name is not in a true format");

            return (true, "name is valid");
        }

        // Regex for a true eamil type
        public static (bool Valid, string Message) IsValidEmail(string email)
        {
            string pattern = @"^[A-Za-z0-9.]{3,32}@[A-Za-z]{3,32}\.[A-Za-z]{2,3}$";
            if (!Regex.IsMatch(email, pattern))
                return (false, "Email is not in a true format");

            return (true, "Email is valid");
        }

        // Regex for a true password of customer type
        public static (bool Valid, string Message) IsValidPassword(string password)
        {
            if (password == "")
                return (false, "Password can not be empty");

            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,32}$";
            if (!Regex.IsMatch(password, pattern))
                return (false, "Password is not in a true format");

            return (true, "Password is valid");
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
            Info += "Name: " + FirstName + " " + LastName + "\nPhone: " + PhoneNumber +
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
