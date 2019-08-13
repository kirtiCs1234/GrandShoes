using Helper;
using Model;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class LoginService : ILoginService
    {
        public UserModel Login(UserLoginModel model)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            var user = new UserModel();
            user = ServerResponse.Invoke<UserModel>("api/myaccount/login", body, "POST");
            return user;
        }
    }
}
