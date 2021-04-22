using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.News
{
    public interface INewsService
    {
        List<OnlineAuction.Data.DbEntity.News> GetNews();
        List<OnlineAuction.Data.DbEntity.News> GetActiveNews();
        OnlineAuction.Data.DbEntity.News GetNewsById(int id);
        ReturnModel<object> Add(NewsModel model);
        ReturnModel<object> Update(NewsModel news);
        ReturnModel<object> NewIsActiveUpdate(NewsModel model);
        ReturnModel<object> DeleteAll();
        ReturnModel<object> Delete(int id);
    }
}
