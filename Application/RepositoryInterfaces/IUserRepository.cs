using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Models;

namespace Application.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public Task<User> FindUser(int id);
        public Task<bool> CreateUser(CreateUserModelDTO user);
        public Task<bool> AddClaimToUser(AddClaimToUserModelDTO model);
        public Task<IList<Claim>> UserClaims(string name);
        public Task<bool> EditUser(UserEditModel userModel);
        public Task<bool> DeleteUser(int id);
        public Task<User> SearchUserTweets(string name);
    }
}
