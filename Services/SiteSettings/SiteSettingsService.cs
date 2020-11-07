using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineAuction.Services.SiteSettingsService
{
    public class SiteSettingsService : ISiteSettingsService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        public SiteSettingsService(IUnitOfWork<OnlineAuctionContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                var socialMediaSetting = GetSiteSettingsById(model.Id);

                if (socialMediaSetting != null)
                {
                    _unitOfWork.GetRepository<SiteSettings>().Update(socialMediaSetting);

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
