using Backend.Models;
using Backend.NetWork;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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

            SpecialService.Text = ((Application.Current.MainWindow as MainWindow).Content as Home).customer.SpecialService.ToString();
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

                ((Application.Current.MainWindow as MainWindow).Content as Home).customer.ChangeSpecialService(
                    (int)Enum.Parse<SpecialService>(SpecialService.Text));
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
}
