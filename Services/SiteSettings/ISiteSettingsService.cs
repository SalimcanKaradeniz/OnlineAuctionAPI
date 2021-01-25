using Microsoft.AspNetCore.Http;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.SiteSettingsService
{
    public interface ISiteSettingsService
    {
        public List<SiteSettings> GetSiteSettings();
        ReturnModel<object> Add(SiteSettings model);
        ReturnModel<object> Update(SiteSettingsRequestModel model/*, IFormFile logo*/);
        ReturnModel<object> Delete(int id);
        SiteSettings GetSiteSettingsById(int id);
    }
}
