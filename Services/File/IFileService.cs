using Microsoft.AspNetCore.Http;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services
{
    public interface IFileService
    {
        FileUploadResult FileUpload(IFormFile file);
    }
}
