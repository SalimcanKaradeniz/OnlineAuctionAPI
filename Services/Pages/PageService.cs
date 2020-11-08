using Microsoft.EntityFrameworkCore;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineAuction.Services.Pages
{
    public class PageService : IPageService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        public PageService(IUnitOfWork<OnlineAuctionContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<OnlineAuction.Data.DbEntity.Pages> GetPages()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetAll(include: x => x.Include(pb => pb.PageBanners).Include(psf => psf.PageSpecification)).ToList();
        }

        public OnlineAuction.Data.DbEntity.Pages GetPageById(int id)
        {
            if (id <= 0)
                return new OnlineAuction.Data.DbEntity.Pages();

            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetFirstOrDefault(predicate: x => x.Id == id, include: source => source.Include(b => b.PageBanners));
        }

        public List<PageSpecifications> GetPageSpecifications()
        {
            return _unitOfWork.GetRepository<PageSpecifications>().GetAll().ToList();
        }

        public ReturnModel<object> Add(OnlineAuction.Data.DbEntity.Pages model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            List<PageBanner> pageBanners = new List<PageBanner>();

            using (var transaction = _unitOfWork.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var page = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Insert(model);
                   
                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        transaction.Commit();

                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sayfa başarıyla eklendi";
                        return returnModel;
                    }
                    else
                    {
                        transaction.Rollback();

                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sayfa eklenemedi";
                        return returnModel;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sayfa eklenirken hata oluştu";
                    return returnModel;
                }
            }
        }

        public ReturnModel<object> Update(OnlineAuction.Data.DbEntity.Pages model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            List<PageBanner> pageBanners = new List<PageBanner>();

            using (var transaction = _unitOfWork.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var page = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetFirstOrDefault(predicate: x => x.Id == model.Id);

                    if (page != null || page.Id > 0)
                    {
                        _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Update(model);

                        var result = _unitOfWork.SaveChanges();
                        if (result > 0)
                        {
                            transaction.Commit();

                            returnModel.IsSuccess = true;
                            returnModel.Message = "Sayfa başarıyla düzenlendi";
                            return returnModel;
                        }
                        else
                        {
                            transaction.Rollback();

                            returnModel.IsSuccess = false;
                            returnModel.Message = "Sayfa düzenlendi";
                            return returnModel;
                        }
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sayfa bulunamadı";
                        return returnModel;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sayfa eklenirken hata oluştu";
                    return returnModel;
                }
            }
        }

        public ReturnModel<object> Delete(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Sayfa Bulunamadı";
                return returnModel;
            }

            try
            {
                var page = GetPageById(id);

                if (page != null || page.Id > 0)
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Delete(page);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sayfa Silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sayfa Silinemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sayfa Bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Sayfa Silinirken Bir Hata Oluştu";
                return returnModel;
            }
        }
    }
}
