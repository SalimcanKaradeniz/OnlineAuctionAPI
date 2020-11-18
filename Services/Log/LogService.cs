using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Log
{
    public class LogService : ILogService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        public LogService(IUnitOfWork<OnlineAuctionContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ReturnModel<object> Add(LogSystem model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            _unitOfWork.GetRepository<LogSystem>().Insert(model);
            
            int result = _unitOfWork.SaveChanges();
            
            if (result > 0)
            {
                returnModel.IsSuccess = true;
                returnModel.Message = "Log Eklendi";
            }
            else
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Log Eklenemedi";
            }
           
            return returnModel;
        }
    }
}
