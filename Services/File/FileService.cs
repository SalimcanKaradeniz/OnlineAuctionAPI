using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Log;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OnlineAuction.Services
{
    public class FileService : IFileService
    {
        private readonly IServiceProvider _serviceProvider;
        public FileService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Cdn için istek çıkan method
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public FileUploadResult FileUpload(IFormFile file)
        {
            try
            {
                FileUploadResult fileUploadResults = new FileUploadResult();

                if (file == null || file.Length == 0)
                    return fileUploadResults;

                RestRequest request = new RestRequest("File/Upload", Method.POST);

                request.AlwaysMultipartFormData = true;

                var text = file.FileName.Replace(" ", "-");
                var newFileName = String.Join("", text.Normalize(NormalizationForm.FormD)
                        .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

                newFileName = string.Format("{0}_{1}", new Random().Next(1, 999999).ToString(), newFileName);

                request.Files.Add(new FileParameter
                {
                    Name = file.Name,
                    Writer = (s) =>
                    {
                        var stream = file;
                        stream.CopyTo(s);
                    },
                    FileName = newFileName,
                    ContentType = file.ContentType,
                    ContentLength = file.Length
                });

                //IRestResponse response = _client.Execute(request);

                //if (response.IsSuccessful)
                //    fileUploadResults = JsonConvert.DeserializeObject<List<FileUploadResult>>(response.Content).FirstOrDefault();

                return fileUploadResults;
            }
            catch (Exception ex)
            {
                ex.InsertLog(serviceProvider: _serviceProvider);
                return new FileUploadResult();
            }
        }
    }
}
