﻿using Backend.Models;
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

namespace Wpf_View.AdminPanel
{
    /// <summary>
    /// Interaction logic for SignUpRestaurant.xaml
    /// </summary>
    public partial class SignUpRestaurant : UserControl
    {
        public SignUpRestaurant()
        {
            InitializeComponent();
        }

        bool ClearUsername = true;
        bool ClearPassword = true;
        bool ClearConfirmPassword = true;
        private void Clear_HintText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (ClearUsername && textBox.Name == "Username")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                ClearUsername = false;
                UsernameError.Text = "Username can not be empty";
                UsernameBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
            }
            else if (ClearPassword && textBox.Name == "Password")
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
                if (textBox.Name == "Username")
                {
                    textBox.Foreground = Brushes.DimGray;
                    textBox.Text = "Username";
                    ClearUsername = true;
                    UsernameError.Text = "";
                    UsernameBorder.BorderBrush = null;
                }
                else if (textBox.Name == "Password")
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

        bool ValidUsername = false;
        bool ValidPassword = false;
        bool ValidCofirmPassword = false;
        private void Clear_Error(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Name == "Username")
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
            else if (textBox.Name == "Password")
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

            if (ValidUsername && ValidPassword && ValidCofirmPassword)
            {
                SubmitButton.IsEnabled = true;
                SubmitButton.Opacity = 1;
            }
            else
            {
                SubmitButton.IsEnabled = false;
                SubmitButton.Opacity = 0.4;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            

        }
    }
}
