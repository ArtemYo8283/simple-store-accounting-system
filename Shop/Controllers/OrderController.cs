using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shop.Models;

namespace Shop.Controllers
{
    public class OrderController
    {
        public static async Task<List<Order>> selectAll()
        {
            return await Order.selectAll();
        }
        public static async Task<List<Order>> SelectById(int id)
        {
            return await Order.SelectById(id);
        }
        public static async Task<bool> Create(string fio_client, string phone_client, string email_client, string address_client, Status st)
        {
            int result = await Order.Create(fio_client, phone_client, email_client, address_client, st);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public static async Task<bool> Update(Dictionary<string, Object> field)
        {
            int result = await Order.Update(field);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public static async Task<bool> deleteById(int id)
        {
            int result = await Order.deleteById(id);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
    }
}
