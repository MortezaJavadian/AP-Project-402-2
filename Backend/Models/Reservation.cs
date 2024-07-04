using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Reservation(int reservationId, Customer customer, RestaurantManager restaurant, DateTime reservationTime)
        {
            if (!restaurant.Dine_in)
                throw new Exception("Restaurant does not offer dine-in service");

            if (restaurant.Score < 4.5)
                throw new Exception("Restaurant does not meet the rating criteria for reservations");

            if (!CanMakeReservation(customer, restaurant, reservationTime))
                throw new Exception("Reservation criteria not met");

            ReservationId = reservationId;
            Customer = customer;
            Restaurant = restaurant;
            ReservationTime = reservationTime;
            IsCanceled = false;
            CreatedAt = DateTime.Now;
        }

        public void CancelReservation()
        {
            if (IsCanceled)
                throw new Exception("Reservation is already canceled");
            if (DateTime.Now > ReservationTime)
                throw new Exception("Cannot cancel a past reservation");

            TimeSpan timeBeforeReservation = ReservationTime - DateTime.Now;
            double penaltyPercentage = 0;

            switch (Customer.SpecialService)
            {
                case SpecialService.Bronze:
                    penaltyPercentage = timeBeforeReservation.TotalMinutes <= 30 ? 1.0 : 0.3;
                    break;
                case SpecialService.Silver:
                    penaltyPercentage = timeBeforeReservation.TotalMinutes <= 30 ? 1.0 : 0.3;
                    break;
                case SpecialService.Gold:
                    penaltyPercentage = timeBeforeReservation.TotalMinutes <= 15 ? 1.0 : 0.3;
                    break;
            }

            decimal reservationCost = GetReservationCost(Customer.SpecialService);
            decimal penaltyAmount = reservationCost * (decimal)penaltyPercentage;

            if (penaltyPercentage == 1.0)
                throw new Exception("Reservation cannot be canceled, full penalty applies");

            IsCanceled = true;
            // Log penalty amount and process refund if needed
        }

        private decimal GetReservationCost(SpecialService serviceType)
        {
            return serviceType switch
            {
                SpecialService.Bronze => 100,
                SpecialService.Silver => 150,
                SpecialService.Gold => 300,
                _ => 0
            };
        }

        public static bool CanMakeReservation(Customer customer, RestaurantManager restaurant, DateTime reservationTime)
        {
            if (!restaurant.Dine_in)
                return false;

            TimeSpan timeBeforeReservation = reservationTime - DateTime.Now;

            switch (customer.SpecialService)
            {
                case SpecialService.Bronze:
                    if (timeBeforeReservation.TotalMinutes > 60)
                        return false;
                    break;
                case SpecialService.Silver:
                    if (timeBeforeReservation.TotalMinutes > 90)
                        return false;
                    break;
                case SpecialService.Gold:
                    if (timeBeforeReservation.TotalMinutes > 180)
                        return false;
                    break;
                case SpecialService.Normal:
                    return false; // Normal service don't have reserve
            }

            int maxReservations = customer.SpecialService switch
            {
                SpecialService.Bronze => 2,
                SpecialService.Silver => 5,
                SpecialService.Gold => 15,
                _ => 0
            };

            int currentMonth = DateTime.Now.Month;

            int reservationCount = User.customers
                .SelectMany(c => c.reservations)
                .Count(r => r.Customer.UserName == customer.UserName && r.CreatedAt.Month == currentMonth);

            if (reservationCount >= maxReservations)
                return false;

            return true;
        }

    }
}
