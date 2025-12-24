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

namespace Exam5.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// 
    /// местный бог. Может всё в системе
    /// </summary>
    public partial class AdminPage : Page
    {
        private BuldCompEntities db = new BuldCompEntities();
        private List<Products> products;
        public AdminPage()
        {
            InitializeComponent();
            products = db.Products.Distinct().ToList();
            Adminview.ItemsSource = products;
        }
        private void Searching()
        {
            string searchText = Search.Text.ToLower();
            string selectedCat = Filtr.SelectedItem as string;
            var filtered = products.AsEnumerable();
            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(p => p.Manufacturer.ToLower().Contains(searchText) || 
                p.Category.ToLower().Contains(searchText) ||
                p.Article.ToLower().Contains(searchText));
            }

            if (selectedCat != null && selectedCat != "Все")
            {
                filtered = filtered.Where(p => p.Category.ToLower() == selectedCat);
            }
            Adminview.ItemsSource = filtered;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newProd = new Products {
                    Article = vara.Text,
                    ProductName = varb.Text,
                    Category = "a",
                    Manufacturer = varc.Text,
                    Price = int.Parse(vard.Text),
                    Quantity = 1,
                    Status = ""
                };
                db.Products.Add(newProd);
                db.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Введите нормальные значения");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

      
    }
}
