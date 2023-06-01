using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Fio { get; set; }
        public DateTime Last_login_date { get; set; }
        public int Role_id { get; set; }

        public User(int Id, string Login, string Password, string Fio, DateTime Last_login_date, int Role_id)
        {
            this.Id = Id;
            this.Login = Login;
            this.Password = Password;
            this.Fio = Fio;
            this.Last_login_date = Last_login_date;
            this.Role_id = Role_id;
        }

        public static async Task<List<User>> selectAll()
        {
            string sql = "SELECT * FROM users;";
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<User> result = new List<User>();
            while (reader.Read())
            {
                int ide = reader.GetInt32("id");
                string login = reader.GetString("login");
                string password = reader.GetString("password");
                string fio = reader.GetString("fio");
                DateTime last_login_date = reader.GetDateTime("last_login_date");
                int role_id = reader.GetInt32("role_id");
                result.Add(new User(ide, login, password, fio, last_login_date, role_id));
            }
            reader.Close();
            return result;
        }
        public static async Task<List<User>> SelectById(int id)
        {
            string sql = string.Format("SELECT * FROM users WHERE id={0};", id);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<User> result = new List<User>();
            while (reader.Read())
            {
                int ide = reader.GetInt32("id");
                string login = reader.GetString("login");
                string password = reader.GetString("password");
                string fio = reader.GetString("fio");
                DateTime last_login_date = reader.GetDateTime("last_login_date");
                int role_id = reader.GetInt32("role_id");
                result.Add(new User(ide, login, password, fio, last_login_date, role_id));
            }
            reader.Close();
            return result;
        }
        public static async Task<int> Create(string login, string password, string fio, string last_login_date, int role_id)
        {
            string sql = string.Format("INSERT INTO users (login, password, fio, last_login_date, role_id) VALUES ('{0}', '{1}', '{2}', '{3}', {4});", login, password, fio, last_login_date, role_id);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        public static async Task<int> Update(Dictionary<string, Object> field, int id)
        {
            string sql = "";
            foreach (var item in field)
            {
                sql += string.Format("UPDATE users SET {0}='{1}' WHERE id={2};", item.Key, item.Value, id);
            }
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        public static async Task<int> deleteById(int id)
        {
            string sql = string.Format("DELETE FROM users WHERE id={0};", id);
            MySqlCommand command = new MySqlCommand(sql, App.connection);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
    }
}
