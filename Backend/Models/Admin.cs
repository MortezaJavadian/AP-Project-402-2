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
            admins.Add(this);
        }

        public void RegisterRestaurant(string initialUsername, string initialPassword)
        {
            RestaurantManager newRestaurant = new RestaurantManager(initialUsername, initialPassword);
        }

        public List<RestaurantManager> SearchRestaurants(string cityName, string restaurantName, string minRate, bool unresolvedComplaints)
        {
            List<RestaurantManager> filteredRestaurants = new List<RestaurantManager>();

            double minRating = double.Parse(minRate);
            foreach (var restaurant in User.restaurantManagers)
            {
                bool match = true;

                // Filter by city name
                if (!string.IsNullOrEmpty(cityName) && !restaurant.City.Equals(cityName, StringComparison.OrdinalIgnoreCase))
                {
                    match = false;
                }

                // Filter by restaurant name
                if (!string.IsNullOrEmpty(restaurantName) && !restaurant.NameOfRestaurant.Contains(restaurantName, StringComparison.OrdinalIgnoreCase))
                    match = false;


                // Filter by minimum rating
                if (restaurant.Score < minRating)
                    match = false;


                // Filter by unresolved complaints
                if (unresolvedComplaints)
                {
                    bool hasUnresolvedComplaints = restaurant.complaints.Any(c => c.Status == ComplaintStatus.UnderReview);
                    if (!hasUnresolvedComplaints)
                        match = false;
                }

                if (match)
                    filteredRestaurants.Add(restaurant);
            }

            return filteredRestaurants;
        }

        public List<Complaint> SearchComplaints(string username , string title, string firstName, string lastName, string restaurantName, ComplaintStatus? status)
        {
            var result = complaints
                .Where(c => (username == "" || c.Customer.UserName == username) &&
                            (title == "" || c.Title == title) &&
                            (firstName == "" || c.Customer.FirstName == firstName) &&
                            (lastName == "" || c.Customer.LastName == lastName) &&
                            (restaurantName == "" || c.Restaurant.NameOfRestaurant == restaurantName) &&
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

        public void RespondToUnresolvedComplaints(string index , string response) // here first show the complaint with the last method and with index of that we can response to it 
        {
            var unresolvedComplaints = complaints.Where(c => c.Status == ComplaintStatus.UnderReview).ToList();
        
            int selectedIndex = int.Parse(index) - 1;
        
            var selectedComplaint = unresolvedComplaints[selectedIndex];
            selectedComplaint.Response = response;
            selectedComplaint.Status = ComplaintStatus.Reviewed;
        }
    }
}
