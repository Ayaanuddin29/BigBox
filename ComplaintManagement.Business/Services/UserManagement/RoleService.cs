using ComplaintManagement.Business.Interfaces.UserManagement;
using ComplaintManagement.Data.Interfaces.UserManagement;
using ComplaintManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Services.UserManagement
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RoleViewModel>> GetRolesAsync()
        {
            var roles = await _repository.GetAllAsync();
            return roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
        }

        public async Task<RoleViewModel> GetRoleAsync(string id)
        {
            var role = await _repository.GetByIdAsync(id);
            return new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public async Task CreateRoleAsync(RoleViewModel model)
        {
            await _repository.CreateAsync(model.Name);
        }

        public async Task UpdateRoleAsync(RoleViewModel model)
        {
            var role = await _repository.GetByIdAsync(model.Id);
            role.Name = model.Name;
            role.NormalizedName = model.Name.ToUpper();
            await _repository.UpdateAsync(role);
        }
    }
}
