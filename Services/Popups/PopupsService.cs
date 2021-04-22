using Core.Extensions;
using Microsoft.Extensions.Options;
using OnlineAuction.Core.Models;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineAuction.Services.Popups
{
    public class PopupsService : IPopupsService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _appSettings;

        public PopupsService(IUnitOfWork<OnlineAuctionContext> unitOfWork, IServiceProvider serviceProvider, IAppContext appContext, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appContext = appContext;
            _serviceProvider = serviceProvider;
            _appSettings = appSettings.Value;
        }

        public List<OnlineAuction.Data.DbEntity.Popups> GetPopups()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().GetAll().ToList();
        }
        public List<OnlineAuction.Data.DbEntity.Popups> GetActivePopups()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().GetAll(x => x.IsActive).ToList();
        }
        public OnlineAuction.Data.DbEntity.Popups GetPopupById(int id)
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().GetFirstOrDefault(predicate: x => x.Id == id);
        }
        public ReturnModel<object> Add(PopupsModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            OnlineAuction.Data.DbEntity.Popups popupEntity = new OnlineAuction.Data.DbEntity.Popups();

            try
            {
                popupEntity.LangId = model.LangId;
                popupEntity.Title = model.Title;
                popupEntity.Description = model.Description;
                popupEntity.RedirectionLink = model.RedirectionLink;
                popupEntity.PictureUrl = model.PictureUrl;
                popupEntity.IsActive = model.IsActive;
                popupEntity.Rank = model.Rank;
                popupEntity.LangId = model.LangId;

                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().Insert(popupEntity);

                var result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Popup başarıyla eklendi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Popup eklenemedi";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Popup eklenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> Update(PopupsModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var popup = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().GetFirstOrDefault(predicate: x => x.Id == model.Id);

                if (popup != null)
                {

                    popup.Title = model.Title;
                    popup.Description = model.Description;
                    popup.RedirectionLink = model.RedirectionLink;
                    popup.PictureUrl = model.PictureUrl;
                    popup.IsActive = model.IsActive;
                    popup.Rank = model.Rank;
                    popup.LangId = model.LangId;

                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().Update(popup);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Popup başarıyla düzenlendi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Popup düzenlenemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Popup bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Popup düzenlenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> PopupIsActiveUpdate(PopupsModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var popup = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().GetFirstOrDefault(predicate: x => x.Id == model.Id);
                if (popup != null)
                {
                    popup.IsActive = model.IsActive;
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().Update(popup);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Popup başarıyla düzenlendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Popup düzenlenemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Popup bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Popup eklenirken hata oluştu";
            }

            return returnModel;
        }
        public ReturnModel<object> Delete(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var data = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().GetFirstOrDefault(predicate: x => x.Id == id);

                if (data != null)
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().Delete(data);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Popups başarıyla silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Popups silinemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Popups bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Popups silinirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var popups = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().GetAll().ToList();

                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().Delete(popups);

                var result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Popups başarıyla silindi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Popups silinemedi";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Popups silinirken hata oluştu";
                return returnModel;
            }
        }
    }
}