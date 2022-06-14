using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
   public class ShowTweetModelFromDapper
    {
        public int Id{ get; set; }
        public string Text{ get; set; }
        public string UserId{ get; set; }
        public int TagCount { get; set; }
        public int TweetViewCount { get; set; }
        public int Likes { get; set; }


    }
}
