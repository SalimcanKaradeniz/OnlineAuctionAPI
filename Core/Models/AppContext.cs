using Microsoft.AspNetCore.Http;
using System;

namespace OnlineAuction.Core.Models
{
    public interface IAppContext
    {
        int UserId { get; }
    }

    public class AppContext : IAppContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public int UserId
        {
            get
            {
                try
                {
                    string userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId").Value;

                    if (int.TryParse(userId, out int UserId))
                        return UserId;
                    else
                        return -1;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }
    }
}
