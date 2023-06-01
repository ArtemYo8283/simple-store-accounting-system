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

namespace Shop.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (App.Role_Session == "cashier") {
                productsGrid.IsReadOnly = true;
                Users_Page.Visibility = Visibility.Hidden;
                CreateProductbtn.Visibility = Visibility.Hidden;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Role> x = await RoleController.selectAll();
            ordersGrid.ItemsSource = x;

            List<Product> x1 = await ProductController.selectAll();
            productsGrid.ItemsSource = x1;

            if (App.Role_Session == "admin" || App.Role_Session == "director")
            {
                List<User> x2 = await UserController.selectAll();
                usersGrid.ItemsSource = x2;
            }
        }

        private void CreateOrderbtn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewOrder newWindow = new CreateNewOrder();
            newWindow.ShowDialog();
        }

        private void CreateProductbtn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewProduct newWindow = new CreateNewProduct();
            newWindow.ShowDialog();
        }

        private void RegisterUserbtn_Click(object sender, RoutedEventArgs e)
        {
            Register newWindow = new Register();
            newWindow.ShowDialog();
        }

    }
}
