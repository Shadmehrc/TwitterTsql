using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ServiceInterfaces;
using Core.ApiResponse;
using Core.Models;
using Infrastructure.SQL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserCrudService _iUserCrudService;
        public UserController(IUserCrudService iUserCrudService)
        {
            _iUserCrudService = iUserCrudService;
        }


        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create([FromQuery] UserRegisterModel user)
        {
            var result = await _iUserCrudService.CreateUser(user);
            return result ? new ApiResponse().Success("User successfully registered.")
                : new ApiResponse().FailedToFind("Some error happened");
        }


        [HttpGet("/ShowUserTweets")]
        public async Task<IActionResult> Search(string id)
        {
            var user = await _iUserCrudService.SearchUserTweets(id);
            return Ok(user);
        }
        [HttpPost("AddClaimToUser")]
        public async Task<IActionResult> AddClaim([FromQuery] AddClaimToUserModel model)
        {
            var result = await _iUserCrudService.AddClaimToUser(model);

            return result ? new ApiResponse().Success("Claim successfully added to user.")
                : new ApiResponse().FailedToFind("Some error happened");
        }

        [HttpGet("UserClaims")]
        public async Task<IActionResult> UserClaims(string userName)
        {
            var result = await _iUserCrudService.UserClaims(userName);
            return Ok(result);
        }
        [HttpGet("UserNotifications")]
        public async Task<IActionResult> UserNotifications(string userId)
        {
            var result =await _iUserCrudService.GetUserNotifications(userId);
            return Ok(result);
        }
        [HttpPut("EditUser")]
        public async Task<IActionResult> Edit([FromQuery] UserEditModel user)
        {
            var result = await _iUserCrudService.EditUser(user);
            return result ? new ApiResponse().Success("User successfully edited.")
                : new ApiResponse().FailedToFind("User not found.");
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _iUserCrudService.DeleteUser(id);
            return result ? new ApiResponse().Success("User successfully deleted.")
                : new ApiResponse().FailedToFind("User not found.");
        }

    }
}
