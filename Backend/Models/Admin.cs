using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Backend.Models
{
    public class Admin : User
    {
        public static ObservableCollection<Complaint> complaints { get; set; }
        public int AdminId { get; set; }

        public Admin(string username, string pass) : base(username,pass) 
        {
            AdminId = User.admins.Count + 1;
            complaints = new ObservableCollection<Complaint>();
            admins.Add(this);
        }

        public static ObservableCollection<RestaurantManager> SearchRestaurants(string cityName, string restaurantName, string minRate, string unresolvedComplaints)
        {
            // Start with the full list of restaurant managers
            var filteredRestaurants = User.restaurantManagers.AsQueryable();

            // Filter by city name
            if (!string.IsNullOrEmpty(cityName))
            {
                filteredRestaurants = filteredRestaurants.Where(r => r.City.StartsWith(cityName));
            }

            // Filter by restaurant name
            if (!string.IsNullOrEmpty(restaurantName))
            {
                filteredRestaurants = filteredRestaurants.Where(r => r.NameOfRestaurant.StartsWith(restaurantName));
            }

            // Filter by minimum rating
            if (minRate != "" && minRate != null)
            {
                float minRating = float.TryParse(minRate, out float parsedRating) ? parsedRating : 0;
                filteredRestaurants = filteredRestaurants.Where(r => r.Score >= minRating);
            }

            // Filter by unresolved complaints
            if (unresolvedComplaints !=  "")
            {
                if(Enum.Parse<ComplaintStatus>(unresolvedComplaints) == ComplaintStatus.UnderReview)
                    filteredRestaurants = filteredRestaurants.Where(r => r.complaints.Any(c => c.Status == ComplaintStatus.UnderReview));
            }

            return new ObservableCollection<RestaurantManager>(filteredRestaurants.ToList());
        }

        public static ObservableCollection<Complaint> SearchComplaints(string username , string title, string firstName, string lastName, string restaurantName, string? status)
        {
            var result = new ObservableCollection<Complaint>(complaints
                .Where(c => (username == "" || c.Customer.UserName == username) &&
                            (title == "" || c.Title == title) &&
                            (firstName == "" || c.Customer.FirstName == firstName) &&
                            (lastName == "" || c.Customer.LastName == lastName) &&
                            (restaurantName == "" || c.Restaurant.NameOfRestaurant == restaurantName) &&
                            (status == "" || c.Status == Enum.Parse<ComplaintStatus>(status)))
                .ToList());
            return result;
        }

        public ObservableCollection<Complaint> GetUnresolvedComplaints()
        {
            var result = new ObservableCollection<Complaint>(complaints
                .Where(c => c.Status == ComplaintStatus.UnderReview)
                .ToList());
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
