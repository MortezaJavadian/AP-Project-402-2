﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
            customer.complaints.Add(this);
            restaurant.complaints.Add(this);
            Admin.Complaints.Add(this);
        }

        public Complaint( int complaintId, Customer customer, RestaurantManager restaurant, Admin admin, string title,
                        string description, ComplaintStatus status, string response, DateTime createAt, DateTime? responseAt)
        {
            ComplaintId = complaintId;
            Customer = customer;
            Restaurant = restaurant;
            Admin = admin;
            Title = title;
            Description = description;
            Status = status;
            Response = response;
            CreateAt = createAt;
            ResponseAt = responseAt;
        }

        public static int GenerateUniqueIdComplaint()
        {
            int newId = Admin.Complaints.Count + 1;

            while (Admin.Complaints.Any(f => f.ComplaintId == newId))
                newId++;
            
            return newId;
        }
    }
}
