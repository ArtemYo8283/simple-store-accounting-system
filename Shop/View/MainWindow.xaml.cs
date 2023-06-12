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
using System.Windows.Controls.Primitives;

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
            Orders_grid_upd();

            Products_grid_upd();

            if (App.Role_Session == "admin" || App.Role_Session == "director")
            {
                Users_grid_upd();
            }
        }

        //Page load
        private async void Orders_Page_Loaded(object sender, RoutedEventArgs e)
        {
            Orders_grid_upd();
        }

        private async void Products_Page_Loaded(object sender, RoutedEventArgs e)
        {
            Products_grid_upd();
        }

        private async void Users_Page_Loaded(object sender, RoutedEventArgs e)
        {
            Users_grid_upd();
        }
        //Update data at data grid
        private async void Orders_grid_upd()
        {
            List<Order> x = await OrderController.selectAll();
            ordersGrid.ItemsSource = x;
            x = null;
            clear_fields(1, 2);
        }

        private async void Products_grid_upd()
        {
            List<Product> x = await ProductController.selectAll();
            productsGrid.ItemsSource = x;
            x = null;
            clear_fields(2, 2);
        }

        private async void Users_grid_upd()
        {
            List<User> x = await UserController.selectAll();
            usersGrid.ItemsSource = x;
            x = null;
            clear_fields(3, 2);
        }
        //Create items button
        private async void CreateOrderbtn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewOrder newWindow = new CreateNewOrder();
            newWindow.ShowDialog();
            Orders_grid_upd();
        }

        private async void CreateProductbtn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewProduct newWindow = new CreateNewProduct();
            newWindow.ShowDialog();
            Products_grid_upd();
        }

        private async void RegisterUserbtn_Click(object sender, RoutedEventArgs e)
        {
            Register newWindow = new Register();
            newWindow.ShowDialog();
            Users_grid_upd();
        }
        //Delete button
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
        //Update button
        private async void UpdateOrderbtn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            clear_fields(1, 3);
            if (fio_order_upd.Text.Length == 0)
            {
                err1_order_upd.Content = "Fio must be filled!";
                valid = false;
            }
            else if (fio_order_upd.Text.Length > 500)
            {
                err1_order_upd.Content = "Fio must be less than 500 symbols!";
                valid = false;
            }

            if (phone_order_upd.Text.Length == 0)
            {
                err2_order_upd.Content = "Phone must be filled!";
                valid = false;
            }
            else if (phone_order_upd.Text.Length > 500)
            {
                err2_order_upd.Content = "Phone must be less than 500 symbols!";
                valid = false;
            }
            else if (!Regex.IsMatch(phone_order_upd.Text, @"^\+?\d{1,3}\s?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$"))
            {
                err2_order_upd.Content = "Phone must be correct!";
                valid = false;
            }

            if (email_order_upd.Text.Length == 0)
            {
                err3_order_upd.Content = "Email must be filled!";
                valid = false;
            }
            else if (email_order_upd.Text.Length > 500)
            {
                err3_order_upd.Content = "Email must be less than 50 symbols!";
                valid = false;
            }
            else if (!Regex.IsMatch(email_order_upd.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                err3_order_upd.Content = "Email must be correct!";
                valid = false;
            }

            if (address_order_upd.Text.Length == 0)
            {
                err4_order_upd.Content = "Address must be filled!";
                valid = false;
            }
            else if (address_order_upd.Text.Length > 500)
            {
                err4_order_upd.Content = "Address must be less than 50 symbols!";
                valid = false;
            }

            if (StatusBox_order_upd.SelectedItem == null)
            {
                err5_order_upd.Content = "Status must be selected!";
                valid = false;
            }
            if (valid)
            {
                Order selectedItem = (Order)ordersGrid.SelectedItem;
                Dictionary<string, object> x = new Dictionary<string, object>();
                x.Add("fio_client", fio_order_upd.Text);
                x.Add("phone_client", phone_order_upd.Text);
                x.Add("email_client", email_order_upd.Text);
                x.Add("address_client", address_order_upd.Text);
                x.Add("status", Utils.StatusToString((Status)(int)StatusBox_order_upd.SelectedValue));
                await OrderController.Update(x, selectedItem.Id);
                clear_fields(1, 2);
                Orders_grid_upd();
            }
        }

        private async void UpdateProductbtn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            clear_fields(2, 3);
            if (title_product_upd.Text.Length == 0)
            {
                err1_product_upd.Content = "Title must be filled!";
                valid = false;
            }
            else if (title_product_upd.Text.Length > 255)
            {
                err1_product_upd.Content = "Title must be less than 255 symbols!";
                valid = false;
            }

            if (description_product_upd.Text.Length == 0)
            {
                err2_product_upd.Content = "Description must be filled!";
                valid = false;
            }
            else if (description_product_upd.Text.Length > 1000)
            {
                err2_product_upd.Content = "Description must be less than 1000 symbols!";
                valid = false;
            }

            if (price_product_upd.Text.Length == 0)
            {
                err3_product_upd.Content = "Description must be filled!";
                valid = false;
            }
            else if (price_product_upd.Text[price_product_upd.Text.Length - 1] == ',')
            {
                err3_product_upd.Content = "Price must be correct!";
                valid = false;
            }
            else if (Double.Parse(price_product_upd.Text) == 0)
            {
                err3_product_upd.Content = "Price must be more than 0!";
                valid = false;
            }

            if (count_product_upd.Text.Length == 0)
            {
                err4_product_upd.Content = "Count must be filled!";
                valid = false;
            }
            else if (Int32.Parse(count_product_upd.Text) == 0)
            {
                err4_product_upd.Content = "Count must be more than 0!";
                valid = false;
            }
            if (valid)
            {
                Product selectedItem = (Product)productsGrid.SelectedItem;
                Dictionary<string, object> x = new Dictionary<string, object>();
                x.Add("title", title_product_upd.Text);
                x.Add("description", description_product_upd.Text);
                x.Add("price", Double.Parse(price_product_upd.Text).ToString("0." +
                    "##", System.Globalization.CultureInfo.InvariantCulture));
                x.Add("count", Int32.Parse(count_product_upd.Text));
                await ProductController.Update(x, selectedItem.Id);
                clear_fields(2, 2);
                Products_grid_upd();
            }
        }

        private async void UpdateUserbtn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            clear_fields(3, 3);
            User selectedItem = (User)usersGrid.SelectedItem;
            if (login_user_upd.Text.Length == 0)
            {
                err1_user_upd.Content = "Login must be filled!";
                valid = false;
            }
            else if (login_user_upd.Text.Length < 2)
            {
                err1_user_upd.Content = "Login must be more than 1 symbol!";
                valid = false;
            }
            else if (login_user_upd.Text.Length > 20)
            {
                err1_user_upd.Content = "Login must be less than 20 symbols!";
                valid = false;
            }
            else if (!Regex.IsMatch(login_user_upd.Text, @"^(?=.*[A-Za-z])[A-Za-z0-9]+$"))
            {
                err1_user_upd.Content = "Login must include only Latin characters and, optionally, numbers!";
                valid = false;
            }
            else
            {
                List<User> x = await UserController.selectAll();
                bool find = false;
                foreach (User item in x)
                {
                    if (item.Login == login_user_upd.Text && selectedItem.Login != login_user_upd.Text)
                    {
                        find = true;
                    }
                }
                if (find)
                {
                    err1_user_upd.Content = "Login already exists!";
                    valid = false;
                }
            }

            if (fio_user_upd.Text.Length == 0)
            {
                err2_user_upd.Content = "Fio must be filled!";
                valid = false;
            }
            else if (fio_user_upd.Text.Length < 5)
            {
                err2_user_upd.Content = "Fio must be more than 5 symbols!";
                valid = false;
            }

            if (password_user_upd.Password.Length != 0)
            {
                if (password_user_upd.Password.Length < 8)
                {
                    err3_user_upd.Text = "Password must be more than 8 symbols!";
                    valid = false;
                }
                else if (!Regex.IsMatch(password_user_upd.Password, @"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]+$"))
                {
                    err3_user_upd.Text = "Password must contain only Latin characters, at least 1 capital letter and at least 1 number!";
                    valid = false;
                }
                else if (password_rep_user_upd.Password.Length == 0)
                {
                    err4_user_upd.Content = "Repeat password must be filled!";
                    valid = false;
                }
                else if (password_user_upd.Password != password_rep_user_upd.Password)
                {
                    err4_user_upd.Content = "Passwords must be identical!";
                    valid = false;
                }
            }
            else if (password_rep_user_upd.Password.Length != 0)
            {
                err4_user_upd.Content = "Passwords must be identical!";
                valid = false;
            }
           

            if (RoleBox_user_upd.SelectedItem == null)
            {
                err5_user_upd.Content = "Role must be selected!";
                valid = false;
            }

            if (valid)
            {
                Dictionary<string, object> x = new Dictionary<string, object>();
                x.Add("login", login_user_upd.Text);
                x.Add("fio", fio_user_upd.Text);
                if (password_user_upd.Password.Length != 0) {
                    x.Add("password", await Utils.HashPsw(password_user_upd.Password));
                }

                if (App.Role_Session == "director") {
                    x.Add("role_id", (int)RoleBox_user_upd.SelectedValue);
                }
                else if (App.Role_Session == "admin" && (int)RoleBox_user_upd.SelectedValue > 1)
                {
                    x.Add("role_id", (int)RoleBox_user_upd.SelectedValue);
                }
                await UserController.Update(x, selectedItem.Id);
                clear_fields(3, 2);
                Users_grid_upd();
            }
        }
        //Logout btn
        private void Logout_menu_btn_Click(object sender, RoutedEventArgs e)
        {
            Login newWindow = new Login();
            newWindow.Show();
            App.Login_Session = null;
            App.Fio_Session = null;
            App.Role_Session = null;
            this.Close();
        }
        private void OrderReceiptbtn_Click(object sender, RoutedEventArgs e)
        {
            Order selectedItem = (Order)ordersGrid.SelectedItem;
            OrderReceipt newWindow = new OrderReceipt(selectedItem);
            newWindow.ShowDialog();
        }
        private void EditListOrderProductUpdbtn_Click(object sender, RoutedEventArgs e)
        {
            Order selectedItem = (Order)ordersGrid.SelectedItem;
            EditListOfOrderProducts newWindow = new EditListOfOrderProducts(selectedItem);
            newWindow.ShowDialog();
            
        }
        private async void ordersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order selectedItem = (Order)ordersGrid.SelectedItem;
            if (selectedItem != null)
            {
                fio_order_upd.Text = selectedItem.Fio_client;
                phone_order_upd.Text = selectedItem.Phone_client;
                email_order_upd.Text = selectedItem.Email_client;
                address_order_upd.Text = selectedItem.Address_client;
                List<ComboBoxItem> items = new List<ComboBoxItem>
                {
                    new ComboBoxItem { Text = "New", Value = 0 },
                    new ComboBoxItem { Text = "Waiting for payment", Value = 1 },
                    new ComboBoxItem { Text = "Paid", Value = 2 },
                    new ComboBoxItem { Text = "Confirmed", Value = 3 },
                    new ComboBoxItem { Text = "Awaiting shipment", Value = 4 },
                    new ComboBoxItem { Text = "Delivery in progress", Value = 5 },
                    new ComboBoxItem { Text = "Delivered", Value = 6 },
                    new ComboBoxItem { Text = "Received", Value = 7 },
                    new ComboBoxItem { Text = "Completed", Value = 8 },
                    new ComboBoxItem { Text = "Canceled", Value = 9 }
                };
                StatusBox_order_upd.ItemsSource = items;
                foreach (ComboBoxItem item in StatusBox_order_upd.Items)
                {
                    if ((Status)(int)item.Value == selectedItem.status)
                    {
                        StatusBox_order_upd.SelectedItem = item;
                        break;
                    }
                }
                clear_fields(1, 3);
                clear_fields(1, 1);
                if (selectedItem.products.Count == 0)
                {
                    EditListOrderProductUpdbtn.IsEnabled = false;
                }
            }
            else
            {
                clear_fields(1, 2);
            }
        }

        private async void productsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product selectedItem = (Product)productsGrid.SelectedItem;
            if (selectedItem != null)
            {
                title_product_upd.Text = selectedItem.Title;
                description_product_upd.Text = selectedItem.Description;
                price_product_upd.Text = selectedItem.Price.ToString();
                count_product_upd.Text = selectedItem.Count.ToString();
                if (App.Role_Session != "cashier")
                {
                    clear_fields(2, 1);
                }
            }
            else
            {
                clear_fields(2, 3);
                clear_fields(2, 2);
            }
        }

        private async void usersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selectedItem = (User)usersGrid.SelectedItem;
            if (selectedItem != null)
            {
                login_user_upd.Text = selectedItem.Login;
                fio_user_upd.Text = selectedItem.Fio;

                List<Role> x = await RoleController.selectAll();
                RoleBox_user_upd.Items.Clear();
                foreach (Role item in x)
                {
                    RoleBox_user_upd.Items.Add(item);
                }
                foreach (Role item in RoleBox_user_upd.Items)
                {
                    if (item.Id == selectedItem.Role_id)
                    {
                        RoleBox_user_upd.SelectedItem = item;
                        break;
                    }
                }
                if (App.Role_Session == "admin" && selectedItem.Role_id > 1)
                {
                    login_user_upd.IsEnabled = false;
                    fio_user_upd.IsEnabled = false;
                    password_user_upd.IsEnabled = false;
                    password_rep_user_upd.IsEnabled = false;
                    RoleBox_user_upd.IsEnabled = false;
                    UpdateUserbtn.IsEnabled = false;
                }
                else 
                {
                    clear_fields(3, 1);
                }
            }
            else
            {
                clear_fields(3, 3);
                clear_fields(3, 2);
            }
        }
        //Keyboard inputs
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
        private void clear_fields(int page, int section)
        { 
            switch (page)
            {
                case 1:
                    switch (section) {
                        case 1:
                            fio_order_upd.IsEnabled = true;
                            phone_order_upd.IsEnabled = true;
                            email_order_upd.IsEnabled = true;
                            address_order_upd.IsEnabled = true;
                            StatusBox_order_upd.IsEnabled = true;
                            UpdateOrderbtn.IsEnabled = true;
                            OrderReceiptbtn.IsEnabled = true;
                            EditListOrderProductUpdbtn.IsEnabled = true;
                            break;
                        case 2:
                            fio_order_upd.Text = "";
                            phone_order_upd.Text = "";
                            email_order_upd.Text = "";
                            address_order_upd.Text = "";
                            StatusBox_order_upd.ItemsSource = null;
                            fio_order_upd.IsEnabled = false;
                            phone_order_upd.IsEnabled = false;
                            email_order_upd.IsEnabled = false;
                            address_order_upd.IsEnabled = false;
                            StatusBox_order_upd.IsEnabled = false;
                            UpdateOrderbtn.IsEnabled = false;
                            OrderReceiptbtn.IsEnabled = false;
                            EditListOrderProductUpdbtn.IsEnabled = false;
                            break;
                        case 3:
                            err1_order_upd.Content = "";
                            err2_order_upd.Content = "";
                            err3_order_upd.Content = "";
                            err4_order_upd.Content = "";
                            err5_order_upd.Content = "";
                            break;
                    }
                    break;
                case 2:
                    switch (section)
                    {
                        case 1:
                            title_product_upd.IsEnabled = true;
                            description_product_upd.IsEnabled = true;
                            price_product_upd.IsEnabled = true;
                            count_product_upd.IsEnabled = true;
                            UpdateProductbtn.IsEnabled = true;
                            break;
                        case 2:
                            title_product_upd.Text = "";
                            description_product_upd.Text = "";
                            price_product_upd.Text = "";
                            count_product_upd.Text = "";
                            title_product_upd.IsEnabled = false;
                            description_product_upd.IsEnabled = false;
                            price_product_upd.IsEnabled = false;
                            count_product_upd.IsEnabled = false;
                            UpdateProductbtn.IsEnabled = false;
                            break;
                        case 3:
                            err1_product_upd.Content = "";
                            err2_product_upd.Content = "";
                            err3_product_upd.Content = "";
                            err4_product_upd.Content = "";
                            break;
                    }
                    break;
                case 3:
                    switch (section)
                    {
                        case 1:
                            login_user_upd.IsEnabled = true;
                            fio_user_upd.IsEnabled = true;
                            password_user_upd.IsEnabled = true;
                            password_rep_user_upd.IsEnabled = true;
                            RoleBox_user_upd.IsEnabled = true;
                            UpdateUserbtn.IsEnabled = true;
                            break;
                        case 2:
                            login_user_upd.Text = "";
                            fio_user_upd.Text = "";
                            password_user_upd.Password = "";
                            password_rep_user_upd.Password = "";
                            RoleBox_user_upd.Items.Clear();
                            login_user_upd.IsEnabled = false;
                            fio_user_upd.IsEnabled = false;
                            password_user_upd.IsEnabled = false;
                            password_rep_user_upd.IsEnabled = false;
                            RoleBox_user_upd.IsEnabled = false;
                            UpdateUserbtn.IsEnabled = false;
                            break;
                        case 3:
                            err1_user_upd.Content = "";
                            err2_user_upd.Content = "";
                            err3_user_upd.Text = "";
                            err4_user_upd.Content = "";
                            err5_user_upd.Content = "";
                            break;
                    }
                    break;
            }
        }
        //Delete func
        private async void ordersDelete(Order order)
        {
            if (App.Role_Session == "admin" || App.Role_Session == "director")
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this product?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    await OrderController.deleteById(order.Id);
                    Orders_grid_upd();
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
                    Products_grid_upd();
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
                    Users_grid_upd();
                }
            }
            else
            {
                MessageBox.Show("You don`t have permission to perform this operation!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void EditProfile_menu_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void title_product_upd_TextChanged(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show("You don`t have permission to perform this operation!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
