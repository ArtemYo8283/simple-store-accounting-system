using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace Shop.Models
{
    public class Utils
    {
        public static async Task<string> HashPsw(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                stringBuilder.Append(hashedBytes[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
        public static string StatusToString(Status st)
        {
            switch (st)
            {
                case Status.New:
                    return "New";
                    break;
                case Status.Waiting_for_payment:
                    return "Waiting for payment";
                    break;
                case Status.Paid:
                    return "Paid";
                    break;
                case Status.Confirmed:
                    return "Confirmed";
                    break;
                case Status.Awaiting_shipment:
                    return "Awaiting shipment";
                    break;
                case Status.Delivery_in_progress:
                    return "Delivery in progress";
                    break;
                case Status.Delivered:
                    return "Delivered";
                    break;
                case Status.Received:
                    return "Received";
                    break;
                case Status.Completed:
                    return "Completed";
                    break;
                case Status.Canceled:
                    return "Canceled";
                    break;
            }
            return "";
        }
        public static Status StringToStatus(string st)
        {
            switch (st)
            {
                case "New":
                    return Status.New;
                    break;
                case "Waiting for payment":
                    return Status.Waiting_for_payment;
                    break;
                case "Paid":
                    return Status.Paid;
                    break;
                case "Confirmed":
                    return Status.Confirmed;
                    break;
                case "Awaiting shipment":
                    return Status.Awaiting_shipment;
                    break;
                case "Delivery in progress":
                    return Status.Delivery_in_progress;
                    break;
                case "Delivered":
                    return Status.Delivered;
                    break;
                case "Received":
                    return Status.Received;
                    break;
                case "Completed":
                    return Status.Completed;
                    break;
                case "Canceled":
                    return Status.Canceled;
                    break;
            }
            return Status.New;
        }
    }
}
