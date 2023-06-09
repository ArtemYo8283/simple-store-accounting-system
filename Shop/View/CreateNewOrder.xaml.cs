using Shop.Controllers;
using Shop.Models;
using System;
using System.Collections.Generic;
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

namespace Shop.View
{
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
    public partial class CreateNewOrder : Window
    {
        List<Product> selectedProducts;
        public CreateNewOrder()
        {
            InitializeComponent();
        }

        private async void Submit_btn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            err1.Content = "";
            err2.Content = "";
            err3.Content = "";
            err4.Content = "";
            err5.Content = "";
            if (fio.Text.Length == 0)
            {
                err1.Content = "Fio must be filled!";
                valid = false;
            }
            else if (fio.Text.Length > 500)
            {
                err1.Content = "Fio must be less than 500 symbols!";
                valid = false;
            }

            if (phone.Text.Length == 0)
            {
                err2.Content = "Phone must be filled!";
                valid = false;
            }
            else if (phone.Text.Length > 500)
            {
                err2.Content = "Phone must be less than 500 symbols!";
                valid = false;
            }
            else if (!Regex.IsMatch(phone.Text, @"^\+?\d{1,3}\s?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$"))
            {
                err2.Content = "Phone must be correct!";
                valid = false;
            }

            if (email.Text.Length == 0)
            {
                err3.Content = "Email must be filled!";
                valid = false;
            }
            else if (email.Text.Length > 500)
            {
                err3.Content = "Email must be less than 50 symbols!";
                valid = false;
            }
            else if (!Regex.IsMatch(email.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                err3.Content = "Email must be correct!";
                valid = false;
            }

            if (address.Text.Length == 0)
            {
                err4.Content = "Address must be filled!";
                valid = false;
            }
            else if (address.Text.Length > 500)
            {
                err4.Content = "Address must be less than 50 symbols!";
                valid = false;
            }

            if (StatusBox.SelectedItem == null)
            {
                err5.Content = "Status must be selected!";
                valid = false;
            }

            if (valid)
            {
                if (await OrderController.Create(fio.Text, phone.Text, email.Text, address.Text, (Status)(int)StatusBox.SelectedValue, selectedProducts))
                {
                    this.Close();
                }
                else
                {
                    err1.Content = "DB server error!";
                }
            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

            StatusBox.ItemsSource = items;
        }

        private void CreateListOrderProductUpdbtn_Click(object sender, RoutedEventArgs e)
        {
            CreateListOfOrderProducts newWindow = new CreateListOfOrderProducts();
            newWindow.ShowDialog();
            selectedProducts = newWindow.SelectedProducts;
        }
    }
}
