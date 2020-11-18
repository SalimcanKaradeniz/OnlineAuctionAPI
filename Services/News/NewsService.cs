using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Languages;
using OnlineAuction.Services.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.News
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly ILanguagesService _languagesService;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        public NewsService(IUnitOfWork<OnlineAuctionContext> unitOfWork,
            ILanguagesService languagesService,
            IAppContext appContext,
            IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _languagesService = languagesService;
            _serviceProvider = serviceProvider;
            _appContext = appContext;
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
        public ReturnModel<object> Add(NewsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            OnlineAuction.Data.DbEntity.News newEntity = new OnlineAuction.Data.DbEntity.News();

            try
            {
                newEntity.IsActive = model.News.IsActive;
                newEntity.Picture = model.News.Picture;
                newEntity.StartDate = model.News.StartDate;
                newEntity.EndDate = model.News.EndDate;

                newEntity.Title_tr = model.News.Title_tr;
                newEntity.ShortDescription_tr = model.News.ShortDescription_tr;
                newEntity.Detail_tr = model.News.Detail_tr;

                newEntity.Title_en = model.News.Title_en;
                newEntity.ShortDescription_en = model.News.ShortDescription_en;
                newEntity.Detail_en = model.News.Detail_en;

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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Haber eklenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> Update(NewsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var newsById = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().GetFirstOrDefault(predicate: x => x.Id == model.News.Id);

                if (newsById != null)
                {
                    newsById.Picture = model.News.Picture;
                    newsById.IsActive = model.News.IsActive;
                    newsById.StartDate = model.News.StartDate;
                    newsById.EndDate = model.News.EndDate;

                    newsById.Title_tr = model.News.Title_tr;
                    newsById.ShortDescription_tr = model.News.ShortDescription_tr;
                    newsById.Detail_tr = model.News.Detail_tr;

                    newsById.Title_en = model.News.Title_en;
                    newsById.ShortDescription_en = model.News.ShortDescription_en;
                    newsById.Detail_en = model.News.Detail_en;

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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Haber düzenlenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> NewIsActiveUpdate(NewsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var newData = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().GetFirstOrDefault(predicate: x => x.Id == model.News.Id);
                if (newData != null)
                {
                    newData.IsActive = model.News.IsActive;
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                
                returnModel.IsSuccess = false;
                returnModel.Message = "Tüm haberler silinirken hata oluştu";
            }

            return returnModel;
        } 
    }
}
