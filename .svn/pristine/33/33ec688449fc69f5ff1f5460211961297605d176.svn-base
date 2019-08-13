using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using Model.User;

namespace Services.IService
{
    public interface IUserService
    {
        List<UserModel> GetAll();
        List<UserModel> GetPaging(int? page, out int TotalCount);
        List<UserModel> GetSearchData(UserSearch userSearch, int? page, out int TotalCount);
        UserModel GetById(int? id);
        bool Create(UserModel userModel);
        bool Edit(UserModel userModel);
        UserModel Delete(UserModel userModel);
        bool ResetPassword(UserResetPassword userModel);
        bool CheckUserPhone(UserModel userModel);
        bool CheckUserEmail(UserModel userModel);
        UserLoginModel Login(UserLoginModel model);
        bool CheckUserEmail1(UserLoginModel userModel);
        UserLoginModel GetUserByEmail(string email);
        bool ChangePassword(UserLoginModel model);
        bool ResetPassword1(UserLoginModel userModel);
        UserModel GetUserId(string sku);
        Dictionary<string, string> CreateList(Dictionary<int, UserModel> list);
        Dictionary<string, string> UpdateList(Dictionary<int, UserModel> list);

        UserModel GetByUserName(string name);
		bool GetByUserFullName(string name);
        bool CheckEmail(string name);

    }
}
