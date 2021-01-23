using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineAuction.Core.Extensions;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.Models;
using OnlineAuction.Data.Models.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using OnlineAuction.Core.Models;
using Core.Extensions;

namespace OnlineAuction.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly AppSettings _appSettings;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAppContext _appContext;
        public UserService(IUnitOfWork<OnlineAuctionContext> unitOfWork,
            IOptions<AppSettings> appSettings,
            IServiceProvider serviceProvider,
            IAppContext appContext)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
            _serviceProvider = serviceProvider;
            _appContext = appContext;
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
                ex.InsertLog(loginResponseModel.Id, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Giriş işlemi gerçekleştirilemedi";
            }

            return returnModel;
        }

        public List<OnlineAuction.Data.DbEntity.Users> GetUsers() 
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Users>().GetAll().ToList();
        }

        public OnlineAuction.Data.DbEntity.Users GetUserById(int id)
        {
            if (id <= 0)
                return new OnlineAuction.Data.DbEntity.Users();

            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Users>().GetFirstOrDefault(predicate: x=> x.Id == id);
        }

        public ReturnModel<object> Add(UserRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                model.User.Password = model.User.Password.ToSHAHash();
                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Users>().Insert(model.User.MapTo<OnlineAuction.Data.DbEntity.Users>());

                var result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Kullanıcı başarıyla eklendi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Kullanıcı eklenemedi";
                    return returnModel;
                }

            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Kullanıcı ekleme işlemi gerçekleştirilemedi";
            }
            return returnModel;
        }

        public ReturnModel<object> Update(UserRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var user = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Users>().GetFirstOrDefault(predicate: x => x.Id == model.User.Id);

                if (user != null)
                {
                    user.UserName = model.User.UserName;
                    user.Email = model.User.Email;
                    user.FirstName = model.User.FirstName;
                    user.LastName = model.User.LastName;
                    user.LandPhone = model.User.LandPhone;
                    user.IdentityNumber = model.User.IdentityNumber;
                    user.CellPhone = model.User.CellPhone;
                    user.CityName = model.User.CityName;
                    user.DistrictName = model.User.DistrictName;
                    user.Address = model.User.Address;
                    user.IsActive = model.User.IsActive;
                    user.MemberType = model.User.MemberType;

                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Users>().Update(user);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Kullanıcı başarıyla düzenlendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Kullanıcı düzenlemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Kullanıcı bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Kullanıcı düzenleme işlemi gerçekleştirilemedi";
            }

            return returnModel;
        }

        public ReturnModel<object> UserIsActiveUpdate(OnlineAuction.Data.DbEntity.Users model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var user = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Users>().GetFirstOrDefault(predicate: x => x.Id == model.Id);
                if (user != null)
                {
                    user.IsActive = model.IsActive;
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Users>().Update(user);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Kullanıcı başarıyla düzenlendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Kullanıcı düzenlenemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Kullanıcı bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Kullanıcı eklenirken hata oluştu";
            }

            return returnModel;
        }
        public ReturnModel<object> Delete(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var data = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Users>().GetFirstOrDefault(predicate: x => x.Id == id);

                if (data != null)
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Users>().Delete(data);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Kullanıcı başarıyla silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Kullanıcı silinemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Kullanıcı bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Kullanıcı silinirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var users = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Users>().GetAll().ToList();

                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Users>().Delete(users);

                var result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Kullanıcı başarıyla silindi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Kullanıcı silinemedi";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Kullanıcı silinirken hata oluştu";
                return returnModel;
            }
        }
    }
}
