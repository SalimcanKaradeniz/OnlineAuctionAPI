using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Pages;
using Services.Artists;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ArtistController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly IArtistService _artistService;
        public ArtistController(IOptions<AppSettings> appSettings,
            IArtistService artistService)
        {
            _appSettings = appSettings.Value;
            _artistService = artistService;
        }


        [HttpGet]
        [Route("/artists")]
        public IActionResult GetArtists()
        {
            return Ok(_artistService.GetArtists());
        }

        [HttpGet]
        [Route("/artists/{id}")]
        public IActionResult GetArtistById([FromRoute]int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Sanatçı bulunamadı";
                return BadRequest(returnModel);
            }

            return Ok(_artistService.GetArtistById(id));
        }

        [HttpGet]
        [Route("/activeartists")]
        public IActionResult GetActiveNews()
        {
            return Ok(_artistService.GetActiveArtists());
        }

        [HttpPost]
        [Route("/artists/add")]
        public IActionResult Add([FromBody] ArtistsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _artistService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/artists/update")]
        public IActionResult Update([FromBody] ArtistsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _artistService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/artists/isactiveupdate")]
        public IActionResult ArtistIsActiveUpdate([FromBody] ArtistsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _artistService.ArtistIsActiveUpdate(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/artists/delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Sanatçı bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _artistService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/artists/deleteall")]
        public IActionResult DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            returnModel = _artistService.DeleteAll();

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }
    }
}