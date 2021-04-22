using Core.Extensions;
using Microsoft.Extensions.Options;
using OnlineAuction.Core.Models;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Languages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineAuction.Services.News
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly ILanguagesService _languagesService;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _appSettings;

        public NewsService(IUnitOfWork<OnlineAuctionContext> unitOfWork,
            ILanguagesService languagesService,
            IAppContext appContext,
            IServiceProvider serviceProvider,
            IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _languagesService = languagesService;
            _serviceProvider = serviceProvider;
            _appContext = appContext;
            _appSettings = appSettings.Value;
        }

        public List<OnlineAuction.Data.DbEntity.News> GetNews()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().GetAll().ToList();
        }
        public List<OnlineAuction.Data.DbEntity.News> GetActiveNews()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().GetAll(x => x.IsActive).ToList();
        }
        public OnlineAuction.Data.DbEntity.News GetNewsById(int id)
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().GetFirstOrDefault(predicate: x => x.Id == id);
        }
        public ReturnModel<object> Add(NewsModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            OnlineAuction.Data.DbEntity.News newEntity = new OnlineAuction.Data.DbEntity.News();

            try
            {
                newEntity.IsActive = model.IsActive;
                newEntity.Picture = model.Picture;
                newEntity.StartDate = model.StartDate;
                newEntity.EndDate = model.EndDate;
                newEntity.Title = model.Title;
                newEntity.ShortDescription = model.ShortDescription;
                newEntity.Detail = model.Detail;
                newEntity.Rank = model.Rank;
                newEntity.LangId = model.LangId;

                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().Insert(newEntity);

                var result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Haber başarıyla eklendi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Haber eklenemedi";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Haber eklenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> Update(NewsModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var newsById = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().GetFirstOrDefault(predicate: x => x.Id == model.Id);

                if (newsById != null)
                {
                    newsById.Picture = model.Picture;
                    newsById.IsActive = model.IsActive;
                    newsById.StartDate = model.StartDate;
                    newsById.EndDate = model.EndDate;

                    newsById.Title = model.Title;
                    newsById.ShortDescription = model.ShortDescription;
                    newsById.Detail = model.Detail;
                    newsById.Rank = model.Rank;

                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().Update(newsById);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Haber başarıyla düzenlendi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Haber düzenlenemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Haber bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Haber düzenlenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> NewIsActiveUpdate(NewsModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var newData = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().GetFirstOrDefault(predicate: x => x.Id == model.Id);
                if (newData != null)
                {
                    newData.IsActive = model.IsActive;
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().Update(newData);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Haber başarıyla düzenlendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Haber düzenlenemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Haber bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Haber düzenlenirken hata oluştu";
            }

            return returnModel;
        }
        public ReturnModel<object> Delete(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var data = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().GetFirstOrDefault(predicate: x => x.Id == id);

                if (data != null)
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().Delete(data);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Haber başarıyla silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Haber silinemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Haber bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Haber silinirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var news = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().GetAll().ToList();
                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().Delete(news);

                int result = _unitOfWork.SaveChanges();

                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Haberler Silindi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Haberler Silinemedi";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                
                returnModel.IsSuccess = false;
                returnModel.Message = "Tüm haberler silinirken hata oluştu";
            }

            return returnModel;
        } 
    }
}