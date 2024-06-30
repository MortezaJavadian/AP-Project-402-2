using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Backend.Models;

namespace Wpf_View.CustomerPanel
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Customer customer;
        public Home(Customer customer)
        {
            this.customer = customer;
            InitializeComponent();
            StartPannel();
            FristName.Text = customer.FirstName;
            LastName.Text = customer.LastName;
        }

        private async void StartPannel()
        {
            for (int j = 1; j <= 50; j++)
            {
                await Task.Delay(15);
                PannelBorder.Opacity += 0.02;
            }
        }

        private async void SignOut_Click(object sender, RoutedEventArgs e)
        {
            for (int j = 1; j <= 25; j++)
            {
                await Task.Delay(5);
                PannelBorder.Opacity -= 0.04;
            }
            (Application.Current.MainWindow as MainWindow).Content = new Login();
        }

        private void Click_Choices(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            if (border.Name == "Profile")
            {
                Profile.Background = Brushes.Gray;
                Reservation.Background = null;
                History.Background = null;
                Complaint.Background = null;
            }
            else if (border.Name == "Reservation")
            {
                Profile.Background = null;
                Reservation.Background = Brushes.Gray;
                History.Background = null;
                Complaint.Background = null;
            }
            else if (border.Name == "History")
            {
                Profile.Background = null;
                Reservation.Background = null;
                History.Background = Brushes.Gray;
                Complaint.Background = null;
            }
            else if (border.Name == "Complaint")
            {
                Profile.Background = null;
                Reservation.Background = null;
                History.Background = null;
                Complaint.Background = Brushes.Gray;
            }
        }
    }
}
