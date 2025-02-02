﻿using System;
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

namespace Wpf_View.AdminPanel
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Admin admin;

        public Home(Admin admin)
        {
            this.admin = admin;
            InitializeComponent();
            StartPannel();
            Username.Text = admin.UserName;
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
            if (border.Name == "Sign_Up")
            {
                Sign_Up.Background = Brushes.Gray;
                Restaurants.Background = null;
                Complaints.Background = null;

                SignUpPage.Visibility = Visibility.Visible;
                RestaurantsPage.Visibility = Visibility.Collapsed;
                ComplaintsPage.Visibility = Visibility.Collapsed;
            }
            else if (border.Name == "Restaurants")
            {
                Sign_Up.Background = null;
                Restaurants.Background = Brushes.Gray;
                Complaints.Background = null;

                SignUpPage.Visibility = Visibility.Collapsed;
                RestaurantsPage.Visibility = Visibility.Visible;
                ComplaintsPage.Visibility = Visibility.Collapsed;
            }
            else if (border.Name == "Complaints")
            {
                Sign_Up.Background = null;
                Restaurants.Background = null;
                Complaints.Background = Brushes.Gray;

                SignUpPage.Visibility = Visibility.Collapsed;
                RestaurantsPage.Visibility = Visibility.Collapsed;
                ComplaintsPage.Visibility = Visibility.Visible;
            }
        }
    }
}
