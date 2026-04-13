using ComplaintManagement.Util.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.UserManagement
{
    public interface IAssociateService
    {
        Task<IEnumerable<AssociateDto>> GetAllAsync();
        Task<AssociateDto> GetByIdAsync(int id);
        Task UpdateAsync(AssociateDto model);
        Task SyncAssociateByRolesAsync(string username, IEnumerable<string> selectedRoles);

    }

}
