using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;

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
        public bool IsReserveService { get; set; }
        public bool Delivery { get; set; }
        public bool Dine_in { get; set; }
        public ObservableCollection<Food> foods { get; set; }
        public ObservableCollection<Complaint> complaints { get; set; }
        public ObservableCollection<Reservation> reservation { get; set; }
        public ObservableCollection<Orders> orders { get; set; }
        public ObservableCollection<string> categories { get; set; } = new ObservableCollection<string>();

        public RestaurantManager(string username, string pass, string nameofRest, string city, string address, bool delivey, bool dine_in) : base(username, pass)
        {
            Restaurant_Id = User.restaurantManagers.Count + 1;
            NameOfRestaurant = nameofRest;
            City = city;
            Address = address;
            Delivery = delivey;
            Dine_in = dine_in;
            ServiceScore = 0;
            IsReserveService = false;
            foods = new ObservableCollection<Food>();
            complaints = new ObservableCollection<Complaint>();
            reservation = new ObservableCollection<Reservation>();
            orders = new ObservableCollection<Orders>();

            User.restaurantManagers.Add(this);
        }

        public static ObservableCollection<Food> GetAllFoods()
        {
            var allFoodsList = restaurantManagers
                .Where(restaurant => restaurant.foods != null && restaurant.foods.Count > 0)
                .SelectMany(restaurant => restaurant.foods)
                .ToList();

            return new ObservableCollection<Food>(allFoodsList);
        }

        public ObservableCollection<Food> GetMenuByCategory() // For Customer Panel that want to see Menu with Categories
        {
            // Create an ObservableCollection from the ordered list
            return new ObservableCollection<Food>(
                           foods.OrderBy(food => string.IsNullOrEmpty(food.Category))
                                .ThenBy(food => food.Category)
                                .ToList()
            );
        }// این متد اول او
         // نهایی که دسته بندی دارند رو در لیست میچینه و سپس اونهایی که دسته بندی ندارند

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

            if (totalCount > 0)
                averageRating = totalRatingSum / totalCount;
            Score = averageRating;
        }

        public void AddFood(Food food)
        {
            foods.Add(food);
        }

        public void RemoveFood(int foodId)
        {
            var foodToRemove = foods.FirstOrDefault(f => f.Food_Id == foodId);
            if (foodToRemove != null)
            {
                foods.Remove(foodToRemove);
            }
        }

        public void UpdateFood(Food updatedFood)
        {
            Food foodToUpdate = foods.FirstOrDefault(f => f.Food_Id == updatedFood.Food_Id);
            if (foodToUpdate != null)
            {
                foodToUpdate.Name = updatedFood.Name;
                foodToUpdate.Description = updatedFood.Description;
                foodToUpdate.Available = updatedFood.Available;
                foodToUpdate.foodNum = updatedFood.foodNum;
                foodToUpdate.Price = updatedFood.Price;
                foodToUpdate.AverageRate = updatedFood.AverageRate;
                foodToUpdate.foodComments = updatedFood.foodComments;
                foodToUpdate.restaurant = updatedFood.restaurant;
                foodToUpdate.Category = updatedFood.Category;
            }
        }

        public void ReplyToComment(int foodId, int commentId, Comment reply)
        {
            var food = foods.FirstOrDefault(f => f.Food_Id == foodId);
            if (food != null)
            {
                var comment = food.foodComments.FirstOrDefault(c => c.CommentId == commentId);
                if (comment != null)
                    comment.Replies.Add(reply);
            }
        }

        public void AddCategory(string category)
        {
            if (!categories.Contains(category))
                categories.Add(category);
        }

        public void RemoveCategory(string category)
        {
            if (categories.Contains(category))
            {
                categories.Remove(category);
                var foodsInCategory = foods.Where(f => f.Category == category).ToList();
                foreach (var food in foodsInCategory)
                    food.Category = "";
            }
        }

        // افزودن دسته‌بندی غذا
        public void AddCategoryToFood(int foodId, string category)
        {
            var food = foods.FirstOrDefault(f => f.Food_Id == foodId);
            if (food != null)
                food.Category = category;
        }

        public void ChangeFoodCategory(int foodId, string newCategory)
        {
            var food = foods.FirstOrDefault(f => f.Food_Id == foodId);
            if (food != null)
            {
                food.Category = newCategory;
            }
        }

        public void UpdateFoodQuantity(string foodId, string newQuantity)
        {
            int foodID = int.Parse(foodId);
            int quantity = int.Parse(newQuantity);

            if (quantity < 0)
                throw new Exception("Just positive integer");

            Food food = foods.FirstOrDefault(f => f.Food_Id == foodID);
            if (food != null)
            {
                food.foodNum = quantity;
                if (quantity > 0)
                    food.Available = true;
                else food.Available = false;
            }
            else
                throw new ArgumentException("Food item not found");
        }

        public void changeReserveService(bool reserve)
        {
            if (ServiceScore < 4.5)
            {
                if (reserve)
                    throw new Exception("We can't active restaurant reservation (Score under 4.5)");
            }
            IsReserveService = reserve;
        }

        public ObservableCollection<Orders> FilterOrders(string customerUserName, string customerPhoneNumber, string foodName, string minPrice, string maxPrice, DateTime? startDate = null, DateTime? endDate = null)
        {
            var filteredOrders = orders.AsQueryable();

            if (customerUserName != "")
                filteredOrders = filteredOrders.Where(order => order.customer.UserName.StartsWith(customerUserName));

            if (customerPhoneNumber != "")
                filteredOrders = filteredOrders.Where(order => order.customer.PhoneNumber.StartsWith(customerPhoneNumber));

            if (foodName != "")
                filteredOrders = filteredOrders.Where(order => order.CartItems.Any(x => x.Food.Name.StartsWith(foodName)));

            if (minPrice != "")
            {
                float price = float.Parse(minPrice);
                filteredOrders = filteredOrders.Where(order => order.TotalPrice >= price);
            }

            if (maxPrice != "")
            {
                float price = float.Parse(maxPrice);
                filteredOrders = filteredOrders.Where(order => order.TotalPrice <= price);
            }

            if (startDate != null)
                filteredOrders = filteredOrders.Where(order => order.dataTime >= startDate.Value);

            if (endDate != null)
                filteredOrders = filteredOrders.Where(order => order.dataTime <= endDate.Value);

            return new ObservableCollection<Orders>(filteredOrders.ToList());
        }

        public ObservableCollection<Reservation> FilterReservations(string customerUserName, string customerPhoneNumber, string foodName, string minPrice, string maxPrice, DateTime? startDate = null, DateTime? endDate = null)
        {
            var filteredReservations = reservation.AsQueryable();

            if (customerUserName != "")
                filteredReservations = filteredReservations.Where(reservation => reservation.Customer.UserName.StartsWith(customerUserName));

            if (customerPhoneNumber != "")
                filteredReservations = filteredReservations.Where(reservation => reservation.Customer.PhoneNumber.StartsWith(customerPhoneNumber));

            if (foodName != "")
                filteredReservations = filteredReservations.Where(reservation => reservation.CartItems.Any(x => x.Food.Name.StartsWith(foodName)));

            if (minPrice != "")
            {
                float price = float.Parse(minPrice);
                filteredReservations = filteredReservations.Where(reservation => reservation.TotalPrice >= price);
            }

            if (maxPrice != "")
            {
                float price = float.Parse(maxPrice);
                filteredReservations = filteredReservations.Where(reservation => reservation.TotalPrice <= price);
            }

            if (startDate != null)
                filteredReservations = filteredReservations.Where(reservation => reservation.CreatedAt >= startDate.Value);
            

            if (endDate != null)
                filteredReservations = filteredReservations.Where(reservation => reservation.CreatedAt <= endDate.Value);

            return new ObservableCollection<Reservation>(filteredReservations.ToList());
        }


        // Get orders to CSV
        public static void ExportFilteredOrdersToCsv(ObservableCollection<Orders> filteredOrders, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(filteredOrders);
            }
        }

        // Get reservations to CSV
        public static void ExportFilteredReservationsToCsv(ObservableCollection<Reservation> filteredReservations, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(filteredReservations);
            }
        }
    }
}
