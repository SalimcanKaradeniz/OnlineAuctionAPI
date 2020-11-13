using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineAuction.Data.Models;
using Services.News;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly INewsService _newsService;
        public NewsController(IOptions<AppSettings> appSettings,
            INewsService newsService)
        {
            _appSettings = appSettings.Value;
            _newsService = newsService;
        }


        [HttpGet]
        [Route("/news")]
        public IActionResult GetNews()
        {
            return Ok(_newsService.GetNews());
        }

        [HttpGet]
        [Route("/news/{id}")]
        public IActionResult GetNewsById([FromRoute]int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Haber bulunamadı";
                return BadRequest(returnModel);
            }

            return Ok(_newsService.GetNewsById(id));
        }

        [HttpGet]
        [Route("/activenews")]
        public IActionResult GetActiveNews()
        {
            return Ok(_newsService.GetActiveNews());
        }

        [HttpPost]
        [Route("/news/add")]
        public IActionResult Add([FromBody] OnlineAuction.Data.DbEntity.News model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _newsService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/news/update")]
        public IActionResult Update([FromBody] OnlineAuction.Data.DbEntity.News model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _newsService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/news/delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Sayfa bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _newsService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }
    }
}