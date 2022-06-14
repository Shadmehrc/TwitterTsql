using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Models
{
    public class CreateTextTweetModel
    {
        public List<string> TagsWords { get; set; }
        public List<UserTagged> UserTaggeds { get; set; }
        public Tweet Tweet { get; set; }

    }
}
