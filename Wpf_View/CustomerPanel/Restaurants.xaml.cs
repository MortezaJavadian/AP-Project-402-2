using Backend.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Wpf_View.CustomerPanel
{
    /// <summary>
    /// Interaction logic for Restaurants.xaml
    /// </summary>
    public partial class Restaurants : UserControl
    {
        public Restaurants()
        {
            InitializeComponent();
        }

        private void Score_input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (new Regex("[^0-9.-]+").IsMatch(e.Text));
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                Name.Text = (item as RestaurantManager).NameOfRestaurant;
            }
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
