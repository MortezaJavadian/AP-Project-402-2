using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Admin : User
    {
        public static List<Complaint> complaints { get; set; }

        public int AdminId { get; set; }

        public Admin(string username, string pass) : base(username,pass) 
        {
            AdminId = User.admins.Count + 1;
            complaints = new List<Complaint>();
        }

        public void RegisterRestaurant(string name, string city, string initialUsername, string initialPassword, string address, bool delivery, bool dine_in)
        {
            RestaurantManager newRestaurant = new RestaurantManager(initialUsername, initialPassword ,name, city, address ,delivery, dine_in);
        }

        public List<Complaint> SearchComplaints(string username = null, string title = null, string firstName = null, string lastName = null, string restaurantName = null, ComplaintStatus? status = null)
        {
            var result = complaints
                .Where(c => (username == null || c.Customer.UserName == username) &&
                            (title == null || c.Title == title) &&
                            (firstName == null || c.Customer.FirstName == firstName) &&
                            (lastName == null || c.Customer.LastName == lastName) &&
                            (restaurantName == null || c.Restaurant.NameOfRestaurant == restaurantName) &&
                            (status == null || c.Status == status))
                .ToList();
            return result;
        }

        public List<Complaint> GetUnresolvedComplaints()
        {
            var result = complaints
                .Where(c => c.Status == ComplaintStatus.UnderReview)
                .ToList();
            return result;
        }

    }
}
