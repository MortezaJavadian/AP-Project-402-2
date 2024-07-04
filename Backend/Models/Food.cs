using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Food 
    {
        public int Food_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public int foodNum {  get; set; }
        public int Price { get; set; }
        public double AverageRate { get; set; }
        public List<string> foodComments {  get; set; }
        public RestaurantManager restaurant {  get; set; }

        public Food(string name, string description, bool available, int num, int price) 
        {
            Food_Id = find_ID();
            Available = available;
            Name = name;
            Description = description;
            Available = available;
            Price = price;
            foodComments = new List<string>();
        }

        public static int find_ID()
        {
            int count = 0;
            foreach (var rest in User.restaurantManagers)
            {
                count += rest.foods.Count;
            }
            return (count+1);
        }
    }
}
