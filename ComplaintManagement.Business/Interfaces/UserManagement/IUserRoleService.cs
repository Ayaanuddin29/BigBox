using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.UserManagement
{
    public interface IUserRoleService
    {
        Task<List<IdentityUser>> GetAllUsersAsync();
        Task<ManageUserRolesDto> GetUserRolesAsync(string userId);
        Task UpdateUserRolesAsync(ManageUserRolesDto model);
    }
}
