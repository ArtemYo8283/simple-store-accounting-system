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
using Shop.Models;

namespace Shop.View
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void lgn_submit_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            err1.Content = "";
            err2.Content = "";
            if (login.Text.Length == 0)
            {
                err1.Content = "Login must be filled!";
                valid = false;
            }
            if (password.Password.Length == 0)
            {
                err2.Content = "Password must be filled!";
                valid = false;
            }

            if (valid)
            {
                if (await AuthController.Login(login.Text, password.Password))
                {
                    MainWindow newWindow = new MainWindow();
                    newWindow.Show();
                    this.Close();
                }
                else
                {
                    err1.Content = "Сredentials are not correct!";
                }
            }
        }
    }
}
