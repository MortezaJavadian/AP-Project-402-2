using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Net.NetworkInformation;

namespace Backend.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; } // if updated
        public bool? IsEdited { get; set; }
        public int? ParentCommentId { get; set; } // برای مدیریت پاسخ‌ها
        public Comment? ParentComment { get; set; } // مرجع به کامنت والد
        public Customer customer { get; set; }
        public Food food { get; set; }
        public ObservableCollection<Comment> Replies { get; set; } = new ObservableCollection<Comment>();

        public Comment(Food food, Customer customer, string content, int parent)
        {

            CommentId = GenerateUniqueIdComment();
            ParentComment = GetParent(parent);
            ParentCommentId = (parent == 0) ? null : parent;
            Content = content;
            IsEdited = false;
            CreatedAt = DateTime.UtcNow;
            this.customer = customer;
            this.food = food;
            food.foodComments.Add(this);
            customer.comments.Add(this);
        }


        public static int GenerateUniqueIdComment()
        {
            ObservableCollection<Comment> comments = Customer.GetAllComments();
            int newId = comments.Count + 1;

            while (comments.Any(f => f.CommentId == newId))
                newId++;
            
            return newId;
        }

        public static Comment GetParent(int id)
        {
            ObservableCollection<Comment> comments = Customer.GetAllComments();
            return comments.Where(x => x.CommentId == id).FirstOrDefault();
        }

        public void EditComment(string newContent)
        {
            Content = newContent;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
