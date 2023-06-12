using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }

        public Product(int Id, string Title, string Description, double Price, int Count)
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.Price = Price;
            this.Count = Count;
        }
        public static async Task<List<Product>> selectAll()
        {
            string sql = "SELECT * FROM products;";
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<Product> result = new List<Product>();
            while (reader.Read())
            {
                int ide = reader.GetInt32("id");
                string title = reader.GetString("title");
                string description = reader.GetString("description");
                double price = reader.GetDouble("price");
                int count = reader.GetInt32("count");
                result.Add(new Product(ide, title, description, price, count));
            }
            reader.Close();
            return result;
        }
        public static async Task<List<Product>> SelectById(int id)
        {
            string sql = string.Format("SELECT * FROM products WHERE id={0};", id);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<Product> result = new List<Product>();
            while (reader.Read())
            {
                int ide = reader.GetInt32("id");
                string title = reader.GetString("title");
                string description = reader.GetString("description");
                double price = reader.GetDouble("price");
                int count = reader.GetInt32("count");
                result.Add(new Product(ide, title, description, price, count));
            }
            reader.Close();
            return result;
        }
        public static async Task<List<Product>> SelectByOrderId(int id)
        {
            string sql = string.Format("SELECT products.id, products.title, products.description, products.price, order_product.product_count FROM products INNER JOIN order_product ON order_product.order_id = {0} AND products.id = order_product.product_id;", id);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<Product> result = new List<Product>();
            while (reader.Read())
            {
                int ide = reader.GetInt32("id");
                string title = reader.GetString("title");
                string description = reader.GetString("description");
                double price = reader.GetDouble("price");
                int count = reader.GetInt32("product_count");
                result.Add(new Product(ide, title, description, price, count));
            }
            reader.Close();
            return result;
        }
        public static async Task<int> Create(string title, string description, double price, int count)
        {
            string sql = string.Format("INSERT INTO products (title, description, price, count) VALUES ('{0}', '{1}', '{2}', {3});", title, description, price.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture), count);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        public static async Task<int> Create_ListOfPRoduct(int order_id, int product_id, int product_count)
        {
            string sql = string.Format("INSERT INTO order_product (order_id, product_id, product_count) VALUES ('{0}', '{1}', '{2}');", order_id, product_id, product_count);
            MessageBox.Show(sql, "Message Box Title", MessageBoxButton.OK, MessageBoxImage.Information);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        public static async Task<int> Update(Dictionary<string, Object> field, int id)
        {
            string sql = "";
            foreach (var item in field)
            {
                sql += string.Format("UPDATE products SET {0}='{1}' WHERE id={2};", item.Key, item.Value, id);
            }
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        public static async Task<int> deleteById(int id)
        {
            string sql = string.Format("DELETE FROM products WHERE id={0};", id);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
    }
}
