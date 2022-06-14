using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.ServiceInterfaces;
using Core.ApiResponse;
using Core.Entities;
using Core.Models;
using Infrastructure.SQL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Roles = ("Admin"))]
    public class TweetController : ControllerBase
    {
        private readonly ITweetCrudService _iTweetCrudService;

        public TweetController(ITweetCrudService iTweetCrudService)
        {
            _iTweetCrudService = iTweetCrudService;
        }

        [HttpPost("AddPhotoTweet")]
        public async Task<IActionResult> CreatePhotoTweet([FromQuery] CreatePhotoTweetModelDTO model)
        {
            var result = await _iTweetCrudService.CreatePhotoTweet(model);
            return result ? new ApiResponse().Success(true)
                : new ApiResponse().FailedToFind("User Not found");
        }


        [HttpPost("AddTextTweet")]
        public async Task<IActionResult> CreateText([FromQuery] TweetPostModelView tweetPostModelView)
        {
            var result = await _iTweetCrudService.CreateTextTweet(tweetPostModelView);
            return result ? new ApiResponse().Success(true)
                : new ApiResponse().FailedToFind("User Not found");
        }

        [HttpPost]
        [Route("Retweet")]
        public async Task<IActionResult> Retweet([FromQuery] RetweetModel retweetModel)
        {
            var result = await _iTweetCrudService.Retweet(retweetModel);
            return result ? new ApiResponse().Success(true) : new ApiResponse().FailedToFind("Tweet or user not found");
        }

        [HttpGet("ShowTextTweet")]
        public async Task<IActionResult> Get(int id)
        {
            var tweet = await _iTweetCrudService.GetTextTweet(id);
            if (tweet != null)
            {
                return Ok(tweet);
            }
            else
            {
                return Ok(new ApiResponse().FailedToFind("Tweet not found"));
            }

        }

        [HttpGet("ShowUserTaggedTweet")]
        public async Task<IActionResult> GetUserTaggedTweets(string userId)
        {
            var tweet = await _iTweetCrudService.FindUserTaggedTweets(userId);
            if (tweet != null)
            {
                return Ok(tweet);
            }
            else
            {
                return Ok(new ApiResponse().FailedToFind("User is not tagged in any tweets!"));
            }
        }
        [HttpGet("ShowPhotoTweet")]
        public async Task<IActionResult> GetPhotoTweet(int id)
        {
            var tweet = await _iTweetCrudService.GetPhotoTweet(id);
            if (tweet != null)
            {
                return new FileContentResult(tweet.Photo, "image/png");
            }
            return Ok(new ApiResponse().FailedToFind("Tweet not found"));
        }

        [HttpPut("EditTweet")]
        public async Task<IActionResult> Edit(int id, string text)
        {
            var result = await _iTweetCrudService.EditTweet(id, text);
            return result ? new ApiResponse().Success("Tweet successfully edited")
                : new ApiResponse().FailedToFind("User Not found");

        }

        [HttpPost("LikeTweet")]
        public async Task<IActionResult> LikeTweet(int id)
        {
            var result = await _iTweetCrudService.LikeTweet(id);
            return result ? new ApiResponse().Success("Tweet Liked")
                : new ApiResponse().FailedToFind("Tweet Not found");
        }

        [HttpDelete("DeleteTweet")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _iTweetCrudService.DeleteTweet(id);

            return result ? new ApiResponse().Success("Tweet successfully deleted.")
                : new ApiResponse().FailedToFind("Tweet not found.");
        }

        [HttpGet("MostTaggedTweet")]
        public async Task<IActionResult> FindMostTaggedTweet()
        {
            var result = await _iTweetCrudService.MostTaggedTweet();
            return Ok(result);
        }

        [HttpGet("MostViewedTweet")]
        public async Task<IActionResult> MostViewedTweet()
        {
            var result = await _iTweetCrudService.MostViewedTweet();
            return Ok(result);
        }
        [HttpGet("MostLikedTweet")]
        public async Task<IActionResult> MostTaggedTweet()
        {
            var result = await _iTweetCrudService.MostLikedTweet();
            return Ok(result);
        }
    }
}
