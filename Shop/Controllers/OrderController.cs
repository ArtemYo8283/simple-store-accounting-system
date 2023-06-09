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
        public static async Task<bool> Create(string fio_client, string phone_client, string email_client, string address_client, Status st, List<Product> selectedProducts)
        {
            DateTime currentDateTime = DateTime.Now;
            string last_upd_date = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            int insertedID = await Order.Create(fio_client, phone_client, email_client, address_client, last_upd_date, st);
            if (selectedProducts != null)
            {
                foreach (Product item in selectedProducts)
                {
                    await ProductController.Create_ListOfPRoduct(insertedID, item.Id, item.Count);
                }
            }
            return true;
        }
        public static async Task<bool> Update(Dictionary<string, Object> field, int id)
        {
            DateTime currentDateTime = DateTime.Now;
            string last_upd_date = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            field.Add("last_upd_date", last_upd_date);
            int result = await Order.Update(field, id);
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
