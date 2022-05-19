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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
            LVClient.ItemsSource = ClassHelper.AppData.Context.Client.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow acw = new AddClientWindow();
            acw.ShowDialog();
            this.Close();
        }
    }
}
