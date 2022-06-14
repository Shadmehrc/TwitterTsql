using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
   public class Notification
    {
        public int Id{ get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public string Text{ get; set; }
    }
}
