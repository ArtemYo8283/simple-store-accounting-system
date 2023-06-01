using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Shop.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Role(int Id, string Title) { 
            this.Id = Id;
            this.Title = Title;
        }

        public static async Task<List<Role>> selectAll()
        {
            string sql = "SELECT * FROM roles;";
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<Role> result = new List<Role>();
            while (reader.Read())
            {
                int ide = reader.GetInt32("id");
                string name = reader.GetString("title");
                result.Add(new Role(ide, name));
            }
            reader.Close();
            return result;
        }
        public static async Task<List<Role>> SelectById(int id)
        {
            string sql = string.Format("SELECT * FROM roles WHERE id={0};", id);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<Role> result = new List<Role>();
            while (reader.Read())
            {
                int ide = reader.GetInt32("id");
                string name = reader.GetString("title");
                result.Add(new Role(ide, name));
            }
            reader.Close();
            return result;
        }
        public static async Task<int> Create(string title)
        {
            string sql = string.Format("INSERT INTO roles (title) VALUES ('{0}');", title);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        public static async Task<int> Update(string title, int id)
        {
            string sql = string.Format("UPDATE roles SET title='{0}' WHERE id={1};", title, id);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        public static async Task<int> deleteById(int id)
        {
            string sql = string.Format("DELETE FROM roles WHERE id={0};", id);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
    }
}
