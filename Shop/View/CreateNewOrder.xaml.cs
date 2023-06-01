using Shop.Controllers;
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

namespace Shop.View
{
    public partial class CreateNewOrder : Window
    {
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

            if (valid)
            {

            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //!
            StatusBox.Items.Add(Status.New);
            StatusBox.Items.Add(Status.Waiting_for_payment);
            StatusBox.Items.Add(Status.Paid);
            StatusBox.Items.Add(Status.Confirmed);
            StatusBox.Items.Add(Status.Awaiting_shipment);
            StatusBox.Items.Add(Status.Delivery_in_progress);
            StatusBox.Items.Add(Status.Delivered);
            StatusBox.Items.Add(Status.Received);
            StatusBox.Items.Add(Status.Completed);
        }
    }
}
