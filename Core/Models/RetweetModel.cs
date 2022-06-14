using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Models
{
    public class RetweetModel
    {
        public string UserId{ get; set; }
        public int TweetId{ get; set; }
        public List<string> tagRetweet = new List<string>(){"Retweet"};
    }
}
