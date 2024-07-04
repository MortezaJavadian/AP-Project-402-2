using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string? Response { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ResponseAt { get; set; }

        public Complaint(Customer customer, string title, string description, RestaurantManager restaurant)
        {
            ComplaintId = GenerateUniqueIdComplaint();
            Customer = customer;
            Title = title;
            Description = description;
            Restaurant = restaurant;
            Status = ComplaintStatus.UnderReview;
            CreateAt = DateTime.Now;
            Response = null;
        }


        public static int GenerateUniqueIdComplaint()
        {
            int newId = Admin.complaints.Count + 1;

            while (Admin.complaints.Any(f => f.ComplaintId == newId))
                newId++;
            
            return newId;
        }
    }
}
