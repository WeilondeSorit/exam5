using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Exam5.Pages
{
    public partial class GuestPage : Page
    {
        private List<Products> allProducts;

        public GuestPage()
        {
            InitializeComponent();
            LoadProducts(); // Загружаем элементы при открытии
            LoadCategories(); // Загружаем категории для ComboBox

        }

        private void LoadProducts()
        {
            using (var db = new BuldCompEntities())
            {
                // Берём ВСЕ элементы из коробки
                allProducts = db.Products.ToList();

                // Показываем их в витрине
                ProductsListView.ItemsSource = allProducts;
            }
        }

        private void LoadCategories()
        {
            using (var db = new BuldCompEntities())
            {
                var categories = db.Products
                    .Select(p => p.Category) // Берём только названия категорий
                    .Distinct()              // Убираем повторы
                    .ToList();

                // Очищаем наш ящик с кнопками
                Filter.Items.Clear();

                // Добавляем кнопку "все" первой
                Filter.Items.Add("все");

                // Добавляем остальные кнопки (категории)
                foreach (var category in categories)
                {
                    Filter.Items.Add(category);
                }

                // Выбираем кнопку "все" по умолчанию
                Filter.SelectedIndex = 0;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Когда пишем в поиске - сразу фильтруем
            FilterProducts();
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Когда выбираем категорию - сразу фильтруем
            FilterProducts();
        }

        private void FilterProducts()
        {
            if (allProducts == null) return; // Если нет элементов - выходим

            var searchText = SearchTextBox.Text.ToLower(); // Что написали в поиске
            var selectedCategory = Filter.SelectedItem as string; // Какую категорию выбрали

            // Начинаем с ВСЕХ элементов
            var filtered = allProducts.AsEnumerable();

            // Если что-то написали в поиске...
            if (!string.IsNullOrEmpty(searchText))
            {
                // Ищем элементы, у которых в названии есть наш текст
                filtered = filtered.Where(p =>
                    p.ProductName.ToLower().Contains(searchText) ||
                    p.Category.ToLower().Contains(searchText) ||
                    p.Manufacturer.ToLower().Contains(searchText));
            }

            // Если выбрали не "все", а конкретную категорию...
            if (selectedCategory != null && selectedCategory != "все")
            {
                // Оставляем только элементы этой категории
                filtered = filtered.Where(p => p.Category == selectedCategory);
            }

            // Показываем отфильтрованные элементы
            ProductsListView.ItemsSource = filtered.ToList();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}