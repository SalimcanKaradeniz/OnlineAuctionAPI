using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Words
{
    public interface IWordsService
    {
        List<OnlineAuction.Data.DbEntity.Words> GetWords();
        Data.DbEntity.Words GetWordsById(int id);
        ReturnModel<object> Add(WordsRequestModel model);
        ReturnModel<object> Update(WordsRequestModel model);
        ReturnModel<object> Delete(int id);
        ReturnModel<object> DeleteAll();
    }
}
