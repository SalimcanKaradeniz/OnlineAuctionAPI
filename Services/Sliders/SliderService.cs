using Core.Extensions;
using Microsoft.Extensions.Options;
using OnlineAuction.Core.Models;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineAuction.Services.Sliders
{
    public class SliderService : ISliderService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _appSettings;

        public SliderService(IUnitOfWork<OnlineAuctionContext> unitOfWork, IServiceProvider serviceProvider, IAppContext appContext, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appContext = appContext;
            _serviceProvider = serviceProvider;
            _appSettings = appSettings.Value;
        }

        public List<OnlineAuction.Data.DbEntity.Sliders> GetSliders()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().GetAll().ToList();
        }
        public List<OnlineAuction.Data.DbEntity.Sliders> GetActiveSliders()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().GetAll(x => x.IsActive).ToList();
        }
        public OnlineAuction.Data.DbEntity.Sliders GetSliderById(int id)
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().GetFirstOrDefault(predicate: x => x.Id == id);
        }
        public ReturnModel<object> Add(SlidersModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            OnlineAuction.Data.DbEntity.Sliders slidertEntity = new OnlineAuction.Data.DbEntity.Sliders();

            try
            {
                slidertEntity.LangId = model.LangId;
                slidertEntity.Link = model.Link;
                slidertEntity.Picture = model.Picture;
                slidertEntity.IsActive = model.IsActive;

                slidertEntity.Title_tr = model.Title_tr;
                slidertEntity.Summary_tr = model.Summary_tr;
                slidertEntity.Content_tr = model.Content_tr;

                slidertEntity.Title_en = model.Title_en;
                slidertEntity.Summary_en = model.Summary_en;
                slidertEntity.Content_en = model.Content_en;

                slidertEntity.Rank = model.Rank;

                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().Insert(slidertEntity);

                var result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Slider başarıyla eklendi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Slider eklenemedi";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Slider eklenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> Update(SlidersModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var slider = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().GetFirstOrDefault(predicate: x => x.Id == model.Id);

                if (slider != null)
                {
                    slider.LangId = model.LangId;
                    slider.Link = model.Link;
                    slider.Picture = model.Picture;
                    slider.IsActive = model.IsActive;
                    slider.Title_tr = model.Title_tr;
                    slider.Summary_tr = model.Summary_tr;
                    slider.Content_tr = model.Content_tr;
                    slider.Title_en = model.Title_en;
                    slider.Summary_en = model.Summary_en;
                    slider.Content_en = model.Content_en;
                    slider.Rank = model.Rank;

                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().Update(slider);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Slider başarıyla düzenlendi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Slider düzenlenemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Slider bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Slider düzenlenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> SliderIsActiveUpdate(SlidersModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var slider = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().GetFirstOrDefault(predicate: x => x.Id == model.Id);
                if (slider != null)
                {
                    slider.IsActive = model.IsActive;
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().Update(slider);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Slider başarıyla düzenlendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Slider düzenlenemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Slider bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Slider eklenirken hata oluştu";
            }

            return returnModel;
        }
        public ReturnModel<object> Delete(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var data = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().GetFirstOrDefault(predicate: x => x.Id == id);

                if (data != null)
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().Delete(data);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Slider başarıyla silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Slider silinemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Slider bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Slider silinirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var sliders = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().GetAll().ToList();

                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().Delete(sliders);

                var result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Slider başarıyla silindi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Slider silinemedi";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Slider silinirken hata oluştu";
                return returnModel;
            }
        }
    }
}
