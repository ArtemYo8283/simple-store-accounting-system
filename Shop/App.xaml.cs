using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

using Shop.Models;

namespace Shop
{
    public enum Status
    {
        New,
        Waiting_for_payment,
        Paid,
        Confirmed,
        Awaiting_shipment,
        Delivery_in_progress,
        Delivered,
        Received,
        Completed
    }
    public partial class App : Application
    {
        public static MySqlConnection? connection;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string json = File.ReadAllText("Config/db-config.json");
            try
            {
                DBConfig config = JsonConvert.DeserializeObject<DBConfig>(json);
                connection = new MySqlConnection(string.Format("Server={0};Database={1};Uid={2};Pwd={3};", config.host, config.database, config.user, config.password));
                connection.OpenAsync();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                connection.CloseAsync();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
