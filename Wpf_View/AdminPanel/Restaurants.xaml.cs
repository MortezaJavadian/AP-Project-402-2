using Backend.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Wpf_View.AdminPanel
{
    /// <summary>
    /// Interaction logic for Restaurants.xaml
    /// </summary>
    public partial class Restaurants : UserControl
    {
        public Restaurants()
        {
            InitializeComponent();

            RestaurantsList.DataContext = User.restaurantManagers;
        }

        private void Score_input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (Score.Text.Length != 0 || new Regex("[^0-5.-]+").IsMatch(e.Text));
        }

        RestaurantManager RestaurantSelect;
        private async void listView_Click(object sender, RoutedEventArgs e)
        {
            RestaurantSelect = (sender as ListView).SelectedItem as RestaurantManager;

            if (RestaurantSelect != null)
            {
                Panel.SetZIndex(ChangePasswordPage, 1);
                for (int i = 1; i <= 25; i++)
                {
                    await Task.Delay(1);
                    ChangePasswordPage.Opacity += 0.04;
                }
            }
        }

        private void Set_Fillter(object sender, EventArgs e)
        {
            RestaurantsList.DataContext = Admin.SearchRestaurants(City.Text, Name.Text, Score.Text,
                                                                  ComplaintStatus.Text);
        }

        bool ClearPassword = true;
        bool ClearConfirmPassword = true;
        private void Clear_HintText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (ClearPassword && textBox.Name == "Password")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                ClearPassword = false;
                PasswordError.Text = "Password can not be empty";
                PasswordBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
            }
            else if (ClearConfirmPassword && textBox.Name == "ConfirmPassword")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                ClearConfirmPassword = false;
                ConfirmPasswordError.Text = "Confirm Password can not be empty";
                ConfirmPasswordBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
            }
        }

        private void Set_HintText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                if (textBox.Name == "Password")
                {
                    textBox.Foreground = Brushes.DimGray;
                    textBox.Text = "Password";
                    ClearPassword = true;
                    PasswordError.Text = "";
                    PasswordBorder.BorderBrush = null;
                }
                else if (textBox.Name == "ConfirmPassword")
                {
                    textBox.Foreground = Brushes.DimGray;
                    textBox.Text = "Confirm Password";
                    ClearConfirmPassword = true;
                    ConfirmPasswordError.Text = "";
                    ConfirmPasswordBorder.BorderBrush = null;
                }
            }
        }

        bool ValidPassword = false;
        bool ValidCofirmPassword = false;
        private void Clear_Error(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Name == "Password")
            {
                (ValidPassword, string Message) = Customer.IsValidPassword(textBox.Text);
                PasswordBorder.BorderBrush = (ValidPassword) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                PasswordError.Foreground = (ValidPassword) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));

                PasswordError.Text = Message;
            }
            else if (textBox.Name == "ConfirmPassword")
            {
                (ValidCofirmPassword, string Message) = (textBox.Text == Password.Text) ?
                                      (true, "Password match") : (false, "Password doesn't match");
                ConfirmPasswordBorder.BorderBrush = (ValidCofirmPassword) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                ConfirmPasswordError.Foreground = (ValidCofirmPassword) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));

                ConfirmPasswordError.Text = Message;
            }

            if (ValidPassword && ValidCofirmPassword)
            {
                ChangePassword.IsEnabled = true;
                ChangePassword.Opacity = 1;
            }
            else
            {
                ChangePassword.IsEnabled = false;
                ChangePassword.Opacity = 0.4;
            }
        }

        private async void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Password.Foreground = Brushes.DimGray;
            Password.Text = "Password";
            ClearPassword = true;
            PasswordError.Text = "";
            PasswordBorder.BorderBrush = null;

            ConfirmPassword.Foreground = Brushes.DimGray;
            ConfirmPassword.Text = "Confirm Password";
            ClearConfirmPassword = true;
            ConfirmPasswordError.Text = "";
            ConfirmPasswordBorder.BorderBrush = null;

            Panel.SetZIndex(ChangePasswordPage, -1);
            for (int i = 1; i <= 25; i++)
            {
                await Task.Delay(1);
                ChangePasswordPage.Opacity -= 0.04;
            }

            RestaurantSelect = null;
        }

        private async void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            RestaurantSelect.Password = Password.Text;

            Password.Foreground = Brushes.DimGray;
            Password.Text = "Password";
            ClearPassword = true;
            PasswordError.Text = "";
            PasswordBorder.BorderBrush = null;

            ConfirmPassword.Foreground = Brushes.DimGray;
            ConfirmPassword.Text = "Confirm Password";
            ClearConfirmPassword = true;
            ConfirmPasswordError.Text = "";
            ConfirmPasswordBorder.BorderBrush = null;

            Panel.SetZIndex(ChangePasswordPage, -1);
            for (int i = 1; i <= 25; i++)
            {
                await Task.Delay(1);
                ChangePasswordPage.Opacity -= 0.04;
            }

            RestaurantSelect = null;
        }
    }

    public class BooleanToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? "Yes" : "No";
            }
            return "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
