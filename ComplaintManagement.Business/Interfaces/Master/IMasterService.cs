using ComplaintManagement.Util.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.Master
{
    public interface IMasterService
    {
        Task<IEnumerable<MasterModel>> GetAllAsync(string tableName);
        Task<int> SaveAsync(MasterModel model);
        Task<int> DeleteAsync(int id, string tableName);
        Task<IEnumerable<AssociateModel>> GetAssociates();
        Task<IEnumerable<AssociateGroupModel>> GetAssociateGroups();
        

    }
}
