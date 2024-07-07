using Backend.Models;
using Backend.NetWork;
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
using System.Windows.Threading;

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

            RestaurantsList.DataContext = User.restaurantManagers;
        }

        private void Score_input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (Score.Text.Length != 0 || new Regex("[^0-5.-]+").IsMatch(e.Text));
        }

        RestaurantManager restaurant;
        private void listView_Click(object sender, RoutedEventArgs e)
        {
            restaurant = (sender as ListView).SelectedItem as RestaurantManager;
            if (restaurant != null)
            {
                Menu.Visibility = Visibility.Visible;
                MenuFood.DataContext = restaurant.GetMenuByCategory();
                OrdersFood.DataContext =
                    ((Application.Current.MainWindow as MainWindow).Content as Home).customer.shoppingCart.items;
            }
        }

        private void Set_Fillter(object sender, EventArgs e)
        {
            RestaurantsList.DataContext = Customer.SearchRestaurants(City.Text, Name.Text,
                (Delivery.Text != "") ? ((Delivery.Text == "Yes") ? true : false) : null,

                (Dine_in.Text != "") ? ((Dine_in.Text == "Yes") ? true : false) : null, Score.Text);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Collapsed;
        }

        private void MenuFood_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Food food = (sender as ListView).SelectedItem as Food;
            if (food != null)
            {
                Customer customer = ((Application.Current.MainWindow as MainWindow).Content as Home).customer;
                customer.AddToCart(food, 1);
                OrdersFood.DataContext = customer.shoppingCart.items;
                MenuFood.DataContext = restaurant.GetMenuByCategory();

                TotalPrice.Text = customer.shoppingCart.RecalculateTotalPrice().ToString();
            }
        }

        private void OrdersFood_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CartItem CartItems = (sender as ListView).SelectedItem as CartItem;
            if (CartItems != null)
            {
                Customer customer = ((Application.Current.MainWindow as MainWindow).Content as Home).customer;
                customer.RemoveFromCart(CartItems.Food);
                OrdersFood.DataContext = customer.shoppingCart.items;
                MenuFood.DataContext = restaurant.GetMenuByCategory();
                TotalPrice.Text = customer.shoppingCart.RecalculateTotalPrice().ToString();
            }
        }

        private async void PayOrders(object sender, RoutedEventArgs e)
        {
            if (OrdersFood.DataContext != null && (OrdersFood.DataContext as ObservableCollection<CartItem>).Count != 0)
            {
                Button button = sender as Button;
                Customer customer = ((Application.Current.MainWindow as MainWindow).Content as Home).customer;
                if (button.Name == "CashPayButton")
                {
                    customer.PlaceOrder(restaurant, 2);
                    OrdersFood.DataContext = customer.shoppingCart.items;

                    TotalPrice.Text = "0";
                }
                else
                {
                    try
                    {
                        customer.sendEmailPay_Order(restaurant);

                        Panel.SetZIndex(VerifyPage, 1);

                        for (int i = 1; i <= 25; i++)
                        {
                            await Task.Delay(1);
                            VerifyPage.Opacity += 0.04;
                        }

                        Box1.Focus();
                    }
                    catch (Exception ex)
                    {

                        (InternetError.Child as TextBlock).Text = ex.Message;
                        for (int i = 1; i <= 25; i++)
                        {
                            await Task.Delay(10);
                            InternetError.Width += 9.1;
                        }

                        await Task.Delay(6000);

                        for (int i = 1; i <= 25; i++)
                        {
                            await Task.Delay(10);
                            InternetError.Width -= 9.1;
                        }
                    }
                }
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

        private async void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();

            Panel.SetZIndex(VerifyPage, -1);

            for (int i = 1; i <= 25; i++)
            {
                await Task.Delay(1);
                VerifyPage.Opacity -= 0.04;
            }

            Box1.Text = "";
            Box2.Text = "";
            Box3.Text = "";
            Box4.Text = "";
            Box5.Text = "";
        }

        private async void Verify_Click(object sender, RoutedEventArgs e)
        {
            string code = Box1.Text + Box2.Text + Box3.Text + Box4.Text + Box5.Text;

            if (Verification.verify(code))
            {
                Box1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100"));
                await Task.Delay(150);
                Box2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100"));
                await Task.Delay(150);
                Box3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100"));
                await Task.Delay(150);
                Box4.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100"));
                await Task.Delay(150);
                Box5.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D100"));
                await Task.Delay(1000);

                Box1.Text = "";
                Box1.Background = Brushes.White;
                Box2.Text = "";
                Box2.Background = Brushes.White;
                Box3.Text = "";
                Box3.Background = Brushes.White;
                Box4.Text = "";
                Box4.Background = Brushes.White;
                Box5.Text = "";
                Box5.Background = Brushes.White;

                Panel.SetZIndex(VerifyPage, -1);

                for (int i = 1; i <= 25; i++)
                {
                    await Task.Delay(1);
                    VerifyPage.Opacity -= 0.04;
                }

                Customer customer = ((Application.Current.MainWindow as MainWindow).Content as Home).customer;
                customer.PlaceOrder(restaurant, 1);
                OrdersFood.DataContext = customer.shoppingCart.items;

                TotalPrice.Text = "0";
            }
            else
            {
                Box1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                await Task.Delay(150);
                Box2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                await Task.Delay(150);
                Box3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                await Task.Delay(150);
                Box4.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                await Task.Delay(150);
                Box5.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D10000"));
                await Task.Delay(700);

                Box1.Text = "";
                Box1.Background = Brushes.White;
                await Task.Delay(150);
                Box2.Text = "";
                Box2.Background = Brushes.White;
                await Task.Delay(150);
                Box3.Text = "";
                Box3.Background = Brushes.White;
                await Task.Delay(150);
                Box4.Text = "";
                Box4.Background = Brushes.White;
                await Task.Delay(150);
                Box5.Text = "";
                Box5.Background = Brushes.White;

                Box1.Focus();
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
