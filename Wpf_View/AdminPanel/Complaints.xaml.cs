using Backend.Models;
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
    /// Interaction logic for Complaints.xaml
    /// </summary>
    public partial class Complaints : UserControl
    {
        public Complaints()
        {
            InitializeComponent();

            ComplaintsList.DataContext = Admin.complaints;
        }

        private void Set_Fillter(object sender, EventArgs e)
        {
            ComplaintsList.DataContext = Admin.SearchComplaints(PlaintiffUsername.Text, Title.Text, PlaintiffFirstname.Text,
                        PlaintiffLastname.Text, RestaurantName.Text, ComplaintStatus.Text);
        }

        RestaurantManager RestaurantSelect;
        private void listView_Click(object sender, RoutedEventArgs e)
        {
            RestaurantSelect = (sender as ListView).SelectedItem as RestaurantManager;
        }
    }
}
