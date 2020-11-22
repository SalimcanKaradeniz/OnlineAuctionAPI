using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Languages;
using OnlineAuction.Services.Log;
using OnlineAuction.Services.Artists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineAuction.Services.Popups
{
    public class PopupsService : IPopupsService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly ILanguagesService _languagesService;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        public PopupsService(IUnitOfWork<OnlineAuctionContext> unitOfWork, ILanguagesService languagesService, IServiceProvider serviceProvider, IAppContext appContext)
        {
            _unitOfWork = unitOfWork;
            _languagesService = languagesService;
            _appContext = appContext;
            _serviceProvider = serviceProvider;
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
        public ReturnModel<object> Add(PopupsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            OnlineAuction.Data.DbEntity.Popups popupEntity = new OnlineAuction.Data.DbEntity.Popups();

            try
            {
                popupEntity.Title_tr = model.Popups.Title_tr;
                popupEntity.Title_en = model.Popups.Title_en;
                popupEntity.Description_tr = model.Popups.Description_tr;
                popupEntity.Description_en = model.Popups.Description_en;
                popupEntity.RedirectionLink = model.Popups.RedirectionLink;
                popupEntity.PictureUrl = model.Popups.PictureUrl;
                popupEntity.IsActive = model.Popups.IsActive;

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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Popup eklenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> Update(PopupsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var popup = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().GetFirstOrDefault(predicate: x => x.Id == model.Popups.Id);

                if (popup != null)
                {

                    popup.Title_tr = model.Popups.Title_tr;
                    popup.Title_en = model.Popups.Title_en;
                    popup.Description_tr = model.Popups.Description_tr;
                    popup.Description_en = model.Popups.Description_en;
                    popup.RedirectionLink = model.Popups.RedirectionLink;
                    popup.PictureUrl = model.Popups.PictureUrl;
                    popup.IsActive = model.Popups.IsActive;

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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Popup düzenlenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> PopupIsActiveUpdate(PopupsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var popup = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Popups>().GetFirstOrDefault(predicate: x => x.Id == model.Popups.Id);
                if (popup != null)
                {
                    popup.IsActive = model.Popups.IsActive;
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Popups silinirken hata oluştu";
                return returnModel;
            }
        }
    }
}
