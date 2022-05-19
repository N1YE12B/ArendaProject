using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace ArendaProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
            LVEmployye.ItemsSource = ClassHelper.AppData.Context.Employee.ToList();
            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow aew = new AddEmployeeWindow();
            aew.ShowDialog();
            this.Close();
        }

        private void tbSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text == "Поиск..")
            { tbSearch.Clear(); }

            var bc = new BrushConverter();

            tbSearch.Foreground = (Brush)bc.ConvertFrom("#E23E57");
            tbSearch.FontStyle = FontStyles.Normal;
            tbSearch.BorderBrush = (Brush)bc.ConvertFrom("#E23E57");
        }

        private void tbSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text == "Поиск.." || string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                tbSearch.Text = "Поиск..";

                var bc = new BrushConverter();
                tbSearch.Foreground = (Brush)bc.ConvertFrom("#88304E");
                tbSearch.FontStyle = FontStyles.Oblique;
                tbSearch.BorderBrush = (Brush)bc.ConvertFrom("#522546");
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbSearch.Text == "Поиск..")
            {
            
            }
            else
            {
                LVEmployye.ItemsSource = ClassHelper.AppData.Context.Employee.Where(i => i.FirstName.Contains(tbSearch.Text)
                || i.LastName.Contains(tbSearch.Text) || i.Patronymic.Contains(tbSearch.Text) || i.Phone.Contains(tbSearch.Text)
                || i.Email.Contains(tbSearch.Text) || i.Phone.Contains(tbSearch.Text)).ToList();
            }
        }

        private void LVEmployye_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete || e.Key == Key.Back)
            {
               MessageBoxResult msbres = MessageBox.Show("Удалить пользователя", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (msbres == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    if (LVEmployye.SelectedItem is EF.Employee)
                    {
                        var empl = LVEmployye.SelectedItem as EF.Employee;
                        ClassHelper.AppData.Context.Employee.Remove(empl);
                        ClassHelper.AppData.Context.SaveChanges();
                        MessageBox.Show("Пользователь успешно удалён", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                        LVEmployye.ItemsSource = ClassHelper.AppData.Context.Employee.ToList();
                    }
                }
            }
        }

        private void LVEmployye_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LVEmployye.SelectedItem is EF.Employee)
            {
                var empl = LVEmployye.SelectedItem as EF.Employee;
                AddEmployeeWindow aew = new AddEmployeeWindow(empl);
                aew.ShowDialog();
                this.Close();
            }
        }

        private void ComboBoxFilter()
        {
            List<EF.Employee> listEmployee = new List<EF.Employee>();
            listEmployee = ClassHelper.AppData.Context.Employee.ToList();
            

            switch (cmbSort.SelectedIndex)
            {

                case 0:
                    listEmployee = listEmployee.OrderBy(i => i.idEmployee).ToList();
                    LVEmployye.ItemsSource = listEmployee;
                    break;
                case 1:
                    listEmployee = listEmployee.OrderBy(i => i.LastName).ToList();
                    LVEmployye.ItemsSource = listEmployee;
                    break;
                case 2:
                    listEmployee = listEmployee.OrderBy(i => i.Email).ToList();
                    LVEmployye.ItemsSource = listEmployee;
                    break;
                case 3:
                    listEmployee = listEmployee.OrderBy(i => i.Phone).ToList();
                    LVEmployye.ItemsSource = listEmployee;
                    break;
            }

            LVEmployye.ItemsSource = listEmployee;


        }

        List<string> listSort = new List<string>()
        {
        "Стандартная",
        "По фамилии",
        "По Email",
        "По телефону",
        };

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxFilter();
        }
    }
}
