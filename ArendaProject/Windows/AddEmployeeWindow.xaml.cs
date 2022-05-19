using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using ArendaProject.EF;
using Microsoft.Win32;

namespace ArendaProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        EF.Entities content = new EF.Entities();
        EF.Employee editEmployee = new EF.Employee();

        private List<Role> roles = new List<Role>();
        public bool isEdit;
        string fileName = null;
        public AddEmployeeWindow()
        {
            InitializeComponent();

            roles = content.Role.ToList();
            roles.Insert(0, new Role { RoleName = "Выбор роли" });
            cmbRole.ItemsSource = roles;
            cmbRole.DisplayMemberPath = "RoleName";
            cmbRole.SelectedIndex = 0;

            isEdit = false;
        }
        public AddEmployeeWindow(EF.Employee empl)
        {

            InitializeComponent();

            roles = content.Role.ToList();
           
            cmbRole.ItemsSource = roles;
            cmbRole.DisplayMemberPath = "RoleName";
            


            label.Text = "Изменение данных сотрудника";
            btnAddClient.Content = "Изменить";

            tbFirstName.Text = empl.FirstName;
            tbLastName.Text = empl.LastName;
            tbPatronymic.Text = empl.Patronymic;
            tbLogin.Text = empl.Login;
            tbPassword.Text = empl.Password;
            tbEmail.Text = empl.Email;
            tbPhone.Text = empl.Phone;

            cmbRole.SelectedIndex = empl.idRole - 1;

            editEmployee = empl;

            isEdit = true;
        }
        
        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            //empty check
            #region emptcheck

           
            if (tbFirstName.Text == "Введите имя")
            {
                MessageBox.Show("Некоторые поля не заполнены");
                return;             
            }
            if (tbLastName.Text == "Введите фамилию")
            {
                MessageBox.Show("Некоторые поля не заполнены");
                return;
            }
         
            if (tbLogin.Text == "Логин")
            {
                MessageBox.Show("Некоторые поля не заполнены");
                return;
            }
            if (tbPassword.Text == "Пароль")
            {
                MessageBox.Show("Некоторые поля не заполнены");
                return;
            }
          
            if (tbPhone.Text == "Номер телефона(+X(XXX)XXXXXXX)")
            {
                MessageBox.Show("Некоторые поля не заполнены");
                return;
            }

            if (rb1.IsChecked == false && rb2.IsChecked == false)
            {
                MessageBox.Show("Выберите пол");
                return;
            }

          
          
            if(tbPatronymic.Text == "Введите отчество (опционально)")
            {
                tbPatronymic.Text = null;
            }

            #endregion

            //Email def
            int gender;
            if(rb1.IsChecked == true)
            {
                gender = 1;
            }
            else
            {
                gender = 2;
            }
            string email = tbEmail.Text;

            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            bool emailCheck = Regex.IsMatch(email, emailPattern);
            if(tbEmail.Text == "Email (опционально)" || tbEmail.Text == null || string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                tbEmail.Text = null;
            }
            else 
            {
                if (emailCheck == false)
                {
                    MessageBox.Show("Неверный формат Email");
                    return;
                }
            }

            if (isEdit == true)
            {
                try
                {
                    if(Image.Source != null)
                    {
                        editEmployee.Photo = File.ReadAllBytes(fileName);
                    }

                    editEmployee.LastName = tbLastName.Text;
                    editEmployee.FirstName = tbFirstName.Text;
                    editEmployee.Patronymic = tbPatronymic.Text;
                    editEmployee.Login = tbLogin.Text;
                    editEmployee.Password = tbPassword.Text;
                    editEmployee.Email = tbEmail.Text;
                    editEmployee.Phone = tbPhone.Text;
                    editEmployee.idRole = cmbRole.SelectedIndex + 1;
                    editEmployee.idGender = gender;

                   
                    MessageBox.Show("Пользователь изменён!");
                }
                catch(Exception a)
                {
                    throw;
                }

            }
            else
            {
                if (cmbRole.SelectedIndex == 0)
                {
                    MessageBox.Show("Выберите роль");
                    return;
                }


                content.Employee.Add(new Employee
                {

                    LastName = tbLastName.Text,
                    FirstName = tbFirstName.Text,
                    Patronymic = tbPatronymic.Text,
                    Login = tbLogin.Text,
                    Password = tbPassword.Text,
                    Email = tbEmail.Text,
                    Phone = tbPhone.Text,
                    idRole = cmbRole.SelectedIndex,
                    idGender = gender,
                    Photo = File.ReadAllBytes(fileName)


                });
                MessageBox.Show("Пользователь добавлен!");
            }
            content.SaveChanges();
            EmployeeWindow employeeWindow = new EmployeeWindow();
            employeeWindow.Show();
            this.Close();
        }



        // Lost , Got , Preview, Paste
        #region validation


        private void tbFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            if(tbFirstName.Text == "Введите имя")
            { tbFirstName.Clear(); }

            var bc = new BrushConverter();
            
            tbFirstName.Foreground = (Brush)bc.ConvertFrom("#E23E57");
            tbFirstName.FontStyle = FontStyles.Normal;
            tbFirstName.BorderBrush = (Brush)bc.ConvertFrom("#E23E57");
        }

        private void tbFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbFirstName.Text == "Введите имя" || string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                tbFirstName.Text = "Введите имя";

                var bc = new BrushConverter();
                tbFirstName.Foreground = (Brush)bc.ConvertFrom("#88304E");
                tbFirstName.FontStyle = FontStyles.Oblique;
                tbFirstName.BorderBrush = (Brush)bc.ConvertFrom("#522546");
            }
        }

        private void tbFirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Handled != "йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0)
            {
                e.Handled = true;
            }
        }

        private void tbFirstName_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (
       e.Command == ApplicationCommands.Cut ||
       e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void tbLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbLastName.Text == "Введите фамилию")
            { tbLastName.Clear(); }

            var bc = new BrushConverter();

            tbLastName.Foreground = (Brush)bc.ConvertFrom("#E23E57");
            tbLastName.FontStyle = FontStyles.Normal;
            tbLastName.BorderBrush = (Brush)bc.ConvertFrom("#E23E57");
        }

        private void tbLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbLastName.Text == "Введите фамилию" || string.IsNullOrWhiteSpace(tbLastName.Text))
            {
                tbLastName.Text = "Введите фамилию";

                var bc = new BrushConverter();
                tbLastName.Foreground = (Brush)bc.ConvertFrom("#88304E");
                tbLastName.FontStyle = FontStyles.Oblique;
                tbLastName.BorderBrush = (Brush)bc.ConvertFrom("#522546");
            }
        }

        private void tbLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Handled != "йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0)
            {
                e.Handled = true;
            }
        }

        private void tbLastName_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (
      e.Command == ApplicationCommands.Cut ||
      e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void tbPatronymic_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPatronymic.Text == "Введите отчество (опционально)")
            { tbPatronymic.Clear(); }

            var bc = new BrushConverter();

            tbPatronymic.Foreground = (Brush)bc.ConvertFrom("#E23E57");
            tbPatronymic.FontStyle = FontStyles.Normal;
            tbPatronymic.BorderBrush = (Brush)bc.ConvertFrom("#E23E57");
        }

        private void tbPatronymic_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbPatronymic.Text == "Введите отчество (опционально)" || string.IsNullOrWhiteSpace(tbPatronymic.Text))
            {
                tbPatronymic.Text = "Введите отчество (опционально)";

                var bc = new BrushConverter();
                tbPatronymic.Foreground = (Brush)bc.ConvertFrom("#88304E");
                tbPatronymic.FontStyle = FontStyles.Oblique;
                tbPatronymic.BorderBrush = (Brush)bc.ConvertFrom("#522546");
            }
        }

        private void tbPatronymic_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Handled != "йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0)
            {
                e.Handled = true;
            }
        }

        private void tbPatronymic_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (
     e.Command == ApplicationCommands.Cut ||
     e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void tbLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text == "Логин")
            { tbLogin.Clear(); }

            var bc = new BrushConverter();

            tbLogin.Foreground = (Brush)bc.ConvertFrom("#E23E57");
            tbLogin.FontStyle = FontStyles.Normal;
            tbLogin.BorderBrush = (Brush)bc.ConvertFrom("#E23E57");
        }

        private void tbLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text == "Логин" || string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                tbLogin.Text = "Логин";

                var bc = new BrushConverter();
                tbLogin.Foreground = (Brush)bc.ConvertFrom("#88304E");
                tbLogin.FontStyle = FontStyles.Oblique;
                tbLogin.BorderBrush = (Brush)bc.ConvertFrom("#522546");
            }
        }

        

        private void tbLogin_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (
     e.Command == ApplicationCommands.Cut ||
     e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void tbLogin_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void tbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Text == "Пароль")
            { tbPassword.Clear(); }

            var bc = new BrushConverter();

            tbPassword.Foreground = (Brush)bc.ConvertFrom("#E23E57");
            tbPassword.FontStyle = FontStyles.Normal;
            tbPassword.BorderBrush = (Brush)bc.ConvertFrom("#E23E57");
        }

        private void tbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Text == "Пароль" || string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                tbPassword.Text = "Пароль";

                var bc = new BrushConverter();
                tbPassword.Foreground = (Brush)bc.ConvertFrom("#88304E");
                tbPassword.FontStyle = FontStyles.Oblique;
                tbPassword.BorderBrush = (Brush)bc.ConvertFrom("#522546");
            }
        }

        private void tbPassword_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (
    e.Command == ApplicationCommands.Cut ||
    e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void tbPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void tbEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text == "Email (опционально)")
            { tbEmail.Clear(); }

            var bc = new BrushConverter();

            tbEmail.Foreground = (Brush)bc.ConvertFrom("#E23E57");
            tbEmail.FontStyle = FontStyles.Normal;
            tbEmail.BorderBrush = (Brush)bc.ConvertFrom("#E23E57");
        }

        private void tbEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text == "Email (опционально)" || string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                tbEmail.Text = "Email (опционально)";

                var bc = new BrushConverter();
                tbEmail.Foreground = (Brush)bc.ConvertFrom("#88304E");
                tbEmail.FontStyle = FontStyles.Oblique;
                tbEmail.BorderBrush = (Brush)bc.ConvertFrom("#522546");
            }
        }

      

        private void tbEmail_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (
   e.Command == ApplicationCommands.Cut ||
   e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void tbEmail_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void tbPhone_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPhone.Text == "Номер телефона(+X(XXX)XXXXXXX)")
            { tbPhone.Clear(); }

            var bc = new BrushConverter();

            tbPhone.Foreground = (Brush)bc.ConvertFrom("#E23E57");
            tbPhone.FontStyle = FontStyles.Normal;
            tbPhone.BorderBrush = (Brush)bc.ConvertFrom("#E23E57");
        }

        private void tbPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbPhone.Text == "Номер телефона(+X(XXX)XXXXXXX)" || string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                tbPhone.Text = "Номер телефона(+X(XXX)XXXXXXX)";

                var bc = new BrushConverter();
                tbPhone.Foreground = (Brush)bc.ConvertFrom("#88304E");
                tbPhone.FontStyle = FontStyles.Oblique;
                tbPhone.BorderBrush = (Brush)bc.ConvertFrom("#522546");
            }
        }

        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!int.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
            e.Handled = "0123456789 + ( )".IndexOf(e.Text) < 0;
        }

        private void tbPhone_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tbPhone_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {

        }
        //ограничение кириллицы 
        private void tbLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
             if (e.Handled != "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890".IndexOf(e.Text) < 0)
            {
                e.Handled = true;
            }
        }

        private void tbPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(e.Handled != "йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0))
            {
                e.Handled = true;
            }
        }

        private void tbEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(e.Handled != "йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0))
            {
                e.Handled = true;
            }
        }
        #endregion

        private void btnImageLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();



            if (ofd.ShowDialog() == true)
            {

                //fileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
                ofd.Filter = "PNG Photos (*.png, *.jpg)|*.png;*.jpg";
                fileName = ofd.FileName;
                Image.Source = new BitmapImage(new Uri(fileName));
            }
        }
    }
}
