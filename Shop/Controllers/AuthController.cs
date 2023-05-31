using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class AuthController
    {
        public static async Task<bool> Login(string login, string password)
        {
            password = await Utils.HashPsw(password);
            List<User> res = await User.selectAll();
            foreach (var item in res)
            {
                if (item.Login == login && item.Password == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
