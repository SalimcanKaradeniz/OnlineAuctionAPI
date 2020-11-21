using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Pages;
using System.Collections.Generic;

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


        #region Pages

        #region Get

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

        #endregion

        #region Add

        [HttpPost]
        [Route("/pages/add")]
        public IActionResult Add([FromBody] PageRequestModel model)
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

        #endregion

        #region Update

        [HttpPost]
        [Route("/pages/update")]
        public IActionResult Update([FromBody] PageRequestModel model)
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
        [Route("/pages/ismainupdate")]
        public IActionResult PageIsMainUpdate([FromBody] PageRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _pageService.PageIsMainUpdate(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/pages/rangupdate")]
        public IActionResult PageRangUpdate([FromBody] PageRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _pageService.PageRangUpdate(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        #endregion

        #region Delete

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

        [HttpPost]
        [Route("/pages/deleteall")]
        public IActionResult DeleteAllData()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            returnModel = _pageService.DeleteAllData();

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        #endregion

        #endregion

        #region Gallery

        [HttpGet]
        [Route("/subgallerypages/{id}")]
        public IActionResult GetGalleryPagesById([FromRoute]int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Galeri Sayfası bulunamadı";
                return BadRequest(returnModel);
            }

            return Ok(_pageService.GetGalleryPagesById(id));
        }

        [HttpPost]
        [Route("/subgallerypages/add")]
        public IActionResult GalleryAdd([FromBody] PageRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _pageService.GalleryAdd(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/subgallerypages/update")]
        public IActionResult GalleryUpdate([FromBody] PageRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _pageService.GalleryUpdate(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/subgallerypages/deleteall/{id}")]
        public IActionResult DeleteAllData([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Galeri Sayfası Bulunamadı";
                return BadRequest(returnModel);
            }

            returnModel = _pageService.DeleteAllData();

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }
        #endregion
    }
}