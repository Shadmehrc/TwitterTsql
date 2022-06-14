using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TokenModel
    {
        public string Key { get; set; }
        public string issuer { get; set; }
        public string audience { get; set; }
    }
}
