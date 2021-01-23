using OnlineAuction.Data.Models;
using OnlineAuction.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Users
{
    public interface IUserService
    {
        ReturnModel<LoginResponseModel> Login(LoginModel model);
        List<OnlineAuction.Data.DbEntity.Users> GetUsers();
        OnlineAuction.Data.DbEntity.Users GetUserById(int id);
        ReturnModel<object> Add(UserRequestModel model);
        ReturnModel<object> Update(UserRequestModel model);
        ReturnModel<object> UserIsActiveUpdate(OnlineAuction.Data.DbEntity.Users model);
        ReturnModel<object> Delete(int id);
        ReturnModel<object> DeleteAll();
    }
}
