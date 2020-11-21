using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Products
{
    public interface IProductCategoryService
    {
        List<ProductCategory> GetProductCategories();
        ProductCategory GetProductCategoryById(int id);
        ReturnModel<object> Add(ProductCategoryRequestModel model);
        ReturnModel<object> Update(ProductCategoryRequestModel model);
        ReturnModel<object> Delete(int id);
    }
}
