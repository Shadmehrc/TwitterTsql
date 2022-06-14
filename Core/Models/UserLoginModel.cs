using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UserLoginModel
    {
        public string UserName { get; set; }
        public  string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
