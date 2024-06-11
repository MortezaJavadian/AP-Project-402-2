using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Food 
    {
        public static List<string> foodComments {  get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public int foodNum {  get; set; }

        public Food(string name, string description, bool available, int num) 
        {
            foodComments = new List<string>();
            Available = available;
            Name = name;
            Description = description;
            Available = available;
        }

    }
}
