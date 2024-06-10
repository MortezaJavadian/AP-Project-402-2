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
                SignUpPage.Height += 7.5;
                SignUpPage.Width += 6.5;
                SignUpPage.CornerRadius = new CornerRadius(100 - 1.5 * j);
            }
        }

        bool ClearFirstname = true;
        bool ClearLastname = true;
        bool ClearUsername = true;
        bool ClearPassword = true;
        private void Clear_Text(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (ClearUsername && textBox.Text == "Firstname")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                ClearFirstname = false;
            }
            else if (ClearUsername && textBox.Text == "Lastname")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                ClearLastname = false;
            }
            else if (ClearUsername && textBox.Text == "Username")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                ClearUsername = false;
            }
            else if (ClearPassword && textBox.Text == "Password")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                ClearUsername = false;
            }
        }
    }
}
