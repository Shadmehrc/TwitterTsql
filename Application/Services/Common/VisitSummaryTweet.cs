using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Common
{
    public class VisitSummaryTweet
    {
        public Task<List<string>> CheckTweets(List<string> tweetsList)
        {
            var newTweetsList = new List<string>();
            foreach (var item in tweetsList)
            {
                var eachTweetsSeparatedBySpace = item.Split(' ').ToList();
                var fixedTweet = new List<string>();
                var eachTweetWordCount = 0;
                foreach (var tw in eachTweetsSeparatedBySpace)
                {
                    eachTweetWordCount += tw.Length;
                    if (eachTweetWordCount <= 30 && fixedTweet.Count < 3)
                    {
                        fixedTweet.Add(tw);
                    }
                    else
                    {
                        continue;
                    }
                }
                var oneTweet = "";
                foreach (var it in fixedTweet)
                {
                    oneTweet += it + " ";
                }

                newTweetsList.Add(oneTweet);
            }
            return Task.FromResult(newTweetsList);
        }
    }
}
