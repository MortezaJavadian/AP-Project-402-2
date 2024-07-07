using Backend.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Formats.Asn1.AsnWriter;

namespace Wpf_View.RestaurantManagerPanel
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : UserControl
    {
        public History()
        {
            InitializeComponent();
            this.Loaded += History_Loaded;
        }

        private void History_Loaded(object sender, RoutedEventArgs e)
        {
            RestaurantManager restaurantManager = ((Application.Current.MainWindow as MainWindow).Content as Home).restaurantManager;
            OrdersList.DataContext = restaurantManager.orders;
        }

        private void Number_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (new Regex("[^0-9]+").IsMatch(e.Text));
        }

        private void Month_Input(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            e.Handled = !(new Regex(@"^(1|2|3|4|5|6|7|8|9|10|11|12)$").IsMatch(textBox.Text + e.Text));
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
            }
        }

        private void Set_Fillter(object sender, EventArgs e)
        {
            RestaurantManager restaurantManager = ((Application.Current.MainWindow as MainWindow).Content as Home).restaurantManager;
            if (sender is ComboBox)
            {
                if (Type.Text == "Orders")
                {
                    OrdersList.Visibility = Visibility.Visible;
                    ReservationList.Visibility = Visibility.Collapsed;
                }
                else
                {
                    OrdersList.Visibility = Visibility.Collapsed;
                    ReservationList.Visibility = Visibility.Visible;
                }
            }

            OrdersList.DataContext = restaurantManager.FilterOrders(CustomerUsername.Text, PhoneNumber.Text,
                FoodName.Text, MinPrice.Text, MaxPrice.Text,
                (StartMonth.Text != "") ? new DateTime(DateTime.Now.Year, int.Parse(StartMonth.Text), 1) : null,
                (EndMonth.Text != "") ? new DateTime(DateTime.Now.Year, int.Parse(EndMonth.Text), 30) : null);
            ReservationList.DataContext = restaurantManager.FilterReservations(CustomerUsername.Text, PhoneNumber.Text,
                FoodName.Text, MinPrice.Text, MaxPrice.Text,
                (StartMonth.Text != "") ? new DateTime(DateTime.Now.Year, int.Parse(StartMonth.Text), 1) : null,
                (EndMonth.Text != "") ? new DateTime(DateTime.Now.Year, int.Parse(EndMonth.Text), 30) : null);

        }

        private void CSVButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersList.DataContext != null && ReservationList.DataContext != null)
            {
                if (OrdersList.Visibility == Visibility.Visible)
                {
                    RestaurantManager.ExportFilteredOrdersToCsv(OrdersList.DataContext as ObservableCollection<Orders>,
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ExportedOrders.csv");
                }
                else
                {
                    RestaurantManager.ExportFilteredOrdersToCsv(OrdersList.DataContext as ObservableCollection<Orders>,
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ExportedReservations.csv");
                }
            }
        }
    }
}
