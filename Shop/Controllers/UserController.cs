using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shop.Models;

namespace Shop.Controllers
{
    public class UserController
    {
        public static async Task<List<User>> selectAll()
        {
            return await User.selectAll();
        }
        public static async Task<List<User>> SelectById(int id)
        {
            return await User.SelectById(id);
        }
        public static async Task<bool> Create(string login, string password, string fio, int role_id)
        {
            DateTime currentDateTime = DateTime.Now;
            string last_login_date = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            password = await Utils.HashPsw(password);
            int result = await User.Create(login, password, fio, last_login_date, role_id);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public static async Task<bool> Update(Dictionary<string, Object> field, int id)
        {
            //на апдейт пароля, хеш подавать
            int result = await User.Update(field, id);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public static async Task<bool> deleteById(int id)
        {
            int result = await User.deleteById(id);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
    }
}
