using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace OnlineAuction.Services.SocialMediaSettingsService
{
    public class SocialMediaSettingsService : ISocialMediaSettingsService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        public SocialMediaSettingsService(IUnitOfWork<OnlineAuctionContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                var socialMediaSetting = GetSocialMediaSettingsById(model.Id);

                if (socialMediaSetting != null)
                {
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
