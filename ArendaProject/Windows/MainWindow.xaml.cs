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
using ArendaProject.Windows;

namespace ArendaProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow emp = new EmployeeWindow();
            this.Hide();
            emp.ShowDialog();
            this.Show();
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow cw = new ClientWindow();
            this.Hide();
            cw.ShowDialog();
            this.Show();
        }

        private void btnRent_Click(object sender, RoutedEventArgs e)
        {
            RentWindow rw = new RentWindow();
            this.Hide();
            rw.ShowDialog();
            this.Show();
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow pw = new ProductWindow();
            this.Hide();
            pw.ShowDialog();
            this.Show();
        }
    }
}
