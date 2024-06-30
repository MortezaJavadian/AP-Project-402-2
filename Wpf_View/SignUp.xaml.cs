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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Backend.Models;
using Backend.NetWork;

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
                SignUpPage.Opacity += 0.02;
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

        bool ValidFirstname = false;
        bool ValidLastname = false;
        bool ValidUsername = false;
        bool ValidPhoneNumber = false;
        bool ValidEmail = false;
        private void Clear_Error(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Name == "Firstname")
            {
                (ValidFirstname, string Message) = Customer.IsValidName(textBox.Text);
                FirstnameBorder.BorderBrush = (ValidFirstname) ? 
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                FirstnameError.Foreground = (ValidFirstname) ? 
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));

                FirstnameError.Text = "First" + Message;
            }
            else if (textBox.Name == "Lastname")
            {
                (ValidLastname, string Message) = Customer.IsValidName(textBox.Text);
                LastnameBorder.BorderBrush = (ValidLastname) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                LastnameError.Foreground = (ValidLastname) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));

                LastnameError.Text = "Last" + Message;
            }
            else if (textBox.Name == "Username")
            {
                (ValidUsername, string Message) = User.IsValid_UserName(textBox.Text);
                UsernameBorder.BorderBrush = (ValidUsername) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                UsernameError.Foreground = (ValidUsername) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));

                UsernameError.Text = Message;
            }
            else if (textBox.Name == "PhoneNumber")
            {
                (ValidPhoneNumber, string Message) = Customer.IsValidPhonenumber(textBox.Text);
                PhoneNumberBorder.BorderBrush = (ValidPhoneNumber) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                PhoneNumberError.Foreground = (ValidPhoneNumber) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));

                PhoneNumberError.Text = Message;
            }
            else if (textBox.Name == "Email")
            {
                (ValidEmail, string Message) = Customer.IsValidEmail(textBox.Text);
                EmailBorder.BorderBrush = (ValidEmail) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                EmailError.Foreground = (ValidEmail) ?
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100")) :
                                      new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));

                EmailError.Text = Message;
            }

            if (ValidFirstname && ValidLastname && ValidUsername && ValidPhoneNumber && ValidEmail)
            {
                ConfirmButton.IsEnabled = true;
                ConfirmButton.Opacity = 1;
            }
            else
            {
                ConfirmButton.IsEnabled = false;
                ConfirmButton.Opacity = 0.4;
            }
        }

        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Verification.send(Email.Text);

                Style DisabledBorderStyle = new Style(typeof(Border));
                DisabledBorderStyle.Setters.Add(new Setter(Border.IsEnabledProperty, false));
                SignUpPage.Style = DisabledBorderStyle;

                Panel.SetZIndex(VerifyPage, 0);

                SignUpPage.Effect = new BlurEffect();
                for (int i = 1; i <= 25; i++)
                {
                    await Task.Delay(1);
                    (SignUpPage.Effect as BlurEffect).Radius += 0.2;
                    VerifyPage.Opacity += 0.04;
                }

                Box1.Focus();
            }
            catch (Exception ex)
            {
                EmailError.Text = ex.Message;
                await Task.Delay(1000);
                EmailError.Text = "";
            }
        }

        private void FocusNextBox(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            bool isDigit = false;

            if (e.Key >= Key.D0 && e.Key <= Key.D9 && !Keyboard.IsKeyDown(Key.LeftShift) &&
                                                      !Keyboard.IsKeyDown(Key.RightShift))
                isDigit = true;
            else if (Keyboard.IsKeyToggled(Key.NumLock) && e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                isDigit = true;

            e.Handled = !((textBox.Text.Length == 0 && isDigit) || 
                          (textBox.Text.Length != 0 && textBox.SelectedText.Length == textBox.Text.Length));

            if (!e.Handled)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (textBox.Name == "Box1")
                    {
                        Box2.Focus();
                        Box2.SelectAll();
                    }
                    else if (textBox.Name == "Box2")
                    {
                        Box3.Focus();
                        Box3.SelectAll();
                    }
                    else if (textBox.Name == "Box3")
                    {
                        Box4.Focus();
                        Box4.SelectAll();
                    }
                    else if (textBox.Name == "Box4")
                    {
                        Box5.Focus();
                        Box5.SelectAll();
                    }
                    else if (textBox.Name == "Box5")
                    {
                        FocusManager.SetFocusedElement(FocusManager.GetFocusScope(textBox), null);
                        Keyboard.ClearFocus();
                    }
                }), DispatcherPriority.ContextIdle);
            }
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();

            Style EnabledBorderStyle = new Style(typeof(Border));
            EnabledBorderStyle.Setters.Add(new Setter(Border.IsEnabledProperty, true));
            SignUpPage.Style = EnabledBorderStyle;

            Panel.SetZIndex(VerifyPage, -1);

            for (int i = 1; i <= 25; i++)
            {
                await Task.Delay(1);
                (SignUpPage.Effect as BlurEffect).Radius -= 0.4;
                VerifyPage.Opacity -= 0.04;
            }

            Box1.Text = "";
            Box2.Text = "";
            Box3.Text = "";
            Box4.Text = "";
            Box5.Text = "";
        }

        private void Verify_Click(object sender, RoutedEventArgs e)
        {
            string code = Box1.Text + Box2.Text + Box3.Text + Box4.Text + Box5.Text;

            if (Verification.verify(code))
            {
                
            }
            else
            {
                Box1.Text = "";
                Box2.Text = "";
                Box3.Text = "";
                Box4.Text = "";
                Box5.Text = "";

                Box1.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                Box2.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                Box3.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                Box4.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                Box5.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
            }
        }
    }
}
