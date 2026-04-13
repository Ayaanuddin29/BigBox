using ComplaintManagement.Util.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.UserManagement
{
    public interface IApproverRepository
    {
        Task<IEnumerable<ApproverDto>> GetAllAsync();
        Task<ApproverDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(ApproverDto dto);
        Task<int> UpdateAsync(ApproverDto dto);

        Task<ApproverDto?> GetByUsernameAsync(string username);
        Task InsertAsync(ApproverDto dto);
        Task ActivateAsync(string username);
        Task DeactivateAsync(string username);
        Task<IEnumerable<ApproverDto>> GetAvailableManagersAsync(string excludeUsername);

    }
}
