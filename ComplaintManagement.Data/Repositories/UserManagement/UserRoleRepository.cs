using ComplaintManagement.Data.Interfaces.UserManagement;
using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Repositories.UserManagement
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleRepository(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<IdentityUser>> GetAllUsersAsync()
        {
            return _userManager.Users.ToList();
        }

        public async Task<ManageUserRolesDto> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var model = new ManageUserRolesDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = new List<RoleSelectionDto>()
            };

            foreach (var role in _roleManager.Roles)
            {
                model.Roles.Add(new RoleSelectionDto
                {
                    RoleName = role.Name,
                    IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
                });
            }

            return model;
        }

        public async Task UpdateUserRolesAsync(ManageUserRolesDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            foreach (var role in _roleManager.Roles)
            {
                var selected = model.Roles.FirstOrDefault(r => r.RoleName == role.Name);

                if (selected != null && selected.IsSelected)
                {
                    if (!await _userManager.IsInRoleAsync(user, role.Name))
                        await _userManager.AddToRoleAsync(user, role.Name);
                }
                else
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                        await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }
        }
    }

}
