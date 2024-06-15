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

namespace Wpf_View
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public SignUp()
        {
            InitializeComponent();
            StartSignUpPage();
        }

        public async void StartSignUpPage()
        {
            for (int j = 1; j <= 50; j++)
            {
                await Task.Delay(15);
                SignUpPage.Height += 8;
            }
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            for (int j = 1; j <= 25; j++)
            {
                await Task.Delay(1);
                SignUpPage.Height -= 16;
            }
            (Application.Current.MainWindow as MainWindow).Content = new Login();
        }
        
        bool ClearFirstname = true;
        bool ClearLastname = true;
        bool ClearUsername = true;
        bool ClearPhoneNumber = true;
        bool ClearEmail = true;
        private void Clear_HintText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (ClearFirstname && textBox.Name == "Firstname")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                ClearFirstname = false;
                FirstnameError.Text = "Firstname can not be empty";
                FirstnameBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
            }
            else if (ClearLastname && textBox.Name == "Lastname")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                ClearLastname = false;
                LastnameError.Text = "Lastname can not be empty";
                LastnameBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
            }
            else if (ClearUsername && textBox.Name == "Username")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                ClearUsername = false;
                UsernameError.Text = "Username can not be empty";
                UsernameBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
            }
            else if (ClearPhoneNumber && textBox.Name == "PhoneNumber")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                ClearPhoneNumber = false;
                PhoneNumberError.Text = "Phone Number can not be empty";
                PhoneNumberBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
            }
            else if (ClearEmail && textBox.Name == "Email")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                ClearEmail = false;
                EmailError.Text = "Email can not be empty";
                EmailBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
            }
        }

        private void Set_HintText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                if (textBox.Name == "Firstname")
                {
                    textBox.Foreground = Brushes.DimGray;
                    textBox.Text = "Firstname";
                    ClearFirstname = true;
                    FirstnameError.Text = "";
                    FirstnameBorder.BorderBrush = null;
                }
                else if (textBox.Name == "Lastname")
                {
                    textBox.Foreground = Brushes.DimGray;
                    textBox.Text = "Lastname";
                    ClearLastname = true;
                    LastnameError.Text = "";
                    LastnameBorder.BorderBrush = null;
                }
                else if (textBox.Name == "Username")
                {
                    textBox.Foreground = Brushes.DimGray;
                    textBox.Text = "Username";
                    ClearUsername = true;
                    UsernameError.Text = "";
                    UsernameBorder.BorderBrush = null;
                }
                else if (textBox.Name == "PhoneNumber")
                {
                    textBox.Foreground = Brushes.DimGray;
                    textBox.Text = "Phone Number";
                    ClearPhoneNumber = true;
                    PhoneNumberError.Text = "";
                    PhoneNumberBorder.BorderBrush = null;
                }
                else if (textBox.Name == "Email")
                {
                    textBox.Foreground = Brushes.DimGray;
                    textBox.Text = "Email";
                    ClearEmail = true;
                    EmailError.Text = "";
                    EmailBorder.BorderBrush = null;
                }
            }
        }

        private void Clear_Error(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Name == "Firstname")
            {
                (bool Valid, string Message) = Customer.IsValidName(textBox.Text);
                FirstnameBorder.BorderBrush = (Valid) ? 
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                FirstnameError.Foreground = (Valid) ? 
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));

                FirstnameError.Text = "First" + Message;
            }
            else if (textBox.Name == "Lastname")
            {
                (bool Valid, string Message) = Customer.IsValidName(textBox.Text);
                LastnameBorder.BorderBrush = (Valid) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                LastnameError.Foreground = (Valid) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));

                LastnameError.Text = "Last" + Message;
            }
            else if (textBox.Name == "Username")
            {
                (bool Valid, string Message) = User.IsValid_UserName(textBox.Text);
                UsernameBorder.BorderBrush = (Valid) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                UsernameError.Foreground = (Valid) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));

                UsernameError.Text = Message;
            }
            else if (textBox.Name == "PhoneNumber")
            {
                (bool Valid, string Message) = Customer.IsValidPhonenumber(textBox.Text);
                PhoneNumberBorder.BorderBrush = (Valid) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                PhoneNumberError.Foreground = (Valid) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));

                PhoneNumberError.Text = Message;
            }
            else if (textBox.Name == "Email")
            {
                (bool Valid, string Message) = Customer.IsValidEmail(textBox.Text);
                EmailBorder.BorderBrush = (Valid) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                EmailError.Foreground = (Valid) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));

                EmailError.Text = Message;
            }
        }
    }
}
