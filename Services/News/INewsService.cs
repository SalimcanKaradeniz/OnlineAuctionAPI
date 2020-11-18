using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.News
{
    public interface INewsService
    {
        List<OnlineAuction.Data.DbEntity.News> GetNews();
        List<OnlineAuction.Data.DbEntity.News> GetActiveNews();
        OnlineAuction.Data.DbEntity.News GetNewsById(int id);
        ReturnModel<object> Add(NewsRequestModel model);
        ReturnModel<object> Update(NewsRequestModel news);
        ReturnModel<object> NewIsActiveUpdate(NewsRequestModel model);
        ReturnModel<object> DeleteAll();
        ReturnModel<object> Delete(int id);
    }
}
