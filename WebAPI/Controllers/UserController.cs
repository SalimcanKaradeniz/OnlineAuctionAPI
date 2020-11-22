﻿using System;
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

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;
        public UserController(IOptions<AppSettings> appSettings,
            IUserService userService)
        {
            _appSettings = appSettings.Value;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            ReturnModel<LoginResponseModel> returnModel = new ReturnModel<LoginResponseModel>();

            if (ModelState.IsValid)
            {
                var userLogin = _userService.Login(model);

                if (!userLogin.IsSuccess)
                    return Unauthorized(returnModel);

                returnModel.IsSuccess = true;
                returnModel.Data = userLogin.Data;

                return Ok(returnModel);
            }
            else
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Model doğrulanamadı";

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("/users")]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpGet]
        [Route("/users/{id}")]
        public IActionResult GetUserById([FromRoute]int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Kullanıcı bulunamadı";
                return BadRequest(returnModel);
            }

            return Ok(_userService.GetUserById(id));
        }


        [HttpPost]
        [Route("/users/add")]
        public IActionResult Add([FromBody] Users model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _userService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/users/update")]
        public IActionResult Update([FromBody] Users model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _userService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/users/isactiveupdate")]
        public IActionResult UserIsActiveUpdate([FromBody] Users model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _userService.UserIsActiveUpdate(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/users/delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Kullanıcı bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _userService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/users/deleteall")]
        public IActionResult DeleteAll()
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            returnModel = _userService.DeleteAll();

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }
    }
}