using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Backend.Models
{
    public enum OrderStatus
    {
        Delivered, Cancelled 
    }
    public enum PaymentMethod
    {
        Cash, Online, None
    }
    public class Orders
    {

        public int Order_Id { get; set; }
        public Customer customer { get; set; }
        public RestaurantManager Restaurant { get; set; }
        public float TotalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
        public int Rating { get; set; } // Rating from 1 to 5
        public string Comment { get; set; }
        public DateTime dataTime { get; set; }
        public ObservableCollection<CartItem> CartItems { get; set; } = new ObservableCollection<CartItem>();

        public Orders(Customer customer, RestaurantManager restaurant, ObservableCollection<CartItem> items)
        {
            Order_Id = GenerateUniqueIdOrder();
            this.customer = customer;
            Restaurant = restaurant;
            dataTime = DateTime.Now;
            CartItems = items;
            TotalPrice = CalculateTotalPrice(items);
            customer.orders.Add(this);
            restaurant.orders.Add(this);
        }

        public static int GenerateUniqueIdOrder()
        {
            ObservableCollection<Orders> AllOrders = Customer.GetAllOrders();
            int newId = AllOrders.Count + 1;

            while (AllOrders.Any(f => f.Order_Id == newId))
            {
                newId++;
            }

            return newId;
        }

        public float CalculateTotalPrice(ObservableCollection<CartItem> items)
            => items.Sum(i => i.Food.Price * i.Quantity);

        public void ChangeStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }

        public void AddComment(string comment)
        {
            Comment = comment;
        }

        public void RateOrder(int rating)
        {
            if (rating < 1 || rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }
            Rating = rating;
        }
    }
}
