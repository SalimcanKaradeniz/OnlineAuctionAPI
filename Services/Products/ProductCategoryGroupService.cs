using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace OnlineAuction.Services.Products
{
    public class ProductCategoryGroupService : IProductCategoryGroupService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        public ProductCategoryGroupService(IUnitOfWork<OnlineAuctionContext> unitOfWork,
            IAppContext appContext,
            IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _appContext = appContext;
            _serviceProvider = serviceProvider;
        }
        public List<ProductCategoryGroup> GetProductCategoryGroups()
        {
            return _unitOfWork.GetRepository<ProductCategoryGroup>().GetAll().ToList();
        }

        public ProductCategoryGroup GetProductCategoryGroupsById(int id)
        {
            return _unitOfWork.GetRepository<ProductCategoryGroup>().GetFirstOrDefault(predicate: x => x.Id == id);
        }

        public ReturnModel<object> Add(ProductCategoryGroup model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                _unitOfWork.GetRepository<ProductCategoryGroup>().Insert(model);
                int result = _unitOfWork.SaveChanges();

                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Ürün Kategori Grubu Eklendi";
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Ürün Kategori Grubu Eklenemedi";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId:_appContext.UserId,serviceProvider:_serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Kategori Grubu Eklenirken Hata Oluştu";
            }

            return returnModel;
        }

        public ReturnModel<object> Update(ProductCategoryGroup model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var productCategoryGroup = _unitOfWork.GetRepository<ProductCategoryGroup>().GetFirstOrDefault(predicate: x => x.Id == model.Id);

                if (productCategoryGroup != null)
                {
                    productCategoryGroup.Name = model.Name;
                    _unitOfWork.GetRepository<ProductCategoryGroup>().Update(productCategoryGroup);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Ürün Kategori Grubu Düzenlendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Ürün Kategori Grubu Düzenlenemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Ürün Kategori Grubu Bulunamadı";
                }
                
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Kategori Grubu Düzenlenirken Hata Oluştu";
            }

            return returnModel;
        }

        public ReturnModel<object> Delete(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var productCategoryGroup = _unitOfWork.GetRepository<ProductCategoryGroup>().GetFirstOrDefault(predicate: x => x.Id == id);

                if (productCategoryGroup != null)
                {
                    _unitOfWork.GetRepository<ProductCategoryGroup>().Delete(productCategoryGroup);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Ürün Kategori Grubu Silindi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Ürün Kategori Grubu Silinenemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Ürün Kategori Grubu Bulunamadı";
                }

            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Kategori Grubu Silinirken Hata Oluştu";
            }

            return returnModel;
        }
    }
}
