using Microsoft.EntityFrameworkCore;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Enums.Pages;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Languages;
using OnlineAuction.Services.Log;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineAuction.Services.Pages
{
    public class PageService : IPageService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly ILanguagesService _languagesService;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        public PageService(IUnitOfWork<OnlineAuctionContext> unitOfWork,
            ILanguagesService languagesService,
            IAppContext appContext,
            IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _languagesService = languagesService;
            _appContext = appContext;
            _serviceProvider = serviceProvider;
        }

        #region Utilities

        public List<OnlineAuction.Data.Models.PagesModel> PrepareGetPages(List<OnlineAuction.Data.DbEntity.Pages> query)
        {
            if (query.Any())
            {
                var pages = (from p in query
                             select new PagesModel()
                             {
                                 Id = p.Id,
                                 ParentId = p.ParentId,
                                 Rank = p.Rank,
                                 SpecificationId = p.SpecificationId,
                                 RedirectionLink = p.RedirectionLink,
                                 PageSpecification = new PageSpecificationsModel()
                                 {
                                     Id = p.PageSpecification.Id,
                                     Name = p.PageSpecification.Name,
                                     IsActive = p.PageSpecification.IsActive,
                                     CreatedAt = p.CreatedAt
                                 },
                                 PageBanners = (from pb in p.PageBanners
                                                select new PageBannerModel()
                                                {
                                                    Id = pb.Id,
                                                    PageId = pb.PageId,
                                                    PictureUrl = pb.PictureUrl,
                                                    CreatedAt = pb.CreatedAt
                                                }).ToList(),
                                 Title_tr = p.Title_tr,
                                 Title_en = p.Title_en,
                                 Keywords_tr = p.Keywords_tr,
                                 Keywords_en = p.Keywords_en,
                                 Detail_tr = p.Detail_tr,
                                 Detail_en = p.Detail_en,
                                 Description_tr = p.Description_tr,
                                 Description_en = p.Description_en,
                                 IsActive = p.IsActive,
                                 IsFooter = p.IsFooter,
                                 IsMain = p.IsMain,
                                 CreatedAt = p.CreatedAt,
                                 SubPages = PrepareGetSubPages(p.Id)
                             }).ToList();

                return pages;
            }
            else
            {
                return new List<OnlineAuction.Data.Models.PagesModel>();
            }
        }
        public List<OnlineAuction.Data.Models.SubPageModel> PrepareGetSubPages(int id)
        {
            if (id > 0)
            {
                var pages = (from sp in _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetAll(x => x.ParentId == id, include: x => x.Include(pb => pb.PageBanners).Include(psf => psf.PageSpecification)).ToList()
                             select new SubPageModel()
                             {
                                 Id = sp.Id,
                                 ParentId = sp.ParentId,
                                 Rank = sp.Rank,
                                 SpecificationId = sp.SpecificationId,
                                 RedirectionLink = sp.RedirectionLink,
                                 PageSpecification = new PageSpecificationsModel()
                                 {
                                     Id = sp.PageSpecification.Id,
                                     Name = sp.PageSpecification.Name,
                                     IsActive = sp.PageSpecification.IsActive,
                                     CreatedAt = sp.CreatedAt
                                 },
                                 PageBanners = (from pb in sp.PageBanners
                                                select new PageBannerModel()
                                                {
                                                    Id = pb.Id,
                                                    PageId = pb.PageId,
                                                    PictureUrl = pb.PictureUrl,
                                                    CreatedAt = pb.CreatedAt
                                                }).ToList(),
                                 Title_tr = sp.Title_tr,
                                 Title_en = sp.Title_en,
                                 Keywords_tr = sp.Keywords_tr,
                                 Keywords_en = sp.Keywords_en,
                                 Detail_tr = sp.Detail_tr,
                                 Detail_en = sp.Detail_en,
                                 Description_tr = sp.Description_tr,
                                 Description_en = sp.Description_en,
                                 IsActive = sp.IsActive,
                                 IsFooter = sp.IsFooter,
                                 IsMain = sp.IsMain,
                                 CreatedAt = sp.CreatedAt
                             }).ToList();

                return pages;
            }
            else
            {
                return new List<OnlineAuction.Data.Models.SubPageModel>();
            }
        }

        #endregion

        #region Get

        public List<OnlineAuction.Data.Models.PagesModel> GetPages()
        {
            var pages = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetAll(include: x => x.Include(pb => pb.PageBanners).Include(psf => psf.PageSpecification)).OrderByDescending(x => x.CreatedAt).ToList();

            if (pages.Any())
            {
                return PrepareGetPages(pages);
            }
            else
            {
                return new List<OnlineAuction.Data.Models.PagesModel>();
            }
        }

        public OnlineAuction.Data.DbEntity.Pages GetPageById(int id)
        {
            if (id <= 0)
                return new OnlineAuction.Data.DbEntity.Pages();

            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetFirstOrDefault(predicate: x => x.Id == id, include: source => source.Include(b => b.PageBanners).Include(spf => spf.PageSpecification));
        }

        public List<OnlineAuction.Data.DbEntity.Pages> GetGalleryPagesById(int id)
        {
            if (id <= 0)
                return new List<OnlineAuction.Data.DbEntity.Pages>();

            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetAll(predicate: x => x.ParentId == id && x.SpecificationId == (int)PageType.Gallery, include: source => source.Include(b => b.PageBanners).Include(spf => spf.PageSpecification)).ToList();
        }

        public List<PageSpecifications> GetPageSpecifications()
        {
            return _unitOfWork.GetRepository<PageSpecifications>().GetAll().ToList();
        }

        #endregion

        #region Add
        public ReturnModel<object> Add(PageRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            OnlineAuction.Data.DbEntity.Pages pageEntity = new Data.DbEntity.Pages();
            List<PageBanner> pageBanners = new List<PageBanner>();

            using (var transaction = _unitOfWork.DbContext.Database.BeginTransaction())
            {
                try
                {
                    #region Update Fields

                    pageEntity.ParentId = model.Page.ParentId;
                    pageEntity.IsActive = model.Page.IsActive;
                    pageEntity.IsFooter = model.Page.IsFooter;
                    pageEntity.IsMain = model.Page.IsMain;
                    pageEntity.RedirectionLink = model.Page.RedirectionLink;
                    pageEntity.SpecificationId = model.Page.SpecificationId;

                    pageEntity.Title_tr = model.Page.Title_tr;
                    pageEntity.Keywords_tr = model.Page.Keywords_tr;
                    pageEntity.Description_tr = model.Page.Description_tr;
                    pageEntity.Detail_tr = model.Page.Detail_tr;

                    pageEntity.Title_en = model.Page.Title_en;
                    pageEntity.Keywords_en = model.Page.Keywords_en;
                    pageEntity.Description_en = model.Page.Description_en;
                    pageEntity.Detail_en = model.Page.Detail_en;
                    pageEntity.Rank = model.Page.Rank;

                    #endregion

                    var page = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Insert(pageEntity);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        #region Prepare Page Banner Model

                        if (model.Page.PageBanners.Any())
                        {
                            foreach (var item in model.Page.PageBanners)
                            {
                                pageBanners.Add(new PageBanner()
                                {
                                    PageId = page.Id,
                                    PictureUrl = item.PictureUrl
                                });
                            }

                            page.PageBanners = pageBanners;

                            _unitOfWork.GetRepository<PageBanner>().Insert(pageBanners);
                            _unitOfWork.SaveChanges();
                        }

                        #endregion

                        transaction.Commit();

                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sayfa başarıyla eklendi";
                        returnModel.Data = page.Id;
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
                    ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                    transaction.Rollback();

                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sayfa eklenirken hata oluştu";
                    return returnModel;
                }
            }
        }
        public ReturnModel<object> GalleryAdd(PageRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            OnlineAuction.Data.DbEntity.Pages pageEntity = new Data.DbEntity.Pages();
            List<PageBanner> pageBanners = new List<PageBanner>();

            using (var transaction = _unitOfWork.DbContext.Database.BeginTransaction())
            {
                try
                {
                    #region Update Fields

                    pageEntity.SpecificationId = (int)PageType.Gallery;
                    pageEntity.Rank = model.Page.Rank;
                    pageEntity.ParentId = model.Page.ParentId;
                    pageEntity.IsActive = model.Page.IsActive;
                    pageEntity.IsFooter = model.Page.IsFooter;
                    pageEntity.IsMain = model.Page.IsMain;
                    pageEntity.RedirectionLink = model.Page.RedirectionLink;

                    pageEntity.Title_tr = model.Page.Title_tr;
                    pageEntity.Keywords_tr = model.Page.Keywords_tr;
                    pageEntity.Description_tr = model.Page.Description_tr;
                    pageEntity.Detail_tr = model.Page.Detail_tr;

                    pageEntity.Title_en = model.Page.Title_en;
                    pageEntity.Keywords_en = model.Page.Keywords_en;
                    pageEntity.Description_en = model.Page.Description_en;
                    pageEntity.Detail_en = model.Page.Detail_en;

                    #endregion

                    var page = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Insert(pageEntity);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        #region Prepare Page Banner Model

                        if (model.Page.PageBanners.Any())
                        {
                            foreach (var item in model.Page.PageBanners)
                            {
                                pageBanners.Add(new PageBanner()
                                {
                                    PageId = page.Id,
                                    PictureUrl = item.PictureUrl
                                });
                            }

                            page.PageBanners = pageBanners;

                            _unitOfWork.GetRepository<PageBanner>().Insert(pageBanners);
                            _unitOfWork.SaveChanges();
                        }

                        #endregion

                        transaction.Commit();

                        returnModel.IsSuccess = true;
                        returnModel.Message = "Galeri Sayfası başarıyla eklendi";
                        returnModel.Data = page.Id;
                        return returnModel;
                    }
                    else
                    {
                        transaction.Rollback();

                        returnModel.IsSuccess = false;
                        returnModel.Message = "Galeri Sayfası eklenemedi";
                        return returnModel;
                    }
                }
                catch (Exception ex)
                {
                    ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                    transaction.Rollback();

                    returnModel.IsSuccess = false;
                    returnModel.Message = "Galeri Sayfası eklenirken hata oluştu";
                    return returnModel;
                }
            }

        }
        #endregion

        #region Update

        public ReturnModel<object> Update(PageRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            List<PageBanner> pageBanners = new List<PageBanner>();

            using (var transaction = _unitOfWork.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var page = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetFirstOrDefault(predicate: x => x.Id == model.Page.Id);

                    if (page != null)
                    {
                        #region Update Fields

                        page.ParentId = model.Page.ParentId;
                        page.IsActive = model.Page.IsActive;
                        page.IsFooter = model.Page.IsFooter;
                        page.IsMain = model.Page.IsMain;
                        page.RedirectionLink = model.Page.RedirectionLink;
                        page.SpecificationId = model.Page.SpecificationId;

                        page.Title_tr = model.Page.Title_tr;
                        page.Keywords_tr = model.Page.Keywords_tr;
                        page.Description_tr = model.Page.Description_tr;
                        page.Detail_tr = model.Page.Detail_tr;

                        page.Title_en = model.Page.Title_en;
                        page.Keywords_en = model.Page.Keywords_en;
                        page.Description_en = model.Page.Description_en;
                        page.Detail_en = model.Page.Detail_en;
                        page.Rank = model.Page.Rank;

                        #endregion

                        _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Update(page);

                        var result = _unitOfWork.SaveChanges();
                        if (result > 0)
                        {
                            if (model.Page.PageBanners.Any())
                            {
                                foreach (var item in model.Page.PageBanners)
                                {
                                    var pageBanner = _unitOfWork.GetRepository<PageBanner>().GetFirstOrDefault(predicate: x => x.Id == item.Id);
                                    if (pageBanner != null)
                                    {
                                        pageBanner.PictureUrl = item.PictureUrl;
                                        _unitOfWork.GetRepository<PageBanner>().Update(pageBanner);
                                    }
                                    else
                                    {
                                        pageBanners.Add(new PageBanner()
                                        {
                                            PageId = page.Id,
                                            PictureUrl = item.PictureUrl
                                        });
                                        _unitOfWork.GetRepository<PageBanner>().Insert(pageBanners);
                                    }
                                }

                                _unitOfWork.SaveChanges();
                            }
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
                    ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                    transaction.Rollback();

                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sayfa eklenirken hata oluştu";
                    return returnModel;
                }
            }
        }

        public ReturnModel<object> GalleryUpdate(PageRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            List<PageBanner> pageBanners = new List<PageBanner>();

            using (var transaction = _unitOfWork.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var page = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetFirstOrDefault(predicate: x => x.Id == model.Page.Id);

                    if (page != null)
                    {
                        #region Update Fields

                        page.Rank = model.Page.Rank;
                        page.IsActive = model.Page.IsActive;
                        page.IsFooter = model.Page.IsFooter;
                        page.IsMain = model.Page.IsMain;
                        page.RedirectionLink = model.Page.RedirectionLink;
                        page.SpecificationId = (int)PageType.Gallery;

                        page.Title_tr = model.Page.Title_tr;
                        page.Keywords_tr = model.Page.Keywords_tr;
                        page.Description_tr = model.Page.Description_tr;
                        page.Detail_tr = model.Page.Detail_tr;

                        page.Title_en = model.Page.Title_en;
                        page.Keywords_en = model.Page.Keywords_en;
                        page.Description_en = model.Page.Description_en;
                        page.Detail_en = model.Page.Detail_en;


                        #endregion

                        _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Update(page);

                        var result = _unitOfWork.SaveChanges();
                        if (result > 0)
                        {
                            if (model.Page.PageBanners.Any())
                            {
                                foreach (var item in model.Page.PageBanners)
                                {
                                    var pageBanner = _unitOfWork.GetRepository<PageBanner>().GetFirstOrDefault(predicate: x => x.Id == item.Id);
                                    if (pageBanner != null)
                                    {
                                        pageBanner.PictureUrl = item.PictureUrl;
                                        _unitOfWork.GetRepository<PageBanner>().Update(pageBanner);
                                    }
                                    else
                                    {
                                        pageBanners.Add(new PageBanner()
                                        {
                                            PageId = page.Id,
                                            PictureUrl = item.PictureUrl
                                        });
                                        _unitOfWork.GetRepository<PageBanner>().Insert(pageBanners);
                                    }
                                }

                                _unitOfWork.SaveChanges();
                            }
                            transaction.Commit();

                            returnModel.IsSuccess = true;
                            returnModel.Message = "Galeri Sayfası başarıyla düzenlendi";
                            return returnModel;
                        }
                        else
                        {
                            transaction.Rollback();

                            returnModel.IsSuccess = false;
                            returnModel.Message = "Galeri Sayfası düzenlendi";
                            return returnModel;
                        }
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Galeri Sayfası bulunamadı";
                        return returnModel;
                    }
                }
                catch (Exception ex)
                {
                    ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                    transaction.Rollback();

                    returnModel.IsSuccess = false;
                    returnModel.Message = "Galeri Sayfası eklenirken hata oluştu";
                    return returnModel;
                }
            }
        }

        public ReturnModel<object> PageIsMainUpdate(PageRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var page = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetFirstOrDefault(predicate: x => x.Id == model.Page.Id);
                if (page != null)
                {
                    page.IsMain = model.Page.IsMain;
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Update(page);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sayfa başarıyla düzenlendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sayfa düzenlenemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sayfa bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sayfa eklenirken hata oluştu";
            }

            return returnModel;
        }

        public ReturnModel<object> PageRangUpdate(PageRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var pages = _unitOfWork.GetRepository<Data.DbEntity.Pages>().GetAll().ToList();

                if (pages.Any())
                {
                    foreach (var item in model.Pages)
                    {
                        var page = pages.FirstOrDefault(predicate: x => x.Id == item.Id);
                        if (page != null)
                        {
                            page.Rank = item.Rank;
                            _unitOfWork.GetRepository<Data.DbEntity.Pages>().Update(page);
                        }
                    }

                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sayfa başarıyla düzenlendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sayfa başarıyla düzenlenemdi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sayfa bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sayfa düzenlenirken hata oluştu";
            }

            return returnModel;
        }

        #endregion

        #region Delete

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

                if (page != null)
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sayfa Silinirken Bir Hata Oluştu";
                return returnModel;
            }
        }

        public ReturnModel<object> DeleteAllData()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var pages = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetAll(include: source => source.Include(b => b.PageBanners));

                if (pages.Any())
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Delete(pages);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sayfalar Silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sayfalar Silinemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sayfalar Bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sayfa Silinirken Bir Hata Oluştu";
                return returnModel;
            }
        }

        #endregion
    }
}
