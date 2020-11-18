using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Languages;
using OnlineAuction.Services.Log;
using Services.Artists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.News
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        private readonly ILanguagesService _languagesService;
        private readonly IAppContext _appContext;
        private readonly IServiceProvider _serviceProvider;
        public ArtistService(IUnitOfWork<OnlineAuctionContext> unitOfWork,
            ILanguagesService languagesService,
            IServiceProvider serviceProvider,
            IAppContext appContext)
        {
            _unitOfWork = unitOfWork;
            _languagesService = languagesService;
            _serviceProvider = serviceProvider;
            _appContext = appContext;
        }

        public List<OnlineAuction.Data.DbEntity.Artists> GetArtists()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Artists>().GetAll().ToList();
        }

        public List<OnlineAuction.Data.DbEntity.Artists> GetActiveArtists()
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Artists>().GetAll(x => x.IsActive).ToList();
        }

        public OnlineAuction.Data.DbEntity.Artists GetArtistById(int id)
        {
            return _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Artists>().GetFirstOrDefault(predicate: x => x.Id == id);
        }

        public ReturnModel<object> Add(ArtistsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            OnlineAuction.Data.DbEntity.Artists artistEntity = new OnlineAuction.Data.DbEntity.Artists();

            try
            {
                artistEntity.NameSurname = model.Artist.NameSurName;
                artistEntity.Picture = model.Artist.Picture;
                artistEntity.IsActive = model.Artist.IsActive;
                artistEntity.BirthDate = model.Artist.BirthDate;
                artistEntity.DateOfDeath = model.Artist.DateOfDeath;
                artistEntity.About_tr = model.Artist.About_tr;
                artistEntity.About_en = model.Artist.About_en;
                
                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Artists>().Insert(artistEntity);

                var result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Sanatçı başarıyla eklendi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sanatçı eklenemedi";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sanatçı eklenirken hata oluştu";
                return returnModel;
            }
        }

        public ReturnModel<object> Update(ArtistsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var artist = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Artists>().GetFirstOrDefault(predicate: x => x.Id == model.Artist.Id);

                if (artist != null)
                {
                    artist.NameSurname = model.Artist.NameSurName;
                    artist.Picture = model.Artist.Picture;
                    artist.IsActive = model.Artist.IsActive;
                    artist.BirthDate = model.Artist.BirthDate;
                    artist.DateOfDeath = model.Artist.DateOfDeath;
                    artist.About_tr = model.Artist.About_tr;
                    artist.About_en = model.Artist.About_en;

                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Artists>().Update(artist);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sanatçı başarıyla düzenlendi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sanatçı düzenlenemedi";
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sanatçı düzenlenirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> ArtistIsActiveUpdate(ArtistsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var artist = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Artists>().GetFirstOrDefault(predicate: x => x.Id == model.Artist.Id);
                if (artist != null)
                {
                    artist.IsActive = model.Artist.IsActive;
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Artists>().Update(artist);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sanatçı başarıyla düzenlendi";
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sanatçı düzenlenemedi";
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sanatçı bulunamadı";
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sanatçı eklenirken hata oluştu";
            }

            return returnModel;
        }
        public ReturnModel<object> Delete(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var data = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Artists>().GetFirstOrDefault(predicate: x => x.Id == id);

                if (data != null)
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Artists>().Delete(data);

                    var result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Sanatçı başarıyla silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Sanatçı silinemedi";
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
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sanatçı silinirken hata oluştu";
                return returnModel;
            }
        }
        public ReturnModel<object> DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var artists = _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Artists>().GetAll();

                _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Artists>().Delete(artists);

                var result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Sanatçı başarıyla silindi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Sanatçı silinemedi";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                ex.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider);
                returnModel.IsSuccess = false;
                returnModel.Message = "Sanatçı silinirken hata oluştu";
                return returnModel;
            }
        }
    }
}
