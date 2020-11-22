using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Log;
using OnlineAuction.Services.Popups;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PopupsController : Controller
    {
        private readonly IPopupsService _popupsService;
        public PopupsController(IOptions<AppSettings> appSettings,
            IPopupsService popupsService,
            IServiceProvider serviceProvider,
            IAppContext appContext,
            ILogService logService)
        {
            _popupsService = popupsService;
        }


        [HttpGet]
        [Route("/popups")]
        public IActionResult Getpopups()
        {
            return Ok(_popupsService.GetPopups());
        }

        [HttpGet]
        [Route("/popups/{id}")]
        public IActionResult GetPopupById([FromRoute]int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Popup bulunamadı";
                return BadRequest(returnModel);
            }

            return Ok(_popupsService.GetPopupById(id));
        }

        [HttpGet]
        [Route("/activepopups")]
        public IActionResult GetActivePopups()
        {
            return Ok(_popupsService.GetActivePopups());
        }

        [HttpPost]
        [Route("/popups/add")]
        public IActionResult Add([FromBody] PopupsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _popupsService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/popups/update")]
        public IActionResult Update([FromBody] PopupsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _popupsService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/popups/isactiveupdate")]
        public IActionResult PopupIsActiveUpdate([FromBody] PopupsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _popupsService.PopupIsActiveUpdate(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }
        
        [HttpPost]
        [Route("/popups/delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Sanatçı bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _popupsService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/popups/deleteall")]
        public IActionResult DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            returnModel = _popupsService.DeleteAll();

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }
    }
}