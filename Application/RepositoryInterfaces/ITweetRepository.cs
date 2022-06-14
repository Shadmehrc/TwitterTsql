using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Entities;
using Core.Models;
using Dapper;

namespace Application.RepositoryInterfaces
{
    public interface ITweetRepository
    {
        public Task<bool> CreateTextTweet(CreateTextTweetModelDTO createTextTweetModel);
        public Task<bool> CreatePhotoTweet(PhotoTweet model);
        public Task<bool> EditTweet(int id, string text);
        public Task<bool> DeleteTweet(int id);
        public Task<SqlMapper.GridReader> GetTextTweet(int id);
        public Task<PhotoTweet> GetPhotoTweet(int id);
        public Task<SqlMapper.GridReader> MostTaggedTweet();
        public Task<Tweet> MostViewedTweet();
        public Task<Tweet> MostLikedTweet();
        public Task<bool> LikeTweet(int id);
        public Task<bool> Retweet(RetweetModel retweetModel);
        public Task<List<string>> FindUserTaggedTweets(string userId);

    }
}
