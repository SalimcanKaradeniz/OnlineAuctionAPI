using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.SocialMediaSettingsService
{
    public interface ISocialMediaSettingsService
    {
        public List<SocialMediaSettings> GetSocialMediaSettings();
        ReturnModel<object> Add(SocialMediaSettings model);
        ReturnModel<object> Update(SocialMediaSettings model);
        ReturnModel<object> Delete(int id);
        SocialMediaSettings GetSocialMediaSettingsById(int id);
        List<SocialMediaTypes> GetSocialMediaTypes();
    }
}
