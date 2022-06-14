using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Models
{
    public class TweetPostModelView
    {
        public string TweetText { get; set; }
        public List<string> Tags { get; set; }
        public string UserId { get; set; }
        public List<string> TagUsers { get; set; }
    }
}
