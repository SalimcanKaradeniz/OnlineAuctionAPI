using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Pages
{
    public interface IPageService
    {
        #region Get

        List<OnlineAuction.Data.Models.PagesModel> GetPages();
        List<PageSpecifications> GetPageSpecifications();
        List<OnlineAuction.Data.DbEntity.Pages> GetGalleryPagesById(int id);
        OnlineAuction.Data.DbEntity.Pages GetPageById(int id);

        #endregion

        #region Add

        ReturnModel<object> Add(PageRequestModel model);
        ReturnModel<object> GalleryAdd(PageRequestModel model);

        #endregion

        #region Update

        ReturnModel<object> Update(PageRequestModel model);
        ReturnModel<object> GalleryUpdate(PageRequestModel model);
        ReturnModel<object> PageIsMainUpdate(PageRequestModel model);
        ReturnModel<object> PageRangUpdate(PageRequestModel model);

        #endregion

        #region Delete

        ReturnModel<object> DeleteAllData();
        ReturnModel<object> Delete(int id);

        #endregion
    }
}
