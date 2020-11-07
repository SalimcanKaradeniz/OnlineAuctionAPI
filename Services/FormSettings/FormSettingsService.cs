using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace OnlineAuction.Services.FormSettingsService
{
    public class FormSettingsService : IFormSettingsService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        public FormSettingsService(IUnitOfWork<OnlineAuctionContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<FormSettings> GetFormSettings()
        {
            return _unitOfWork.GetRepository<FormSettings>().GetAll().ToList();
        }

        public ReturnModel<object> Add(FormSettings model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                _unitOfWork.GetRepository<FormSettings>().Insert(model);
                int result = _unitOfWork.SaveChanges();

                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Form Ayarları Eklendi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Form Ayarları Eklenirken hata oluştu";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Form Ayarları Eklenirken hata oluştu";
                return returnModel;
            }
        }

        public ReturnModel<object> Update(FormSettings model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var formSetting = GetFormSettingById(model.Id);

                if (formSetting != null)
                {
                    
                    _unitOfWork.GetRepository<FormSettings>().Update(formSetting);

                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Form Ayarları düzenlendi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Form Ayarları düzenlenirken hata oluştu";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Form Ayarları bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Form Ayarları düzenlenirken hata oluştu";
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
                    returnModel.Message = "Form Ayarları bulunamadı";
                    return returnModel;
                }

                var formSetting = GetFormSettingById(id);

                if (formSetting != null)
                {
                    _unitOfWork.GetRepository<FormSettings>().Delete(formSetting);

                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Form Ayarları Silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Form Ayarları silinirken hata oluştu";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Form Ayarları bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Form Ayarları silinirken hata oluştu";
                return returnModel;
            }
        }

        public FormSettings GetFormSettingById(int id) 
        {
            return _unitOfWork.GetRepository<FormSettings>().GetFirstOrDefault(predicate: x => x.Id == id);
        }
    }
}
