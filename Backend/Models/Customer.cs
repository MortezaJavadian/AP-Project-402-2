using Backend.NetWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Backend.Models
{
    public enum Gender { Male, Female, Unknown }
    public enum SpecialService { Normal, Bronze, Silver, Gold }

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
        public DateTime? CreatedService { get; set; }
        public ObservableCollection<Reservation > reservations { get; set; }
        public ObservableCollection<Orders > orders { get; set; }
        public ObservableCollection<Complaint> complaints { get; set; }
        public ObservableCollection<Comment> comments { get; set; }
        public ShoppingCart shoppingCart { get; set; }

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
            reservations = new ObservableCollection<Reservation>();
            orders = new ObservableCollection<Orders>();
            complaints = new ObservableCollection<Complaint>();
            comments = new ObservableCollection<Comment>();
            shoppingCart = new ShoppingCart();
            customers.Add(this);
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

        public static ObservableCollection<RestaurantManager> SearchRestaurants(string city, string restaurantName, bool? delivery = null, bool? dineIn = null, string? minAverageRating = null)
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
            if (minAverageRating != "" && minAverageRating != null)
            {
                float rate = float.Parse(minAverageRating);
                filteredRestaurants = filteredRestaurants.Where(r => r.Score >= rate).ToList();
            }
            return new ObservableCollection<RestaurantManager>(filteredRestaurants.ToList());
        }

        public static ObservableCollection<Comment> GetAllComments()
            => new ObservableCollection<Comment>(customers
                .Where(customer => customer.comments != null && customer.comments.Count > 0)
                .SelectMany(customer => customer.comments).ToList());

        public void AddComplaint(RestaurantManager restaurant, string title, string description)
        {
            var complaint = new Complaint(this, title, description, restaurant);

            complaints.Add(complaint);
            restaurant.complaints.Add(complaint);
        }

        public void GetMoneyForService(int service)
        {
            switch (service)
            {
                case 1:
                    Verification.sendAnEmail_Text(this, "You choose Bronze sercive, if you want this service you should pay 100." +
                                                                "\nWith this service you can reserve 2 times in this month");
                    break;
                case 2:
                    Verification.sendAnEmail_Text(this, "You choose Silver sercive, if you want this service you should pay 150." +
                                                                "\nWith this service you can reserve 5 times in this month");
                    break;
                case 3:
                    Verification.sendAnEmail_Text(this, "You choose Gold sercive, if you want this service you should pay 300." +
                                                                "\nWith this service you can reserve 15 times in this month");
                    break;
            }
        }

        public void ChangeSpecialService(int service)
        {
            switch (service)
            {
                case 1:
                    SpecialService = SpecialService.Bronze;
                    break;
                case 2:
                    SpecialService = SpecialService.Silver;
                    break;
                case 3:
                    SpecialService = SpecialService.Gold;
                    break;
            }
            CreatedService = DateTime.Now;
        }

        public void AddToCart(Food food, int quantity)
        {
            if (food.Available && quantity <= food.foodNum)
            {
                shoppingCart.AddItem(food, quantity);
                food.foodNum -= quantity;
            }
            //else
            //{
            //    throw new Exception("Requested quantity not available.");
            //}
        }

        public void RemoveFromCart(Food food)
        {
            shoppingCart.RemoveItem(food);
        }

        public void ChangeCartItemQuantity(Food food, int quantity)
        {
            if (quantity <= food.foodNum)
                shoppingCart.ChangeItemQuantity(food, quantity);
            else
                throw new Exception("Requested quantity not available.");
        }

        
        public Orders PlaceOrder(RestaurantManager restaurant, int i) // i == 1 & 2 => pay & deliver / i == 3 => None and Cancel
        {
            var order = new Orders(customer: this, restaurant: restaurant, items: shoppingCart.items);
            
            if(i == 1)
            {
                order.PaymentMethod = PaymentMethod.Online;
                order.Status = OrderStatus.Delivered;
            }
            else if(i == 2)
            {
                order.PaymentMethod = PaymentMethod.Cash;
                order.Status = OrderStatus.Delivered;
            }
            else if(i == 3)
            {
                order.PaymentMethod = PaymentMethod.None;
                order.Status = OrderStatus.Cancelled;
            }

            shoppingCart.ClearCart();
            return order;
        }
        public void sendEmailPay_Order(RestaurantManager restaurant)
        {
            string text = "Hello " + this.FirstName + this.LastName + "\nYou have an order from restaurant " + restaurant.NameOfRestaurant +
                "\nYour ordered food :" + shoppingCart.CartOrdered() + "\n\nThe payable amount for the order is " + shoppingCart.RecalculateTotalPrice() +
                "\nIf you are sure about your order put the verification code in program. \n\n\n";
            Verification.sendAnEmail_Text(this, text);
        }


        public static ObservableCollection<Orders> GetAllOrders()
        {
            List<Orders> allOrders = new List<Orders>();

            foreach (var custom in customers)
            {
                if (custom.orders != null && custom.orders.Count > 0)
                    allOrders.AddRange(custom.orders);
            }

            return new ObservableCollection<Orders>(allOrders.ToList());
        }

        public static ObservableCollection<Reservation> GetAllReservations()
        {
            List<Reservation> allReservations = new List<Reservation>();

            foreach (var custom in customers)
            {
                if (custom.reservations != null && custom.reservations.Count > 0)
                    allReservations.AddRange(custom.reservations);
            }

            return new ObservableCollection<Reservation>(allReservations.ToList());
        }


        public void AddComment(Food food, string content, int parentCommentId = 0)
        {
            var comment = new Comment(food, this, content, parentCommentId);
            food.foodComments.Add(comment);
            comments.Add(comment);
        }

        public void DeleteComment(Food food, int commentId)
        {
            var comment = comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment != null)
            {
                food.DeleteComment(commentId);
                comments.Remove(comment);
            }
        }

        public void EditComment(Food food, int commentId, string newContent)
        {
            var comment = comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment != null)
            {
                food.EditComment(commentId, newContent);
            }
        }
    }
}
