using ComplaintManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.UserManagement
{
    public interface IRoleService
    {
        Task<List<RoleViewModel>> GetRolesAsync();
        Task<RoleViewModel> GetRoleAsync(string id);
        Task CreateRoleAsync(RoleViewModel model);
        Task UpdateRoleAsync(RoleViewModel model);
    }
}
