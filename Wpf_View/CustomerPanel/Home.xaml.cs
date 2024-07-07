using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Backend.Models;
using Backend.NetWork;

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
            FirstName.Text = customer.FirstName;
            LastName.Text = customer.LastName;

            ProfilePage.Firstname.Text = customer.FirstName;
            ProfilePage.Lastname.Text = customer.LastName;
            ProfilePage.Username.Text = customer.UserName;
            ProfilePage.PhoneNumber.Text = customer.PhoneNumber;
            ProfilePage.Email.Text = customer.Email;
            ProfilePage.Gender.Text = customer.gender.ToString();
            ProfilePage.Address.Text = customer.Address;
            ProfilePage.SpecialService.Text = customer.SpecialService.ToString();
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

                ProfilePage.Visibility = Visibility.Visible;
                RestaurantsPage.Visibility = Visibility.Collapsed;
                HistoryPage.Visibility = Visibility.Collapsed;
                ComplaintPage.Visibility = Visibility.Collapsed;
            }
            else if (border.Name == "Reservation")
            {
                Profile.Background = null;
                Reservation.Background = Brushes.Gray;
                History.Background = null;
                Complaint.Background = null;

                ProfilePage.Visibility = Visibility.Collapsed;
                RestaurantsPage.Visibility = Visibility.Visible;
                HistoryPage.Visibility = Visibility.Collapsed;
                ComplaintPage.Visibility = Visibility.Collapsed;
            }
            else if (border.Name == "History")
            {
                Profile.Background = null;
                Reservation.Background = null;
                History.Background = Brushes.Gray;
                Complaint.Background = null;

                ProfilePage.Visibility = Visibility.Collapsed;
                RestaurantsPage.Visibility = Visibility.Collapsed;
                HistoryPage.Visibility = Visibility.Visible;
                ComplaintPage.Visibility = Visibility.Collapsed;
            }
            else if (border.Name == "Complaint")
            {
                Profile.Background = null;
                Reservation.Background = null;
                History.Background = null;
                Complaint.Background = Brushes.Gray;

                ProfilePage.Visibility = Visibility.Collapsed;
                RestaurantsPage.Visibility = Visibility.Collapsed;
                HistoryPage.Visibility = Visibility.Collapsed;
                ComplaintPage.Visibility = Visibility.Visible;
            }
        }

        public static void Gender_Changed()
            => ((Application.Current.MainWindow as MainWindow).Content as Home).customer.gender =
                Enum.Parse<Gender>(
                    ((Application.Current.MainWindow as MainWindow).Content as Home).ProfilePage.Gender.Text);

        public static void Address_Changed()
            => ((Application.Current.MainWindow as MainWindow).Content as Home).customer.Address =
                ((Application.Current.MainWindow as MainWindow).Content as Home).ProfilePage.Address.Text;

        public async static void BuySpecialService()
        {
            Home HomePage = ((Application.Current.MainWindow as MainWindow).Content as Home);

            if (Enum.Parse<SpecialService>(HomePage.ProfilePage.SpecialService.Text) <=
                HomePage.customer.SpecialService)
            {
                HomePage.ProfilePage.SpecialService.Text = HomePage.customer.SpecialService.ToString();
                MessageBox.Show("Your service is allready above of this service");
            }
            else
            {
                try
                {
                    HomePage.customer.GetMoneyForService((int)Enum.Parse<SpecialService>(HomePage.ProfilePage.SpecialService.Text));

                    Panel.SetZIndex(HomePage.ProfilePage.VerifyPage, 1);

                    for (int i = 1; i <= 25; i++)
                    {
                        await Task.Delay(1);
                        HomePage.ProfilePage.VerifyPage.Opacity += 0.04;
                    }

                    HomePage.ProfilePage.Box1.Focus();
                }
                catch (Exception ex)
                {
                    HomePage.ProfilePage.SpecialService.Text = HomePage.customer.SpecialService.ToString();

                    (HomePage.ProfilePage.InternetError.Child as TextBlock).Text = ex.Message;
                    for (int i = 1; i <= 25; i++)
                    {
                        await Task.Delay(10);
                        HomePage.ProfilePage.InternetError.Width += 9.1;
                    }

                    await Task.Delay(6000);

                    for (int i = 1; i <= 25; i++)
                    {
                        await Task.Delay(10);
                        HomePage.ProfilePage.InternetError.Width -= 9.1;
                    }
                }
            }
        }
    }
}
