using ComplaintManagement.Business.Interfaces.UserManagement;
using ComplaintManagement.Data.Interfaces.UserManagement;
using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Services.UserManagement
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _repository;

        public UserRoleService(IUserRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<IdentityUser>> GetAllUsersAsync()
        {
            return await _repository.GetAllUsersAsync();
        }

        public async Task<ManageUserRolesDto> GetUserRolesAsync(string userId)
        {
            return await _repository.GetUserRolesAsync(userId);
        }

        public async Task UpdateUserRolesAsync(ManageUserRolesDto model)
        {
            await _repository.UpdateUserRolesAsync(model);
        }
    }

}
