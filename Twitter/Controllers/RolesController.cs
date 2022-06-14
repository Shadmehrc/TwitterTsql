using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ServiceInterfaces;
using Core.ApiResponse;
using Core.Models;
using Infrastructure.SQL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleCrudService _iRolRoleCrudService;
        public RolesController(IRoleCrudService iRolRoleCrudService)
        {
            _iRolRoleCrudService = iRolRoleCrudService;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> AddRole([FromQuery] AddRoleModel role)
        {
            var result = await _iRolRoleCrudService.AddRole(role);
            return result ? new ApiResponse().Success("Role successfully added.")
                : new ApiResponse().FailedToFind("Some error happened.");
        }

        [HttpGet("ShowRoles")]
        public async Task<IActionResult> ShowRoles()
        {
            var roles = await _iRolRoleCrudService.ShowRoles();
            return Ok(roles);
        }

        [HttpPost("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser([FromQuery] AddRoleToUserModel model)
        {
            var result = await _iRolRoleCrudService.AddRoleToUser(model);
            return result ? new ApiResponse().Success("Role successfully added to the user.")
                : new ApiResponse().FailedToFind("Some error happened.");
        }

        [HttpGet("UserRoles")]
        public async Task<IActionResult> ShowUserRoles(string id)
        {
            var role = await _iRolRoleCrudService.ShowUserRoles(id);
            return Ok(role);
        }

        [HttpGet("RoleUsers")]
        public async Task<IActionResult> GetUsersInRole(string name)
        {
            var result = await _iRolRoleCrudService.GetUsersInRole(name);
            return Ok(result);
        }

    }
}
