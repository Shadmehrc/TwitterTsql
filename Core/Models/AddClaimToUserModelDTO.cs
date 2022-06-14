using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AddClaimToUserModelDTO
    {
        public string UserName { get; set; }
        public Claim Claim { get; set; }
    }
}
