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

            if (valid)
            {

            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
