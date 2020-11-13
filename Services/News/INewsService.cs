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
        ReturnModel<object> Add(OnlineAuction.Data.DbEntity.News news);
        ReturnModel<object> Update(OnlineAuction.Data.DbEntity.News news);
        ReturnModel<object> Delete(int id);
    }
}
