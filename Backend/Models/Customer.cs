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

    public enum SpecialService { Normal, Bronze , Silver , Gold }

    public class Customer : User
    {
        public int Customer_Id { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; } // optional
        public Gender gender { get; set; } // optional
        public SpecialService SpecialService { get; set; }
        public List <Reservation > reservations { get; set; }
        public List <Orders > orders { get; set; }
        public List <Complaint> complaints { get; set; }

        public Customer(string username, string pass, string phone, string firstname, string lastname, string email) : base (username, pass)
        {
            this.Customer_Id = User.customers.Count + 1 ;
            this.PhoneNumber = phone;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = email;
            this.Address = "";
            this.gender = Gender.Unknown;
            this.SpecialService = SpecialService.Normal;
            reservations = new List<Reservation>();
            orders = new List<Orders>();
            complaints = new List<Complaint>();
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
                foreach (var user in User.customers)
                {
                    if (user is Customer)
                    {
                        Customer customer = user as Customer;
                        if (customer.PhoneNumber.ToString() == phonenumber) 
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

        public static List<RestaurantManager> SearchRestaurants(string city, string restaurantName, bool? delivery = null, bool? dineIn = null, double? minAverageRating = null)
        {
            List<RestaurantManager> filteredRestaurants = new List<RestaurantManager>();

            // Filter by city
            if (city != "")
                filteredRestaurants = restaurantManagers.Where(r => r.City.StartsWith(city)).ToList();
            else
                filteredRestaurants = restaurantManagers.ToList(); 
            
            // Filter by restaurant name
            if (restaurantName != "")
                filteredRestaurants = filteredRestaurants.Where(r => r.NameOfRestaurant.StartsWith(restaurantName)).ToList();
            
            // Filter by delivery or dine in
            if (delivery != null)
                filteredRestaurants = filteredRestaurants.Where(r => r.Delivery == delivery.Value).ToList();

            if (dineIn != null)
                filteredRestaurants = filteredRestaurants.Where(r => r.Dine_in == dineIn.Value).ToList();
            
            // Filter by average score
            if (minAverageRating != null)
                filteredRestaurants = filteredRestaurants.Where(r => r.Score >= minAverageRating.Value).ToList();

            return filteredRestaurants;
        }
    }
}
