using Microsoft.AspNetCore.Http;
using OnlineAuction.Core.UnitOfWork;
using OnlineAuction.Data.Context;
using OnlineAuction.Data.DbEntity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineAuction.Services.Log
{
    public static class LogExtensions
    {
        public static void InsertLog(this Exception exception, int userId = -99999, HttpRequest request = null, IServiceProvider serviceProvider = null)
        {
            StringBuilder s = new StringBuilder();
            s.AppendLine("Exception type: " + exception.GetType().FullName);
            s.AppendLine();
            s.AppendLine("Message       : " + exception.Message);
            s.AppendLine();
            s.AppendLine("Stacktrace:");
            s.AppendLine(exception.StackTrace);
            s.AppendLine();
            string message = s.ToString();

            var path = request == null ? "-" : request.Path.Value;
            if (request != null && request.QueryString != null && request.QueryString.HasValue)
            {
                path = $"{path}/{request.QueryString.Value}";
            }

            var logSystem = serviceProvider.GetService<ILogService>().Add(new LogSystem
            {
                UserId = userId,
                Url = path,
                ShortDescription = exception.Message,
                Detail = message,
                Title = "Hata"
            });
        }
    }
}
