using Core.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineAuction.Core.Models;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Enums.Pages;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineAuction.Services.Pages
{
    public class PageService : IPageService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _appSettings;
        public PageService(IUnitOfWork<OnlineAuctionContext> unitOfWork,
            IAppContext appContext,
            IServiceProvider serviceProvider,
            IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appContext = appContext;
            _serviceProvider = serviceProvider;
            _appSettings = appSettings.Value;
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
                                 Title = p.Title,
                                 Keywords = p.Keywords,
                                 Detail = p.Detail,
                                 Description = p.Description,
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
                                 Title = sp.Title,
                                 Keywords = sp.Keywords,
                                 Detail = sp.Detail,
                                 Description = sp.Description,
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

                    pageEntity.ParentId = model.ParentId;
                    pageEntity.IsActive = model.IsActive;
                    pageEntity.IsFooter = model.IsFooter;
                    pageEntity.IsMain = model.IsMain;
                    pageEntity.RedirectionLink = model.RedirectionLink;
                    pageEntity.SpecificationId = model.SpecificationId;
                    pageEntity.Title = model.Title;
                    pageEntity.Keywords = model.Keywords;
                    pageEntity.Description = model.Description;
                    pageEntity.Detail = model.Detail;
                    pageEntity.Rank = model.Rank;

                    #endregion

                    var page = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Insert(pageEntity);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        #region Prepare Page Banner Model

                        if (model.PageBanners.Any())
                        {
                            foreach (var item in model.PageBanners)
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
                    ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
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
                    pageEntity.ParentId = model.ParentId;
                    pageEntity.IsActive = model.IsActive;
                    pageEntity.IsFooter = model.IsFooter;
                    pageEntity.IsMain = model.IsMain;
                    pageEntity.RedirectionLink = model.RedirectionLink;
                    pageEntity.Title = model.Title;
                    pageEntity.Keywords = model.Keywords;
                    pageEntity.Description = model.Description;
                    pageEntity.Detail = model.Detail;
                    pageEntity.Rank = model.Rank;

                    #endregion

                    var page = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Insert(pageEntity);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        #region Prepare Page Banner Model

                        if (model.PageBanners.Any())
                        {
                            foreach (var item in model.PageBanners)
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
                    ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
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
                    var page = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetFirstOrDefault(predicate: x => x.Id == model.Id);

                    if (page != null)
                    {
                        #region Update Fields

                        page.ParentId = model.ParentId;
                        page.IsActive = model.IsActive;
                        page.IsFooter = model.IsFooter;
                        page.IsMain = model.IsMain;
                        page.RedirectionLink = model.RedirectionLink;
                        page.SpecificationId = model.SpecificationId;

                        page.Title = model.Title;
                        page.Keywords = model.Keywords;
                        page.Description = model.Description;
                        page.Detail = model.Detail;
                        page.Rank = model.Rank;

                        #endregion

                        _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Update(page);

                        var result = _unitOfWork.SaveChanges();
                        if (result > 0)
                        {
                            if (model.PageBanners.Any())
                            {
                                foreach (var item in model.PageBanners)
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
                    ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
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
                    var page = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetFirstOrDefault(predicate: x => x.Id == model.Id);

                    if (page != null)
                    {
                        #region Update Fields

                        page.SpecificationId = (int)PageType.Gallery;

                        page.ParentId = model.ParentId;
                        page.IsActive = model.IsActive;
                        page.IsFooter = model.IsFooter;
                        page.IsMain = model.IsMain;
                        page.RedirectionLink = model.RedirectionLink;
                        page.Title = model.Title;
                        page.Keywords = model.Keywords;
                        page.Description = model.Description;
                        page.Detail = model.Detail;
                        page.Rank = model.Rank;


                        #endregion

                        _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Update(page);

                        var result = _unitOfWork.SaveChanges();
                        if (result > 0)
                        {
                            if (model.PageBanners.Any())
                            {
                                foreach (var item in model.PageBanners)
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
                    ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
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
                var page = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetFirstOrDefault(predicate: x => x.Id == model.Id);
                if (page != null)
                {
                    page.IsMain = model.IsMain;
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
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

                var page = _unitOfWork.GetRepository<Data.DbEntity.Pages>().GetFirstOrDefault(predicate: x => x.Id == model.Id);
                if (page != null)
                {
                    page.Rank = model.Rank;
                    _unitOfWork.GetRepository<Data.DbEntity.Pages>().Update(page);
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sayfa bulunamadı";
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
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sayfa Silinirken Bir Hata Oluştu";
                return returnModel;
            }
        }

        public ReturnModel<object> GalleryAllData(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Galeri sayfası bulunamadı";
                return returnModel;
            }

            try
            {
                var pages = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().GetAll(x => x.ParentId == id && x.SpecificationId ==(int)PageType.Gallery, include: source => source.Include(b => b.PageBanners));

                if (pages.Any())
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Pages>().Delete(pages);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Galeri Sayfalar Silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Galeri Sayfalar Silinemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Galeri Sayfalar Bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = " Galeri Sayfa Silinirken Bir Hata Oluştu";
                return returnModel;
            }
        }

        #endregion
    }
}
