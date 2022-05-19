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
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void txtLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text == "Введите Логин")
            {
                txtLogin.Text = string.Empty;
                txtLogin.Opacity = 1;
                txtLogin.FontStyle = FontStyles.Normal;
                txtLogin.BorderBrush = Brushes.Green;
            }
            else
            {
               
            }
        }

        private void txtLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if(txtLogin.Text == string.Empty)
            {
                txtLogin.Text = "Введите Логин";               
                txtLogin.FontStyle = FontStyles.Oblique;
                txtLogin.BorderBrush = Brushes.Black;
            }
        }

        private void txtPass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPass.Password == string.Empty)
            {
                txtPass.BorderBrush = Brushes.Black;
            }
            else
            {
                txtPass.BorderBrush = Brushes.Green;
            }
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            var authUser = ClassHelper.AppData.Context.Employee.ToList().
                Where(i => i.Login == txtLogin.Text && i.Password == txtPass.Password).FirstOrDefault();
            if (authUser != null)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            else
            {
                txtPass.BorderBrush = Brushes.Red;
                txtLogin.BorderBrush = Brushes.Red;
                MessageBox.Show("Пользователь не найден", "Ошибка",  MessageBoxButton.OK, MessageBoxImage.Error);
              
                return;
            }
        }
    }
}
