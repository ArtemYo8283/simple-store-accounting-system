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
    /// <summary>
    /// Interaction logic for OrderReceipt.xaml
    /// </summary>
    public partial class OrderReceipt : Window
    {
        public OrderReceipt()
        {
            InitializeComponent();
        }

        private void Print_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrintToPDF_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrintToTXT_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrintToExcel_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
