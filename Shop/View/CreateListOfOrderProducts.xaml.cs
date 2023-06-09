using Shop.Controllers;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static Google.Protobuf.WellKnownTypes.Field.Types;
using static iTextSharp.text.pdf.AcroFields;

namespace Shop.View
{
    /// <summary>
    /// Interaction logic for CreateListOfOrderProducts.xaml
    /// </summary>
    public partial class CreateListOfOrderProducts : Window
    {
        public List<Product> SelectedProducts { get; set; }
        public CreateListOfOrderProducts()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Product> x = await ProductController.selectAll();
            foreach (Product product in x)
            {
                productsEnterGrid.Items.Add(product);
            }
            AddItemBtn.IsEnabled = false;
            RemoveItemBtn.IsEnabled = false;
        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            err.Content = "";
            Product selectedItem = (Product)productsEnterGrid.SelectedItem;
            if (selectedItem != null)
            {
                if (count_product.Text.Length == 0)
                {
                    err.Content = "Count must be filled!";
                    valid = false;
                }
                else if (Int32.Parse(count_product.Text) == 0)
                {
                    err.Content = "Count must be more than 0!";
                    valid = false;
                }
                else if (selectedItem.Count < Int32.Parse(count_product.Text))
                {
                    err.Content = "Count must be less than in db!";
                    valid = false;
                }

                if (valid)
                {
                    selectedItem.Count -= Int32.Parse(count_product.Text);
                    productsEnterGrid.SelectedItem = selectedItem;
                    bool find = false;
                    int id = 0;
                    foreach (var item in productsOutGrid.Items)
                    {
                        if (item is Product product)
                        {
                            if (product.Id == selectedItem.Id)
                            {
                                id = productsOutGrid.Items.IndexOf(item);
                                find = true;
                            }
                        }
                    }
                    if (find)
                    {
                        Product product = (Product)productsOutGrid.Items[id];
                        product.Count += Int32.Parse(count_product.Text);
                        productsOutGrid.Items[id] = product;
                    }
                    else 
                    {
                        Product product = new Product(selectedItem.Id, selectedItem.Title, selectedItem.Description, selectedItem.Price, Int32.Parse(count_product.Text));
                        productsOutGrid.Items.Add(product);
                    }
                    productsOutGrid.Items.Refresh();
                    productsEnterGrid.Items.Refresh();
                }
            }
        }

        private void RemoveItemBtn_Click(object sender, RoutedEventArgs e)
        {
            Product selectedItem = (Product)productsOutGrid.SelectedItem;
            for (int i = 0; i < productsEnterGrid.Items.Count; i++)
            {
                Product tmp = (Product)productsEnterGrid.Items[i];
                if (selectedItem.Id == tmp.Id)
                {
                    tmp.Count += selectedItem.Count;
                    productsEnterGrid.Items[i] = tmp;
                }
            }
            productsOutGrid.Items.Remove(selectedItem);
            productsOutGrid.Items.Refresh();
            productsEnterGrid.Items.Refresh();
        }

        private void RemoveAllItemBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in productsOutGrid.Items)
            {
                if (item is Product product)
                {
                    for (int i = 0; i < productsEnterGrid.Items.Count; i++)
                    {
                        Product tmp = (Product)productsEnterGrid.Items[i];
                        if (product.Id == tmp.Id) 
                        {
                            tmp.Count += product.Count;
                            productsEnterGrid.Items[i] = tmp;
                        }
                    }
                }
            }
            productsOutGrid.Items.Clear();
            productsEnterGrid.Items.Refresh();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Product> x = new List<Product>();
            foreach (var item in productsOutGrid.Items)
            {
                if (item is Product product)
                {
                    x.Add(product);
                }
            }
            SelectedProducts = x;
            this.Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close the window? Changes will not be saved", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void productsEnterGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product selectedItem = (Product)productsEnterGrid.SelectedItem;
            if (selectedItem != null)
            {
                AddItemBtn.IsEnabled = true;
                RemoveItemBtn.IsEnabled = false;
            }
            else
            {
                AddItemBtn.IsEnabled = false;
                RemoveItemBtn.IsEnabled = false;
            }
        }

        private void productsOutGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product selectedItem = (Product)productsOutGrid.SelectedItem;
            if (selectedItem != null)
            {
                AddItemBtn.IsEnabled = false;
                RemoveItemBtn.IsEnabled = true;
            }
            else
            {
                AddItemBtn.IsEnabled = false;
                RemoveItemBtn.IsEnabled = false;
            }
        }
        private void count_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = searchTextBox.Text;
            productsEnterGrid.Items.Filter = item =>
            {
                Product product = item as Product;
                return product.Title.ToLower().Contains(searchTerm.ToLower());
            };
            productsEnterGrid.Items.Refresh();
        }
    }
}
