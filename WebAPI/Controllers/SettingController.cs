using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using OnlineAuction.Data.Models.Users;
using OnlineAuction.Services.FormSettingsService;
using OnlineAuction.Services.Languages;
using OnlineAuction.Services.SiteSettingsService;
using OnlineAuction.Services.SocialMediaSettingsService;
using OnlineAuction.Services.Users;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class SettingController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly ILanguagesService _languageService;
        private readonly ISocialMediaSettingsService _socialMediaSettingsService;
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly IFormSettingsService _formSettingsService;
        public SettingController(IOptions<AppSettings> appSettings,
            ILanguagesService languageService,
            ISocialMediaSettingsService socialMediaSettingsService,
            ISiteSettingsService siteSettingsService,
            IFormSettingsService formSettingsService)
        {
            _appSettings = appSettings.Value;
            _languageService = languageService;
            _socialMediaSettingsService = socialMediaSettingsService;
            _siteSettingsService = siteSettingsService;
            _formSettingsService = formSettingsService;
        }

        #region Site Settings

        [HttpGet]
        [Route("/sitesettings")]
        public IActionResult GetSiteSettings()
        {
            return Ok(_siteSettingsService.GetSiteSettings());
        }

        [HttpPost]
        [Route("/sitesettings/add")]
        public IActionResult SiteSettingsAdd([FromBody] SiteSettings model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _siteSettingsService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/sitesettings/update")]
        public IActionResult SiteSettingsUpdate([FromBody] SiteSettings model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _siteSettingsService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/sitesettings/delete/{id}")]
        public IActionResult SiteSettingDelete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Site Ayarı bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _siteSettingsService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        #endregion

        #region Form Settings
        [HttpGet]
        [Route("/formsettings")]
        public IActionResult GetFormSettings()
        {
            return Ok(_formSettingsService.GetFormSettings());
        }

        [HttpPost]
        [Route("/formsettings/add")]
        public IActionResult FormSettingsAdd([FromBody] FormSettings model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _formSettingsService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/formsettings/update")]
        public IActionResult FormSettingsUpdate([FromBody] FormSettings model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _formSettingsService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/formsettings/delete/{id}")]
        public IActionResult FormSettingDelete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Form Ayarı bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _formSettingsService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }
        #endregion

        #region Social Media Settings

        [HttpGet]
        [Route("/socialmediasettings")]
        public IActionResult GetSocialMediaSettings()
        {
            return Ok(_socialMediaSettingsService.GetSocialMediaSettings());
        }

        [HttpGet]
        [Route("/socialmediatypes")]
        public IActionResult GetSocialMediaTypes()
        {
            return Ok(_socialMediaSettingsService.GetSocialMediaTypes());
        }

        [HttpPost]
        [Route("/socialmediasettings/add")]
        public IActionResult SocialMediaSettingAdd([FromBody] SocialMediaSettings model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _socialMediaSettingsService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/socialmediasettings/update")]
        public IActionResult SocialMediaSettingUpdate([FromBody] SocialMediaSettings model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _socialMediaSettingsService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/socialmediasettings/delete/{id}")]
        public IActionResult SocialMediaSettingDelete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Sosyal Medya Ayarı bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _socialMediaSettingsService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        #endregion

        #region Languages

        [HttpGet]
        [Route("/languages")]
        public IActionResult GetLanguages() 
        {
            return Ok(_languageService.GetLanguages());
        }

        [HttpGet]
        [Route("/languages/getactivelanguages")]
        public IActionResult GetActiveLanguages()
        {
            return Ok(_languageService.GetActiveLanguages());
        }

        [HttpPost]
        [Route("/languages/add")]
        public IActionResult LanguagesAdd([FromBody] OnlineAuction.Data.DbEntity.Languages model) 
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _languageService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/languages/update")]
        public IActionResult LanguagesUpdate([FromBody] OnlineAuction.Data.DbEntity.Languages model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _languageService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/languages/delete/{id}")]
        public IActionResult LanguageDelete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Dil bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _languageService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        #endregion
    }
}