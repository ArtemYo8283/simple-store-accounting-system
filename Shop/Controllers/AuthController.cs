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
            List<User> res = await UserController.selectAll();
            foreach (var item in res)
            {
                if (item.Login == login && item.Password == password)
                {
                    App.Login_Session = item.Login;
                    App.Fio_Session = item.Fio;
                    List<Role> res1 = await Role.SelectById(item.Role_id);
                    App.Role_Session = res1[0].Title;
                    DateTime currentDateTime = DateTime.Now;
                    string last_login_date = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                    myDictionary.Add("last_login_date", last_login_date);
                    await UserController.Update(myDictionary, item.Id);
                    return true;
                }
            }
            return false;
        }
    }
}
