﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UserRegisterModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email  { get; set; }
        public string Password  { get; set; }
    }
}
