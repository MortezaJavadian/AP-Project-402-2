using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } // if updated
        public bool IsDeleted { get; set; }
        public Customer customer { get; set; }
        public Food food { get; set; }
        public List<Comment> Replies { get; set; } = new List<Comment>();

        public Comment(Food food, Customer customer, string content)
        {
            CommentId = GenerateUniqueIdComment();
            Content = content;
            CreatedAt = DateTime.UtcNow;
            IsDeleted = false;
            this.customer = customer;
            this.food = food;
            food.foodComments.Add(this);
            customer.comments.Add(this);
        }


        public static int GenerateUniqueIdComment()
        {
            List<Comment> comments = Customer.GetAllComments();
            int newId = comments.Count + 1;

            while (comments.Any(f => f.CommentId == newId))
                newId++;
            
            return newId;
        }

        public void EditComment(string newContent)
        {
            Content = newContent;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DeleteComment()
        {
            IsDeleted = true;
        }
    }
}
