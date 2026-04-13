using ComplaintManagement.Data.Interfaces.UserManagement;
using ComplaintManagement.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Repositories.UserManagement
{
    public class RoleRepository :IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<IdentityRole>> GetAllAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IdentityRole> GetByIdAsync(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task CreateAsync(string roleName)
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task UpdateAsync(IdentityRole role)
        {
            await _roleManager.UpdateAsync(role);
        }
    }
}
