using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
    }
}