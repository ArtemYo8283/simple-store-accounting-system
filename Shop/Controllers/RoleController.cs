using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shop.Models;

namespace Shop.Controllers
{
    public class RoleController
    {
        public static async Task<List<Role>> selectAll()
        {
            return await Role.selectAll();
        }
        public static async Task<List<Role>> SelectById(int id)
        {
            return await Role.SelectById(id);
        }
        public static async Task<bool> Create(string title)
        {
            int result = await Role.Create(title);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public static async Task<bool> Update(string title)
        {
            int result = await Role.Update(title);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public static async Task<bool> deleteById(int id)
        {
            int result = await Role.deleteById(id);
            if (result == 0) {
                return false;
            }
            return true;
        }
    }
}
