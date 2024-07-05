using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class ShoppingCart
    {
        public ObservableCollection<CartItem> items;

        public ShoppingCart()
        {
            items = new ObservableCollection<CartItem>();
        }

        public void AddItem(Food food, int quantity)
        {
            var existingItem = items.FirstOrDefault(i => i.Food.Food_Id == food.Food_Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                items.Add(new CartItem(food, quantity));
            }
        }

        public void RemoveItem(Food food)
        {
            var item = items.FirstOrDefault(i => i.Food.Food_Id == food.Food_Id);

            if (item != null)
            {
                items.Remove(item);
            }
        }

        public void ChangeItemQuantity(Food food, int quantity)
        {
            var item = items.FirstOrDefault(i => i.Food.Food_Id == food.Food_Id);

            if (item != null)
            {
                item.Quantity = quantity;
            }
        }

        public void ClearCart() { items.Clear(); }

        public string CartOrdered()
        {
            string itemsOF = "";
            for (int i = 0; i < items.Count; i++)
            {
                itemsOF += "\n" + (i+1) + ". " + items[i].Food.Name + "    " + items[i].Quantity;
            }
            return itemsOF;
        }

        public float RecalculateTotalPrice()
            => items.Sum(item => item.Food.Price * item.Quantity);
    }
}
