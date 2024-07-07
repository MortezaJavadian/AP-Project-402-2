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

namespace Wpf_View.RestaurantManagerPanel
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
            this.Loaded += Menu_Loaded;
        }

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            RestaurantManager restaurantManager = ((Application.Current.MainWindow as MainWindow).Content as Home).restaurantManager;
            MenuFood.DataContext = restaurantManager.GetMenuByCategory();
            Reservation.Text = (restaurantManager.IsReserveService) ? "Yes" : "No";
        }

        private void Number_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (new Regex("[^0-9]+").IsMatch(e.Text));
        }

        bool AddOrChange = true;
        Food FoodSelected;
        private void listView_Click(object sender, RoutedEventArgs e)
        {
            FoodSelected = (sender as ListView).SelectedItem as Food;
            if (FoodSelected != null)
            {
                AddOrChange = false;

                ChangeFoodButton.IsEnabled = true;
                ChangeFoodButton.Opacity = 1;

                DeleteFoodButton.IsEnabled = true;
                DeleteFoodButton.Opacity = 1;

                FoodName.Text = FoodSelected.Name;
                Description.Text = FoodSelected.Description;
                Inventory.Text = FoodSelected.foodNum.ToString();
                Price.Text = FoodSelected.Price.ToString();
                Category.Text = FoodSelected.Category;
            }
        }

        bool ValidName = false;
        bool ValidInventory = false;
        bool ValidPrice = false;
        private void EnableAddFood(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Name == "FoodName" && textBox.Text != "")
            {
                ValidName = true;
            }
            else if (textBox.Name == "Inventory" && textBox.Text != "")
            {
                ValidInventory = true;
            }
            else if (textBox.Name == "Price" && textBox.Text != "")
            {
                ValidPrice = true;
            }

            if (ValidName && ValidInventory && ValidPrice)
            {
                if (AddOrChange)
                {
                    AddFoodButton.IsEnabled = true;
                    AddFoodButton.Opacity = 1;
                }
                else
                {
                    ChangeFoodButton.IsEnabled = true;
                    ChangeFoodButton.Opacity = 1;
                }
            }
            else
            {
                if (AddOrChange)
                {
                    AddFoodButton.IsEnabled = false;
                    AddFoodButton.Opacity = 0.4;
                }
                else
                {
                    ChangeFoodButton.IsEnabled = false;
                    AddFoodButton.Opacity = 0.4;
                }
            }
        }

        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {
            new Food(((Application.Current.MainWindow as MainWindow).Content as Home).restaurantManager,
                FoodName.Text, Description.Text, int.Parse(Inventory.Text), int.Parse(Price.Text), Category.Text);

            MenuFood.DataContext = ((Application.Current.MainWindow as MainWindow).Content as Home).restaurantManager.GetMenuByCategory();

            FoodName.Text = "";
            Description.Text = "";
            Inventory.Text = "";
            Price.Text = "";
            Category.Text = "";

            AddFoodButton.IsEnabled = false;
            AddFoodButton.Opacity = 0.4;
        }

        private void ChangeFoodButton_Click(object sender, RoutedEventArgs e)
        {
            FoodSelected.Name = FoodName.Text;
            FoodSelected.Description = Description.Text;
            FoodSelected.foodNum = int.Parse(Inventory.Text);
            FoodSelected.Available = FoodSelected.foodNum != 0;
            FoodSelected.Price = int.Parse(Price.Text);
            FoodSelected.Category = Category.Text;

            MenuFood.DataContext = ((Application.Current.MainWindow as MainWindow).Content as Home).restaurantManager.GetMenuByCategory();

            FoodName.Text = "";
            Description.Text = "";
            Inventory.Text = "";
            Price.Text = "";
            Category.Text = "";

            ChangeFoodButton.IsEnabled = false;
            ChangeFoodButton.Opacity = 0.4;

            DeleteFoodButton.IsEnabled = false;
            DeleteFoodButton.Opacity = 0.4;

            AddOrChange = true;
        }

        private void DeleteFoodButton_Click(object sender, RoutedEventArgs e)
        {
            ((Application.Current.MainWindow as MainWindow).Content as Home).restaurantManager.RemoveFood(FoodSelected.Food_Id);

            MenuFood.DataContext = ((Application.Current.MainWindow as MainWindow).Content as Home).restaurantManager.GetMenuByCategory();

            FoodName.Text = "";
            Description.Text = "";
            Inventory.Text = "";
            Price.Text = "";
            Category.Text = "";

            ChangeFoodButton.IsEnabled = false;
            ChangeFoodButton.Opacity = 0.4;

            DeleteFoodButton.IsEnabled = false;
            DeleteFoodButton.Opacity = 0.4;

            AddOrChange = true;
        }

        private void Reservation_DropDownClosed(object sender, EventArgs e)
        {
            if (Reservation.Text == "Yes")
            {
                try
                {
                    ((Application.Current.MainWindow as MainWindow).Content as Home).restaurantManager.changeReserveService(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Reservation.Text = "No";
                }
            }
            else
            {
                ((Application.Current.MainWindow as MainWindow).Content as Home).restaurantManager.changeReserveService(false);
            }
        }
    }
}
