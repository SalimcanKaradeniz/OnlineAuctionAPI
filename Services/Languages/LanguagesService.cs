using Core.Extensions;
using Microsoft.Extensions.Options;
using OnlineAuction.Core.Models;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineAuction.Services.Languages
{
    public class LanguagesService : ILanguagesService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _appSettings;

        public LanguagesService(IUnitOfWork<OnlineAuctionContext> unitOfWork,
            IAppContext appContext,
            IServiceProvider serviceProvider,
            IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _serviceProvider = serviceProvider;
            _appContext = appContext;
            _appSettings = appSettings.Value;
        }

        public List<OnlineAuction.Data.DbEntity.Languages> GetLanguages()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Languages>().GetAll().ToList();
        }

        public List<OnlineAuction.Data.DbEntity.Languages> GetActiveLanguages()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Languages>().GetAll(x => x.IsActive).ToList();
        }

        public ReturnModel<object> Add(OnlineAuction.Data.DbEntity.Languages language)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Languages>().Insert(language);
                int result = _unitOfWork.SaveChanges();

                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Dil Eklendi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Dil Eklenirken hata oluştu";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Dil Eklenirken hata oluştu";
                return returnModel;
            }
        }

        public ReturnModel<object> Update(OnlineAuction.Data.DbEntity.Languages model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var language = GetLangaugeById(model.Id);

                if (language != null)
                {
                    language.Name = model.Name;
                    language.Code = model.Code;
                    language.IsActive = model.IsActive;

                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Languages>().Update(language);

                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Dil düzenlendi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Dil düzenlenirken hata oluştu";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Dil bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Dil düzenlenirken hata oluştu";
                return returnModel;
            }
        }

        public ReturnModel<object> Delete(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                if (id <= 0)
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Dil bulunamadı";
                    return returnModel;
                }

                var language = GetLangaugeById(id);

                if (language != null)
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Languages>().Delete(language);

                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Dil Silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Dil silinirken hata oluştu";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Dil bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Dil silinirken hata oluştu";
                return returnModel;
            }
        }

        public OnlineAuction.Data.DbEntity.Languages GetLangaugeById(int id)
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Languages>().GetFirstOrDefault(predicate: x => x.Id == id);
        }
    }
}
