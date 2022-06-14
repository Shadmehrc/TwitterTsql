using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Application.RepositoryInterfaces;
using Core;
using Core.Entities;
using Core.Models;
using Dapper;
using Infrastructure.SQL.Context;
using Mapster;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.SQL.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        private readonly DatabaseContext _context;
        private readonly string _connectionString;

        public TweetRepository(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _context = databaseContext;
            _connectionString = configuration["connection"];
        }
        public Task<bool> Retweet(RetweetModel retweetModel)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = sqlConnection.Query<bool>("Retweet", new { TweetId = retweetModel.TweetId, @UserId = retweetModel.UserId },
                commandType: CommandType.StoredProcedure);
            return Task.FromResult(result.First());
        }
        public Task<List<string>> FindUserTaggedTweets(string userId)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = sqlConnection.Query<string>("FindUserTaggedTweets", new { TweetId = userId },
                commandType: CommandType.StoredProcedure).ToList();
            return Task.FromResult(result);
        }
        public async Task<bool> CreateTextTweet(CreateTextTweetModelDTO model)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result =await sqlConnection.QueryMultipleAsync("CreateTextTweet", new { @Text = model.Text, @UserId = model.UserId, @Hashtag = model.Hashtags, @UserTagIds = model.UserTagIds },
                commandType: CommandType.StoredProcedure);
            return true;
        }
        public async Task<bool> CreatePhotoTweet(PhotoTweet model)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result =await sqlConnection.QueryAsync<bool>("CreatePhotoTweet",
                new { @PhotoAddress = model.PhotoAddress, @UserId = model.UserId },
                commandType: CommandType.StoredProcedure);
            return result.First();
        }
        public Task<PhotoTweet> GetPhotoTweet(int id)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = sqlConnection.Query<PhotoTweet>("GetPhotoTweet",
                new { @Id = id }, commandType: CommandType.StoredProcedure);
            return Task.FromResult(result.First());
        }
        public async Task<SqlMapper.GridReader> MostTaggedTweet()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = await sqlConnection.QueryMultipleAsync("GetTweetWithNMostTag",
                commandType: CommandType.StoredProcedure);
            return result;
        }
        public Task<bool> EditTweet(int id, string text)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = sqlConnection.Query<bool>("EditTweet",
                new { @Word = text, @Id = id },
                commandType: CommandType.StoredProcedure);
            return Task.FromResult(result.First());
        }
        public Task<bool> DeleteTweet(int id)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = sqlConnection.Query<bool>("DeleteTweet",
                new { @Id = id },
                commandType: CommandType.StoredProcedure);
            return Task.FromResult(result.First());
        }
        public async Task<SqlMapper.GridReader> GetTextTweet(int id)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var tweet = await sqlConnection.QueryMultipleAsync("FindTextTweet", new { @TweetId = id},
                    commandType: CommandType.StoredProcedure);
            await _context.SaveChangesAsync();
             return tweet;
        }
        public async Task<Tweet> MostViewedTweet()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = await sqlConnection.QueryAsync<Tweet>("MostViewedTweet", commandType: CommandType.StoredProcedure);
            return result.First();
        }
        public async Task<Tweet> MostLikedTweet()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = await sqlConnection.QueryAsync<Tweet>("MostLikedTweet",
                commandType: CommandType.StoredProcedure);
            return result.First();
        }
        public Task<bool> LikeTweet(int id)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = sqlConnection.Query<bool>("LikeTweet",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            return Task.FromResult(result.First());
        }
    }
}
