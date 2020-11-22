using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineAuction.Services.Words
{
    public class WordsService : IWordsService
    {
        private readonly IUnitOfWork<OnlineAuctionContext> _unitOfWork;
        public WordsService(IUnitOfWork<OnlineAuctionContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Data.DbEntity.Words> GetWords()
        {
            return _unitOfWork.GetRepository<Data.DbEntity.Words>().GetAll().ToList();
        }

        public Data.DbEntity.Words GetWordsById(int id)
        {
            if (id <= 0)
                return new Data.DbEntity.Words();

            return _unitOfWork.GetRepository<Data.DbEntity.Words>().GetFirstOrDefault(predicate: x => x.Id == id);
        }

        public ReturnModel<object> Add(WordsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();
            Data.DbEntity.Words words = new Data.DbEntity.Words();

            try
            {
                words.Sef = ReplaceTurkishCharecter(model.Words.Value_tr).Replace(",", "-").Replace(".", "-").Replace(" ","-");
                words.Value_tr = model.Words.Value_tr;
                words.Value_en = model.Words.Value_en;

                _unitOfWork.GetRepository<Data.DbEntity.Words>().Insert(words);

                int result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.Message = "Kelimeler başarıyla eklendi";
                    return returnModel;
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Kelimeler eklenemedi";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Kelimeler eklenirken hata oluştu";
                return returnModel;
            }
        }

        public ReturnModel<object> Update(WordsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var word = _unitOfWork.GetRepository<Data.DbEntity.Words>().GetFirstOrDefault(predicate: x=> x.Id == model.Words.Id);

                if (word != null)
                {
                    word.Value_en = model.Words.Value_en;
                    word.Value_tr = model.Words.Value_tr;

                    _unitOfWork.GetRepository<Data.DbEntity.Words>().Update(word);
                    int result = _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Kelimeler başarıyla düzenlendi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Kelimeler düzenlenemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Kelimeler bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Kelimeler düzenlenirken hata oluştu";
                return returnModel;
            }
        }

        public ReturnModel<object> Delete(int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Kelime Bulunamadı";
                return returnModel;
            }

            try
            {
                var word = _unitOfWork.GetRepository<Data.DbEntity.Words>().GetFirstOrDefault(predicate: x => x.Id == id);

                if (word != null)
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Words>().Delete(word);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Kelime Silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Kelime Silinemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Kelime Bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Kelime Silinirken Bir Hata Oluştu";
                return returnModel;
            }
        }

        public ReturnModel<object> DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            try
            {
                var words = _unitOfWork.GetRepository<Data.DbEntity.Words>().GetAll().ToList();

                if (words != null)
                {
                    _unitOfWork.GetRepository<OnlineAuction.Data.DbEntity.Words>().Delete(words);
                    int result = _unitOfWork.SaveChanges();

                    if (result > 0)
                    {
                        returnModel.IsSuccess = true;
                        returnModel.Message = "Kelime Silindi";
                        return returnModel;
                    }
                    else
                    {
                        returnModel.IsSuccess = false;
                        returnModel.Message = "Kelime Silinemedi";
                        return returnModel;
                    }
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Kelime Bulunamadı";
                    return returnModel;
                }
            }
            catch (Exception ex)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Kelime Silinirken Bir Hata Oluştu";
                return returnModel;
            }
        }

        private string ReplaceTurkishCharecter(string data)
        {

            data = data.Replace("ü", "u"); 
            data = data.Replace("ı", "i");
            data = data.Replace("ö", "o");
            data = data.Replace("ü", "u");
            data = data.Replace("ş", "s");
            data = data.Replace("ğ", "g");
            data = data.Replace("ç", "c");
            data = data.Replace("Ü", "U");
            data = data.Replace("İ", "I");
            data = data.Replace("Ö", "O");
            data = data.Replace("Ü", "U");
            data = data.Replace("Ş", "S");
            data = data.Replace("Ğ", "G");
            data = data.Replace("Ç", "C");

            return data;
        }
    }
}
