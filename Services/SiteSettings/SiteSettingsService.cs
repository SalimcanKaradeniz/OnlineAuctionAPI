using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineAuction.Services.SiteSettingsService
{
    public class SiteSettingsService : ISiteSettingsService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly ILanguagesService _languagesService;
        public SiteSettingsService(IUnitOfWork<OnlineAuctionContext> unitOfWork, ILanguagesService languagesService)
        {
            _unitOfWork = unitOfWork;
            _languagesService = languagesService;
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

        public ReturnModel<object> Update(SiteSettings model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var siteSetting = GetSiteSettingsById(model.Id);

                if (siteSetting != null)
                {
                    siteSetting.Title = model.Title;
                    siteSetting.Description = model.Description;
                    siteSetting.CellPhone = model.CellPhone;
                    siteSetting.LandPhone = model.LandPhone;
                    siteSetting.Fax = model.Fax;
                    siteSetting.Email = model.Email;
                    siteSetting.Address = model.Address;
                    siteSetting.FacebookUrl = model.FacebookUrl;
                    siteSetting.InstagramUrl = model.InstagramUrl;
                    siteSetting.TwitterUrl = model.TwitterUrl;
                    siteSetting.LinkedinUrl = model.LinkedinUrl;
                    siteSetting.PinterestUrl = model.PinterestUrl;
                    siteSetting.OtherSocialMediaUrl = model.OtherSocialMediaUrl;
                    siteSetting.Map = model.Map;
                    siteSetting.ComissionRate = model.ComissionRate;
                    siteSetting.TaxRate = model.TaxRate;
                    siteSetting.IsActive = model.IsActive;

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
