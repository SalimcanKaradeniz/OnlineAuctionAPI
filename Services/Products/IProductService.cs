using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Products
{
    public interface IProductService
    {
        List<OnlineAuction.Data.DbEntity.Products> GetProducts();
        OnlineAuction.Data.DbEntity.Products GetProductById(int id);
        ReturnModel<object> Add(ProductsModel model);
        ReturnModel<object> Update(ProductsModel model);
        ReturnModel<object> ProductIsActiveUpdate(ProductsModel model);
        ReturnModel<object> Delete(int id);
        ReturnModel<object> DeleteAll();
        List<OnlineAuction.Data.DbEntity.ProductTypes> GetProductTypes();
    }
}
