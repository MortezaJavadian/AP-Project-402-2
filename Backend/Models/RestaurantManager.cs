using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class RestaurantManager : User
    {
        public int Restaurant_Id { get; set; }
        public string NameOfRestaurant { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public double Score { get; set; }
        public bool Delivery { get; set; }
        public bool Dine_in { get; set; }
        public List<Food> foods {  get; set; }
        public List<Complaint> complaints { get; set; }
        public List<Reservation> reservation { get; set; }
        public List<Orders> orders { get; set; }
        
        public RestaurantManager(string username, string pass, string nameofRest, string city, string address, bool delivey, bool dine_in) : base(username, pass)
        {
            Restaurant_Id = User.restaurantManagers.Count + 1 ;
            NameOfRestaurant = nameofRest;
            City = city;
            Address = address;
            Delivery = delivey;
            Dine_in = dine_in;
            foods = new List<Food>();
            complaints = new List<Complaint>();
            reservation = new List<Reservation>();
            orders = new List<Orders>();
        }
    }
}
