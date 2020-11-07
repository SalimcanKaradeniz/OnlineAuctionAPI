using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineAuction.Core.Extensions;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using OnlineAuction.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineAuction.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly AppSettings _appSettings;
        public UserService(IUnitOfWork<OnlineAuctionContext> unitOfWork,
            IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public ReturnModel<LoginResponseModel> Login(LoginModel model)
        {
            ReturnModel<LoginResponseModel> returnModel = new ReturnModel<LoginResponseModel>();
            LoginResponseModel loginResponseModel = new LoginResponseModel();

            try
            {
                var user = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Users>().GetFirstOrDefault(predicate: x => x.Email == model.Email && x.Password == model.Password.ToSHAHash() && x.IsActive);

                if (user == null)
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Kullanıcı bulunamadı";
                    
                    return returnModel;
                }
                
                var key = Encoding.ASCII.GetBytes(_appSettings.JwtConfiguration.SigningKey);
                var singingKey = new SymmetricSecurityKey(key);

                DateTime expiresDate = DateTime.Now.AddDays(1);

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _appSettings.JwtConfiguration.Audience,
                    Issuer = _appSettings.JwtConfiguration.Issuer,
                    SigningCredentials = new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256Signature),
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.FirstName),
                        new Claim("UserId", user.Id.ToString())
                    }),
                    Expires = expiresDate,
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                #region login response model set
                
                loginResponseModel.Id = user.Id;
                loginResponseModel.UserName = user.UserName;
                loginResponseModel.Email = user.Email;
                loginResponseModel.FirstName = user.FirstName;
                loginResponseModel.LastName = user.LastName;
                loginResponseModel.IdentityNumber = user.IdentityNumber;
                loginResponseModel.Address = user.Address;
                loginResponseModel.DistrictName = user.DistrictName;
                loginResponseModel.CityName = user.CityName;
                loginResponseModel.LandPhone = user.LandPhone;
                loginResponseModel.CellPhone = user.CellPhone;
                loginResponseModel.Token = tokenString;
                loginResponseModel.TokenExpireDate = expiresDate;

                #endregion

                returnModel.IsSuccess = true;
                returnModel.Message = "Giriş işlemi başarıyla yapıldı.";
                returnModel.Data = loginResponseModel;
            }
            catch (Exception ex)
            {

                returnModel.IsSuccess = false;
                returnModel.Message = "Giriş işlemi gerçekleştirilemedi";
            }

            return returnModel;
        }
    }
}
