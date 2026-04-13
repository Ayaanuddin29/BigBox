using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.UserManagement
{
    public interface IUserRoleRepository
    {
        Task<List<IdentityUser>> GetAllUsersAsync();
        Task<ManageUserRolesDto> GetUserRolesAsync(string userId);
        Task UpdateUserRolesAsync(ManageUserRolesDto model);
    }

}
