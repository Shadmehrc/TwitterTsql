using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Models
{
    public class CreateUserModelDTO
    {
        public User User { get; set; } 
        public string Password { get; set; } 
    }
}
