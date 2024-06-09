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

namespace Wpf_View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
            StartLoginPage();
        }

        public async void StartLoginPage()
        {
            for (int j = 1; j <= 50; j++)
            {
                await Task.Delay(15);
                LoginPage.Height += 7.5;
                LoginPage.Width += 6.5;
                LoginPage.CornerRadius = new CornerRadius(100 - 1.5 * j);
            }
        }

        bool AllowedUsername = true;
        bool AllowedPassword = true;
        private void Clear_UsernameOrPassword(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (AllowedUsername && textBox.Text == "Username")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                AllowedUsername = false;
            }
            else if (AllowedPassword && textBox.Text == "Password")
            {
                textBox.Text = "";
                textBox.Visibility = Visibility.Collapsed;
                LoginPasswordBox.Visibility = Visibility.Visible;
                LoginPasswordBox.Foreground = Brushes.Black;
                LoginPasswordBox.Focus();
                AllowedPassword = false;
            }
        }

        private async void SignUpCustomer_Click(object sender, RoutedEventArgs e)
        {
            for (int j = 1; j <= 25; j++)
            {
                await Task.Delay(1);
                LoginPage.Height -= 15;
                LoginPage.Width -= 13;
                LoginPage.CornerRadius = new CornerRadius(25 + 3 * j);
            }
            (Application.Current.MainWindow as MainWindow).Content = new SignUp();
        }
    }
}
