using System;
using System.Collections.Generic;
using System.Data;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces;
using Core;
using Core.Entities;
using Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class TweetCrudService : ITweetCrudService
    {
        private readonly ITweetRepository _iTweetRepository;
        private readonly INotificationRepository _notificationRepository;

        public TweetCrudService(ITweetRepository iTweetRepository, IConfiguration configuration, INotificationRepository notificationRepository)
        {
            _iTweetRepository = iTweetRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> CreateTextTweet(TweetPostModelView tweetModel)
        {
            var hashtags = string.Empty;
            var userTagIds = string.Empty;
            if (tweetModel.TagUsers!=null)
            {
                 userTagIds = string.Join(',', tweetModel.TagUsers);
            }
            if (tweetModel.Tags != null)
            {
             hashtags = string.Join(',', tweetModel.Tags);

            }
            var model = new CreateTextTweetModelDTO()
            {
                UserId = tweetModel.UserId,
                Text = tweetModel.TweetText,
                Hashtags = hashtags,
                UserTagIds = userTagIds
            };

            var result = await _iTweetRepository.CreateTextTweet(model);
            if (result == true)
            {
                if (tweetModel.TagUsers != null)
                    foreach (var item in tweetModel.TagUsers)
                    {
                        var sendNotif =
                            await _notificationRepository.SendTaggedUserNotification(tweetModel.UserId, item);
                    }
            }
            return result;
        }

        public async Task<bool> CreatePhotoTweet(CreatePhotoTweetModelDTO model)
        {
            if (model.Photo != null)
            {
                var stream = new FileStream(model.Photo.FileName, FileMode.Create);
                await model.Photo.CopyToAsync(stream);
                var tweet = new PhotoTweet()
                {
                    PhotoAddress = stream.Name,
                    UserId = model.UserId
                };
                var result = await _iTweetRepository.CreatePhotoTweet(tweet);
                return result;
            }

            return false;
        }

        public async Task<bool> DeleteTweet(int id)
        {
            var result = await _iTweetRepository.DeleteTweet(id);

            return result;
        }

        public async Task<bool> EditTweet(int id, string text)
        {
            var result = await _iTweetRepository.EditTweet(id, text);
            return result;
        }

        public async Task<ShowPhotoModel> GetPhotoTweet(int id)
        {
            var result = await _iTweetRepository.GetPhotoTweet(id);
            var tweetWithPhoto = new ShowPhotoModel()
            {
                Photo = await File.ReadAllBytesAsync(result.PhotoAddress)
            };
            return tweetWithPhoto;
        }

        public async Task<ShowMostTaggedTweetModel> MostTaggedTweet()
        {
            var result = await _iTweetRepository.MostTaggedTweet();
            var tweet = result.Read<Tweet>();
            var tags = result.Read<string>().ToList();
            var model = new ShowMostTaggedTweetModel
            {
                Id = tweet.First().Id,
                TagCount = tweet.First().TagCount,
                Word = tags,
                Text = tweet.First().Text
            };
            return model;
        }

        public async Task<Tweet> MostViewedTweet()
        {
            var result = await _iTweetRepository.MostViewedTweet();
            return result;
        }

        public async Task<Tweet> MostLikedTweet()
        {
            var result = await _iTweetRepository.MostLikedTweet();
            return result;
        }

        public async Task<SearchTweetByIdModelView> GetTextTweet(int id)
        {
            var result = await _iTweetRepository.GetTextTweet(id);
            var tweet = await result.ReadAsync<ShowTweetModelFromDapper>();
            var tags = await result.ReadAsync<ShowTagsModelFromDapper>();
            var users = await result.ReadAsync<ShowUsersTagModelFromDapper>();

            var tagsList = new List<string>();
            foreach (var item in tags)
            {
                tagsList.Add(item.Word);
            } 
            var usersList = new List<string>();
            foreach (var item in users)
            {
                usersList.Add(item.FullName);
            }
            var model = new SearchTweetByIdModelView()
            {
                Tags = tagsList,
                Tweet = tweet,
                UserTagged = usersList
            };
            return model;
        }

        public async Task<bool> LikeTweet(int id)
        {
            var result = await _iTweetRepository.LikeTweet(id);
            return result;
        }

        public async Task<bool> Retweet(RetweetModel retweetModel)
        {
            var result = await _iTweetRepository.Retweet(retweetModel);
            return result;
        }

        public async Task<List<string>> FindUserTaggedTweets(string userId)
        {
            var result = await _iTweetRepository.FindUserTaggedTweets(userId);
            return result;
        }

    }
}
