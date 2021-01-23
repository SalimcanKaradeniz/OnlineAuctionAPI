using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Core.Extensions;
using Microsoft.Extensions.Options;
using OnlineAuction.Core.Models;

namespace OnlineAuction.Services.SocialMediaSettingsService
{
    public class SocialMediaSettingsService : ISocialMediaSettingsService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _appSettings;

        public SocialMediaSettingsService(IUnitOfWork<OnlineAuctionContext> unitOfWork, IAppContext appContext, IServiceProvider serviceProvider, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _serviceProvider = serviceProvider;
            _appContext = appContext;
            _appSettings = appSettings.Value;
        }

        public List<SocialMediaSettings> GetSocialMediaSettings()
        {
            return _unitOfWork.GetRepository<SocialMediaSettings>().GetAll(include: x => x.Include(s => s.SocialMediaTypes)).ToList();
        }

        public ReturnModel<object> Add(SocialMediaSettings model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                _unitOfWork.GetRepository<SocialMediaSettings>().Insert(model);
                int result = _unitOfWork.SaveChanges();

                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Sosyal Medya Ayarları Eklendi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sosyal Medya Ayarları Eklenirken hata oluştu";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sosyal Medya Ayarları Eklenirken hata oluştu";
                return returnModel;
            }
        }

        public ReturnModel<object> Update(SocialMediaSettings model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var socialMediaSetting = _unitOfWork.GetRepository<SocialMediaSettings>().GetFirstOrDefault(predicate: x => x.Id == model.Id);

                if (socialMediaSetting != null)
                {
                    socialMediaSetting.Address = model.Address;
                    socialMediaSetting.SocialMediaTypesId = model.SocialMediaTypesId;
                    socialMediaSetting.IsActive = model.IsActive;

                    _unitOfWork.GetRepository<SocialMediaSettings>().Update(socialMediaSetting);

                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sosyal Medya Ayarları düzenlendi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sosyal Medya Ayarları düzenlenirken hata oluştu";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sosyal Medya Ayarları bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sosyal Medya Ayarları düzenlenirken hata oluştu";
                return returnModel;
            }
        }

        public ReturnModel<object> Delete(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                if (id <= 0)
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sosyal Medya Ayarları bulunamadı";
                    return returnModel;
                }

                var socialMediaSettings = GetSocialMediaSettingsById(id);

                if (socialMediaSettings != null)
                {
                    _unitOfWork.GetRepository<SocialMediaSettings>().Delete(socialMediaSettings);

                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sosyal Medya Ayarları Silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sosyal Medya Ayarları silinirken hata oluştu";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sosyal Medya Ayarları bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sosyal Medya Ayarları silinirken hata oluştu";
                return returnModel;
            }
        }

        public SocialMediaSettings GetSocialMediaSettingsById(int id)
        {
            return _unitOfWork.GetRepository<SocialMediaSettings>().GetFirstOrDefault(predicate: x => x.Id == id, include: source => source.Include(source => source.SocialMediaTypes));
        }

        public List<SocialMediaTypes> GetSocialMediaTypes()
        {
            return _unitOfWork.GetRepository<SocialMediaTypes>().GetAll().ToList();
        }
    }
}