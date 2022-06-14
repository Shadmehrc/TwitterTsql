using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.RepositoryInterfaces;
using Core.Entities;
using Core.Models;
using Infrastructure.SQL.Context;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.SQL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public RoleRepository(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public Task<bool> AddRole(Role role)
        {
            var result = _roleManager.CreateAsync(role).Result;
            if (result.Succeeded)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }

        }
        public Task<List<Role>> ShowRoles()
        {
            var result = _roleManager.Roles.ToList();
            return Task.FromResult(result);
        }

        public Task<bool> AddRoleToUser(AddRoleToUserModel model)
        {
            var user = _userManager.FindByIdAsync(model.UserId).Result;
            var result = _userManager.AddToRoleAsync(user, model.RoleName).Result;
            if (result.Succeeded)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }

        }
        public Task<IList<string>> ShowUserRoles(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            var result = _userManager.GetRolesAsync(user).Result;
            var userName = user.FullName;
            result.Add(userName);
            return Task.FromResult(result);
        }
        public Task<IList<User>> GetUsersInRole(string name)
        {
            var result = _userManager.GetUsersInRoleAsync(name).Result;
            return Task.FromResult(result);
        }
    }
}
