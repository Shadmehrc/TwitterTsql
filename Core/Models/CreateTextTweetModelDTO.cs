using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
   public class CreateTextTweetModelDTO
    {
        public string Hashtags{ get; set; }
        public string UserTagIds { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }

    }
}
