using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.News
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        public NewsService(IUnitOfWork<OnlineAuctionContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public ReturnModel<object> Add(OnlineAuction.Data.DbEntity.News news)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().Insert(news);

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
                returnModel.IsSuccess = false;
                returnModel.Message = "Haber eklenirken hata oluştu";
                return returnModel;
            }
        }

        public ReturnModel<object> Update(OnlineAuction.Data.DbEntity.News news)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var newsById = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().GetFirstOrDefault(predicate: x => x.Id == news.Id);

                if (newsById != null)
                {
                    newsById.TR_Title = news.TR_Title;
                    newsById.EN_Title = news.EN_Title;
                    newsById.TR_Detail = news.TR_Detail;
                    newsById.EN_Detail = news.EN_Detail;
                    newsById.TR_ShortDescription = news.TR_ShortDescription;
                    newsById.EN_ShortDescription = news.EN_ShortDescription;
                    newsById.Picture = news.Picture;
                    newsById.IsActive = news.IsActive;
                    newsById.StartDate = news.StartDate;
                    newsById.EndDate = news.EndDate;

                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.News>().Update(news);

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
                returnModel.IsSuccess = false;
                returnModel.Message = "Haber düzenlenirken hata oluştu";
                return returnModel;
            }
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
                returnModel.IsSuccess = false;
                returnModel.Message = "Haber silinirken hata oluştu";
                return returnModel;
            }
        }
    }
}
