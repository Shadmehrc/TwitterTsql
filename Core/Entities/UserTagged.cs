using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserTagged
    {
        public int Id{ get; set; }
        public string UserId{ get; set; }
        public int TweetId{ get; set; }
    }
}
