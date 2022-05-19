using System;
using System.Collections.Generic;
using System.IO;
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
using ArendaProject.ClassHelper;
using ArendaProject.EF;
using Microsoft.Win32;

namespace ArendaProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private List<TypeProduct> types = new List<TypeProduct>();
        EF.Entities content = new EF.Entities();
        string fileName = null;
        public AddProductWindow()
        {
            InitializeComponent();
            types = AppData.Context.TypeProduct.ToList();
            cmbType.DisplayMemberPath = "TypeName";
            cmbType.ItemsSource = types;

            DateTime D = DateTime.Now;
            dpStart.Text = D.ToString();

            dpDeath.SelectedDate = DateTime.Today.AddDays(1);
            dpDeath.DisplayDateStart = DateTime.Today.AddDays(1);
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            //check
            #region check

           
            if (tbName.Text == "Название инструмента")
            {
                MessageBox.Show("Некоторые поля не заполнены");
                return;
            }
            if(tbPrice.Text == "Цена")
            {
                MessageBox.Show("Некоторые поля не заполнены");
                return;
            }
            if(Convert.ToInt32(tbPrice.Text) <= 0)
            {
                MessageBox.Show("Укажите цену > 0");
                return;
            }
            if(cmbUse.SelectedIndex != 0 && cmbUse.SelectedIndex != 1)
            {
                MessageBox.Show("Выберите использование");
                return;
            }
            if (cmbType.SelectedIndex != 0 && cmbType.SelectedIndex != 1)
            {
                MessageBox.Show("Выберите тип");
                return;
            }
            #endregion

            bool inuse = false;
            if(cmbUse.SelectedIndex == 0)
            {
                inuse = false;
            }
            if (cmbUse.SelectedIndex == 1)
            {
                inuse = true;
            }

            content.Instrument.Add(new Instrument{

                InstrumentName = tbName.Text,
                InUse = inuse,
                idTypeProduct = cmbType.SelectedIndex + 1,
                Price = Convert.ToInt32(tbPrice.Text),
                InstrumentLifeStart = dpStart.DisplayDate,
                InstrumentLifeDead = dpDeath.DisplayDate,
                Photo = File.ReadAllBytes(fileName)

            });
            MessageBox.Show("Инструмент добавлен!");
            content.SaveChanges();
            this.Close();
        }

        private void tbName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "Название инструмента")
            { tbName.Clear(); }

            var bc = new BrushConverter();

            tbName.Foreground = (Brush)bc.ConvertFrom("#E23E57");
            tbName.FontStyle = FontStyles.Normal;
            tbName.BorderBrush = (Brush)bc.ConvertFrom("#E23E57");
        }

        private void tbName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "Название инструмента" || string.IsNullOrWhiteSpace(tbName.Text))
            {
                tbName.Text = "Название инструмента";

                var bc = new BrushConverter();
                tbName.Foreground = (Brush)bc.ConvertFrom("#88304E");
                tbName.FontStyle = FontStyles.Oblique;
                tbName.BorderBrush = (Brush)bc.ConvertFrom("#522546");
            }
        }

        private void tbPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPrice.Text == "Цена")
            { tbPrice.Clear(); }

            var bc = new BrushConverter();

            tbPrice.Foreground = (Brush)bc.ConvertFrom("#E23E57");
            tbPrice.FontStyle = FontStyles.Normal;
            tbPrice.BorderBrush = (Brush)bc.ConvertFrom("#E23E57");
        }

        private void tbPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbPrice.Text == "Цена" || string.IsNullOrWhiteSpace(tbPrice.Text))
            {
                tbPrice.Text = "Цена";

                var bc = new BrushConverter();
                tbPrice.Foreground = (Brush)bc.ConvertFrom("#88304E");
                tbPrice.FontStyle = FontStyles.Oblique;
                tbPrice.BorderBrush = (Brush)bc.ConvertFrom("#522546");
            }
        }

        private void tbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!int.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
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
