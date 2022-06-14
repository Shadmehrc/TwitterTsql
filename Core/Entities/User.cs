using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Core.Entities
{
    public class User:IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Tweet> Tweets { get; set; }
        public ICollection<PhotoTweet> PhotoTweets { get; set; }
        public ICollection<Notification>    Notifications { set; get; }
        public bool IsRemoved { get; set; }
    }
}
