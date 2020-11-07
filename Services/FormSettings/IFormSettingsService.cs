using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.FormSettingsService
{
    public interface IFormSettingsService
    {
        public List<FormSettings> GetFormSettings();
        ReturnModel<object> Add(FormSettings model);
        ReturnModel<object> Update(FormSettings model);
        ReturnModel<object> Delete(int id);
        FormSettings GetFormSettingById(int id);
    }
}
