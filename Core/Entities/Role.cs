using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
        public bool IsRemoved { get; set; }
    }
}
