using ComplaintManagement.Util.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.Master
{
    public interface IMasterRepository
    {
        Task<IEnumerable<MasterModel>> GetAllAsync(string tableName);
        Task<int> InsertAsync(MasterModel model);
        Task<int> UpdateAsync(MasterModel model);
        Task<int> DeleteAsync(int id, string tableName);

        Task<IEnumerable<AssociateModel>> GetAssociates();
        Task<IEnumerable<AssociateGroupModel>> GetAssociateGroups();

    }
}
