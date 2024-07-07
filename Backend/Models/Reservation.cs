using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Backend.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public Customer Customer { get; set; }
        public RestaurantManager Restaurant { get; set; }
        public DateTime ReservationTime { get; set; }
        public int Rating { get; set; } // Rating from 1 to 5
        public bool IsCanceled { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public float TotalPrice { get; set; }
        public ObservableCollection<CartItem> CartItems { get; set; } = new ObservableCollection<CartItem>();

        public Reservation(Customer customer, RestaurantManager restaurant, DateTime reservationTime, ObservableCollection<CartItem> items)
        {
            if (!restaurant.Dine_in)
                throw new Exception("Restaurant does not offer dine-in service");

            if (restaurant.Score < 4.5)
                throw new Exception("Restaurant does not meet the rating criteria for reservations");

            if (!restaurant.IsReserveService)
                throw new Exception("Restaurant cancel its reservation service");

            //if (!CanMakeReservation(customer, restaurant, reservationTime))
            //    throw new Exception("Reservation criteria not met");

            ReservationId = GenerateUniqueIdReserve();
            Customer = customer;
            Restaurant = restaurant;
            ReservationTime = reservationTime;
            IsCanceled = false;
            CreatedAt = DateTime.Now;
            CartItems = items;
            TotalPrice = CalculateTotalPrice(items);
            customer.reservations.Add(this);
            restaurant.reservation.Add(this);
        }

        public static int GenerateUniqueIdReserve()
        {
            ObservableCollection<Reservation> reservations = Customer.GetAllReservations();
            int newId = reservations.Count + 1;

            while (reservations.Any(f => f.ReservationId == newId))
            {
                newId++;
            }

            return newId;
        }

        public float CalculateTotalPrice(ObservableCollection<CartItem> items)
        {
            float total = 0;
            foreach (var item in items)
            {
                total += item.Food.Price;
            }
            return total;
        }
    }
}
