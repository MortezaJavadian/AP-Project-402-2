using Backend.Models;
using System;
using System.Collections.Generic;
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
            Customer customer = ((Application.Current.MainWindow as MainWindow).Content as Home).customer;
            HistorList.DataContext = customer.orders;
        }

        Orders order;
        private async void HistorList_ClickItem(object sender, MouseButtonEventArgs e)
        {
            order = (sender as ListView).SelectedItem as Orders;
            if (order != null)
            {
                Panel.SetZIndex(RatingPage, 1);

                for (int i = 1; i <= 25; i++)
                {
                    await Task.Delay(1);
                    RatingPage.Opacity += 0.04;
                }
            }
        }

        private void Score_input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (Score.Text.Length != 0 || new Regex("[^0-5.-]+").IsMatch(e.Text));
        }

        private async void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();

            Panel.SetZIndex(RatingPage, -1);

            for (int i = 1; i <= 25; i++)
            {
                await Task.Delay(1);
                RatingPage.Opacity -= 0.04;
            }

            Score.Text = "";
        }

        private async void Verify_Click(object sender, RoutedEventArgs e)
        {
            if (Score.Text != "")
            {
                order.RateOrder(int.Parse(Score.Text));
                order.Restaurant.CalculateAllAvergeRating();
                Customer customer = ((Application.Current.MainWindow as MainWindow).Content as Home).customer;
                HistorList.DataContext = customer.orders;

                Panel.SetZIndex(RatingPage, -1);

                for (int i = 1; i <= 25; i++)
                {
                    await Task.Delay(1);
                    RatingPage.Opacity -= 0.04;
                }
            }
        }
    }
}
