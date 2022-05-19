using ArendaProject.EF;
using Microsoft.Win32;
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

namespace ArendaProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        EF.Entities content = new EF.Entities();
        string fileName = null;

        public AddClientWindow()
        {
            InitializeComponent();
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

            if (tbSeria.Text == "Серия паспорта")
            {
                MessageBox.Show("Некоторые поля не заполнены");
                return;
            }
            if (tbPassNum.Text == "Номер Паспорта")
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



            if (tbPatronymic.Text == "Введите отчество (опционально)")
            {
                tbPatronymic.Text = null;
            }

            #endregion
            int gender;
            if (rb1.IsChecked == true)
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
            if (tbEmail.Text == "Email (опционально)" || tbEmail.Text == null || string.IsNullOrWhiteSpace(tbEmail.Text))
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

            content.Client.Add(new Client
            {

                LastName = tbLastName.Text,
                FirstName = tbFirstName.Text,
                Patronymic = tbPatronymic.Text,
               
                Email = tbEmail.Text,
                Phone = tbPhone.Text,
               
                idGender = gender,
                IsActive = true,
                Photo = File.ReadAllBytes(fileName)



            });
            MessageBox.Show("Клиент добавлен!");
            content.SaveChanges();
            ClientWindow cw = new ClientWindow();
            cw.Show();
            this.Close();
        }

        //valid for tb's
        #region valid
     

        private void tbFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbFirstName.Text == "Введите имя")
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

        private void tbEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(e.Handled != "йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0))
            {
                e.Handled = true;
            }
        }
        #endregion

        private void tbPhone_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tbPhone_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void tbSeria_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbSeria.Text == "Серия Паспорта")
            { tbSeria.Clear(); }

            var bc = new BrushConverter();

            tbSeria.Foreground = (Brush)bc.ConvertFrom("#E23E57");
            tbSeria.FontStyle = FontStyles.Normal;
            tbSeria.BorderBrush = (Brush)bc.ConvertFrom("#E23E57");
        }

        private void tbSeria_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbSeria.Text == "Серия Паспорта" || string.IsNullOrWhiteSpace(tbSeria.Text))
            {
                tbSeria.Text = "Серия Паспорта";

                var bc = new BrushConverter();
                tbSeria.Foreground = (Brush)bc.ConvertFrom("#88304E");
                tbSeria.FontStyle = FontStyles.Oblique;
                tbSeria.BorderBrush = (Brush)bc.ConvertFrom("#522546");
            }
        }

        private void tbSeria_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void tbSeria_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (
    e.Command == ApplicationCommands.Cut ||
    e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void tbPassNum_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassNum.Text == "Номер Паспорта" || string.IsNullOrWhiteSpace(tbPassNum.Text))
            {
                tbPassNum.Text = "Номер Паспорта";

                var bc = new BrushConverter();
                tbPassNum.Foreground = (Brush)bc.ConvertFrom("#88304E");
                tbPassNum.FontStyle = FontStyles.Oblique;
                tbPassNum.BorderBrush = (Brush)bc.ConvertFrom("#522546");
            }
        }

        private void tbPassNum_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassNum.Text == "Номер Паспорта")
            { tbPassNum.Clear(); }

            var bc = new BrushConverter();

            tbPassNum.Foreground = (Brush)bc.ConvertFrom("#E23E57");
            tbPassNum.FontStyle = FontStyles.Normal;
            tbPassNum.BorderBrush = (Brush)bc.ConvertFrom("#E23E57");
        }

        private void tbPersNum_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbPersNum.Text == "Персональный номер" || string.IsNullOrWhiteSpace(tbPersNum.Text))
            {
                tbPersNum.Text = "Персональный номер";

                var bc = new BrushConverter();
                tbPersNum.Foreground = (Brush)bc.ConvertFrom("#88304E");
                tbPersNum.FontStyle = FontStyles.Oblique;
                tbPersNum.BorderBrush = (Brush)bc.ConvertFrom("#522546");
            }
        }

        private void tbPersNum_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPersNum.Text == "Персональный номер")
            { tbPersNum.Clear(); }

            var bc = new BrushConverter();

            tbPersNum.Foreground = (Brush)bc.ConvertFrom("#E23E57");
            tbPersNum.FontStyle = FontStyles.Normal;
            tbPersNum.BorderBrush = (Brush)bc.ConvertFrom("#E23E57");
        }

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
