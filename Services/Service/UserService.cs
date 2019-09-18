using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Services.IService;
using DAL;
using Helper;

using Newtonsoft.Json;
using System.Web;

namespace Services.Service
{
    public class UserService : IUserService
    {
        public List<UserModel> GetAll()

        {
            var body = "";
            List<UserModel> UserModelList = ServerResponse.Invoke<List<UserModel>>("api/user/getDetails", body, "get");
            return UserModelList;
        }
        public List<UserModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
           // int pageSize = 10;
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<UserModel>>>("api/user/getUserPaging?pageNumber=" + page, body, "GET");
            TotalCount = result.TotalCount;
            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {

            }
            return result.data.ToList();
        }
		public Dictionary<string,string> CreateList(Dictionary<int,UserModel> list)
		{
			return ServerResponse.Invoke<Dictionary<string, string>>("api/user/createList", JsonConvert.SerializeObject(list), "post");
		}
        public Dictionary<string, string> UpdateList(Dictionary<int, UserModel> list)
        {
            return ServerResponse.Invoke<Dictionary<string, string>>("api/user/updateList", JsonConvert.SerializeObject(list), "post");
        }
        public UserLoginModel Login(UserLoginModel model)
        {
            var body = JsonConvert.SerializeObject(model);
            var login = ServerResponse.Invoke<UserLoginModel>("api/user/login", body, "POST");
            return login;
        }
        public UserModel GetUserId(string sku)
        {
            var getUserId = ServerResponse.Invoke<UserModel>("api/user/getUserId?sku=" + sku, "", "POST");
            return getUserId;
        }
		public UserModel GetByUserName(string name)
		{
			return ServerResponse.Invoke<UserModel>("api/user/CheckUserName?name=" + name, "", "POST");
		}
		public bool GetByUserFullName(string name)
		{
			return ServerResponse.Invoke<bool>("api/user/CheckUserFullName?name=" + name, "", "POST");
		}
        //checkEmail
        public bool CheckEmail(string name)
        {
            return ServerResponse.Invoke<bool>("api/user/checkEmail?email=" + name, "", "POST");
        }
        public List<UserModel> GetSearchData(UserSearch userSearch, int? page, out int TotalCount)
        {

           // int pageSize = 10;
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(userSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<UserModel>>>("api/user/getSearchData", body, "Post");
            TotalCount = result.TotalCount;
            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {

            }
            return result.data.ToList();

        }
        public UserModel GetById(int? id)
        {
            var body = "";
            UserModel UserModelById = ServerResponse.Invoke<UserModel>("api/user/getDetail?id=" + id, body, "GET");
            return UserModelById;
        }
        
        public UserLoginModel GetUserByEmail(string email)
        {
            var result = ServerResponse.Invoke<UserLoginModel>("api/user/userByEmail?email=" + email, null, "GET");
            return result;
        }
        public bool ChangePassword(UserLoginModel model)
        {
            var body = JsonConvert.SerializeObject(model);
            bool UserResetPassword = ServerResponse.Invoke<bool>("api/user/resetPassword?id=" + model.Id, body, "Post");
            return UserResetPassword;
        }
        public bool Create(UserModel userModel)
        {
            var body = JsonConvert.SerializeObject(userModel);
            bool UserCreate = ServerResponse.Invoke<bool>("api/user/create", body, "Post");
            return UserCreate;
        }
        public bool Edit(UserModel userModel)
        {
            var body = JsonConvert.SerializeObject(userModel);
            bool UserEdit = ServerResponse.Invoke<bool>("api/user/edit?id=" + userModel.Id, body, "POST");
            return UserEdit;
        }
        public UserModel Delete(UserModel userModel)
        {
            var body = JsonConvert.SerializeObject(userModel);
            UserModel UserDelete = ServerResponse.Invoke<UserModel>("api/user/Delete?id=" + userModel.Id, body, "POST");
            return UserDelete;
        }
        public bool CheckUserPhone(UserModel userModel)
        {
            var body = JsonConvert.SerializeObject(userModel);
            var CheckUserPhone = ServerResponse.Invoke<bool>("api/user/checkUserPhoneNumber" , body, "POST");
            return CheckUserPhone;
        }
        public bool CheckUserEmail(UserModel userModel)
        {
            var body = JsonConvert.SerializeObject(userModel);
            var CheckUserEmail = ServerResponse.Invoke<bool>("api/user/checkUserEmail", body, "POST");
            return CheckUserEmail;
        }
        public bool CheckUserEmail1(UserLoginModel userModel)
        {
            var body = JsonConvert.SerializeObject(userModel);
            var CheckUserEmail = ServerResponse.Invoke<bool>("api/user/checkUserEmail1", body, "POST");
            return CheckUserEmail;
        }
        public bool ResetPassword(UserResetPassword userModel)
        {
            var body = JsonConvert.SerializeObject(userModel);
            bool UserResetPassword = ServerResponse.Invoke<bool>("api/user/resetPassword?id=" + userModel.Id, body, "Post");
            return UserResetPassword;

        }
        public bool ResetPassword1(UserLoginModel userModel)
        {
            var body = JsonConvert.SerializeObject(userModel);
            bool UserResetPassword = ServerResponse.Invoke<bool>("api/user/resetPassword?id=" + userModel.Id, body, "Post");
            return UserResetPassword;
        }
        
        public Dictionary<int, bool> CheckEmailDict(Dictionary<int, string> list)
        {
            return ServerResponse.Invoke<Dictionary<int, bool>>("api/user/checkUser", JsonConvert.SerializeObject(list), "Post");
        }
        public Dictionary<int, Tuple<string, string, string, string>> UserImportFilter(Dictionary<int, Tuple<string,string, string, string>> list)
        {
            var body = JsonConvert.SerializeObject(list);
            var obj = ServerResponse.Invoke<Dictionary<int, Tuple<string, string, string, string>>>("api/product/getUserImportFilter", body, "POST");
            return obj;
        }
    }
}
