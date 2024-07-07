using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Backend.Models;
using Wpf_View.CustomerPanel;

namespace Wpf_View.RestaurantManagerPanel
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public RestaurantManager restaurantManager;

        public  Home(RestaurantManager restaurantManager)
        {
            this.restaurantManager = restaurantManager;
            InitializeComponent();
            StartPannel();
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
            if (border.Name == "Menu")
            {
                Menu.Background = Brushes.Gray;
                History.Background = null;

                MenuPage.Visibility = Visibility.Visible;
                HistoryPage.Visibility = Visibility.Collapsed;
            }
            else if (border.Name == "History")
            {
                Menu.Background = null;
                History.Background = Brushes.Gray;

                MenuPage.Visibility = Visibility.Collapsed;
                HistoryPage.Visibility = Visibility.Visible;
            }
        }

        public void SetHistoryList(ListView OrdersList, ListView ReservationsList)
        {
            OrdersList.DataContext = restaurantManager.orders;
            ReservationsList.DataContext = restaurantManager.reservation;
        }
    }
}
