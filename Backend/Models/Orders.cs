using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public enum OrderStatus
    {
        Delivered, Cancelled
    }
    public enum PaymentMethod
    {
        Cash, Online
    }
    public class Orders
    {

        public int Order_Id { get; set; }
        public Customer customer { get; set; }
        public RestaurantManager Restaurant { get; set; }
        public double TotalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
        public int Rating { get; set; } // Rating from 1 to 5
        public string Comment { get; set; }
        public DateTime dataTime { get; set; }
        public List<Food> Items { get; set; }

        public Orders(int order_Id, Customer customer, RestaurantManager restaurant, List<Food> items)
        {
            Order_Id = order_Id;
            this.customer = customer;
            Restaurant = restaurant;
            dataTime = DateTime.Now;
            Items = items;
            TotalPrice = CalculateTotalPrice(items);
        }

        public double CalculateTotalPrice(List<Food> items)
        {
            double total = 0;
            foreach (var item in items)
            {
                total += item.Price;
            }
            return total;
        }

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
