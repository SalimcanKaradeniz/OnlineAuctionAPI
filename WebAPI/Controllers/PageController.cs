using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Pages;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PageController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly IPageService _pageService;
        public PageController(IOptions<AppSettings> appSettings,
            IPageService pageService)
        {
            _appSettings = appSettings.Value;
            _pageService = pageService;
        }


        [HttpGet]
        [Route("/pages")]
        public IActionResult GetPages()
        {
            return Ok(_pageService.GetPages());
        }

        [HttpGet]
        [Route("/pages/{id}")]
        public IActionResult GetPageById([FromRoute]int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Sayfa bulunamadı";
                return BadRequest(returnModel);
            }

            return Ok(_pageService.GetPageById(id));
        }

        [HttpGet]
        [Route("/pagespecifications")]
        public IActionResult GetPageSpecifications() 
        {
            return Ok(_pageService.GetPageSpecifications());
        }

        [HttpPost]
        [Route("/pages/add")]
        public IActionResult Add([FromBody] OnlineAuction.Data.DbEntity.Pages model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _pageService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/pages/update")]
        public IActionResult Update([FromBody] OnlineAuction.Data.DbEntity.Pages model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _pageService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/pages/delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Sayfa bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _pageService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }
    }
}