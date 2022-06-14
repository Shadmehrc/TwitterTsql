using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Application.ServiceInterfaces
{
    public interface IAccountManageService
    {
        public Task<bool> Login(UserLoginModel userModel);
        public Task<bool> Logout();
        public Task<string> JwtToken(AuthenticationJwtDTO model);
    }
}
