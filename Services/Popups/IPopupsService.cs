using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Popups
{
    public interface IPopupsService
    {
        List<OnlineAuction.Data.DbEntity.Popups> GetPopups();
        List<OnlineAuction.Data.DbEntity.Popups> GetActivePopups();
        OnlineAuction.Data.DbEntity.Popups GetPopupById(int id);
        ReturnModel<object> Add(PopupsRequestModel model);
        ReturnModel<object> Update(PopupsRequestModel model);
        ReturnModel<object> PopupIsActiveUpdate(PopupsRequestModel model);
        ReturnModel<object> Delete(int id);
        ReturnModel<object> DeleteAll();
    }
}
