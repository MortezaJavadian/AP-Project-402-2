using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
        public float Score { get; set; }
        public float ServiceScore { get; set; }
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
            ServiceScore = 0;
            foods = new List<Food>();
            complaints = new List<Complaint>();
            reservation = new List<Reservation>();
            orders = new List<Orders>();
        }

        public RestaurantManager(string username , string pass) : base(username, pass) 
        {
            Restaurant_Id = User.restaurantManagers.Count + 1;
            foods = new List<Food>();
            complaints = new List<Complaint>();
            reservation = new List<Reservation>();
            orders = new List<Orders>();
            User.restaurantManagers.Add(this);
        }


        public static List<Food> GetAllFoods()
        {
            List<Food> allFoods = new List<Food>();

            foreach (var restaurant in restaurantManagers)
            {
                if (restaurant.foods != null && restaurant.foods.Count > 0)
                {
                    allFoods.AddRange(restaurant.foods);
                }
            }

            return allFoods;
        }


        public IEnumerable<IGrouping<string, Food>> GetMenuByCategory()
            => foods.GroupBy(food => GetCategoryString(food.Category));

        public string GetCategoryString(Category category)
        {
            switch (category)
            {
                case Category.Main:
                    return "Main";
                case Category.Dessert:
                    return "Dessert";
                case Category.Drink:
                    return "Drink";
                default:
                    throw new ArgumentException("Unknown category");
            }
        }

        public void CalculateAllAvergeRating()
        {
            foreach (var restaurant in restaurantManagers)
            {
                restaurant.CalculateAverageRatingUsingList();
            }
        }
        public void CalculateAverageRatingUsingList()
        {
            float averageRating = 0;
            float totalRatingSum = 0;
            int totalCount = 0;

            // Sum of order ratings
            foreach (var order in orders)
            {
                    totalRatingSum += order.Rating;
                    totalCount++;
            }

            // Sum of reservation ratings
            foreach (var reservation in reservation)
            {
                    totalRatingSum += reservation.Rating;
                    totalCount++;
            }

            if (totalCount > 0)
                averageRating = totalRatingSum / totalCount;
            ServiceScore = averageRating;

            // Sum of food ratings
            foreach (var food in foods)
            {
                    totalRatingSum += food.AverageRate;
                    totalCount++;
            }


            // Calculate average rating
            if (totalCount > 0)
                averageRating = totalRatingSum / totalCount;
            Score = averageRating;
        }
    }
}
