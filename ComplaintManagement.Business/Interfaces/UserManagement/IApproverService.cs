using ComplaintManagement.Util.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.UserManagement
{
    public interface IApproverService
    {
        Task<IEnumerable<ApproverDto>> GetAllAsync();
        Task<ApproverDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ApproverDto dto, string loginUser);
        Task<bool> UpdateAsync(ApproverDto dto, string loginUser);

        Task SyncManagerByRolesAsync(string username, List<string> roles);
        Task<IEnumerable<ApproverDto>> GetAvailableManagersAsync(string excludeUsername);
    }
}
