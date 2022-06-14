using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Models;

namespace Application.ServiceInterfaces
{
    public interface IUserCrudService
    {
        public Task<User> FindUser(int id);
        public Task<bool> CreateUser(UserRegisterModel user);
        public Task<bool> AddClaimToUser(AddClaimToUserModel model);
        public Task<IList<Claim>> UserClaims(string name);
        public Task<User> Find(int id);
        public Task<bool> EditUser(UserEditModel userModel);
        public Task<bool> DeleteUser(int id);
        public Task<List<string>> SearchUserTweets(string name);
        public Task<List<string>> GetUserNotifications(string userId);
    }
}
