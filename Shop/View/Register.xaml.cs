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
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        private async void Submit_btn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            err1.Content = "";
            err2.Content = "";
            err3.Text = "";
            err4.Content = "";
            err5.Content = "";

            if (login.Text.Length == 0)
            {
                err1.Content = "Login must be filled!";
                valid = false;
            }
            else if (login.Text.Length < 2)
            {
                err1.Content = "Login must be more than 1 symbol!";
                valid = false;
            }
            else if (login.Text.Length > 20)
            {
                err1.Content = "Login must be less than 20 symbols!";
                valid = false;
            }
            else if (!Regex.IsMatch(login.Text, @"^(?=.*[A-Za-z])[A-Za-z0-9]+$")) {
                err1.Content = "Login must include only Latin characters and, optionally, numbers!";
                valid = false;
            }
            else
            {
                List<User> x = await UserController.selectAll();
                bool find = false;
                foreach (User item in x)
                {
                    if (item.Login == login.Text) {
                        find = true;
                    }
                }
                if (find)
                {
                    err1.Content = "Login already exists!";
                    valid = false;
                }
            }

            if (fio.Text.Length == 0)
            {
                err2.Content = "Fio must be filled!";
                valid = false;
            }
            else if (fio.Text.Length < 5)
            {
                err2.Content = "Fio must be more than 5 symbols!";
                valid = false;
            }

            if (password.Password.Length == 0)
            {
                err3.Text = "Password must be filled!";
                valid = false;
            }
            else if (password.Password.Length < 8)
            {
                err3.Text = "Password must be more than 8 symbols!";
                valid = false;
            }
            else if (!Regex.IsMatch(password.Password, @"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]+$"))
            {
                err3.Text = "Password must contain only Latin characters, at least 1 capital letter and at least 1 number!";
                valid = false;
            }
            else if (password_rep.Password.Length == 0)
            {
                err4.Content = "Repeat password must be filled!";
                valid = false;
            }
            else if (password.Password != password_rep.Password)
            {
                err4.Content = "Passwords must be identical!";
                valid = false;
            }

            if (RoleBox.SelectedItem == null)
            {
                err5.Content = "Role must be selected!";
                valid = false;
            }

            if (valid)
            {
                if (await UserController.Create(login.Text, password.Password, fio.Text, (int)RoleBox.SelectedValue))
                { 
                    this.Close();
                } else
                {
                    err1.Content = "DB server error!";
                }
            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Role> x = await RoleController.selectAll();
            
            if (App.Role_Session == "admin")
            {
                foreach (Role item in x)
                {
                    if (item.Title == "cashier") {
                        RoleBox.Items.Add(item);
                    }
                }
            }
            else if (App.Role_Session == "director") {
                foreach (Role item in x)
                {
                    RoleBox.Items.Add(item);
                }
            }
        }
    }
}
