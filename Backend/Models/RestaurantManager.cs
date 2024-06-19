using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class RestaurantManager : User
    {
        public string NameOfRestaurant { get; set; }
        public string City {  get; set; }
        public string Address { get; set; }
        public double Score { get; set; }
        public bool Delivery { get; set; }
        public bool Dine_in { get; set; }
<<<<<<< HEAD

=======
>>>>>>> Morteza
        public List<Food> foods;

        //public int Complaint_Investigated {  get; set; }
        //public int Complaint_NOT_Investigated {  get; set; }
        
        public RestaurantManager(string username, string pass, string nameofRest, string city, string address, bool delivey, bool dine_in) : base(username, pass)
        {
            NameOfRestaurant = nameofRest;
            City = city;
            Address = address;
            Delivery = delivey;
            Dine_in = dine_in;
            foods = new List<Food>();
        }
    }
}
