using Shop.Models;
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
using Shop.Controllers;
using System.Collections;
using System.Threading;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;

namespace Shop.View
{
    public partial class MainWindow : Window
    {
        public bool OrderDelBtnVisibility { get; set; }
        public bool ProductDelBtnVisibility { get; set; }
        public bool UserDelBtnVisibility { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ordersGrid.IsReadOnly = true;
            productsGrid.IsReadOnly = true;
            usersGrid.IsReadOnly = true;
            if (App.Role_Session == "cashier")
            {
                productsGrid.IsReadOnly = true;
                Users_Page.Visibility = Visibility.Hidden;
                CreateProductbtn.Visibility = Visibility.Hidden;

                OrderDelBtnVisibility = false;
                ProductDelBtnVisibility = false;
                UserDelBtnVisibility = false;
            }
            else if (App.Role_Session == "admin")
            {
                OrderDelBtnVisibility = true;
                ProductDelBtnVisibility = true;
                UserDelBtnVisibility = true;
            }
            else if (App.Role_Session == "director")
            {
                OrderDelBtnVisibility = true;
                ProductDelBtnVisibility = true;
                UserDelBtnVisibility = true;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Order> x = await OrderController.selectAll();
            ordersGrid.ItemsSource = x;

            List<Product> x1 = await ProductController.selectAll();
            productsGrid.ItemsSource = x1;

            if (App.Role_Session == "admin" || App.Role_Session == "director")
            {
                List<User> x2 = await UserController.selectAll();
                usersGrid.ItemsSource = x2;
                x2 = null;
            }
            x = null;
            x1 = null;
        }

        private async void CreateOrderbtn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewOrder newWindow = new CreateNewOrder();
            newWindow.ShowDialog();
            List<Order> x = await OrderController.selectAll();
            ordersGrid.ItemsSource = x;
            x = null;
        }

        private async void CreateProductbtn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewProduct newWindow = new CreateNewProduct();
            newWindow.ShowDialog();
            List<Product> x = await ProductController.selectAll();
            productsGrid.ItemsSource = x;
            x = null;
        }

        private async void RegisterUserbtn_Click(object sender, RoutedEventArgs e)
        {
            Register newWindow = new Register();
            newWindow.ShowDialog();
            List<User> x = await UserController.selectAll();
            usersGrid.ItemsSource = x;
            x = null;
        }

        private async void Orders_Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Order> x = await OrderController.selectAll();
            ordersGrid.ItemsSource = x;
            x = null;
        }

        private async void Products_Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Product> x = await ProductController.selectAll();
            productsGrid.ItemsSource = x;
            x = null;
        }

        private async void Users_Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<User> x = await UserController.selectAll();
            usersGrid.ItemsSource = x;
            x = null;
        }
        private async void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Order selectedItem = (Order)button.DataContext;
            ordersDelete(selectedItem);
        }

        private async void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Product selectedItem = (Product)button.DataContext;
            productsDelete(selectedItem);
        }

        private async void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User selectedItem = (User)button.DataContext;
            usersDelete(selectedItem);
        }
        private async void ordersGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (ordersGrid.SelectedItem != null)
                {
                    Order selectedItem = (Order)ordersGrid.SelectedItem;
                    ordersDelete(selectedItem);
                }
            }
        }

        private async void productsGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (productsGrid.SelectedItem != null)
                {
                    Product selectedItem = (Product)productsGrid.SelectedItem;
                    productsDelete(selectedItem);
                }
            }
        }

        private async void usersGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (usersGrid.SelectedItem != null)
                {
                    User selectedItem = (User)usersGrid.SelectedItem;
                    usersDelete(selectedItem);
                }
            }
        }

        private async void ordersDelete(Order order)
        {
            if (App.Role_Session == "admin" || App.Role_Session == "director")
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this product?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    await OrderController.deleteById(order.Id);
                }
            }
            else
            {
                MessageBox.Show("You don`t have permission to perform this operation!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void productsDelete(Product product)
        {
            if (App.Role_Session == "admin" || App.Role_Session == "director")
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this product?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    await ProductController.deleteById(product.Id);
                }
            }
            else
            {
                MessageBox.Show("You don`t have permission to perform this operation!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void usersDelete(User user)
        {
            if ((App.Role_Session == "admin" && user.Role_id == 1) || App.Role_Session == "director")
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    await UserController.deleteById(user.Id);
                }
            }
            else
            {
                MessageBox.Show("You don`t have permission to perform this operation!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void productsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void ordersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void usersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void count_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");
        }

        private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string proposedText = textBox.Text + e.Text;

            if (!Regex.IsMatch(proposedText, @"^[0-9]*(?:\,[0-9]*)?$"))
            {
                e.Handled = true; // Prevents the input from being entered in the TextBox
            }
        }


        private async void UpdateOrderbtn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            err1_product_upd.Content = "";
            err2_product_upd.Content = "";
            err3_product_upd.Content = "";
            err4_product_upd.Content = "";
        }

        private async void UpdateProductbtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void UpdateUserbtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Logout_menu_btn_Click(object sender, RoutedEventArgs e)
        {
            Login newWindow = new Login();
            newWindow.Show();
            App.Login_Session = null;
            App.Fio_Session = null;
            App.Role_Session = null;
            this.Close();
        }

        private void EditProfile_menu_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
