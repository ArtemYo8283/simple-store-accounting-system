using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shop.Models;

namespace Shop.Controllers
{
    public class ProductController
    {
        public static async Task<List<Product>> selectAll()
        {
            return await Product.selectAll();
        }
        public static async Task<List<Product>> SelectById(int id)
        {
            return await Product.SelectById(id);
        }
        public static async Task<List<Product>> SelectByOrderId(int id)
        {
            return await Product.SelectByOrderId(id);
        }
        public static async Task<bool> Create(string title, string description, double price, int count)
        {
            int result = await Product.Create(title, description, price, count);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public static async Task<bool> Create_ListOfPRoduct(int order_id, int product_id, int product_count)
        {
            int result = await Product.Create_ListOfPRoduct(order_id, product_id, product_count);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public static async Task<bool> Update(Dictionary<string, Object> field, int id)
        {
            int result = await Product.Update(field, id);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public static async Task<bool> deleteById(int id)
        {
            int result = await Product.deleteById(id);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
    }
}
