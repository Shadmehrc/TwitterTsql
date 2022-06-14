using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Entities
{
   public class PhotoTweet
    {
        public int Id { get; set; }
        public string PhotoAddress { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public bool IsRemoved { get; set; }
    }
}
