using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Models
{
   public class GetTextTweetModel
    {
        public Tweet Tweet{ get; set; }
        public List<string> UserlistName{ get; set; }
    }
}
