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
    public class AssociateGroupService : IAssociateGroupService
    {
        private readonly IAssociateGroupRepository _repo;

        public AssociateGroupService(IAssociateGroupRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AssociateGroupDto>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<AssociateGroupDto> PrepareCreateModelAsync()
        {
            return new AssociateGroupDto
            {
                StaffList = (await _repo.GetUsersByRoleAsync("Support 1")).ToList(),
                ManagerList = (await _repo.GetUsersByRoleAsync("Support 1")).ToList()
            };
        }

        public async Task<AssociateGroupDto> PrepareEditModelAsync(int groupId)
        {
            var model = await _repo.GetByIdAsync(groupId);

            model.StaffList = (await _repo.GetUsersByRoleAsync("Support 1")).ToList();
            model.ManagerList = (await _repo.GetUsersByRoleAsync("Support 1")).ToList();

            model.SelectedStaffIds = await _repo.GetStaffIdsByGroupAsync(groupId);
            model.SelectedManagerIds = await _repo.GetManagerIdsByGroupAsync(groupId);

            return model;
        }

        public async Task CreateAsync(AssociateGroupDto model)
        {
            var id = await _repo.CreateAsync(model);
            await _repo.SaveStaffLinksAsync(id, model.SelectedStaffIds);
            await _repo.SaveManagerLinksAsync(id, model.SelectedManagerIds);
        }

        public async Task UpdateAsync(AssociateGroupDto model)
        {
            await _repo.UpdateAsync(model);
            await _repo.SaveStaffLinksAsync(model.AssociateGroupId, model.SelectedStaffIds);
            await _repo.SaveManagerLinksAsync(model.AssociateGroupId, model.SelectedManagerIds);
        }
    }
}
