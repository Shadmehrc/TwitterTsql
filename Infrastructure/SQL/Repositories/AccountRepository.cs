using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces;
using Core.Entities;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.SQL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;

        public AccountRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public Task<User> Login(UserLoginModel userModel)
        {
            var user = _userManager.FindByNameAsync(userModel.UserName).Result;
            return Task.FromResult(user);
        }

        public Task<bool> Logout()
        {
            return Task.FromResult(true);//
        }

        public Task<User> JwtToken(AuthenticationJwtDTO model)
        {
            var user = _userManager.FindByNameAsync(model.Username).Result;
            var result = _userManager.CheckPasswordAsync(user, model.Password).Result;
            if (result)
            {
                return Task.FromResult(user);
            }
            else
            {
                return Task.FromResult<User>(null);
            }
        }
    }
}
