using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TweetTags
    {
        public int Id { get; set; }
        public int TweetId { get; set; }
        public int TagId { get; set; }
        public string Word { get; set; }
    }
}
