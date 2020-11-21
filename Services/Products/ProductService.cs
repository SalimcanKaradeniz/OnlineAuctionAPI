using Microsoft.EntityFrameworkCore;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineAuction.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        public ProductService(IUnitOfWork<OnlineAuctionContext> unitOfWork,
            IAppContext appContext,
            IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _appContext = appContext;
            _serviceProvider = serviceProvider;
        }

        public List<OnlineAuction.Data.DbEntity.Products> GetProducts()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().GetAll(include: x => x.Include(source => source.ProductCategory).Include(source => source.ProductType).Include(source => source.Artist)).ToList();
        }

        public OnlineAuction.Data.DbEntity.Products GetProductById(int id)
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().GetFirstOrDefault(predicate: x => x.Id == id);
        }

        public ReturnModel<object> Add(ProductRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            OnlineAuction.Data.DbEntity.Products product = new OnlineAuction.Data.DbEntity.Products();

            try
            {
                product.Name = model.Product.Name;
                product.CategoryId = model.Product.CategoryId;
                product.ArtistId = model.Product.ArtistId;
                product.Type = model.Product.Type;
                product.DiscountRate = model.Product.DiscountRate;
                product.Code = model.Product.Code;
                product.Stock = model.Product.Stock;
                product.Description_tr = model.Product.Description_tr;
                product.Description_en = model.Product.Description_en;
                product.Price = model.Product.Price;
                product.IsActive = model.Product.IsActive;
                

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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Eklenirken Hata Oluştu";
            }

            return returnModel;
        }

        public ReturnModel<object> Update(ProductRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            try
            {
                var product = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Products>().GetFirstOrDefault(predicate: x => x.Id == model.Product.Id);

                if (product != null)
                {
                    product.Name = model.Product.Name;
                    product.CategoryId = model.Product.CategoryId;
                    product.ArtistId = model.Product.ArtistId;
                    product.Type = model.Product.Type;
                    product.DiscountRate = model.Product.DiscountRate;
                    product.Code = model.Product.Code;
                    product.Stock = model.Product.Stock;
                    product.Description_tr = model.Product.Description_tr;
                    product.Description_en = model.Product.Description_en;
                    product.Price = model.Product.Price;
                    product.IsActive = model.Product.IsActive;

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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Düzenlenirken Hata Oluştu";
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Silinirken Hata Oluştu";
            }

            return returnModel;
        }
    }
}
