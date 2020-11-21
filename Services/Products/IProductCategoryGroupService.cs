using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Products
{
    public interface IProductCategoryGroupService
    {
        List<ProductCategoryGroup> GetProductCategoryGroups();
        ProductCategoryGroup GetProductCategoryGroupsById(int id);
        ReturnModel<object> Add(ProductCategoryGroup model);
        ReturnModel<object> Update(ProductCategoryGroup model);
        ReturnModel<object> Delete(int id);
    }
}
