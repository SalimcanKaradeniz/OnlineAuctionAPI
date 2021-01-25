using Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OnlineAuction.Core.Models;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OnlineAuction.Services.SiteSettingsService
{
    public class SiteSettingsService : ISiteSettingsService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAppContext _appContext;
        private readonly AppSettings _appSettings;
        private readonly IFileService _fileService;
        public SiteSettingsService(IUnitOfWork<OnlineAuctionContext> unitOfWork, IServiceProvider serviceProvider, IAppContext appContext, IOptions<AppSettings> appSettings, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _serviceProvider = serviceProvider;
            _appContext = appContext;
            _appSettings = appSettings.Value;
            _fileService = fileService;
        }

        public List<SiteSettings> GetSiteSettings()
        {
            return _unitOfWork.GetRepository<SiteSettings>().GetAll().ToList();
        }

        public ReturnModel<object> Add(SiteSettings model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                _unitOfWork.GetRepository<SiteSettings>().Insert(model);
                int result = _unitOfWork.SaveChanges();

                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Site Ayarları Eklendi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Site Ayarları Eklenirken hata oluştu";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Site Ayarları Eklenirken hata oluştu";
                return returnModel;
            }
        }

        public ReturnModel<object> Update(SiteSettingsRequestModel model/*, IFormFile logo*/)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var siteSetting = GetSiteSettingsById(model.Settings.Id);

                if (siteSetting != null)
                {
                    siteSetting.Title = model.Settings.Title;
                    siteSetting.Description = model.Settings.Description;
                    siteSetting.CellPhone = model.Settings.CellPhone;
                    siteSetting.LandPhone = model.Settings.LandPhone;
                    siteSetting.Fax = model.Settings.Fax;
                    siteSetting.Email = model.Settings.Email;
                    siteSetting.Address = model.Settings.Address;
                    siteSetting.FacebookUrl = model.Settings.FacebookUrl;
                    siteSetting.InstagramUrl = model.Settings.InstagramUrl;
                    siteSetting.TwitterUrl = model.Settings.TwitterUrl;
                    siteSetting.LinkedinUrl = model.Settings.LinkedinUrl;
                    siteSetting.PinterestUrl = model.Settings.PinterestUrl;
                    siteSetting.OtherSocialMediaUrl = model.Settings.OtherSocialMediaUrl;
                    siteSetting.Map = model.Settings.Map;
                    siteSetting.ComissionRate = model.Settings.ComissionRate;
                    siteSetting.TaxRate = model.Settings.TaxRate;
                    siteSetting.IsActive = model.Settings.IsActive;

                    //if (!string.IsNullOrEmpty(model.Logo))
                    //{
                    //    siteSetting.Logo = model.Logo;
                    //}
                    //else
                    //{
                    //    if (logo != null)
                    //        siteSetting.Logo = _fileService.FileUplod(logo);
                    //}

                    _unitOfWork.GetRepository<SiteSettings>().Update(siteSetting);

                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Site Ayarları düzenlendi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Site Ayarları düzenlenirken hata oluştu";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Site Ayarları bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Site Ayarları düzenlenirken hata oluştu";
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
                    returnModel.Message = "Site Ayarları bulunamadı";
                    return returnModel;
                }

                var socialMediaSettings = GetSiteSettingsById(id);

                if (socialMediaSettings != null)
                {
                    _unitOfWork.GetRepository<SiteSettings>().Delete(socialMediaSettings);

                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Site Ayarları Silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Site Ayarları silinirken hata oluştu";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Site Ayarları bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Site Ayarları silinirken hata oluştu";
                return returnModel;
            }
        }

        public SiteSettings GetSiteSettingsById(int id)
        {
            return _unitOfWork.GetRepository<SiteSettings>().GetFirstOrDefault(predicate: x => x.Id == id);
        }

    }
}