using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineAuction.Core.Models;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace OnlineAuction.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _appSettings;

        public ProductService(IUnitOfWork<OnlineAuctionContext> unitOfWork,
            IAppContext appContext,
            IServiceProvider serviceProvider,
            IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appContext = appContext;
            _serviceProvider = serviceProvider;
            _appSettings = appSettings.Value;
        }

        public List<OnlineAuction.Data.DbEntity.Products> GetProducts()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().GetAll(include: x => x.Include(source => source.ProductCategory).Include(source => source.ProductType).Include(source => source.Artist)).ToList();
        }

        public OnlineAuction.Data.DbEntity.Products GetProductById(int id)
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().GetFirstOrDefault(predicate: x => x.Id == id);
        }

        public ReturnModel<object> Add(ProductsModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            OnlineAuction.Data.DbEntity.Products product = new OnlineAuction.Data.DbEntity.Products();

            try
            {
                product.Name = model.Name;
                product.CategoryId = model.CategoryId;
                product.ArtistId = model.ArtistId;
                product.Type = model.Type;
                product.DiscountRate = model.DiscountRate;
                product.Code = model.Code;
                product.Stock = model.Stock;
                product.Description = model.Description;
                product.Price = model.Price;
                product.IsActive = model.IsActive;
                product.Rank = model.Rank;
                product.LangId = model.LangId;

                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().Insert(product);
                int result = _unitOfWork.SaveChanges();

                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Ürün Eklendi";
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Ürün Eklenemedi";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Eklenirken Hata Oluştu";
            }

            return returnModel;
        }

        public ReturnModel<object> Update(ProductsModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            try
            {
                var product = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().GetFirstOrDefault(predicate: x => x.Id == model.Id);

                if (product != null)
                {
                    product.Name = model.Name;
                    product.CategoryId = model.CategoryId;
                    product.ArtistId = model.ArtistId;
                    product.Type = model.Type;
                    product.DiscountRate = model.DiscountRate;
                    product.Code = model.Code;
                    product.Stock = model.Stock;
                    product.Description = model.Description;
                    product.Price = model.Price;
                    product.IsActive = model.IsActive;
                    product.Rank = model.Rank;
                    product.LangId = model.LangId;

                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().Update(product);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Ürün Eklendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Ürün Eklenemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Ürün Bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Düzenlenirken Hata Oluştu";
            }

            return returnModel;
        }

        public ReturnModel<object> ProductIsActiveUpdate(ProductsModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            try
            {
                var product = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().GetFirstOrDefault(predicate: x => x.Id == model.Id);

                if (product != null)
                {
                    product.IsActive = model.IsActive;

                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().Update(product);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Ürün Durumu Güncellendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Ürün Durumu Güncellenemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Ürün Durumu Güncellenemedi";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Durumu Güncellenirken Hata Oluştu";
            }

            return returnModel;
        }

        public ReturnModel<object> ProductRankUpdate(ProductsModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            try
            {
                var product = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().GetFirstOrDefault(predicate: x => x.Id == model.Id);

                if (product != null)
                {
                    product.IsActive = model.IsActive;

                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().Update(product);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Ürün Durumu Güncellendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Ürün Durumu Güncellenemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Ürün Durumu Güncellenemedi";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Durumu Güncellenirken Hata Oluştu";
            }

            return returnModel;
        }

        public ReturnModel<object> Delete(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            try
            {
                var product = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().GetFirstOrDefault(predicate: x => x.Id == id);

                if (product != null)
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().Delete(product);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Ürün Silindi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Ürün Silinemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Ürün Bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Silinirken Hata Oluştu";
            }

            return returnModel;
        }

        public List<OnlineAuction.Data.DbEntity.ProductTypes> GetProductTypes()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.ProductTypes>().GetAll().ToList();
        }

        public ReturnModel<object> DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            try
            {
                var products = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().GetAll();

                if (products != null)
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().Delete(products);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Ürünler Silindi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Ürünler Silinemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Ürünler Bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürünler Silinirken Hata Oluştu";
            }

            return returnModel;
        }
    }
}
