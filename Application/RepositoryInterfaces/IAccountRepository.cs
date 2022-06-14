using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Models;

namespace Application.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        public Task<User> Login(UserLoginModel user);
        public Task<bool> Logout();
        public Task<User> JwtToken(AuthenticationJwtDTO model);

    }
}
