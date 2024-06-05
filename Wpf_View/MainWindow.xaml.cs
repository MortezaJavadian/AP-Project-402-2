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
            await Task.Delay(2000);
            for (int i = 0; i < 10; i++)
            {

            }
            StartPage.Visibility = Visibility.Collapsed;
            LoginPage.Visibility = Visibility.Visible;
        }
    }
}