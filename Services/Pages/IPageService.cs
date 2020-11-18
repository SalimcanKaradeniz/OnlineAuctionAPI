using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Pages
{
    public interface IPageService
    {
        List<OnlineAuction.Data.Models.PagesModel> GetPages();
        List<PageSpecifications> GetPageSpecifications();
        ReturnModel<object> Add(PageRequestModel model);
        ReturnModel<object> Update(PageRequestModel model);
        ReturnModel<object> PageIsActiveUpdate(PageRequestModel model);
        ReturnModel<object> PageRangUpdate(PageRequestModel model);
        ReturnModel<object> DeleteAllData();
        ReturnModel<object> Delete(int id);
        OnlineAuction.Data.DbEntity.Pages GetPageById(int id);
    }
}
