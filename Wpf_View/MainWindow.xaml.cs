using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Wpf_View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowLoginPage();
        }

        private async void ShowLoginPage()
        {
            char[] StartText = { 'R', 'e', 's', 't', 'a', 'u', 'r', 'a', 'n', 't', ' ',
                                 'M', 'a', 'n', 'a', 'g', 'e', 'm', 'e', 'n', 't' };
            for (int i = 0; i < StartText.Length; i++)
            {
                await Task.Delay(100);
                StartPage.Text += StartText[i];
            }
            
            await Task.Delay(2000);
            StartPage.Visibility = Visibility.Collapsed;

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
    }
}