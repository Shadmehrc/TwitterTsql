using System;
using System.Collections.Generic;
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

namespace Application.Services
{
    public class AccountManageService : IAccountManageService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountManageService(IAccountRepository accountRepository, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<bool> Login(UserLoginModel userModel)
        {
            var user = await _accountRepository.Login(userModel);
            var result = _signInManager.PasswordSignInAsync(user, userModel.Password, userModel.RememberMe, false)
                .Result;
            if (result.Succeeded)
                return true;
            else
                return false;
        }

        public Task<bool> Logout()
        {
            var result = _signInManager.SignOutAsync();
            if (result.IsCompletedSuccessfully)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public async Task<string> JwtToken(AuthenticationJwtDTO model)
        {
            var result = await _accountRepository.JwtToken(model);
            if (result != null)
            {
                var key = _configuration["JwtConfig:Key"];
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                    new Claim("UserId", result.Id),
                    new Claim("UserName", model.Username)
                };

                var token = new JwtSecurityToken
                (
                    issuer: _configuration["JwtConfig:issuer"],
                    audience: _configuration["JwtConfig:audience"],
                    expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JwtConfig:expires"])),
                    notBefore: DateTime.Now,
                    claims: claims,
                    signingCredentials: credential
                );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return (jwtToken);
            }
            else
            {
                return ("Username or password is wrong");
            }
        }
    }
}
