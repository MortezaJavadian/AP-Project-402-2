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

namespace Wpf_View.CustomerPanel
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void Gender_Changed(object sender, EventArgs e)
            => Home.Gender_Changed();

        private void Address_Change(object sender, KeyEventArgs e)
            => Home.Address_Changed();

        private void SpecialService_Buy(object sender, EventArgs e)
            => Home.BuySpecialService();
    }
}
