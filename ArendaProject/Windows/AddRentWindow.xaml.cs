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
using ArendaProject.EF;

namespace ArendaProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddRentWindow.xaml
    /// </summary>
    /// 
   
    public partial class AddRentWindow : Window
    {
        EF.Entities content = new EF.Entities();
        int idEmp;
        int idClient;
        int RentDayCount;
        double instPrice;

        private List<Instrument> instrument = new List<Instrument>();
        public AddRentWindow()
        {
            InitializeComponent();
            DateTime D = DateTime.Now;
            DatePick1.Text = D.ToString();

            DatePick2.SelectedDate = DateTime.Today.AddDays(1);
            DatePick2.DisplayDateStart = DateTime.Today.AddDays(1);

            instrument = content.Instrument.ToList();
            cmbTools.ItemsSource = instrument;
            cmbTools.DisplayMemberPath = "InstrumentName";

            LVEmpl.ItemsSource = ClassHelper.AppData.Context.Employee.ToList();

            LVClient.ItemsSource = ClassHelper.AppData.Context.Client.ToList();

        }

       

       

        private void btnMoveClient_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow acw = new AddClientWindow();
            acw.Show();
            this.Close();
        }

        private void tbEmpl_TextChanged(object sender, TextChangedEventArgs e)
        {
            LVEmpl.ItemsSource = ClassHelper.AppData.Context.Employee.Where(i => i.FirstName.Contains(tbEmpl.Text)
               || i.LastName.Contains(tbEmpl.Text) || i.Patronymic.Contains(tbEmpl.Text)).ToList();
        }

        private void tbClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            LVClient.ItemsSource = ClassHelper.AppData.Context.Client.Where(i => i.FirstName.Contains(tbEmpl.Text)
               || i.LastName.Contains(tbEmpl.Text) || i.Patronymic.Contains(tbEmpl.Text)).ToList();
        }

        private void LVEmpl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LVEmpl.SelectedItem is EF.Employee)
            {
                var empl = LVEmpl.SelectedItem as EF.Employee;
                string fio = empl.LastName + " " + empl.FirstName + " " + empl.Patronymic;
                tbEmpl.Text = fio;
                idEmp = empl.idEmployee;
            }
        }

        private void LVClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LVClient.SelectedItem is EF.Client)
            {
                var client = LVClient.SelectedItem as EF.Client;
                string fio = client.LastName + " " + client.FirstName + " " + client.Patronymic;
                tbClient.Text = fio;
                idClient = client.idClient;
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           
            
            //добавить в бд столбец цены аренды в ёё записи , сделать мягкое удаление, грузить фотки из бд, реализовать возврат товара

            if(string.IsNullOrWhiteSpace(tbClient.Text))
            {
                MessageBox.Show("Выберите клиента");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbEmpl.Text))
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }

            if(cmbTools.SelectedIndex == 0)
            {
                MessageBox.Show("Выберите инструмент");
                return;
            }

            content.Rent.Add(new Rent
            {
                idEmployee = idEmp,
                idClient = idClient,
                idInstrument = cmbTools.SelectedIndex + 1,
                TimeRentStart = DateTime.Now,
                TimeRentEnd = DatePick2.DisplayDate
            }
                );
            MessageBox.Show("Запись добавлена!");
            content.SaveChanges();
            this.Close();

        }

        private void cmbTools_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTools.SelectedItem is EF.Instrument)
            {
                RentDayCount = (DatePick2.DisplayDate - DateTime.Now).Days;
                var instr = cmbTools.SelectedItem as EF.Instrument;
                //string price = instr.Price.ToString();
                double price = Convert.ToDouble(instr.Price);
                price = price * RentDayCount;
                tbInstPrice.Text = price.ToString();

                instPrice = price;
            }
        }
    }
}
