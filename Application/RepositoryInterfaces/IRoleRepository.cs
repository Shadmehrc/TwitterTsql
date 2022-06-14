using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Models;

namespace Application.RepositoryInterfaces
{
    public interface IRoleRepository
    {
        public Task<bool> AddRole(Role role);
        public Task<List<Role>> ShowRoles();
        public Task<bool> AddRoleToUser(AddRoleToUserModel model);
        public Task<IList<string>> ShowUserRoles(string userId);
        public Task<IList<User>> GetUsersInRole(string name);

    }
}
