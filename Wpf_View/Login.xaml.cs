using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
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

        bool ClearUsername = true;
        private void Clear_HintText_Username(object sender, RoutedEventArgs e)
        {
            if (ClearUsername)
            {
                TextBox textBox = sender as TextBox;
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                ClearUsername = false;
            }
        }

        private void Set_HintText_Username(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                textBox.Foreground = Brushes.DimGray;
                textBox.Text = "Username";
                ClearUsername = true;
            }
        }

        bool ClearPassword = true;
        private void Clear_HintText_Password(object sender, RoutedEventArgs e)
        {
            if (ClearPassword && ShowPassword.IsChecked == true)
            {
                LoginTextBoxPassword.Text = "";
                LoginTextBoxPassword.Foreground = Brushes.Black;
                ClearPassword = false;
            }
            else if (ClearPassword)
            {
                LoginTextBoxPassword.Text = "";
                LoginTextBoxPassword.Visibility = Visibility.Hidden;
                LoginPasswordBox.Visibility = Visibility.Visible;
                LoginPasswordBox.Focus();
                ClearPassword = false;
            }
        }

        private void Set_HintText_Password(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox && LoginTextBoxPassword.Text == "" &&
                LoginTextBoxPassword.Visibility == Visibility.Visible)
            {
                LoginTextBoxPassword.Foreground = Brushes.DimGray;
                LoginTextBoxPassword.Text = "Password";
                ClearPassword = true;
            }
            else if (sender is PasswordBox && LoginPasswordBox.Password == "" &&
                     LoginPasswordBox.Visibility == Visibility.Visible)
            {
                LoginTextBoxPassword.Foreground = Brushes.DimGray;
                LoginTextBoxPassword.Text = "Password";
                LoginTextBoxPassword.Visibility = Visibility.Visible;
                LoginPasswordBox.Visibility = Visibility.Hidden;
                ClearPassword = true;
            }
        }

        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (!ClearPassword)
            {
                if (LoginPasswordBox.Visibility == Visibility.Visible)
                {
                    LoginPasswordBox.Visibility = Visibility.Hidden;
                    LoginTextBoxPassword.Visibility = Visibility.Visible;
                    LoginTextBoxPassword.Text = LoginPasswordBox.Password;
                    LoginTextBoxPassword.Focus();
                    LoginTextBoxPassword.CaretIndex = LoginTextBoxPassword.Text.Length;
                }
                else
                {
                    LoginPasswordBox.Visibility = Visibility.Visible;
                    LoginTextBoxPassword.Visibility = Visibility.Hidden;
                    LoginPasswordBox.Password = LoginTextBoxPassword.Text;
                    LoginPasswordBox.GetType()
                                .GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic)
                                .Invoke(LoginPasswordBox, new object[] { LoginPasswordBox.Password.Length, 0 });
                    LoginPasswordBox.Focus();
                }
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

    public class ChangeRadioButton : RadioButton
    {
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (IsChecked == false)
            {
                IsChecked = true;
                base.OnClick();
            }
            else
            {
                base.OnClick();
                IsChecked = false;
            }
        }
    }
}
