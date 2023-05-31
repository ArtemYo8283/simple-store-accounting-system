﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return result;
        }
        public static async Task<List<Product>> SelectByOrderId(int id)
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
            return result;
        }
        public static async Task<int> Create(string title, string description, double price, int count)
        {
            string sql = string.Format("INSERT INTO products (title, description, price, count) VALUES ('{0}', '{1}', '{2}', {3});", title, description, price, count);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        public static async Task<int> Update(Dictionary<string, Object> field)
        {
            string sql = "";
            foreach (var item in field)
            {
                sql += string.Format("UPDATE products SET {0}='{1}';", item.Key, item.Value);
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