using ComplaintManagement.Util.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.UserManagement
{
    public interface IAssociateGroupService
    {
        Task<IEnumerable<AssociateGroupDto>> GetAllAsync();
        Task<AssociateGroupDto> PrepareCreateModelAsync();
        Task<AssociateGroupDto> PrepareEditModelAsync(int groupId);

        Task CreateAsync(AssociateGroupDto model);
        Task UpdateAsync(AssociateGroupDto model);
    }
}
