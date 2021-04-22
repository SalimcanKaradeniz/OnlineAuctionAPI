using Microsoft.AspNetCore.Http;
using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Exhibitions
{
    public interface IExhibitionsService
    {
        List<OnlineAuction.Data.DbEntity.Exhibitions> GetExhibitions();
        List<OnlineAuction.Data.DbEntity.Exhibitions> GetActiveExhibitions();
        OnlineAuction.Data.DbEntity.Exhibitions GetExhibitionById(int id);
        ReturnModel<object> Add(ExhibitionsRequestModel model);
        ReturnModel<object> Update(ExhibitionsRequestModel model);
        ReturnModel<object> ExhibitionsIsActiveUpdate(ExhibitionsRequestModel model);
        ReturnModel<object> Delete(int id);
        ReturnModel<object> DeleteAll();
    }
}
