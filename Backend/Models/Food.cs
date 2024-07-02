using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Food 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public int foodNum {  get; set; }
        public int Price { get; set; }
        public double AverageRate { get; set; }
        public List<string> foodComments {  get; set; }

        public Food(string name, string description, bool available, int num) 
        {
            Available = available;
            Name = name;
            Description = description;
            Available = available;
            foodComments = new List<string>();
        }

    }
}
