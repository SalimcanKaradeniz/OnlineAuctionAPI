using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Artists;
using OnlineAuction.Services.Exhibitions;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ExhibitionsController : ControllerBase
    {
        private readonly IExhibitionsService _exhibitionsService;
        public ExhibitionsController(IExhibitionsService exhibitionsService)
        {
            _exhibitionsService = exhibitionsService;
        }

        [HttpGet]
        [Route("/exhibitions")]
        public IActionResult GetExhibitions()
        {
            return Ok(_exhibitionsService.GetExhibitions());
        }

        [HttpGet]
        [Route("/exhibitions/{id}")]
        public IActionResult GetExhibitionById([FromRoute]int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Sergi bulunamadı";
                return BadRequest(returnModel);
            }

            return Ok(_exhibitionsService.GetExhibitionById(id));
        }

        [HttpGet]
        [Route("/activeexhibitions")]
        public IActionResult GetActiveExhibitions()
        {
            return Ok(_exhibitionsService.GetActiveExhibitions());
        }

        [HttpPost]
        [Route("/exhibitions/add")]
        public IActionResult Add([FromBody] ExhibitionsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _exhibitionsService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/exhibitions/update")]
        public IActionResult Update([FromBody] ExhibitionsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _exhibitionsService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/exhibitions/isactiveupdate")]
        public IActionResult ExhibitionsIsActiveUpdate([FromBody] ExhibitionsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _exhibitionsService.ExhibitionsIsActiveUpdate(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/exhibitions/delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Sergi bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _exhibitionsService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/exhibitions/deleteall")]
        public IActionResult DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            returnModel = _exhibitionsService.DeleteAll();

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }
    }
}