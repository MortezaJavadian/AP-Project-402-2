using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public enum Category { Main , Dessert , Drink }
    public class Food 
    {
        public int Food_Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Available { get; set; }
        public int foodNum {  get; set; }
        public int Price { get; set; }
        public float AverageRate { get; set; }
        public Category Category { get; set; }
        public List<Comment> foodComments {  get; set; }
        public RestaurantManager restaurant {  get; set; }

        public Food(string name, string? description, bool available, int num, int price) 
        {
            Food_Id = GenerateUniqueId();
            Available = available;
            Name = name;
            Description = description;
            Available = available;
            Price = price;
            foodComments = new List<Comment>();
            AverageRate = 0;
        }

        public static int GenerateUniqueId()
        {
            List<Food> AllFoods = RestaurantManager.GetAllFoods();
            int newId = AllFoods.Count + 1;

            // چک کردن وجود آی‌دی در لیست غذاها
            while (AllFoods.Any(f => f.Food_Id == newId))
            {
                newId++;
            }

            return newId;
        }

    }
}
