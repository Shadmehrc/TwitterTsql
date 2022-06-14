using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Models;

namespace Application.ServiceInterfaces
{
    public interface IRoleCrudService
    {
        public Task<bool> AddRole(AddRoleModel newRole);
        public Task<List<ShowRolesModel>> ShowRoles();
        public Task<bool> AddRoleToUser(AddRoleToUserModel model);
        public Task<IList<string>> ShowUserRoles(string userId);
        public Task<IList<User>> GetUsersInRole(string name);
    }
}
