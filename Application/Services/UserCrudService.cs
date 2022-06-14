using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces;
using Application.Services.Common;
using Core.Entities;
using Core.Models;

namespace Application.Services
{
    public class UserCrudService : IUserCrudService
    {
        private readonly IUserRepository _iUserRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly VisitSummaryTweet __visitSummaryTweet;
        public UserCrudService(IUserRepository iUserRepository, VisitSummaryTweet visitSummaryTweet, INotificationRepository notificationRepository)
        {
            _iUserRepository = iUserRepository;
            __visitSummaryTweet = visitSummaryTweet;
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> AddClaimToUser(AddClaimToUserModel model)
        {

            var claim = new Claim(model.ClaimType, model.ClaimValue, ClaimValueTypes.String);
            var user = new AddClaimToUserModelDTO()
            {
                Claim = claim,
                UserName = model.UserName
            };
            var result = await _iUserRepository.AddClaimToUser(user);
            return result;
        }

        public async Task<bool> CreateUser(UserRegisterModel model)
        {
            var user = new User() { FullName = model.FullName, Email = model.Email, UserName = model.UserName };
            var userModel = new CreateUserModelDTO()
            {
                User = user,
                Password = model.Password
            };
            var result = await _iUserRepository.CreateUser(userModel);
            return result;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var result = await _iUserRepository.DeleteUser(id);
            return result;
        }

        public async Task<bool> EditUser(UserEditModel userModel)
        {
            var result = await _iUserRepository.EditUser(userModel);
            return result;
        }

        public async Task<User> Find(int id)
        {
            var result = await _iUserRepository.FindUser(id);
            return result;
        }

        public async Task<User> FindUser(int id)
        {
            var result = await _iUserRepository.FindUser(id);
            return result;
        }
        public async Task<List<string>> SearchUserTweets(string id)
        {
            var user = await _iUserRepository.SearchUserTweets(id);

            var tweetsText = new List<string>();
            if (user != null)
                foreach (var item in user.Tweets)
                {
                    tweetsText.Add(item.Text);
                }
            var fixedTweets = await __visitSummaryTweet.CheckTweets(tweetsText);
            return fixedTweets;
        }

        public  async Task<List<string>> GetUserNotifications(string userId)
        {
            var result =await _notificationRepository.GetUserNotifications(userId);
            return result;
        }


        public async Task<IList<Claim>> UserClaims(string name)
        {
            var result = await _iUserRepository.UserClaims(name);
            return result;
        }
    }
}
