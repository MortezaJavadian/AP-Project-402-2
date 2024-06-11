using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Admin : User
    {
        public static List<Admin> admins = new List<Admin>();  
        public Admin(string username, string pass) : base(username,pass) { }

        public static void AddToList()
        {
            foreach (var user in User.Users)
            {
                if (user is RestaurantManager)
                    admins.Add((Admin)user);
            }
        }

        //public static IEnumerable<RestaurantManager> SearchBy_City(string name)
        //{
            
        //}
    }

    
}
