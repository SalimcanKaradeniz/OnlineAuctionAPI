using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Pages;
using OnlineAuction.Services.Sliders;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class SliderController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly ISliderService _sliderService;
        public SliderController(IOptions<AppSettings> appSettings,
            ISliderService sliderService)
        {
            _appSettings = appSettings.Value;
            _sliderService = sliderService;
        }


        [HttpGet]
        [Route("/sliders")]
        public IActionResult Getsliders()
        {
            return Ok(_sliderService.GetSliders());
        }

        [HttpGet]
        [Route("/sliders/{id}")]
        public IActionResult GetSliderById([FromRoute]int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Slider bulunamadı";
                return BadRequest(returnModel);
            }

            return Ok(_sliderService.GetSliderById(id));
        }

        [HttpGet]
        [Route("/activesliders")]
        public IActionResult GetActiveSliders()
        {
            return Ok(_sliderService.GetActiveSliders());
        }

        [HttpPost]
        [Route("/sliders/add")]
        public IActionResult Add([FromBody] SliderRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _sliderService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/sliders/update")]
        public IActionResult Update([FromBody] SliderRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _sliderService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/sliders/isactiveupdate")]
        public IActionResult SliderIsActiveUpdate([FromBody] SliderRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _sliderService.SliderIsActiveUpdate(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }
        
        [HttpPost]
        [Route("/sliders/delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Sanatçı bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _sliderService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/sliders/deleteall")]
        public IActionResult DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            returnModel = _sliderService.DeleteAll();

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/file/fileupload")]
        public IActionResult FileUplod([FromForm] IFormFile file) 
        {
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (file != null && file.Length > 0)
            {
                var fullPath = Path.Combine(pathToSave, file.FileName);
                var dbPath = Path.Combine(folderName, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok(new { dbPath });
            }
            else
            {
                return BadRequest();
            }

        }
    }
}