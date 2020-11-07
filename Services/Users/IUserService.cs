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
    }
}
