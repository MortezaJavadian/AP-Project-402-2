using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using Backend.Models;
using Microsoft.Data.SqlClient;

namespace Backend.DataBase
{
    public class Database
    {
        public static string connectionString = "your_connection_string_here";

        //public static ObservableCollection<Customer> GetAllCustomers()
        //{
        //    ObservableCollection<Customer> customers = new ObservableCollection<Customer>();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", conn);
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            Customer customer = new Customer(
        //                (int)reader["Customer_Id"],
        //                reader["UserName"].ToString(),
        //                reader["Password"].ToString(),
        //                reader["PhoneNumber"].ToString(),
        //                reader["FirstName"].ToString(),
        //                reader["LastName"].ToString(),
        //                reader["Email"].ToString(),
        //                reader["Address"].ToString(),
        //                (Gender)Enum.Parse(typeof(Gender), reader["Gender"].ToString()),
        //                (SpecialService)Enum.Parse(typeof(SpecialService), reader["SpecialService"].ToString()),
        //                reader["CreatedService"] != DBNull.Value ? (DateTime?)reader["CreatedService"] : null
        //                //GetReservationsByCustomerId((int)reader["Customer_Id"]),
        //                //GetOrdersByCustomerId((int)reader["Customer_Id"]),
        //                //GetComplaintsByCustomerId((int)reader["Customer_Id"]),
        //                //GetCommentsByCustomerId((int)reader["Customer_Id"])
        //            );

        //            customers.Add(customer);
        //        }
        //    }
        //    return customers;
        //}


        //public static ObservableCollection<RestaurantManager> GetAllRestaurantManagers()
        //{
        //    ObservableCollection<RestaurantManager> restaurantManagers = new ObservableCollection<RestaurantManager>();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT * FROM RestaurantManagers", conn);
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            RestaurantManager manager = new RestaurantManager(
        //                (int)reader["Restaurant_Id"],
        //                reader["UserName"].ToString(),
        //                reader["Password"].ToString(),
        //                reader["NameOfRestaurant"].ToString(),
        //                reader["City"].ToString(),
        //                reader["Address"].ToString(),
        //                (float)reader["Score"],
        //                (float)reader["ServiceScore"],
        //                (bool)reader["IsReserveService"],
        //                (bool)reader["Delivery"],
        //                (bool)reader["Dine_in"],
        //                //GetFoodsByRestaurantId((int)reader["Restaurant_Id"]),
        //                //GetComplaintsByRestaurantId((int)reader["Restaurant_Id"]),
        //                //GetReservationsByRestaurantId((int)reader["Restaurant_Id"]),
        //                //GetOrdersByRestaurantId((int)reader["Restaurant_Id"]),
        //                new ObservableCollection<string>(reader["Categories"].ToString().Split(','))
        //            );

        //            restaurantManagers.Add(manager);
        //        }
        //    }
        //    return restaurantManagers;
        //}


        //public static ObservableCollection<Admin> GetAllAdmins()
        //{
        //    ObservableCollection<Admin> admins = new ObservableCollection<Admin>();
            
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT * FROM Admins", conn);
        //        SqlDataReader reader = cmd.ExecuteReader();
            
        //        while (reader.Read())
        //        {
        //            Admin admin = new Admin(
        //                (int)reader["AdminId"],
        //                reader["UserName"].ToString(),
        //                reader["Password"].ToString()
        //                //GetComplaintsByAdminId((int)reader["AdminId"])
        //            );
            
        //            admins.Add(admin);
        //        }
        //    }
        //    return admins;
        //}


        public static void InsertCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Customers (UserName, Password, PhoneNumber, FirstName, LastName, Email, Address, Gender, SpecialService, CreatedService) " +
                    "VALUES (@UserName, @Password, @PhoneNumber, @FirstName, @LastName, @Email, @Address, @Gender, @SpecialService, @CreatedService)", conn);

                cmd.Parameters.AddWithValue("@UserName", customer.UserName);
                cmd.Parameters.AddWithValue("@Password", customer.Password);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Gender", customer.gender.ToString());
                cmd.Parameters.AddWithValue("@SpecialService", customer.SpecialService.ToString());
                cmd.Parameters.AddWithValue("@CreatedService", customer.CreatedService.HasValue ? (object)customer.CreatedService.Value : DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Customers SET UserName = @UserName, Password = @Password, PhoneNumber = @PhoneNumber, " +
                    "FirstName = @FirstName, LastName = @LastName, Email = @Email, Address = @Address, Gender = @Gender, " +
                    "SpecialService = @SpecialService, CreatedService = @CreatedService WHERE Customer_Id = @Customer_Id", conn);

                cmd.Parameters.AddWithValue("@Customer_Id", customer.Customer_Id);
                cmd.Parameters.AddWithValue("@UserName", customer.UserName);
                cmd.Parameters.AddWithValue("@Password", customer.Password);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Gender", customer.gender.ToString());
                cmd.Parameters.AddWithValue("@SpecialService", customer.SpecialService.ToString());
                cmd.Parameters.AddWithValue("@CreatedService", customer.CreatedService.HasValue ? (object)customer.CreatedService.Value : DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteCustomer(int customerId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE Customer_Id = @Customer_Id", conn);
                cmd.Parameters.AddWithValue("@Customer_Id", customerId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void InsertRestaurantManager(RestaurantManager manager)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO RestaurantManagers (UserName, Password, NameOfRestaurant, City, Address, Score, ServiceScore, IsReserveService, Delivery, Dine_in, Categories) " +
                    "VALUES (@UserName, @Password, @NameOfRestaurant, @City, @Address, @Score, @ServiceScore, @IsReserveService, @Delivery, @Dine_in, @Categories)", conn);

                cmd.Parameters.AddWithValue("@UserName", manager.UserName);
                cmd.Parameters.AddWithValue("@Password", manager.Password);
                cmd.Parameters.AddWithValue("@NameOfRestaurant", manager.NameOfRestaurant);
                cmd.Parameters.AddWithValue("@City", manager.City);
                cmd.Parameters.AddWithValue("@Address", manager.Address);
                cmd.Parameters.AddWithValue("@Score", manager.Score);
                cmd.Parameters.AddWithValue("@ServiceScore", manager.ServiceScore);
                cmd.Parameters.AddWithValue("@IsReserveService", manager.IsReserveService);
                cmd.Parameters.AddWithValue("@Delivery", manager.Delivery);
                cmd.Parameters.AddWithValue("@Dine_in", manager.Dine_in);
                cmd.Parameters.AddWithValue("@Categories", string.Join(",", manager.categories));

                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateRestaurantManager(RestaurantManager manager)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE RestaurantManagers SET UserName = @UserName, Password = @Password, NameOfRestaurant = @NameOfRestaurant, City = @City, Address = @Address, " +
                    "Score = @Score, ServiceScore = @ServiceScore, IsReserveService = @IsReserveService, Delivery = @Delivery, Dine_in = @Dine_in, Categories = @Categories WHERE Restaurant_Id = @Restaurant_Id", conn);

                cmd.Parameters.AddWithValue("@Restaurant_Id", manager.Restaurant_Id);
                cmd.Parameters.AddWithValue("@UserName", manager.UserName);
                cmd.Parameters.AddWithValue("@Password", manager.Password);
                cmd.Parameters.AddWithValue("@NameOfRestaurant", manager.NameOfRestaurant);
                cmd.Parameters.AddWithValue("@City", manager.City);
                cmd.Parameters.AddWithValue("@Address", manager.Address);
                cmd.Parameters.AddWithValue("@Score", manager.Score);
                cmd.Parameters.AddWithValue("@ServiceScore", manager.ServiceScore);
                cmd.Parameters.AddWithValue("@IsReserveService", manager.IsReserveService);
                cmd.Parameters.AddWithValue("@Delivery", manager.Delivery);
                cmd.Parameters.AddWithValue("@Dine_in", manager.Dine_in);
                cmd.Parameters.AddWithValue("@Categories", string.Join(",", manager.categories));

                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteRestaurantManager(int managerId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM RestaurantManagers WHERE Restaurant_Id = @Restaurant_Id", conn);
                cmd.Parameters.AddWithValue("@Restaurant_Id", managerId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void InsertAdmin(Admin admin)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Admins (UserName, Password) VALUES (@UserName, @Password)", conn);

                cmd.Parameters.AddWithValue("@UserName", admin.UserName);
                cmd.Parameters.AddWithValue("@Password", admin.Password);

                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateAdmin(Admin admin)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Admins SET UserName = @UserName, Password = @Password WHERE AdminId = @AdminId", conn);

                cmd.Parameters.AddWithValue("@AdminId", admin.AdminId);
                cmd.Parameters.AddWithValue("@UserName", admin.UserName);
                cmd.Parameters.AddWithValue("@Password", admin.Password);

                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteAdmin(int adminId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Admins WHERE AdminId = @AdminId", conn);
                cmd.Parameters.AddWithValue("@AdminId", adminId);
                cmd.ExecuteNonQuery();
            }
        }

        // Helper methods for retrieving related data
        //public static ObservableCollection<Reservation> GetReservationsByCustomerId(int customerId)
        //{
        //    ObservableCollection<Reservation> reservations = new ObservableCollection<Reservation>();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT * FROM Reservations WHERE Customer_Id = @CustomerId", conn);
        //        cmd.Parameters.AddWithValue("@CustomerId", customerId);
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            Reservation reservation = new Reservation
        //            {
        //                ReservationId = (int)reader["ReservationId"],
        //                ReservationTime = (DateTime)reader["ReservationTime"],
        //                Rating = (int)reader["Rating"],
        //                IsCanceled = (bool)reader["IsCanceled"],
        //                CreatedAt = (DateTime)reader["CreatedAt"],
        //                TotalPrice = (float)reader["TotalPrice"]
        //            };
        //            reservations.Add(reservation);
        //        }
        //    }
        //    return reservations;
        //}


        //public static ObservableCollection<Orders> GetOrdersByCustomerId(int customerId)
        //{
        //    ObservableCollection<Orders> orders = new ObservableCollection<Orders>();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT * FROM Orders WHERE Customer_Id = @CustomerId", conn);
        //        cmd.Parameters.AddWithValue("@CustomerId", customerId);
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            Orders order = new Orders(
        //                orderId: (int)reader["Order_Id"],
        //                totalPrice: (float)reader["TotalPrice"],
        //                paymentMethod: (PaymentMethod)Enum.Parse(typeof(PaymentMethod), reader["PaymentMethod"].ToString()),
        //                status: (OrderStatus)Enum.Parse(typeof(OrderStatus), reader["Status"].ToString()),
        //                rating: (int)reader["Rating"],
        //                comment: reader["Comment"].ToString(),
        //                dataTime: (DateTime)reader["dataTime"]
        //            );
        //            orders.Add(order);
        //        }
        //    }
        //    return orders;
        //}

        //public static Customer GetCustomerById(int customerId)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE Customer_Id = @CustomerId", conn);
        //        cmd.Parameters.AddWithValue("@CustomerId", customerId);
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            int id = (int)reader["Customer_Id"];
        //            string userName = reader["UserName"].ToString();
        //            string password = reader["Password"].ToString();
        //            string phoneNumber = reader["PhoneNumber"].ToString();
        //            string firstName = reader["FirstName"].ToString();
        //            string lastName = reader["LastName"].ToString();
        //            string email = reader["Email"].ToString();
        //            string address = reader["Address"].ToString();
        //            Gender gender = (Gender)Enum.Parse(typeof(Gender), reader["Gender"].ToString());
        //            SpecialService specialService = (SpecialService)Enum.Parse(typeof(SpecialService), reader["SpecialService"].ToString());
        //            DateTime? createdService = reader["CreatedService"] != DBNull.Value ? (DateTime?)reader["CreatedService"] : null;

        //            ObservableCollection<Orders> orders = GetOrdersByCustomerId(id);
        //            ObservableCollection<Complaint> complaints = GetComplaintsByCustomerId(id);
        //            ObservableCollection<Comment> comments = GetCommentsByCustomerId(id);

        //            return new Customer(
        //                customerId: id,
        //                userName: userName,
        //                password: password,
        //                phoneNumber: phoneNumber,
        //                firstName: firstName,
        //                lastName: lastName,
        //                email: email,
        //                address: address,
        //                gender: gender,
        //                specialService: specialService,
        //                createdService: createdService,
        //                orders: orders,
        //                complaints: complaints,
        //                comments: comments
        //            );
        //        }
        //    }
        //    return null;
        //}

        //public static RestaurantManager GetRestaurantManagerById(int restaurantId)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT * FROM RestaurantManagers WHERE Restaurant_Id = @RestaurantId", conn);
        //        cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            int id = (int)reader["Restaurant_Id"];
        //            string userName = reader["UserName"].ToString();
        //            string password = reader["Password"].ToString();
        //            string nameOfRestaurant = reader["NameOfRestaurant"].ToString();
        //            string city = reader["City"].ToString();
        //            string address = reader["Address"].ToString();
        //            float score = (float)reader["Score"];
        //            float serviceScore = (float)reader["ServiceScore"];
        //            bool isReserveService = (bool)reader["IsReserveService"];
        //            bool delivery = (bool)reader["Delivery"];
        //            bool dineIn = (bool)reader["Dine_in"];
        //            ObservableCollection<Food> foods = GetFoodsByRestaurantId(id);
        //            ObservableCollection<Complaint> complaints = GetComplaintsByRestaurantId(id);
        //            ObservableCollection<Orders> orders = GetOrdersByRestaurantId(id);
        //            ObservableCollection<string> categories = new ObservableCollection<string>(reader["Categories"].ToString().Split(','));

        //            return new RestaurantManager(
        //                restaurantId: id,
        //                userName: userName,
        //                password: password,
        //                nameOfRestaurant: nameOfRestaurant,
        //                city: city,
        //                address: address,
        //                score: score,
        //                serviceScore: serviceScore,
        //                isReserveService: isReserveService,
        //                delivery: delivery,
        //                dineIn: dineIn,
        //                foods: foods,
        //                complaints: complaints,
        //                orders: orders,
        //                categories: categories
        //            );
        //        }
        //    }
        //    return null;
        //}

    //    public static ObservableCollection<Complaint> GetComplaintsByCustomerId(int customerId)
    //    {
    //        ObservableCollection<Complaint> complaints = new ObservableCollection<Complaint>();

    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            conn.Open();
    //            SqlCommand cmd = new SqlCommand("SELECT * FROM Complaints WHERE Customer_Id = @CustomerId", conn);
    //            cmd.Parameters.AddWithValue("@CustomerId", customerId);
    //            SqlDataReader reader = cmd.ExecuteReader();

    //            while (reader.Read())
    //            {
    //                Complaint complaint = new Complaint
    //                {
    //                    ComplaintId = (int)reader["ComplaintId"],
    //                    Title = reader["Title"].ToString(),
    //                    Description = reader["Description"].ToString(),
    //                    Status = (ComplaintStatus)Enum.Parse(typeof(ComplaintStatus), reader["Status"].ToString()),
    //                    Response = reader["Response"].ToString(),
    //                    CreateAt = (DateTime)reader["CreateAt"],
    //                    ResponseAt = reader["ResponseAt"] != DBNull.Value ? (DateTime?)reader["ResponseAt"] : null
    //                };
    //                complaints.Add(complaint);
    //            }
    //        }
    //        return complaints;
    //    }

    //    public static ObservableCollection<Comment> GetCommentsByCustomerId(int customerId)
    //    {
    //        ObservableCollection<Comment> comments = new ObservableCollection<Comment>();

    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            conn.Open();
    //            SqlCommand cmd = new SqlCommand("SELECT * FROM Comments WHERE Customer_Id = @CustomerId", conn);
    //            cmd.Parameters.AddWithValue("@CustomerId", customerId);
    //            SqlDataReader reader = cmd.ExecuteReader();

    //            while (reader.Read())
    //            {
    //                Comment comment = new Comment
    //                {
    //                    CommentId = (int)reader["CommentId"],
    //                    Content = reader["Content"].ToString(),
    //                    CreatedAt = (DateTime)reader["CreatedAt"],
    //                    EditedAt = reader["EditedAt"] != DBNull.Value ? (DateTime?)reader["EditedAt"] : null,
    //                    IsEdited = reader["IsEdited"] != DBNull.Value ? (bool?)reader["IsEdited"] : null,
    //                    ParentCommentId = reader["ParentCommentId"] != DBNull.Value ? (int?)reader["ParentCommentId"] : null
    //                };
    //                comments.Add(comment);
    //            }
    //        }
    //        return comments;
    //    }

    //    public static ObservableCollection<Food> GetFoodsByRestaurantId(int restaurantId)
    //    {
    //        ObservableCollection<Food> foods = new ObservableCollection<Food>();

    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            conn.Open();
    //            SqlCommand cmd = new SqlCommand("SELECT * FROM Food WHERE Restaurant_Id = @RestaurantId", conn);
    //            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
    //            SqlDataReader reader = cmd.ExecuteReader();

    //            while (reader.Read())
    //            {
    //                Food food = new Food
    //                {
    //                    Food_Id = (int)reader["Food_Id"],
    //                    Name = reader["Name"].ToString(),
    //                    Description = reader["Description"].ToString(),
    //                    Available = (bool)reader["Available"],
    //                    foodNum = (int)reader["foodNum"],
    //                    Price = (float)reader["Price"]
    //                };
    //                foods.Add(food);
    //            }
    //        }
    //        return foods;
    //    }

    //    public static ObservableCollection<Complaint> GetComplaintsByRestaurantId(int restaurantId)
    //    {
    //        ObservableCollection<Complaint> complaints = new ObservableCollection<Complaint>();

    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            conn.Open();
    //            SqlCommand cmd = new SqlCommand("SELECT * FROM Complaints WHERE Restaurant_Id = @RestaurantId", conn);
    //            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
    //            SqlDataReader reader = cmd.ExecuteReader();

    //            while (reader.Read())
    //            {
    //                Complaint complaint = new Complaint
    //                {
    //                    ComplaintId = (int)reader["ComplaintId"],
    //                    Title = reader["Title"].ToString(),
    //                    Description = reader["Description"].ToString(),
    //                    Status = (ComplaintStatus)Enum.Parse(typeof(ComplaintStatus), reader["Status"].ToString()),
    //                    Response = reader["Response"].ToString(),
    //                    CreateAt = (DateTime)reader["CreateAt"],
    //                    ResponseAt = reader["ResponseAt"] != DBNull.Value ? (DateTime?)reader["ResponseAt"] : null
    //                };
    //                complaints.Add(complaint);
    //            }
    //        }
    //        return complaints;
    //    }

    //    public static ObservableCollection<Reservation> GetReservationsByRestaurantId(int restaurantId)
    //    {
    //        ObservableCollection<Reservation> reservations = new ObservableCollection<Reservation>();

    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            conn.Open();
    //            SqlCommand cmd = new SqlCommand("SELECT * FROM Reservations WHERE Restaurant_Id = @RestaurantId", conn);
    //            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
    //            SqlDataReader reader = cmd.ExecuteReader();

    //            while (reader.Read())
    //            {
    //                Reservation reservation = new Reservation
    //                {
    //                    ReservationId = (int)reader["ReservationId"],
    //                    ReservationTime = (DateTime)reader["ReservationTime"],
    //                    Rating = (int)reader["Rating"],
    //                    IsCanceled = (bool)reader["IsCanceled"],
    //                    CreatedAt = (DateTime)reader["CreatedAt"],
    //                    TotalPrice = (float)reader["TotalPrice"]
    //                };
    //                reservations.Add(reservation);
    //            }
    //        }
    //        return reservations;
    //    }

    //    public static ObservableCollection<Orders> GetOrdersByRestaurantId(int restaurantId)
    //    {
    //        ObservableCollection<Orders> orders = new ObservableCollection<Orders>();

    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            conn.Open();
    //            SqlCommand cmd = new SqlCommand("SELECT * FROM Orders WHERE Restaurant_Id = @RestaurantId", conn);
    //            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
    //            SqlDataReader reader = cmd.ExecuteReader();

    //            while (reader.Read())
    //            {
    //                Orders order = new Orders
    //                {
    //                    Order_Id = (int)reader["Order_Id"],
    //                    TotalPrice = (float)reader["TotalPrice"],
    //                    PaymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), reader["PaymentMethod"].ToString()),
    //                    Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), reader["Status"].ToString()),
    //                    Rating = (int)reader["Rating"],
    //                    Comment = reader["Comment"].ToString(),
    //                    dataTime = (DateTime)reader["dataTime"]
    //                };
    //                orders.Add(order);
    //            }
    //        }
    //        return orders;
    //    }

    //    public static ObservableCollection<Complaint> GetComplaintsByAdminId(int adminId)
    //    {
    //        ObservableCollection<Complaint> complaints = new ObservableCollection<Complaint>();

    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            conn.Open();
    //            SqlCommand cmd = new SqlCommand("SELECT * FROM Complaints WHERE AdminId = @AdminId", conn);
    //            cmd.Parameters.AddWithValue("@AdminId", adminId);
    //            SqlDataReader reader = cmd.ExecuteReader();

    //            while (reader.Read())
    //            {
    //                Complaint complaint = new Complaint
    //                {
    //                    ComplaintId = (int)reader["ComplaintId"],
    //                    Title = reader["Title"].ToString(),
    //                    Description = reader["Description"].ToString(),
    //                    Status = (ComplaintStatus)Enum.Parse(typeof(ComplaintStatus), reader["Status"].ToString()),
    //                    Response = reader["Response"].ToString(),
    //                    CreateAt = (DateTime)reader["CreateAt"],
    //                    ResponseAt = reader["ResponseAt"] != DBNull.Value ? (DateTime?)reader["ResponseAt"] : null
    //                };
    //                complaints.Add(complaint);
    //            }
    //        }
    //        return complaints;
    //    }
    }
}
