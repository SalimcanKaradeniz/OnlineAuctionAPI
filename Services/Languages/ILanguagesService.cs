using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Languages
{
    public interface ILanguagesService
    {
        public List<OnlineAuction.Data.DbEntity.Languages> GetLanguages();
        public List<OnlineAuction.Data.DbEntity.Languages> GetActiveLanguages();
        ReturnModel<object> Add(OnlineAuction.Data.DbEntity.Languages language);
        ReturnModel<object> Update(OnlineAuction.Data.DbEntity.Languages language);
        ReturnModel<object> Delete(int id);
        OnlineAuction.Data.DbEntity.Languages GetLangaugeById(int id);
    }
}
