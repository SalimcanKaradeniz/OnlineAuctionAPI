using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Data.Models
{
    public class FileUploadResult
    {
        public bool IsSuccess { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string Directory { get; set; }
        public string FullPath { get; set; }
        public string Url { get; set; }
        public string ErrorMessage { get; set; }
    }
}
