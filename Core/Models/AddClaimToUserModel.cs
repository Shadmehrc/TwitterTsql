using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AddClaimToUserModel
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string UserName { get; set; }
    }
}
