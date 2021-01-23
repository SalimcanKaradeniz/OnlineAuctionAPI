using Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OnlineAuction.Core.Models;
using OnlineAuction.Data.Models;
using RestSharp;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace OnlineAuction.Services
{
    public class FileService : IFileService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _appSettings;
        private readonly IAppContext _appContext;

        public FileService(IServiceProvider serviceProvider, IOptions<AppSettings> appSettings, IAppContext appContext)
        {
            _serviceProvider = serviceProvider;
            _appSettings = appSettings.Value;
            _appContext = appContext;
        }

        /// <summary>
        /// Cdn için istek çıkan method
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        //public FileUploadResult FileUpload(IFormFile file)
        //{
        //    try
        //    {
        //        FileUploadResult fileUploadResults = new FileUploadResult();

        //        if (file == null || file.Length == 0)
        //            return fileUploadResults;

        //        RestRequest request = new RestRequest("File/Upload", Method.POST);

        //        request.AlwaysMultipartFormData = true;

        //        var text = file.FileName.Replace(" ", "-");
        //        var newFileName = String.Join("", text.Normalize(NormalizationForm.FormD)
        //                .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

        //        newFileName = string.Format("{0}_{1}", new Random().Next(1, 999999).ToString(), newFileName);

        //        request.Files.Add(new FileParameter
        //        {
        //            Name = file.Name,
        //            Writer = (s) =>
        //            {
        //                var stream = file;
        //                stream.CopyTo(s);
        //            },
        //            FileName = newFileName,
        //            ContentType = file.ContentType,
        //            ContentLength = file.Length
        //        });

        //        //IRestResponse response = _client.Execute(request);

        //        //if (response.IsSuccessful)
        //        //    fileUploadResults = JsonConvert.DeserializeObject<List<FileUploadResult>>(response.Content).FirstOrDefault();

        //        return fileUploadResults;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.InsertLog(serviceProvider: _serviceProvider);
        //        return new FileUploadResult();
        //    }
        //}

        public string FileUplod(IFormFile file)
        {
            try
            {
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file != null && file.Length > 0)
                {
                    string fileName = $"{DateTime.Now.Ticks.ToString()}_{file.FileName.Replace(" ", "_")}";
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName).Replace("\\", "/");
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    string filePath = $"{_appSettings.App.Link}{dbPath}";
                    return filePath;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                exception.InsertLog(userId: _appContext.UserId, serviceProvider: _serviceProvider, _appSettings: _appSettings);

                return null;
            }
        }
    }
}