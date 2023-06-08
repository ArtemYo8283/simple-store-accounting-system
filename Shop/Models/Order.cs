using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Shop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{

    public class Order
    {
        public int Id { get; set; }
        public string Fio_client { get; set; }
        public string Phone_client { get; set; }
        public string Email_client { get; set; }
        public string Address_client { get; set; }
        public DateTime Last_upd_date { get; set; }
        public Status status { get; set; }
        public List<Product> products { get; set; }

        public Order(int Id, string Fio_client, string Phone_client, string Email_client, string Address_client, DateTime Last_upd_date, Status status)
        {
            this.Id = Id;
            this.Fio_client = Fio_client;
            this.Phone_client = Phone_client;
            this.Email_client = Email_client;
            this.Address_client = Address_client;
            this.Last_upd_date = Last_upd_date;
            this.status = status;
        }
        public static async Task<List<Order>> selectAll()
        {
            string sql = "SELECT * FROM orders;";
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<Order> result = new List<Order>();
            while (reader.Read())
            {
                int ide = reader.GetInt32("id");
                string fio_client = reader.GetString("fio_client");
                string phone_client = reader.GetString("phone_client");
                string email_client = reader.GetString("email_client");
                string address_client = reader.GetString("address_client");
                DateTime last_upd_date = reader.GetDateTime("last_upd_date");
                string st = reader.GetString("status");
                result.Add(new Order(ide, fio_client, phone_client, email_client, address_client, last_upd_date, Utils.StringToStatus(st)));
            }
            reader.Close();
            foreach (Order item in result)
            {
                List<Product> products = await ProductController.SelectByOrderId(item.Id);
                item.products = products;
            }
            return result;
        }
        public static async Task<List<Order>> SelectById(int id)
        {
            string sql = string.Format("SELECT * FROM orders WHERE id={0};", id);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<Order> result = new List<Order>();
            while (reader.Read())
            {
                int ide = reader.GetInt32("id");
                string fio_client = reader.GetString("login");
                string phone_client = reader.GetString("password");
                string email_client = reader.GetString("fio");
                string address_client = reader.GetString("fio");
                DateTime last_upd_date = reader.GetDateTime("last_upd_date");
                string st = reader.GetString("status");
                result.Add(new Order(ide, fio_client, phone_client, email_client, address_client, last_upd_date, Utils.StringToStatus(st)));
            }
            reader.Close();
            foreach (Order item in result)
            {
                List<Product> products = await ProductController.SelectByOrderId(item.Id);
                item.products = products;
            }
            return result;
        }
        public static async Task<int> Create(string fio_client, string phone_client, string email_client, string address_client, string last_upd_date, Status st)
        {
            string sql = string.Format("INSERT INTO orders (fio_client, phone_client, email_client, address_client,last_upd_date, status) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", fio_client, phone_client, email_client, address_client, last_upd_date, Utils.StatusToString(st));
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        public static async Task<int> Update(Dictionary<string, Object> field, int id)
        {
            string sql = "";
            foreach (var item in field)
            {
                sql += string.Format("UPDATE orders SET {0}='{1}' WHERE id={2};", item.Key, item.Value, id);
            }
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        public static async Task<int> deleteById(int id)
        {
            string sql = string.Format("DELETE FROM orders WHERE id={0};", id);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
    }
}
