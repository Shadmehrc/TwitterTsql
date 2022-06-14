using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models{

    public class AddRoleToUserModel
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
