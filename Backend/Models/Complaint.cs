using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public enum ComplaintStatus
    {
        UnderReview, Reviewed
    }
    public class Complaint
    {
        public int ComplaintId { get; set; }
        public Customer Customer { get; set; }
        public RestaurantManager Restaurant { get; set; }
        public Admin Admin { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ComplaintStatus Status { get; set; }
        public string Response { get; set; }

        public Complaint(Customer customer, string title, string description, RestaurantManager restaurant)
        {
            ComplaintId = Admin.complaints.Count + 1;
            Customer = customer;
            Title = title;
            Description = description;
            Restaurant = restaurant;
            Status = ComplaintStatus.UnderReview;
            Response = null;
        }
    }
}
