using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Log
{
    public interface ILogService
    {
        ReturnModel<object> Add(LogSystem model);
    }
}
