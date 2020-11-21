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

namespace OnlineAuction.Services.Sliders
{
    public class SliderService : ISliderService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly ILanguagesService _languagesService;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        public SliderService(IUnitOfWork<OnlineAuctionContext> unitOfWork, ILanguagesService languagesService, IServiceProvider serviceProvider, IAppContext appContext)
        {
            _unitOfWork = unitOfWork;
            _languagesService = languagesService;
            _appContext = appContext;
            _serviceProvider = serviceProvider;
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
        public ReturnModel<object> Add(SliderRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            OnlineAuction.Data.DbEntity.Sliders slidertEntity = new OnlineAuction.Data.DbEntity.Sliders();

            try
            {
                slidertEntity.Link = model.Slider.Link;
                slidertEntity.Picture = model.Slider.Picture;
                slidertEntity.IsActive = model.Slider.IsActive;

                slidertEntity.Title_tr = model.Slider.Title_tr;
                slidertEntity.Summary_tr = model.Slider.Summary_tr;
                slidertEntity.Content_tr = model.Slider.Content_tr;

                slidertEntity.Title_en = model.Slider.Title_en;
                slidertEntity.Summary_en = model.Slider.Summary_en;
                slidertEntity.Content_en = model.Slider.Content_en;

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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Slider eklenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> Update(SliderRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var slider = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().GetFirstOrDefault(predicate: x => x.Id == model.Slider.Id);

                if (slider != null)
                {

                    slider.Link = model.Slider.Link;
                    slider.Picture = model.Slider.Picture;
                    slider.IsActive = model.Slider.IsActive;

                    slider.Title_tr = model.Slider.Title_tr;
                    slider.Summary_tr = model.Slider.Summary_tr;
                    slider.Content_tr = model.Slider.Content_tr;

                    slider.Title_en = model.Slider.Title_en;
                    slider.Summary_en = model.Slider.Summary_en;
                    slider.Content_en = model.Slider.Content_en;

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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Slider düzenlenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> SliderIsActiveUpdate(SliderRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var slider = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Sliders>().GetFirstOrDefault(predicate: x => x.Id == model.Slider.Id);
                if (slider != null)
                {
                    slider.IsActive = model.Slider.IsActive;
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Slider silinirken hata oluştu";
                return returnModel;
            }
        }
    }
}
