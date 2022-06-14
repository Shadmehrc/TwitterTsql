using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces;
using Core.Entities;
using Core.Models;

namespace Application.Services
{
    public class RoleCrudService : IRoleCrudService
    {
        private readonly IRoleRepository _iRoleRepository;

        public RoleCrudService(IRoleRepository iRoleRepository)
        {
            _iRoleRepository = iRoleRepository;
        }

        public async Task<bool> AddRole(AddRoleModel newRole)
        {
            var role = new Role()
            {
                Name = newRole.RoleName,
                Description = newRole.RoleDescription,
            };
            var result = await _iRoleRepository.AddRole(role);

            return result;
        }

        public async Task<List<ShowRolesModel>> ShowRoles()
        {
            var roles = await _iRoleRepository.ShowRoles();
            var result = roles.Select(x => new ShowRolesModel
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
            }).ToList();
            return result;
        }

        public async Task<bool> AddRoleToUser(AddRoleToUserModel model)
        {
            var result = await _iRoleRepository.AddRoleToUser(model);
            return result;
        }

        public async Task<IList<string>> ShowUserRoles(string userId)
        {
            var result = await _iRoleRepository.ShowUserRoles(userId);
            return result;
        }

        public async Task<IList<User>> GetUsersInRole(string name)
        {
            var result = await _iRoleRepository.GetUsersInRole(name);
            return result;
        }
    }
}
