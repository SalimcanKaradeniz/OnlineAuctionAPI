using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Pages
{
    public interface IPageService
    {
        List<OnlineAuction.Data.DbEntity.Pages> GetPages();
        List<PageSpecifications> GetPageSpecifications();
        ReturnModel<object> Add(OnlineAuction.Data.DbEntity.Pages model);
        ReturnModel<object> Update(OnlineAuction.Data.DbEntity.Pages model);
        ReturnModel<object> Delete(int id);
        OnlineAuction.Data.DbEntity.Pages GetPageById(int id);
    }
}
