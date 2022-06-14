using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.ServiceInterfaces;
using Core.ApiResponse;
using Core.Entities;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManageService _accountManageService;
        public AccountsController(IAccountManageService accountManageService)
        {

            _accountManageService = accountManageService;
        }

        [HttpPost("JwtAuth")]
        public async Task<IActionResult> Post([FromQuery] AuthenticationJwtDTO model)
        {
            var result = await _accountManageService.JwtToken(model);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromQuery] UserLoginModel user)
        {
            var result = await _accountManageService.Login(user);
            return result ? new ApiResponse().Success("Successfully logged in.")
                : new ApiResponse().FailedToFind("Some error happened.");
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _accountManageService.Logout();
            return result ? new ApiResponse().Success("Successfully logged out.")
                : new ApiResponse().FailedToFind("Some error happened.");
        }

    }
}
