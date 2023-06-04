using Shop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Shop.View
{
    public partial class CreateNewProduct : Window
    {
        public CreateNewProduct()
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

            if (title.Text.Length == 0)
            {
                err1.Content = "Title must be filled!";
                valid = false;
            }
            else if (title.Text.Length > 255)
            {
                err1.Content = "Title must be less than 255 symbols!";
                valid = false;
            }

            if (description.Text.Length == 0)
            {
                err2.Content = "Description must be filled!";
                valid = false;
            }
            else if (description.Text.Length > 1000)
            {
                err2.Content = "Description must be less than 1000 symbols!";
                valid = false;
            }

            if (price.Text.Length == 0)
            {
                err3.Content = "Description must be filled!";
                valid = false;
            }
            else if (price.Text[price.Text.Length-1] == ',')
            {
                err3.Content = "Price must be correct!";
                valid = false;
            }
            else if (Double.Parse(price.Text) == 0)
            {
                err3.Content = "Price must be more than 0!";
                valid = false;
            }

            if (count.Text.Length == 0)
            {
                err4.Content = "Count must be filled!";
                valid = false;
            }
            else if (Int32.Parse(count.Text) == 0)
            {
                err4.Content = "Count must be more than 0!";
                valid = false;
            }

            if (valid)
            {
                if (await ProductController.Create(title.Text, description.Text, Double.Parse(price.Text), Int32.Parse(count.Text)))
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
    }
}
