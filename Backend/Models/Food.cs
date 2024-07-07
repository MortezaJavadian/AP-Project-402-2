using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Backend.Models
{
    public class Food 
    {
        public int Food_Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Available { get; set; }
        public int foodNum {  get; set; }
        public int Price { get; set; }
        public float AverageRate { get; set; }
        public string Category { get; set; }
        public RestaurantManager restaurant {  get; set; }
        public ObservableCollection<Comment> foodComments {  get; set; }

        public Food(RestaurantManager restaurant, string name, string? description, int num, int price, string category) 
        {
            Food_Id = GenerateUniqueId();
            Name = name;
            Description = description;
            foodNum = num;
            Available = foodNum != 0;
            Price = price;
            foodComments = new ObservableCollection<Comment>();
            AverageRate = 0;
            Category = category;
            this.restaurant = restaurant;
            restaurant.foods.Add(this);
            restaurant.categories.Add(category);
        }

        public static int GenerateUniqueId()
        {
            ObservableCollection<Food> AllFoods = RestaurantManager.GetAllFoods();
            int newId = AllFoods.Count + 1;

            // چک کردن وجود آی‌دی در لیست غذاها
            while (AllFoods.Any(f => f.Food_Id == newId))
                newId++;

            return newId;
        }

        public void DeleteComment(int commentId)
        {
            var comment = foodComments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment != null)
            {
                // حذف پاسخ‌های کامنت
                var replies = foodComments.Where(c => c.ParentCommentId == commentId).ToList();
                foreach (var reply in replies)
                {
                    foodComments.Remove(reply);
                }
                foodComments.Remove(comment);
            }
        }

        public void EditComment(int commentId, string newContent)
        {
            var comment = foodComments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment != null)
            {
                comment.Content = newContent;
                comment.IsEdited = true;
                comment.EditedAt = DateTime.Now;
            }
        }
    }
}
