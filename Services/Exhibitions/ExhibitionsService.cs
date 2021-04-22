using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Languages;
using OnlineAuction.Services.Artists;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using OnlineAuction.Core.Models;
using Microsoft.Extensions.Options;
using OnlineAuction.Core.Extensions;
using Microsoft.AspNetCore.Http;
using OnlineAuction.Data.DbEntity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OnlineAuction.Services.Exhibitions
{
    public class ExhibitionsService : IExhibitionsService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly IFileService _fileService;
        private readonly AppSettings _appSettings;
        public ExhibitionsService(IUnitOfWork<OnlineAuctionContext> unitOfWork,
            IServiceProvider serviceProvider,
            IAppContext appContext,
            IOptions<AppSettings> appSettings,
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _serviceProvider = serviceProvider;
            _appContext = appContext;
            _appSettings = appSettings.Value;
            _fileService = fileService;
        }

        public List<OnlineAuction.Data.DbEntity.Exhibitions> GetExhibitions()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Exhibitions>().GetAll().ToList();
        }
        public List<OnlineAuction.Data.DbEntity.Exhibitions> GetActiveExhibitions()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Exhibitions>().GetAll(x => x.IsActive).ToList();
        }
        public OnlineAuction.Data.DbEntity.Exhibitions GetExhibitionById(int id)
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Exhibitions>().GetFirstOrDefault(predicate: x => x.Id == id);
        }
        public ReturnModel<object> Add(ExhibitionsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            OnlineAuction.Data.DbEntity.Artists artistEntity = new OnlineAuction.Data.DbEntity.Artists();

            try
            {
                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Exhibitions>().Insert(model.MapTo<Data.DbEntity.Exhibitions>());

                var result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Sergi başarıyla eklendi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sergi eklenemedi";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sergi eklenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> Update(ExhibitionsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var exhibition = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Exhibitions>().GetFirstOrDefault(predicate: x => x.Id == model.Id);

                if (exhibition != null)
                {

                    exhibition.Title = model.Title;
                    exhibition.ShortDescription = model.ShortDescription;
                    exhibition.StartDate = model.StartDate;
                    exhibition.EndDate = model.EndDate;
                    exhibition.ExhibitionPlace = model.ExhibitionPlace;
                    exhibition.LongDescription = model.LongDescription;
                    exhibition.Picture = model.Picture;
                    exhibition.PdfLink = model.PdfLink;
                    exhibition.IsOnline = model.IsOnline;
                    exhibition.IsActive = model.IsActive;
                    exhibition.Rank = model.Rank;

                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Exhibitions>().Update(exhibition);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sergi başarıyla düzenlendi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sergi düzenlenemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sergi bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sanatçı düzenlenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> ExhibitionsIsActiveUpdate(ExhibitionsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var exhibitions = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Exhibitions>().GetFirstOrDefault(predicate: x => x.Id == model.Id);
                if (exhibitions != null)
                {
                    exhibitions.IsActive = model.IsActive;
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Exhibitions>().Update(exhibitions);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sergi başarıyla düzenlendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sergi düzenlenemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sergi bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sergi eklenirken hata oluştu";
            }

            return returnModel;
        }
        public ReturnModel<object> Delete(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var data = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Exhibitions>().GetFirstOrDefault(predicate: x => x.Id == id);

                if (data != null)
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Exhibitions>().Delete(data);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sergi başarıyla silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sergi silinemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sanatçı bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sergi silinirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var exhibitions = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Exhibitions>().GetAll();

                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Exhibitions>().Delete(exhibitions);

                var result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Sergiler başarıyla silindi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sergiler silinemedi";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sergiler silinirken hata oluştu";
                return returnModel;
            }
        }
    }
}