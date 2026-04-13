using ComplaintManagement.Util.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.UserManagement
{
    public interface IAssociateGroupRepository
    {
        Task<IEnumerable<AssociateGroupDto>> GetAllAsync();
        Task<AssociateGroupDto> GetByIdAsync(int id);

        Task<int> CreateAsync(AssociateGroupDto model);
        Task UpdateAsync(AssociateGroupDto model);

        Task<IEnumerable<AssociateLookupDto>> GetUsersByRoleAsync(string roleName);

        Task SaveStaffLinksAsync(int groupId, List<int> associateIds);
        Task SaveManagerLinksAsync(int groupId, List<int> associateIds);

        Task<List<int>> GetStaffIdsByGroupAsync(int groupId);
        Task<List<int>> GetManagerIdsByGroupAsync(int groupId);
    }
}
