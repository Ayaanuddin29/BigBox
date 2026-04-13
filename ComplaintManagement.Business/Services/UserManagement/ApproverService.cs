using ComplaintManagement.Business.Interfaces.UserManagement;
using ComplaintManagement.Data.Interfaces.UserManagement;
using ComplaintManagement.Util.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Services.UserManagement
{
    public class ApproverService:IApproverService
    {
        private readonly IApproverRepository _repo;

        public ApproverService(IApproverRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ApproverDto>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<ApproverDto?> GetByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<bool> CreateAsync(ApproverDto dto, string loginUser)
        {
            dto.LoginCreated = loginUser;
            return await _repo.CreateAsync(dto) > 0;
        }

        public async Task<bool> UpdateAsync(ApproverDto dto, string loginUser)
        {
            dto.LoginModified = loginUser;
            return await _repo.UpdateAsync(dto) > 0;
        }

        public async Task SyncManagerByRolesAsync(string username, List<string> roles)
        {
            var associateRoles = new List<string>
                {
                    "Support 1",
                    "Employee Manager"
                };

            bool requiresApprover = roles
               .Any(r => associateRoles.Contains(r));

            // var isApprover = roles.Contains("Approver");

            var existing = await _repo.GetByUsernameAsync(username);

            if (requiresApprover)
            {
                if (existing == null)
                {
                    await _repo.InsertAsync(new ApproverDto
                    {
                        ManagerName = username,
                        Username = username,
                        Active = true,
                        Availability = true,
                        LoginCreated = username
                    });
                }
                else
                {
                    await _repo.ActivateAsync(username);
                }
            }
            else
            {
                if (existing != null)
                {
                    await _repo.DeactivateAsync(username);
                }
            }
        }

        public async Task<IEnumerable<ApproverDto>> GetAvailableManagersAsync(string excludeUsername)
        {
            return await _repo.GetAvailableManagersAsync(excludeUsername);
        }
    }
}
