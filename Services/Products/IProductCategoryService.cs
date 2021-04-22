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
        ReturnModel<object> Add(ProductCategoryModel model);
        ReturnModel<object> Update(ProductCategoryModel model);
        ReturnModel<object> CategoryIsActiveUpdate(ProductCategoryModel model);
        ReturnModel<object> Delete(int id);
    }
}
