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
using System.Windows.Shapes;

namespace ArendaProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для RentWindow.xaml
    /// </summary>
    public partial class RentWindow : Window
    {
        public RentWindow()
        {
            InitializeComponent();
            LVRent.ItemsSource = ClassHelper.AppData.Context.Rent.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddRentWindow arw = new AddRentWindow();
            arw.Show();
            this.Close();
        }
    }
}
