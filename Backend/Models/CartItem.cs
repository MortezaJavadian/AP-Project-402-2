using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Backend.Models
{
    public class CartItem
    {
        public Food Food { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }

        public CartItem(Food food, int quantity) 
        {
            Food = food;
            Quantity = quantity;
            TotalPrice = food.Price * Quantity;
        }
    }

}
