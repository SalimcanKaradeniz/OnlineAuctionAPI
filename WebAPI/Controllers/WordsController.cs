using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using OnlineAuction.Data.Models.Users;
using OnlineAuction.Services.Users;
using OnlineAuction.Services.Words;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordsController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly IWordsService _wordsService;
        public WordsController(IOptions<AppSettings> appSettings,
            IWordsService wordsService)
        {
            _appSettings = appSettings.Value;
            _wordsService = wordsService;
        }

        #region Words

        #region Get

        [HttpGet]
        [Route("/words")]
        public IActionResult GetWords()
        {
            return Ok(_wordsService.GetWords());
        }

        [HttpGet]
        [Route("/words/{id}")]
        public IActionResult GetWords([FromRoute] int id)
        {
            if (id <= 0)
                return Ok(new OnlineAuction.Data.DbEntity.Words());

            return Ok(_wordsService.GetWordsById(id));
        }

        #endregion

        #region Add

        [HttpPost]
        [Route("/words/add")]
        public IActionResult Add([FromBody] WordsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _wordsService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        #endregion

        #region Update

        [HttpPost]
        [Route("/words/update")]
        public IActionResult Update([FromBody] WordsRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _wordsService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        #endregion

        #region Delete

        [HttpPost]
        [Route("/words/delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Kelime bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _wordsService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/words/deleteall")]
        public IActionResult DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            returnModel = _wordsService.DeleteAll();

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        #endregion

        #endregion
    }
}