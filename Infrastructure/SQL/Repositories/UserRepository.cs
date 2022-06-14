using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.RepositoryInterfaces;
using Application.Services.Common;
using Core.Entities;
using Core.Models;
using Infrastructure.SQL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Infrastructure.SQL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;


        public UserRepository(DatabaseContext databaseContext, UserManager<User> user)
        {
            _context = databaseContext;
            _userManager = user;
        }
        public Task<User> FindUser(int id)
        {
            var result = _context.Users.FirstOrDefault(p => p.Id == id.ToString());
            return Task.FromResult(result);
        }
        public Task<bool> CreateUser(CreateUserModelDTO user)
        {
            var result = _userManager.CreateAsync(user.User, user.Password).Result;
            if (result.Succeeded)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }

        }
        public async Task<bool> AddClaimToUser(AddClaimToUserModelDTO model)
        {
            var user = _userManager.FindByNameAsync(model.UserName).Result;
            if (user != null)
            {
                var result = _userManager.AddClaimAsync(user, model.Claim).Result;
                return await Task.FromResult(result.Succeeded);
            }
            else
            {
                return false;
            }
        }
        public Task<IList<Claim>> UserClaims(string name)
        {
            var user = _userManager.FindByNameAsync(name).Result;
            var result = _userManager.GetClaimsAsync(user).Result;
            return Task.FromResult(result);
        }
        public async Task<bool> EditUser(UserEditModel userModel)
        {
            var user = _userManager.FindByIdAsync(userModel.Id).Result;
            if (user != null)
            {
                user.FullName = userModel.FullName;
                user.Email = userModel.EmailAddress;
                var result = _userManager.UpdateAsync(user).Result;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = FindUser(id);
            if (user != null)
            {
                _context.Users.Remove(await user);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        public Task<User> SearchUserTweets(string id)
        {
            //var users = _context.Users.ToList().Take(10).Skip(5).ToList();
            var user = _context.Users.Include(x => x.Tweets).FirstOrDefault(x => x.Id == id);
            return Task.FromResult<User>(user);
        }
    }
}
