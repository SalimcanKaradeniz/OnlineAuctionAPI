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
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        public ProductCategoryService(IUnitOfWork<OnlineAuctionContext> unitOfWork,
            IAppContext appContext,
            IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _appContext = appContext;
            _serviceProvider = serviceProvider;
        }

        public List<ProductCategory> GetProductCategories()
        {
            return _unitOfWork.GetRepository<ProductCategory>().GetAll().ToList();
        }

        public ProductCategory GetProductCategoryById(int id)
        {
            return _unitOfWork.GetRepository<ProductCategory>().GetFirstOrDefault(predicate: x => x.Id == id);
        }

        public ReturnModel<object> Add(ProductCategoryRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            ProductCategory productCategory = new ProductCategory();

            try
            {
                productCategory.Name = model.ProductCategory.Name;
                productCategory.ShortDescription_tr = model.ProductCategory.ShortDescription_tr;
                productCategory.ShortDescription_en = model.ProductCategory.ShortDescription_en;
                productCategory.PictureUrl = model.ProductCategory.PictureUrl;

                _unitOfWork.GetRepository<ProductCategory>().Insert(productCategory);
                int result = _unitOfWork.SaveChanges();

                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Ürün Kategori Eklendi";
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Ürün Kategori Eklenemedi";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Kategori Grubu Eklenirken Hata Oluştu";
            }

            return returnModel;
        }

        public ReturnModel<object> Update(ProductCategoryRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            try
            {
                var productCategory = _unitOfWork.GetRepository<ProductCategory>().GetFirstOrDefault(predicate: x => x.Id == model.ProductCategory.Id);

                if (productCategory != null)
                {
                    productCategory.Name = model.ProductCategory.Name;
                    productCategory.ShortDescription_tr = model.ProductCategory.ShortDescription_tr;
                    productCategory.ShortDescription_en = model.ProductCategory.ShortDescription_en;
                    productCategory.PictureUrl = model.ProductCategory.PictureUrl;

                    _unitOfWork.GetRepository<ProductCategory>().Update(productCategory);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Ürün Kategori Eklendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Ürün Kategori Eklenemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Ürün Kategori Bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Kategorisi Düzenlenirken Hata Oluştu";
            }

            return returnModel;
        }

        public ReturnModel<object> Delete(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            try
            {
                var productCategory = _unitOfWork.GetRepository<ProductCategory>().GetFirstOrDefault(predicate: x => x.Id == id);

                if (productCategory != null)
                {
                    _unitOfWork.GetRepository<ProductCategory>().Delete(productCategory);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Ürün Kategori Silindi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Ürün Kategori Silinemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Ürün Kategori Bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Kategorisi Silinirken Hata Oluştu";
            }

            return returnModel;
        }
    }
}
