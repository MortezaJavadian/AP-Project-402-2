using Backend.Models;
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
            ShowStartPage();
        }

        private async void ShowStartPage()
        {
            this.Content = new RestaurantManagerPanel.Home(new RestaurantManager("1", "1")); return;
            char[] StartText = { 'R', 'e', 's', 't', 'a', 'u', 'r', 'a', 'n', 't', ' ',
                                 'M', 'a', 'n', 'a', 'g', 'e', 'm', 'e', 'n', 't' };
            for (int i = 0; i < StartText.Length; i++)
            {
                await Task.Delay(100);
                StartPage.Text += StartText[i];
            }
            await Task.Delay(2000);
            StartPage.Visibility = Visibility.Collapsed;
            this.Content = new Login();
        }
    }
}