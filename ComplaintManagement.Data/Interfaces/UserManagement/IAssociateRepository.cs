using ComplaintManagement.Util.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.UserManagement
{
    public interface IAssociateRepository
    {
        Task<IEnumerable<AssociateDto>> GetAllAsync();
        Task<AssociateDto> GetByIdAsync(int id);
        Task UpdateAsync(AssociateDto model);

        Task<AssociateDto> GetByUsernameAsync(string username);
        Task InsertAsync(AssociateDto model);
        Task DeactivateByUsernameAsync(string username);
        Task ReactivateAsync(string username);

    }

}
