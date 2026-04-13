using ComplaintManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace ComplaintManagement.Data.Interfaces.UserManagement
{
    public interface IRoleRepository
    {
        Task<List<IdentityRole>> GetAllAsync();
        Task<IdentityRole> GetByIdAsync(string id);
        Task CreateAsync(string roleName);
        Task UpdateAsync(IdentityRole role);
    }
}
